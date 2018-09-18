/*
 * ----------------------------------------------------------------------------
 * "THE BEER-WARE LICENSE" (Revision 42):
 * https://github.com/Blinky0815
 * wrote this file. As long as you retain this notice you
 * can do whatever you want with this stuff. If we meet some day, and you think
 * this stuff is worth it, you can buy me a beer in return Olaf Christ
 * ----------------------------------------------------------------------------
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Mcs.Usb;
using System.Runtime.InteropServices;

namespace McsUsbExample
{
    public partial class Form1 : Form
    {
        private CMcsUsbListNet usblist = new CMcsUsbListNet();
        private CMeaDeviceNet device = null;
        bool serverEnabled = true;
        //TCP server {begin}
        int port = 9090; // The port number we're going to listen on. Any unused port < 65534 preferably above 1024 (protected ports)
        System.Net.IPAddress serverAddress = System.Net.IPAddress.Parse("10.5.162.120");
        ushort sequenceNumber = 0;

        NetworkStream ourStream = null;
        TcpListener listener = null;
        TcpClient ourTCP_Client = null;
        //TCP server {end}
        // The overall number of channels (including data, digital, checksum, timestamp) in one sample. 
        // Checksum and timestamp are not available for MC_Card
        // With the MC_Card you lose one analog channel, when using the digital channel 
        int m_block = 0;

        int channelblocksize = 0;
        int m_channel_handles = 0;
        public Form1()
        {
            InitializeComponent();
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    byte[] octets = ip.GetAddressBytes();
                    firstOctet.Text = octets[0].ToString();
                    secondOctet.Text = octets[1].ToString();
                    thirdOctet.Text = octets[2].ToString();
                    fourthOctet.Text = octets[3].ToString();
                    serverAddress = ip;
                }
            }
        }

        private void btMeaDevice_present_Click(object sender, EventArgs e)
        {
            cbDevices.Items.Clear();
            usblist.Initialize(DeviceEnumNet.MCS_MEA_DEVICE);
            for (uint i = 0; i < usblist.Count; i++)
            {
                cbDevices.Items.Add(usblist.GetUsbListEntry(i).DeviceName + " / " + usblist.GetUsbListEntry(i).SerialNumber);
            }
            if (cbDevices.Items.Count > 0)
            {
                cbDevices.SelectedIndex = 0;
            }

        }

        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (device != null)
            {
                device.StopDacq();

                device.Disconnect();
                device.Dispose();

                device = null;
            }

            uint sel = (uint)cbDevices.SelectedIndex;
            /* choose one of the following contructors:
             * The first one uses the OnNewData callback and gives you a reference to the raw multiplexed data,
             * this could be used without further initialisation
             * The second uses the more advanced callback which gives you the data for each channel in a callback, but need initialistation
             * for buffers and the selected channels
             */

            if (sel >= 0) // a device is selected, enable the sampling buttons
            {
                btStart.Enabled = true;
            }
            else
            {
                btStart.Enabled = false;
            }
            btStop.Enabled = false;

            device = new CMeaDeviceNet(usblist.GetUsbListEntry(sel).DeviceId.BusType, OnChannelData, OnError);
            device.Connect(usblist.GetUsbListEntry(sel));

            device.SendStop(); // only to be sure

            tbDeviceInfo.Text = "";
            tbDeviceInfo.Text += /*"Serialnumber: " +*/ device.SerialNumber + "\r\n";
            int hwchannels;
            device.HWInfo().GetNumberOfHWADCChannels(out hwchannels);
            tbDeviceInfo.Text += "Number of Hardwarechannels: " + hwchannels.ToString() + "\r\n";


            // configure MeaDevice: MC_Card or Usb
            device.SetNumberOfChannels(hwchannels);

            int samplingrate = 5000; // MC_Card does not support all settings, please see MC_Rack for valid settings
            device.SetSampleRate(samplingrate, 1, 0);

            int gain;
            device.GetGain(out gain);

            List<CMcsUsbDacqNet.CHWInfo.CVoltageRangeInfoNet> voltageranges;
            device.HWInfo().GetAvailableVoltageRangesInMicroVoltAndStringsInMilliVolt(out voltageranges);
            for (int i = 0; i < voltageranges.Count; i++)
            {
                tbDeviceInfo.Text += "(" + i.ToString() + ") " + voltageranges[i].VoltageRangeDisplayStringMilliVolt + "\r\n";
            }

            // Set the range acording to the index (only valid for MC_Card)
            // device.SetVoltageRangeInMicroVoltByIndex(0, 0);

            device.EnableDigitalIn(true, 0);

            // Checksum not supported by MC_Card
            device.EnableChecksum(true, 0);


            // Get the layout to know how the data look like that you receive
            int ana, digi, che, tim, block;
            device.GetChannelLayout(out ana, out digi, out che, out tim, out block, 0);

            // or
            device.GetChannelsInBlock(out block);

            m_block = block;
            // set the channel combo box with the channels
            SetChannelCombo(block);

            channelblocksize = samplingrate / 10; // good choice for MC_Card

            bool[] selChannels = new bool[block];

            for (int i = 0; i < block; i++)
            {
                selChannels[i] = true; // With true channel i is selected
                //Console.WriteLine("Selected: " + block);
                // selChannels[i] = false; // With false the channel i is deselected
            }
            // queue size and threshold should be selected carefully

            device.SetSelectedData(selChannels, 10 * channelblocksize, channelblocksize, SampleSizeNet.SampleSize16, block);
            // Alternative call if you want to select all channels
            //device.SetSelectedData(block, 10 * channelblocksize, channelblocksize, CMcsUsbDacqNet.SampleSize.Size16, block);
            m_channel_handles = block; // for this case, if all channels are selected
        }

        /* Here follow the callback funktion for receiving data and receiving error messages
         * Please note, it is an errTcpListener listeneror to use both data receiving callbacks at a time unless you know want you are doing
         */

        delegate void OnChannelDataDelegate(ushort[] data, int offset);

        void OnChannelData(CMcsUsbDacqNet d, int CbHandle, int numSamples)
        {

            int size_ret;
            int totalchannels, offset, channels;
            device.ChannelBlock_GetChannel(0, 0, out totalchannels, out offset, out channels);
            ushort[] data = device.ChannelBlock_ReadFramesUI16(0, channelblocksize, out size_ret);
            
            for (int i = 0; i < 32; i++)
            {
                ushort[] data1 = new ushort[size_ret];
                for (int j = 0; j < size_ret; j++)
                {
                    data1[j] = data[j * m_channel_handles + i];
                    //Console.WriteLine((j * m_channel_handles + i) + " m_channel_handles " + m_channel_handles + " size_ret " + size_ret);
                }
                BeginInvoke(new OnChannelDataDelegate(OnChannelDataLater), new Object[] { data1, i });
            }

            if (ourStream != null)
            {
                try
                {
                    DateTime begin = DateTime.UtcNow;
                    data[0] = sequenceNumber++;
                    byte[] transferBuffer = new byte[data.Length * sizeof(ushort)];
                    //Console.WriteLine("transferBuffer.Length: " + transferBuffer.Length);
                    Buffer.BlockCopy(data, 0, transferBuffer, 0, transferBuffer.Length);
                    ourStream.Write(transferBuffer, 0, transferBuffer.Length);
                    DateTime end = DateTime.UtcNow;
                    Console.WriteLine("Measured time: " + (end - begin).TotalMilliseconds + " ms.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("client is disconnected...");
                      if (listener != null)
                    {
                        Console.WriteLine("stopping listener...");
                        listener.Stop();
                        listener = null;
                    }

                    if (ourStream != null)
                    {
                        Console.WriteLine("closing stream...");
                        ourStream.Close();
                        ourStream = null;
                    }
                    if (ourTCP_Client != null)
                    {
                        ourTCP_Client.Close();
                        ourTCP_Client = null;
                    }
                }
            }
        }

        void OnError(String msg, int info)
        {
            device.StopDacq();
            MessageBox.Show("Mea Device Error: " + msg);
        }

        void OnChannelDataLater(ushort[] data, int offset)
        {

            int channel = cbChannel.SelectedIndex;
            if (channel >= 0 && channel == offset)
            {
                //Console.WriteLine ("Data length: "+data.Length);
                DrawChannel(data);
            }
        }
        private void btStart_Click(object sender, EventArgs e)
        {
            if (serverEnabled)
            {
                Console.WriteLine("starting server " + serverAddress.ToString());

                Console.WriteLine("waiting for client...");
                // Start listening for connections on our IP address + Our Port number 
                listener = new TcpListener(serverAddress, port);
                Console.WriteLine("Listener started on port: " + port);
                listener.Start();
                Console.WriteLine("starting to listen");
                // Is someone trying to call us? Well answer!
                ourTCP_Client = listener.AcceptTcpClient();
                Console.WriteLine("client connected...? " + ourTCP_Client.Connected);
                //A network stream object. We'll use this to send and receive our data, so create a buffer for it...
                ourStream = ourTCP_Client.GetStream();
                Console.WriteLine("client connected...");
            }
            device.StartDacq();
            btMeaDevice_present.Enabled = false;
            cbDevices.Enabled = false;
            btStart.Enabled = false;
            btStop.Enabled = true;
            firstOctet.Enabled = false;
            secondOctet.Enabled = false;
            thirdOctet.Enabled = false;
            fourthOctet.Enabled = false;
            portText.Enabled = false;
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            if (device != null)
            {
                device.StopDacq();
            }
            btMeaDevice_present.Enabled = true;
            cbDevices.Enabled = true;
            btStart.Enabled = true;
            btStop.Enabled = false;
            firstOctet.Enabled = true;
            secondOctet.Enabled = true;
            thirdOctet.Enabled = true;
            fourthOctet.Enabled = true;
            portText.Enabled = true;

            if (listener != null)
            {
                Console.WriteLine("stopping listener...");
                listener.Stop();
            }

            if (ourStream != null)
            {
                Console.WriteLine("closing stream...");
                ourStream.Close();
            }
            if (ourTCP_Client != null)
            {
                ourTCP_Client.Close();
            }
        }

        private void SetChannelCombo(int channels)
        {
            cbChannel.Items.Clear();
            for (int i = 0; i < channels; i++)
            {
                cbChannel.Items.Add((i + 1).ToString());
            }
        }

        private void cbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (device != null)
            {
                device.SendStop();

                device.Disconnect();
                device.Dispose();

                device = null;
            }

        }

        ushort[] m_data = null;

        void DrawChannel(ushort[] data)
        {
            m_data = data;
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int width = panel1.Width;
            int height = panel1.Height;
            int max = 0;
            int min = 65536;
            if (m_data != null)
            {
                for (int i = 0; i < m_data.Length; i++)
                {
                    if (m_data[i] > max)
                    {
                        max = m_data[i];
                    }
                    if (m_data[i] < min)
                    {
                        min = m_data[i];
                    }
                }
                Point[] points = new Point[m_data.Length];
                for (int i = 0; i < m_data.Length; i++)
                {
                    points[i] = new Point(i * width / m_data.Length, (m_data[i] - min + 1) * height / (max - min + 2));
                }
                Pen pen = new Pen(Color.Black, 1);
                e.Graphics.DrawLines(pen, points);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (device != null)
            {
                if (btMeaDevice_present.Enabled == false) // this means whe are sampling
                {
                    e.Cancel = true;
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void firstOctet_TextChanged(object sender, EventArgs e)
        {
            serverAddress = System.Net.IPAddress.Parse(firstOctet.Text + "." + secondOctet.Text + "." + thirdOctet.Text + "." + fourthOctet.Text);
        }

        private void secondOctet_TextChanged(object sender, EventArgs e)
        {
            serverAddress = System.Net.IPAddress.Parse(firstOctet.Text + "." + secondOctet.Text + "." + thirdOctet.Text + "." + fourthOctet.Text);
        }

        private void thirdOctet_TextChanged(object sender, EventArgs e)
        {
            serverAddress = System.Net.IPAddress.Parse(firstOctet.Text + "." + secondOctet.Text + "." + thirdOctet.Text + "." + fourthOctet.Text);
        }

        private void fourthOctet_TextChanged(object sender, EventArgs e)
        {
            serverAddress = System.Net.IPAddress.Parse(firstOctet.Text + "." + secondOctet.Text + "." + thirdOctet.Text + "." + fourthOctet.Text);
        }

        private void portText_TextChanged(object sender, EventArgs e)
        {
            port = Convert.ToInt32(portText.Text.ToString());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            serverEnabled = tcp_checkbox.Checked;
            Console.WriteLine("serverEnabled " + serverEnabled);
        }
    }
}

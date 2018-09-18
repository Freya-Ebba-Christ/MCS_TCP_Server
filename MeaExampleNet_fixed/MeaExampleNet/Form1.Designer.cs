/*
 * ----------------------------------------------------------------------------
 * "THE BEER-WARE LICENSE" (Revision 42):
 * https://github.com/Blinky0815
 * wrote this file. As long as you retain this notice you
 * can do whatever you want with this stuff. If we meet some day, and you think
 * this stuff is worth it, you can buy me a beer in return Olaf Christ
 * ----------------------------------------------------------------------------
 */


namespace McsUsbExample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btMeaDevice_present = new System.Windows.Forms.Button();
            this.tbDeviceInfo = new System.Windows.Forms.TextBox();
            this.cbDevices = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbChannel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.firstOctet = new System.Windows.Forms.TextBox();
            this.secondOctet = new System.Windows.Forms.TextBox();
            this.thirdOctet = new System.Windows.Forms.TextBox();
            this.fourthOctet = new System.Windows.Forms.TextBox();
            this.port_label = new System.Windows.Forms.Label();
            this.portText = new System.Windows.Forms.TextBox();
            this.tcp_checkbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btMeaDevice_present
            // 
            this.btMeaDevice_present.Location = new System.Drawing.Point(12, 12);
            this.btMeaDevice_present.Name = "btMeaDevice_present";
            this.btMeaDevice_present.Size = new System.Drawing.Size(138, 23);
            this.btMeaDevice_present.TabIndex = 0;
            this.btMeaDevice_present.Text = "MEA Device present?";
            this.btMeaDevice_present.UseVisualStyleBackColor = true;
            this.btMeaDevice_present.Click += new System.EventHandler(this.btMeaDevice_present_Click);
            // 
            // tbDeviceInfo
            // 
            this.tbDeviceInfo.Location = new System.Drawing.Point(389, 12);
            this.tbDeviceInfo.Multiline = true;
            this.tbDeviceInfo.Name = "tbDeviceInfo";
            this.tbDeviceInfo.Size = new System.Drawing.Size(282, 66);
            this.tbDeviceInfo.TabIndex = 1;
            // 
            // cbDevices
            // 
            this.cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevices.FormattingEnabled = true;
            this.cbDevices.Location = new System.Drawing.Point(156, 28);
            this.cbDevices.Name = "cbDevices";
            this.cbDevices.Size = new System.Drawing.Size(227, 21);
            this.cbDevices.TabIndex = 2;
            this.cbDevices.SelectedIndexChanged += new System.EventHandler(this.cbDevices_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Devices";
            // 
            // btStart
            // 
            this.btStart.Enabled = false;
            this.btStart.Location = new System.Drawing.Point(159, 55);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 4;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.Enabled = false;
            this.btStop.Location = new System.Drawing.Point(240, 55);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(75, 23);
            this.btStop.TabIndex = 5;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sampling";
            // 
            // cbChannel
            // 
            this.cbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannel.FormattingEnabled = true;
            this.cbChannel.Location = new System.Drawing.Point(15, 81);
            this.cbChannel.Name = "cbChannel";
            this.cbChannel.Size = new System.Drawing.Size(70, 21);
            this.cbChannel.TabIndex = 7;
            this.cbChannel.SelectedIndexChanged += new System.EventHandler(this.cbChannel_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Channel";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(15, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 146);
            this.panel1.TabIndex = 9;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "IPAddress";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // firstOctet
            // 
            this.firstOctet.Location = new System.Drawing.Point(160, 82);
            this.firstOctet.Name = "firstOctet";
            this.firstOctet.Size = new System.Drawing.Size(42, 20);
            this.firstOctet.TabIndex = 12;
            this.firstOctet.Text = "10";
            this.firstOctet.TextChanged += new System.EventHandler(this.firstOctet_TextChanged);
            // 
            // secondOctet
            // 
            this.secondOctet.Location = new System.Drawing.Point(208, 82);
            this.secondOctet.Name = "secondOctet";
            this.secondOctet.Size = new System.Drawing.Size(42, 20);
            this.secondOctet.TabIndex = 13;
            this.secondOctet.Text = "5";
            this.secondOctet.TextChanged += new System.EventHandler(this.secondOctet_TextChanged);
            // 
            // thirdOctet
            // 
            this.thirdOctet.Location = new System.Drawing.Point(256, 82);
            this.thirdOctet.Name = "thirdOctet";
            this.thirdOctet.Size = new System.Drawing.Size(42, 20);
            this.thirdOctet.TabIndex = 13;
            this.thirdOctet.Text = "162";
            this.thirdOctet.TextChanged += new System.EventHandler(this.thirdOctet_TextChanged);
            // 
            // fourthOctet
            // 
            this.fourthOctet.Location = new System.Drawing.Point(304, 82);
            this.fourthOctet.Name = "fourthOctet";
            this.fourthOctet.Size = new System.Drawing.Size(42, 20);
            this.fourthOctet.TabIndex = 13;
            this.fourthOctet.Text = "120";
            this.fourthOctet.TextChanged += new System.EventHandler(this.fourthOctet_TextChanged);
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Location = new System.Drawing.Point(353, 81);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(26, 13);
            this.port_label.TabIndex = 14;
            this.port_label.Text = "Port";
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(389, 82);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(40, 20);
            this.portText.TabIndex = 15;
            this.portText.Text = "9090";
            this.portText.TextChanged += new System.EventHandler(this.portText_TextChanged);
            // 
            // tcp_checkbox
            // 
            this.tcp_checkbox.AutoSize = true;
            this.tcp_checkbox.Checked = true;
            this.tcp_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tcp_checkbox.Location = new System.Drawing.Point(435, 81);
            this.tcp_checkbox.Name = "tcp_checkbox";
            this.tcp_checkbox.Size = new System.Drawing.Size(83, 17);
            this.tcp_checkbox.TabIndex = 16;
            this.tcp_checkbox.Text = "Enable TCP";
            this.tcp_checkbox.UseVisualStyleBackColor = true;
            this.tcp_checkbox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 273);
            this.Controls.Add(this.tcp_checkbox);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.port_label);
            this.Controls.Add(this.fourthOctet);
            this.Controls.Add(this.thirdOctet);
            this.Controls.Add(this.secondOctet);
            this.Controls.Add(this.firstOctet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbChannel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDevices);
            this.Controls.Add(this.tbDeviceInfo);
            this.Controls.Add(this.btMeaDevice_present);
            this.Name = "Form1";
            this.Text = "MCS Data Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btMeaDevice_present;
        private System.Windows.Forms.TextBox tbDeviceInfo;
        private System.Windows.Forms.ComboBox cbDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbChannel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox firstOctet;
        private System.Windows.Forms.TextBox secondOctet;
        private System.Windows.Forms.TextBox thirdOctet;
        private System.Windows.Forms.TextBox fourthOctet;
        private System.Windows.Forms.Label port_label;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.CheckBox tcp_checkbox;
    }
}


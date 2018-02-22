using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Threading;

namespace MessageConsole
{
    public partial class FormSendMessage : Form
    {
        private SerialPort _serialPort;
        public FormSendMessage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _serialPort = new SerialPort("COM9", 115200);
            Thread.Sleep(1000);
            _serialPort.Open();
            Thread.Sleep(1000);
            _serialPort.Write("AT+CMGF=1\r");
            Thread.Sleep(1000);
            _serialPort.Write("AT+CMGS=\"" + txtToNumber.Text + "\"\r");
            Thread.Sleep(1000);
            _serialPort.Write(txtMessageBody.Text + "\x1A");
            Thread.Sleep(1000);
            var response = _serialPort.ReadExisting();

            if (response.Contains("ERROR"))
            {
                MessageBox.Show("Failed");
            }

            else
            {
                MessageBox.Show("Success");
            }

            _serialPort.Close();

            
        }
    }
}

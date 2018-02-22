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
using System.Collections;
using System.Threading;

namespace MessageConsole
{
    public partial class FormMain : Form
    {
        
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            GetGsmPort();
        }

        private void GetGsmPort()
        {
            

            DataTable portList = new DataTable();

            ManagementObjectSearcher ms = new ManagementObjectSearcher("root\\CIMV2","SELECT * FROM Win32_POTSModem");

            foreach(ManagementObject obj in ms.Get())
            {
                portList.Columns.Add("ComPort");
                portList.Columns.Add("PortName");
                portList.Columns.Add("PortDescription");
                DataRow dr = portList.NewRow();
                dr["ComPort"] = obj["AttachedTo"].ToString();
                dr["PortName"] = obj["Name"].ToString();
                dr["PortDescription"] = obj["Description"].ToString();

                portList.Rows.Add(dr);
            }

            if(portList.Rows.Count==0)
            {
                MessageBox.Show("No Modem Found.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            else
            {
                
                dataGridView1.DataSource = portList;
                dataGridView1.Columns[0].HeaderText = "COM PORT";
                dataGridView1.Columns[1].HeaderText = "PORT NAME";
                dataGridView1.Columns[2].HeaderText = "DESCRIPTION";
                
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSendMessage obj = new FormSendMessage();
            obj.Show();
        }
    }
}

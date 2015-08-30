using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnitchController
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            try
            {
                serialPort1.Open();
            }
            catch
            {
                
            }
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
            serialPort1.PortName = comboBox1.Text;
            try
            {
                serialPort1.Open();
            }
            catch
            {
                MessageBox.Show("Unable to connect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine(textBox1.Text);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            listBox1.Items.Add(serialPort1.ReadLine());
        }
        int oldThrust = 600;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (trackBar1.Value != oldThrust)
            {
                oldThrust = trackBar1.Value;
                serialPort1.WriteLine(trackBar1.Value.ToString());
            }
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if (trackBar1.Value != oldThrust)
            {
                oldThrust = trackBar1.Value;
                serialPort1.WriteLine(trackBar1.Value.ToString());
            }
        }

    }
}

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
            //serialPort1.WriteLine(textBox1.Text);
            SendString(textBox1.Text);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        delegate int AddItemDelegate(object o);

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            listBox1.Invoke(new AddItemDelegate(listBox1.Items.Add), serialPort1.ReadLine());
            //listBox1.Items.Add(serialPort1.ReadLine());
        }
        int oldThrust = 600;
        private void timer1_Tick(object sender, EventArgs e)
        {
            SendThrust(trackBar1.Value);
        }

        private void SendThrust(int value)
        {
            if (value != oldThrust)
            {
                oldThrust = value;
                try
                {
                    serialPort1.Write(value.ToString());
                    listBox1.Items.Add("> " + value.ToString());
                    textBox1.Text = value.ToString();
                
                }
                catch (TimeoutException)
                {
                    listBox1.Items.Add("timeout...");
                }
                //listBox1.AutoScrollOffset
            }
        }

        private void SendString(string str)
        {
            int a;
            if (int.TryParse(str, out a))
            {
                if (a >= trackBar1.Minimum && a <= trackBar1.Maximum)
                {
                    trackBar1.Value = a;
                    SendThrust(a);
                    return;
                }
            }
            try
            {
                serialPort1.Write(str);
            }
            catch (TimeoutException)
            {
                listBox1.Items.Add("timeout...");
            }
            listBox1.Items.Add("> " + str);

        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            return;
            if (trackBar1.Value != oldThrust)
            {
                oldThrust = trackBar1.Value;
                serialPort1.Write(trackBar1.Value.ToString());
                listBox1.Items.Add("> " + trackBar1.Value.ToString());
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendString(textBox1.Text);
            }
        }

    }
}

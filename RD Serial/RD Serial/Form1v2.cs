using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;


namespace RD_Serial
{
    public partial class Form1 : Form
    {
       // string RxString;
    

        StringBuilder strbuilder = new StringBuilder();
        char LF = (char)10;  //wykrywanie nowej lini




        public Form1()
        {
            InitializeComponent();
     
        }

        private void serialport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
        

          string Data = serialPort.ReadExisting();

            foreach (char Z in Data)
            {
                if (Z == LF)
                {
                    strbuilder.Append(Z);

                    string CurrentLine = strbuilder.ToString();
                    strbuilder.Clear();
                    textBox.Invoke(new Action(delegate ()
                    {
                        textBox.AppendText(CurrentLine);

                    }));

                }
                else
                {
                    strbuilder.Append(Z);
                }
            }

       
          


        }

 


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPort.Items.AddRange(ports);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialport_DataReceived);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort.Close();
                     


        }
       
  
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.PortName = cBoxCOMPort.Text;
                serialPort.BaudRate = Convert.ToInt32(cBoxBaundRate.Text);
                serialPort.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParity.Text);
                serialPort.Open();
              
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message,"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           serialPort.Close();

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.csv|*.csv";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlg.FileName, textBox.Text);
            }



            
        }

    }
}

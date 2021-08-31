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
        string RxString;
      //  private static string defaultLogFileName = "RDLog.log";
      //  private string logFile = @Environment.CurrentDirectory + @"\" + defaultLogFileName;



        public Form1()
        {
            InitializeComponent();
     

        }

        private void serialport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            RxString = serialPort.Read();
           
            textBox.Invoke(new Action(delegate()
           {
               textBox.AppendText(serialPort.Read());
               
           }));


           // textBox.AppendText(serialPort.ReadLine());
          //  this.Invoke(new EventHandler(DisplayText));
           // this.Invoke(new EventHandler(file_write));
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
       
        
   //     private void file_write(object sender, EventArgs e)
   //     {
   //       
   //             if (!File.Exists(textPath.Text))
   //             {
   //                 File.Create(textPath.Text);
   ///                 File.Open(textPath.Text, FileMode.Create);
    //        }
    //            else
    ///            {
    ///                using (StreamWriter sw = File.AppendText(textPath.Text))
    //                {
    ///                sw.WriteLine(RxString);     
    ///              //  sw.Write(RxString);
     ///               }
      //          }
           
      //  }

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
               // textBox.AppendText(serialPort.ReadLine());

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message,"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();

                if (!File.Exists(textPath.Text))
                {
                    File.Create(textPath.Text);
                    File.Open(textPath.Text, FileMode.Create);
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(textPath.Text))
                    {
                        sw.Write(textBox.Text);

                    }
                }

                //    progressBar1.Value = 0;

            }
        }

        
        private void DisplayText(object sender, EventArgs e)
        {
         //   textBox.AppendText(RxString);
        }

     

        private void bntBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFD = new SaveFileDialog();
            saveFD.Filter = "CSV Files|*.csv|All Files|*.*";
            saveFD.Title = "Select a File to Store the RD Data";
            if (saveFD.ShowDialog() == DialogResult.OK)
            {
                textPath.Text = saveFD.FileName;
            }
        }
    }
}

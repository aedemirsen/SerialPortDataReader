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

namespace SerialPortDataReader
{
    public partial class Form1 : Form
    {
        SerialPort sp;
        bool isOpen;
        private string []paths = new string[5];
        private const string baudRate = "9600";
        private string userName = "aedemirsen";
        private int period;
        double[] datas;
        System.Diagnostics.Stopwatch stopWatch;

        delegate void SetTextCallback(string text);

        public Form1()
        {
            InitializeComponent();
            string[] serialPorts = SerialPort.GetPortNames();  
            if (serialPorts.Length == 0)
            {
                comboBox1.Items.Add("No Serial Port!");
            }
            comboBox1.Items.AddRange(serialPorts); 
            comboBox1.SelectedIndex = 0;
            textBox3.Text = baudRate;
            textBox4.Text = "C:\\Users\\" + userName + "\\Desktop\\Data";
            textBox5.Text = "1.txt";
            textBox6.Text = "2.txt";
            textBox7.Text = "3.txt";
            textBox8.Text = "4.txt";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 0 && textBox5.Text.Length != 0 && textBox6.Text.Length 
                != 0 && textBox7.Text.Length != 0 && textBox8.Text.Length != 0 && textBox11.Text.Length != 0)
            {
                period = int.Parse(textBox11.Text);
                setPaths();
                try
                {
                    sp = new SerialPort(comboBox1.SelectedItem.ToString());                    
                    sp.BaudRate = int.Parse(textBox3.Text);
                    sp.DataReceived +=
                        new SerialDataReceivedEventHandler(sp_DataReceived);
                    sp.Open();
                    sp.Write(period+"");
                    textBox11.Enabled = false;
                    for (int i = 0; i < paths.Length; i++)
                    {
                        System.IO.StreamWriter sw = System.IO.File.AppendText(paths[i]);
                        sw.WriteLine("#Veri yazma periyodu : " + period);
                        sw.Close();
                    }
                    if (sp.IsOpen)
                    {
                        textBox2.Text = "Connected";
                        isOpen = true;
                        stopWatch = new System.Diagnostics.Stopwatch();
                        timer1.Enabled = true;
                        stopWatch.Start();
                    }
                }
                catch (Exception ex) { }
            }            
        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e) 
        {            
            string receivedMessage = (sender as SerialPort).ReadExisting();
            string s = receivedMessage.Substring(1,receivedMessage.Length-3);
            SetText(s);
            Data data = new Data();
            datas = Data.splitToArray(4, receivedMessage);
            for (int i = 0; i < datas.Length;  i++)
            {
                Writer.writeToFile(datas[i], paths[i]);
            }
            Writer.writeBlockToFile(s,paths[4]);
        }
        
        private void button3_Click(object sender, EventArgs e)
        {            
            if (isOpen)
            {
                sp.Close();  
                isOpen = false;
                textBox2.Text = "Disconnected";
                stopWatch.Stop();
                timer1.Enabled = false;
                for (int i = 0; i < paths.Length; i++)
                {
                    System.IO.StreamWriter sw = System.IO.File.AppendText(paths[i]);
                    sw.WriteLine("#Geçen zaman(saat): " + textBox9.Text);
                    sw.Close();
                }
            }
         }

        private void SetText(string text)
        {         
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text += text + Environment.NewLine;
            }
        }

        private void setPaths()
        {
            paths[0] = textBox4.Text + "\\" + textBox5.Text;
            paths[1] = textBox4.Text + "\\" + textBox6.Text;
            paths[2] = textBox4.Text + "\\" + textBox7.Text;
            paths[3] = textBox4.Text + "\\" + textBox8.Text;
            paths[4] = textBox4.Text + "\\blockData.txt";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox9.Text = stopWatch.Elapsed.Hours.ToString("00") + ":" +
                        stopWatch.Elapsed.Minutes.ToString("00") + ":" +
                        stopWatch.Elapsed.Seconds.ToString("00") + ":" +
                        stopWatch.Elapsed.Milliseconds.ToString("00");
        }
    }
}

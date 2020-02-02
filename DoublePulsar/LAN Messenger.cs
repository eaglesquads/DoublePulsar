using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoublePulsar
{
    public partial class LAN_Messenger : Form
    {

        Socket sck;
        EndPoint epLocal, epRemote;

        public LAN_Messenger(string text)
        {
            InitializeComponent();
            textBox3.Text = text;
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        }

        public void wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            //Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                //Console.WriteLine("stop wait timer");
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        private void MessageCallBack(IAsyncResult aResult)
        {

            try
            {
                int Size = sck.EndReceiveFrom(aResult, ref epRemote);
                if (Size > 0)
                {
                    byte[] receivedData = new byte[1464];
                    receivedData = (Byte[])aResult.AsyncState;
                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    string receivedMessage = eEncoding.GetString(receivedData);
                    listBox1.Items.Add(receivedMessage);

                }

                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);

            }

            catch (Exception ex)
            {

            }

        }



        private void LAN_Messenger_Load(object sender, EventArgs e)
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            Console.WriteLine(hostName);
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            textBox1.Text = myIP;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                epLocal = new IPEndPoint(IPAddress.Parse(textBox1.Text), Convert.ToInt32(mPort.Text));
                sck.Bind(epLocal);

                epRemote = new IPEndPoint(IPAddress.Parse(textBox2.Text), Convert.ToInt32(fPort.Text));
                sck.Connect(epRemote);

                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
                button1.Enabled = false;
                label5.Text = "Connected.";
                label5.ForeColor = System.Drawing.Color.Green;
                button2.Focus();
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                button3.Enabled = true;
                listBox1.Enabled = true;
                button2.Enabled = true;
                textBox5.Enabled = true;
                mPort.Enabled = false;
                fPort.Enabled = false;

                try
                {
                    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                    byte[] msg = new byte[1500];
                    msg = enc.GetBytes(textBox3.Text + " has entered the chat.");
                    sck.Send(msg);
                    listBox1.Items.Add("You have entered the chat.");
                    textBox5.Clear();
                }
                catch (Exception ex)
                {

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("An error has occured while trying to establish a connection. Please make sure that there are not connectivity issues and you have entered the corresponding Internet Protocols correctly.", "DoublePulsar");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] msg = new byte[1500];
                msg = enc.GetBytes(textBox3.Text + ": " + textBox5.Text);
                sck.Send(msg);
                listBox1.Items.Add(textBox3.Text + ": " + textBox5.Text);
                textBox5.Clear();
            }
            catch (Exception ex)
            {

            }
        }

        private void LAN_Messenger_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] msg = new byte[1500];
                msg = enc.GetBytes(textBox3.Text + " has terminated the session.");
                sck.Send(msg);
                listBox1.Items.Add("You have terminated the session.");
                textBox5.Clear();
            }
            catch (Exception ex)
            {

            }

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button3.Enabled = false;
            button1.Enabled = true;
            label5.Text = "Disconnected.";
            label5.ForeColor = System.Drawing.Color.DarkRed;

            this.Close();
            LAN_Messenger msger = new LAN_Messenger(textBox3.Text); 
            msger.ShowDialog();
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            if (mPort.Visible == false && fPort.Visible == false)
            {
               MessageBox.Show("If you are facing trouble sending messages or the chat is set to 1 way connection, please try setting custom ports. Make sure that the other users enters the ports accordingly for an efficient connection.", "DoublePulsar Framework");
                mPort.Visible = true;
                fPort.Visible = true;
            }
           
            else { mPort.Visible = false; fPort.Visible = false; }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] msg = new byte[1500];
                msg = enc.GetBytes(textBox3.Text + " has exited the chat.");
                sck.Send(msg);
                listBox1.Items.Add(textBox3.Text + " has left the chat.");
                textBox5.Clear();
            }
            catch (Exception ex)
            {

            }
            this.Close();
        }
    }
}

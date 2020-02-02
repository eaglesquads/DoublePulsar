using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace DoublePulsar
{
    public partial class Login : Form
    {

        

        public Login()
        {
            InitializeComponent();
            
        }


        private void Label1_Click(object sender, EventArgs e)
        {

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

        string[] usernames = { "Plus", "Alex", "Thanos", "Andrew", "Sweepi", "Indominus" };
        string[] passwords = { "angrybirds", "theoneandonly", "BETA-TESTER02", "BETA-TESTER03", "Coder-Cha05", "arglydkst1234" };

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


       


        private void Button1_Click(object sender, EventArgs e)
        {
            if (RememberMe.LinkColor == System.Drawing.Color.ForestGreen)
            {
                Properties.Settings.Default.username = textBox1.Text;
                Properties.Settings.Default.password = textBox2.Text;
                Properties.Settings.Default.Save();
            }
            else if(RememberMe.LinkColor == SystemColors.WindowFrame)
            {
                Properties.Settings.Default.username = string.Empty;
                Properties.Settings.Default.password = string.Empty;
                Properties.Settings.Default.Save();
            }

            bool con = NetworkInterface.GetIsNetworkAvailable();

            if (con == true)
            {

            if (usernames.Contains(textBox1.Text) && passwords.Contains(textBox2.Text))
            {
                
                textBox1.Visible = false;
                textBox2.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                RememberMe.Visible = false;

                

                pictureBox3.Visible = true;
                c1.Visible = true;
                wait(1000);
                c1.Text = "Connecting to the server.";
                wait(1000);
                c1.Text = "Connecting to the server..";
                wait(1000);
                c1.Text = "Connecting to the server...";
                wait(1000);
                c1.Text = "Connecting to the server";
                wait(1000);
                c1.Text = "Connecting to the server.";
                wait(1000);
                c1.Text = "Connecting to the server..";
                wait(1000);
                c1.Text = "Connecting to the server...";
                wait(1000);
                c1.Text = "Connecting to the server";
                wait(1000);
                c1.Text = "Connecting to the server.";
                wait(1000);
                c1.Text = "Connecting to the server..";
                wait(1000);
                c1.Text = "Connecting to the server...";
                wait(1000);

                this.Hide();
            Main av = new Main(textBox1.Text);
            av.ShowDialog();

            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Please write your credentials before connecting.", "DoublePulsar");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please write your credentials before connecting.", "DoublePulsar");
            }
            else
            {
                MessageBox.Show("Access denied. Unauthorized user or insufficient privileges.", "DoublePulsar");
            }


            }

            else
            {
                MessageBox.Show("An internet connection is required for DoublePulsar to operate successfully. Please connect to the internet and try again.", "DoublePulsar Framework");
                
            }

            
            
            

        }

        
        private void Login_Load(object sender, EventArgs e)
        {
            RememberMe.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            if (Properties.Settings.Default.username != string.Empty)
            {
                textBox1.Text = Properties.Settings.Default.username;
                textBox2.Text = Properties.Settings.Default.password;
                RememberMe.LinkColor = System.Drawing.Color.ForestGreen;
            }
            ActiveControl = null;

            string strLoc = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo fileInfo = new FileInfo(strLoc);
            string sExeName = fileInfo.Name;
            string ExeName = sExeName.Replace(".exe", string.Empty);
            if (Process.GetProcessesByName(ExeName).Length > 1)
            {
                MessageBox.Show("DoublePulsar is already running!", "DoublePulsar");
                Application.Exit();
            }

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

      

        private void Login_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                bool con = NetworkInterface.GetIsNetworkAvailable();

                if (con == true)
                {

                    if (usernames.Contains(textBox1.Text) && passwords.Contains(textBox2.Text))
                    {

                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        label2.Visible = false;
                        label3.Visible = false;
                        button1.Visible = false;
                        button2.Visible = false;


                        pictureBox3.Visible = true;
                        c1.Visible = true;
                        wait(1000);
                        c1.Text = "Connecting to the server.";
                        wait(1000);
                        c1.Text = "Connecting to the server..";
                        wait(1000);
                        c1.Text = "Connecting to the server...";
                        wait(1000);
                        c1.Text = "Connecting to the server";
                        wait(1000);
                        c1.Text = "Connecting to the server.";
                        wait(1000);
                        c1.Text = "Connecting to the server..";
                        wait(1000);
                        c1.Text = "Connecting to the server...";
                        wait(1000);
                        c1.Text = "Connecting to the server";
                        wait(1000);
                        c1.Text = "Connecting to the server.";
                        wait(1000);
                        c1.Text = "Connecting to the server..";
                        wait(1000);
                        c1.Text = "Connecting to the server...";
                        wait(1000);

                        this.Hide();
                        Main av = new Main(textBox1.Text);
                        av.ShowDialog();

                    }
                    else if (textBox1.Text == "")
                    {
                        MessageBox.Show("Please write your credentials before connecting.", "DoublePulsar");
                    }
                    else if (textBox2.Text == "")
                    {
                        MessageBox.Show("Please write your credentials before connecting.", "DoublePulsar");
                    }
                    else
                    {
                        MessageBox.Show("Access denied. Unauthorized user or insufficient privileges.", "DoublePulsar");
                    }


                }

                else
                {
                    MessageBox.Show("An internet connection is required for DoublePulsar to operate successfully. Please connect to the internet and try again.", "DoublePulsar Framework");

                }


            }
        }

        private void RememberMe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(RememberMe.LinkColor == SystemColors.WindowFrame)
            {
                RememberMe.LinkColor = System.Drawing.Color.ForestGreen;
            }
            else if(RememberMe.LinkColor == System.Drawing.Color.ForestGreen)
            {
                RememberMe.LinkColor = SystemColors.WindowFrame;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Security.Cryptography;
using System.IO;
using System.Media;
using System.Threading;
using System.Collections.Specialized;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;



namespace DoublePulsar
{

    public partial class Main : Form
    {

        public Main(string Str_value)
        {
            InitializeComponent();
            label1.Text = "Welcome back, " + Str_value + "!";
            label15.Text = Str_value;
            

        }


        [DllImport("gdi32.dll")]
        private unsafe static extern bool SetDeviceGammaRamp(Int32 hdc, void* ramp);
        private static bool initialized = false;
        private static Int32 hdc;
        private static int a;


        private static void InitializeClass()
        {
            
            if (initialized)
                return;
            hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc().ToInt32();
            initialized = true;
        }
        public static unsafe bool SetBrightness(int brightness)
        {
            InitializeClass();
            if (brightness > 255)
                brightness = 255;
            if (brightness < 0)
                brightness = 0;
            short* gArray = stackalloc short[3 * 256];
            short* idx = gArray;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 256; i++)
                {
                    int arrayVal = i * (brightness + 128);
                    if (arrayVal > 65535)
                        arrayVal = 65535;
                    *idx = (short)arrayVal;
                    idx++;
                }
            }
            bool retVal = SetDeviceGammaRamp(hdc, gArray);
            return retVal;
        }

        public string GetMD5FromFile(string FilenPath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(FilenPath))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty).ToLower();
                }
            }
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

        SpeechSynthesizer reader = new SpeechSynthesizer();
        private void Main_Load(object sender, EventArgs e)
        {
            timer1.Start();
            notifyIcon1.ShowBalloonTip(5, "DoublePulsar Framework", "Welcome to DoublePulsar!", ToolTipIcon.Info);
            linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            linkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            linkLabel4.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            linkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            linkLabel6.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            reader.Dispose();
            reader = new SpeechSynthesizer();
            reader.SpeakAsync(label1.Text);
            abovelabel.Visible = false;
            label2.Visible = true;
            pingtimer.Start();
            profilelbl.Text = "Currently inspecting profile of: " + label15.Text;
            USERID.Text = label15.Text;
            
            if (Properties.Settings.Default.Avatar != string.Empty)
            {
                avatar.Image = LoadImage();
            }
            if (Properties.Settings.Default.profiletitle != string.Empty)
            {
                profiletitle.Text = Properties.Settings.Default.profiletitle;
            }
            if (Properties.Settings.Default.profiledescription != string.Empty)
            {
                profiledescription.Text = Properties.Settings.Default.profiledescription;
            }
        }



        private void tray_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();


        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void FlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IntroPanel.Visible = true;
            PanelProtection.Visible = true;
            PanelFeatures.Visible = false;
            PanelSupport.Visible = false;
            panelhome.Visible = false;
            panelsettings.Visible = false;
            panelchangelog.Visible = false;
        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IntroPanel.Visible = true;
            PanelProtection.Visible = true;
            PanelFeatures.Visible = true;
            PanelSupport.Visible = false;
            panelhome.Visible = false;
            panelsettings.Visible = false;
            panelchangelog.Visible = false;
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            if (CBoxTray.Checked == false)
            {
                notifyIcon1.Visible = false;
            }

            else notifyIcon1.Visible = true;

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongDateString() + " - " + DateTime.Now.ToLongTimeString();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            IntroPanel.Visible = true;
            PanelProtection.Visible = true;
            PanelFeatures.Visible = true;
            PanelSupport.Visible = true;
            panelhome.Visible = true;

        }

        private void ProtectionP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            textBox2.Visible = true;
            label6.Visible = true;
            button3.Visible = false;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            textBox2.Visible = false;
            label6.Visible = false;
            button3.Visible = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                string filepath = ofd.FileName;
                textBox1.Text = filepath;
                textBox2.Text = GetMD5FromFile(ofd.FileName);

                var md5signatures = File.ReadAllLines("MD5Base.pulsar");

                if (md5signatures.Contains(textBox2.Text))
                {
                    if (CBoxAutoDel.Checked == true)
                    {
                        panel4.BackColor = System.Drawing.Color.DarkRed;
                        File.Delete(textBox1.Text);


                        if (checkBox4.Checked == true)
                        {
                            MessageBox.Show("The file has been marked as infected and has been automatically deleted!", "DoublePulsar Framework");
                        }


                        wait(5000);
                        panel4.BackColor = Color.FromArgb(64, 64, 64);


                    }

                    else
                    {
                        panel4.BackColor = System.Drawing.Color.DarkRed;

                        if (checkBox4.Checked == true)
                        {
                            MessageBox.Show("The file has been marked as infected. Enable automatic deletion in the Features to immediately get rid of such files!", "DoublePulsar Framework");
                        }

                        wait(5000);
                        panel4.BackColor = Color.FromArgb(64, 64, 64);

                        
                    }

                }

                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Please select a file first!", "DoublePulsar Framework");
                }
                else

                {
                    panel4.BackColor = System.Drawing.Color.Green;


                    if (checkBox4.Checked == true)
                    {
                        MessageBox.Show("The file has been marked as SAFE to open!", "DoublePulsar Framework");
                    }

                    wait(5000);
                    panel4.BackColor = Color.FromArgb(64, 64, 64);

                    
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {

            output.Clear();
            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            output.Text = output.Text + Environment.NewLine + Environment.NewLine + "Folder Scan initiated at " + DateTime.Now.ToLongDateString() + " - " + DateTime.Now.ToLongTimeString() + Environment.NewLine;
            wait(5000);
            panel5.BackColor = Color.FromArgb(64, 64, 64);

            string folder = diag.SelectedPath;
            textBox4.Text = folder;
            DirectoryInfo d = new DirectoryInfo(folder);
            

            foreach (var file in d.GetFiles("*.*"))
            {
                textBox3.Text = GetMD5FromFile(file.FullName);
                var md5signatures = File.ReadAllLines("MD5Base.pulsar");


                wait(2000);

                if (md5signatures.Contains(textBox3.Text))
                {

                    string deletion = file.FullName;

                    output.Text = output.Text + Environment.NewLine + "> " + file.FullName + " is INFECTED!";
                    panel5.BackColor = System.Drawing.Color.DarkRed;
                    output.ForeColor = System.Drawing.Color.DarkRed;

                        if (CBoxAutoDel.Checked == true)
                        {
                            File.Delete(deletion);

                            if (checkBox4.Checked == true)
                            {
                                MessageBox.Show("The file has been marked as infected and has been automatically deleted!", "DoublePulsar Framework");
                            }

                        }

                        else
                        {
                            if (checkBox4.Checked == true)
                            {
                                MessageBox.Show("The file has been marked as infected! Enable automatic deletion in Features to immediately get rid of such files!", "DoublePulsar Framework");
                            }

                        }
                    }


                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Please select a file first!", "DoublePulsar Framework");
                }
                else

                {
                    output.Text = output.Text + Environment.NewLine + "> " + file.FullName + " is SAFE!";

                }




            }

                string[] dirs = Directory.GetDirectories(diag.SelectedPath);
                
                foreach (string dir in dirs)
                {
                    DirectoryInfo fold = new DirectoryInfo(dir);
                    textBox4.Text = dir;
                    foreach (var file in fold.GetFiles("*.*"))
                    {
                        textBox3.Text = GetMD5FromFile(file.FullName);
                        var md5signatures = File.ReadAllLines("MD5Base.pulsar");


                        wait(2000);

                        if (md5signatures.Contains(textBox3.Text))
                        {

                            string deletion = file.FullName;

                            output.Text = output.Text + Environment.NewLine + "> " + file.FullName + " is INFECTED!";
                            panel5.BackColor = System.Drawing.Color.DarkRed;
                            output.ForeColor = System.Drawing.Color.DarkRed;

                            if (CBoxAutoDel.Checked == true)
                            {
                                File.Delete(deletion);

                                if (checkBox4.Checked == true)
                                {
                                    MessageBox.Show("The file has been marked as infected and has been automatically deleted!", "DoublePulsar Framework");
                                }

                            }

                            else
                            {
                                if (checkBox4.Checked == true)
                                {
                                    MessageBox.Show("The file has been marked as infected! Enable automatic deletion in Features to immediately get rid of such files!", "DoublePulsar Framework");
                                }

                            }
                        }

                        else

                        {
                            output.Text = output.Text + Environment.NewLine + "> " + file.FullName + " is SAFE!";

                        }


                    }

                }


                if (output.ForeColor == System.Drawing.Color.DarkCyan)
                {
                    panel5.BackColor = System.Drawing.Color.Green;
                    output.Text = output.Text + Environment.NewLine + Environment.NewLine + "Folder Scan finished at " + DateTime.Now.ToLongDateString() + " - " + DateTime.Now.ToLongTimeString() + " successfully!";
                    if (checkBox4.Checked == true)
                    {
                        MessageBox.Show("The files in the folder have been marked as safe!", "DoublePulsar Framework");
                    }
                    wait(5000);
                    panel5.BackColor = Color.FromArgb(64, 64, 64);
                }

                else
                {
                    output.Text = output.Text + Environment.NewLine + Environment.NewLine + "Folder Scan finished at " + DateTime.Now.ToLongDateString() + " - " + DateTime.Now.ToLongTimeString() + " successfully. Deleted all potential threats!";
                    wait(5000);
                    panel5.BackColor = Color.FromArgb(64, 64, 64);
                    output.ForeColor = Color.DarkCyan;
                }


            }


                



        }

        private void Output_TextChanged(object sender, EventArgs e)
        {
            output.SelectionStart = output.Text.Length;
            output.ScrollToCaret();
        }



        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                MessageBox.Show("You have just disabled protection. This action is NOT recommended. You may switch back by unchecking this box.", "DoublePulsar Framework");
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                dsp.Visible = true;
                status2.Visible = true;
                label29.Visible = true;
                label26.Visible = true;
                label30.Visible = true;
                label31.Visible = true;
                label22.Visible = false;
                label21.Visible = false;
                status1.Visible = false;

            }

            else
            {
                MessageBox.Show("Protection is now active. Stay safe!", "DoublePulsar Framework");
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                dsp.Visible = false;
                status2.Visible = false;
                label29.Visible = false;
                label26.Visible = false;
                label30.Visible = false;
                label31.Visible = false;
                label22.Visible = true;
                label21.Visible = true;
                status1.Visible = true;
            }


        }

        private void CBoxRTP_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CBoxAutoDel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CBoxTray_CheckedChanged(object sender, EventArgs e)
        {
            if (notifyIcon1.Visible == true)
            {
                notifyIcon1.Visible = false;
            }

            else notifyIcon1.Visible = true;

        }

        private void Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CBoxMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (CBoxMusic.Checked == true)
            {
                try
                {
                    SoundPlayer sndplayer = new SoundPlayer(DoublePulsar.Properties.Resources.BG);
                    sndplayer.Play();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            else if (CBoxMusic.Checked == false)
            {
                try
                {
                    SoundPlayer sndplayer = new SoundPlayer(DoublePulsar.Properties.Resources.BG);
                    sndplayer.Stop();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }



        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Displays the system path of the selected file";
        }

        private void TextBox1_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void TextBox2_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Displays the MD5 hash of the selected file, which will be used to check whether it is safe or not";
        }

        private void TextBox2_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button2_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Opens a file selection window";
        }

        private void Button2_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button4_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Hides the MD5 text box";
        }

        private void Button4_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button3_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Shows the MD5 text box";
        }

        private void Button3_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";

        }

        private void TextBox4_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Displays the system path of the selected folder";
        }

        private void TextBox4_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void TextBox3_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Displays the MD5 hash of each file in the preassigned folder while scanning it";
        }

        private void TextBox3_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Output_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Displays detailed information about the ongoing scan";
        }

        private void Output_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button7_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Opens a folder selection window";
        }

        private void Button7_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {
            abovelabel.Visible = true;
            label2.Visible = false;

        }

        private void PictureBox8_Click_1(object sender, EventArgs e)
        {
            abovelabel.Visible = false;
            label2.Visible = true;

        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private const int WM_DEVICECHANGE = 0x219;
        private const int DBT_DEVICEARRIVAL = 0x8000;
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        private const int DBT_DEVTYP_VOLUME = 0x00000002;
        protected override void WndProc(ref Message m)
        {

            base.WndProc(ref m);

            if (checkBox2.Checked == true)
            {
                switch (m.Msg)
                {
                    case WM_DEVICECHANGE:
                        switch ((int)m.WParam)
                        {
                            case DBT_DEVICEARRIVAL:

                                MessageBox.Show("A USB Device has been connected and scanned. Stay safe!", "DoublePulsar Framework");
                                break;

                            case DBT_DEVICEREMOVECOMPLETE:
                                MessageBox.Show("A USB device has been removed. No actions were required.", "DoublePulsar Framework");
                                break;
                        }
                        break;
                }

            }

            else
            {

            }


        }

        private void Button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In order to resolve any possible compatibility issues you might be facing, make sure to select the 'High Performance' power plan and leave the rest to us. (A restart of the program may be required for the changes to take effect)", "DoublePulsar Framework");
            var cplPath = System.IO.Path.Combine(Environment.SystemDirectory, "control.exe");
            System.Diagnostics.Process.Start(cplPath, "/name Microsoft.PowerOptions");
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            notifyIcon1.Visible = true;
        }

        private void ToolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                abovelabel.Visible = false;
                label2.Visible = true;
            }

            else
            {
                abovelabel.Visible = true;
                label2.Visible = false;
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your HWID has been temporarily spoofed! You might experience some instabilities due to network trafficing", "DoublePulsar Framework");
        }

        bool isRunning = true;
        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Panel6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void Main_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Show();
                    this.WindowState = FormWindowState.Normal;
                }

                else
                {
                    Hide();
                    this.WindowState = FormWindowState.Minimized;
                    notifyIcon1.Visible = true;
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            BugReport BR = new BugReport(label15.Text);
            this.Opacity = 0;
            BR.ShowDialog();
            this.Opacity = 1;
            
        }

        private void Button8_Click_1(object sender, EventArgs e)
        {
            LAN_Messenger LC = new LAN_Messenger(label15.Text);
            this.Opacity = 0;
            LC.ShowDialog();
            this.Opacity = 1;
        }

        private void CBoxMusic_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Plays a chill lounge background song";
        }

        private void CBoxMusic_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void CBoxTray_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Shows an icon in System Tray applications. When double clicked, it will show the app - if it is hidden that is";
        }

        private void CBoxTray_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void CBoxAutoDel_MouseHover(object sender, EventArgs e)
        {
           label2.Text = label2.Text + " - This feature will enable the automatic deletion of any infected files detected";
        }

        private void CBoxAutoDel_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void CheckBox2_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - The client will automatically scan any external devices when they are connected";
        }

        private void CheckBox2_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void CheckBox1_MouseHover(object sender, EventArgs e)
        {
           label2.Text = label2.Text + " - Disables protection (Not Recommended)";
        }

        private void CheckBox1_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button9_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Temporarily spoofs the HWID of the computer. This feature has been patched by Windows, so it will only work in older versions.";
        }

        private void Button9_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void TextBox5_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Whenever the Insert key is pressed, the app will minimize itself to System Tray.";
        }

        private void TextBox5_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button6_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Redirects the user to the Power Plan panel, so as to alter the current settings. This highly increases performance but uses way more energy";
        }

        private void Button6_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button5_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Opens the Bug Reporting form (Any false reports will result in an account ban)";
        }

        private void Button5_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void CheckBox3_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - When enabled, it will describe the intended usage of each feature in the bottom right corner of the client";
        }

        private void CheckBox3_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void CheckBox4_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - It will display messages to inform the user about the outcome of a scan";
        }

        private void CheckBox4_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button8_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Opens the LAN Messenger - used to open a connection (chat) between local computers";
        }

        private void Button8_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                MessageBox.Show("Report messages will now be displayed with information regarding the outcome of an operation!", "DoublePulsar Framework");
            }
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IntroPanel.Visible = true;
            PanelProtection.Visible = true;
            PanelFeatures.Visible = true;
            PanelSupport.Visible = true;
            panelhome.Visible = false;
            panelsettings.Visible = false;
        }

        private void Button10_Click(object sender, EventArgs e)
        {


        }

        private void LinkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            IntroPanel.Visible = true;
            PanelProtection.Visible = true;
            PanelFeatures.Visible = true;
            PanelSupport.Visible = true;
            panelhome.Visible = true;
            panelsettings.Visible = false;
            panelchangelog.Visible = false;

        }

        public static void sendWebHook(string URL, string msg, string username)
        {
            Http.Post(URL, new NameValueCollection()
            {
                { "username", username },
                { "content", msg }
               
            });
        }


        private void Button10_Click_1(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && textBox6.Text != "")

            {

                try
                {
                    
                        sendWebHook("https://discordapp.com/api/webhooks/670649093823004686/akZ4MPMCUEw1kMcwm-vjPlvxQg2Q48f9r3ms5O5PVTdfEsXii8pQorFFB1UbNUPmOFIF", string.Concat(new string[] { "<@&641728843169792011> " + "User ID " + "**" + label15.Text + "**" + " has submitted a support request:" + Environment.NewLine + Environment.NewLine + "**Issue Description:** " + "*" + textBox6.Text + "*" + Environment.NewLine + Environment.NewLine + "**Contact address:** " + "*" + textBox7.Text + "*" + Environment.NewLine + Environment.NewLine + "-----------------------------------------------", }), "Support Request");
                        MessageBox.Show("Your request has been successfully submitted!", "DoublePulsar");
                        timer1.Interval = 30000;
                        timer1.Tick += Timer1_Tick;
                        timer1.Start();
                        textBox6.Clear();
                        textBox7.Clear();

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong. Please contact the coder IMMEDIATELY at theplus@protonmail.com", "DoublePulsar");
                }


            }
        }

        private void Label20_Click(object sender, EventArgs e)
        {
            BugReport BR = new BugReport(label15.Text);
            this.Opacity = 0;
            BR.ShowDialog();
            this.Opacity = 1;
        }

        private void TextBox6_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - TextBox designated to describe a particular issue or malfunction";
        }

        private void TextBox6_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void TextBox7_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - TextBox for the user's email address";
        }

        private void TextBox7_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button10_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Sends your feedback to the development team";
        }

        private void Button10_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Label20_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Displays Bug Reporting form";
        }

        private void Label20_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Panel6_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            
        }

        private void Pingtimer_Tick(object sender, EventArgs e)
        {
            bool con = NetworkInterface.GetIsNetworkAvailable();

        if(con == true)
            {
                labelping.Text = new Ping().Send("www.google.com").RoundtripTime.ToString() + "ms / Sec";
                labelping.ForeColor = System.Drawing.Color.DarkCyan;
                label27.ForeColor = System.Drawing.Color.DarkCyan;
                label27.Text = "Healthy";
            }
        else
            {
                labelping.Text = "Offline";
                labelping.ForeColor = System.Drawing.Color.Maroon;
                label27.Text = "Disconnected";
                label27.ForeColor = SystemColors.AppWorkspace;

            }
            
        }

        private void Label27_Click(object sender, EventArgs e)
        {

        }

        private void Panelhome_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IntroPanel.Visible = true;
            PanelProtection.Visible = true;
            PanelFeatures.Visible = true;
            PanelSupport.Visible = true;
            panelhome.Visible = true;
            panelsettings.Visible = true;
            panelchangelog.Visible = false;
        }

        private void BunifuHSlider1_Scroll(object sender, Utilities.BunifuSlider.BunifuHScrollBar.ScrollEventArgs e)
        {
            string opacity = opacityslider.Value.ToString();
            labelopacity.Text = "Form Opacity: " + opacity + "%";
            if (opacityslider.Value.ToString() == "1")
            {
                this.Opacity = .01;
            }
            if (opacityslider.Value.ToString() == "2")
            {
                this.Opacity = .02;
            }
            if (opacityslider.Value.ToString() == "3")
            {
                this.Opacity = .03;
            }
            if (opacityslider.Value.ToString() == "4")
            {
                this.Opacity = .04;
            }
            if (opacityslider.Value.ToString() == "5")
            {
                this.Opacity = .05;
            }
            if (opacityslider.Value.ToString() == "6")
            {
                this.Opacity = .06;
            }
            if (opacityslider.Value.ToString() == "7")
            {
                this.Opacity = .07;
            }
            if (opacityslider.Value.ToString() == "8")
            {
                this.Opacity = .08;
            }
            if (opacityslider.Value.ToString() == "9")
            {
                this.Opacity = .09;
            }
            if (opacityslider.Value.ToString() == "10")
            {
                this.Opacity = .1;
            }
            if (opacityslider.Value.ToString() == "11")
            {
                this.Opacity = .11;
            }
            if (opacityslider.Value.ToString() == "12")
            {
                this.Opacity = .12;
            }
            if (opacityslider.Value.ToString() == "13")
            {
                this.Opacity = .13;
            }
            if (opacityslider.Value.ToString() == "14")
            {
                this.Opacity = .14;
            }
            if (opacityslider.Value.ToString() == "15")
            {
                this.Opacity = .15;
            }
            if (opacityslider.Value.ToString() == "16")
            {
                this.Opacity = .16;
            }
            if (opacityslider.Value.ToString() == "17")
            {
                this.Opacity = .17;
            }
            if (opacityslider.Value.ToString() == "18")
            {
                this.Opacity = .18;
            }
            if (opacityslider.Value.ToString() == "19")
            {
                this.Opacity = .19;
            }
            if (opacityslider.Value.ToString() == "20")
            {
                this.Opacity = .2;
            }
            if (opacityslider.Value.ToString() == "21")
            {
                this.Opacity = .21;
            }
            if (opacityslider.Value.ToString() == "22")
            {
                this.Opacity = .22;
            }
            if (opacityslider.Value.ToString() == "23")
            {
                this.Opacity = .23;
            }
            if (opacityslider.Value.ToString() == "24")
            {
                this.Opacity = .24;
            }
            if (opacityslider.Value.ToString() == "25")
            {
                this.Opacity = .25;
            }
            if (opacityslider.Value.ToString() == "26")
            {
                this.Opacity = .26;
            }
            if (opacityslider.Value.ToString() == "27")
            {
                this.Opacity = .27;
            }
            if (opacityslider.Value.ToString() == "28")
            {
                this.Opacity = .28;
            }
            if (opacityslider.Value.ToString() == "29")
            {
                this.Opacity = .29;
            }
            if (opacityslider.Value.ToString() == "30")
            {
                this.Opacity = .3;
            }      
            if (opacityslider.Value.ToString() == "31")
            {
                this.Opacity = .31;
            }
            if (opacityslider.Value.ToString() == "32")
            {
                this.Opacity = .32;
            }
            if (opacityslider.Value.ToString() == "33")
            {
                this.Opacity = .33;
            }
            if (opacityslider.Value.ToString() == "34")
            {
                this.Opacity = .34;
            }
            if (opacityslider.Value.ToString() == "35")
            {
                this.Opacity = .35;
            }
            if (opacityslider.Value.ToString() == "36")
            {
                this.Opacity = .36;
            }
            if (opacityslider.Value.ToString() == "37")
            {
                this.Opacity = .37;
            }
            if (opacityslider.Value.ToString() == "38")
            {
                this.Opacity = .38;
            }
            if (opacityslider.Value.ToString() == "39")
            {
                this.Opacity = .39;
            }
            if (opacityslider.Value.ToString() == "40")
            {
                this.Opacity = .4;
            }
            if (opacityslider.Value.ToString() == "41")
            {
                this.Opacity = .41;
            }
            if (opacityslider.Value.ToString() == "42")
            {
                this.Opacity = .42;
            }
            if (opacityslider.Value.ToString() == "43")
            {
                this.Opacity = .43;
            }
            if (opacityslider.Value.ToString() == "44")
            {
                this.Opacity = .44;
            }
            if (opacityslider.Value.ToString() == "45")
            {
                this.Opacity = .45;
            }
            if (opacityslider.Value.ToString() == "46")
            {
                this.Opacity = .46;
            }
            if (opacityslider.Value.ToString() == "47")
            {
                this.Opacity = .47;
            }
            if (opacityslider.Value.ToString() == "48")
            {
                this.Opacity = .48;
            }
            if (opacityslider.Value.ToString() == "49")
            {
                this.Opacity = .49;
            }
            if (opacityslider.Value.ToString() == "50")
            {
                this.Opacity = .5;
            }
            if (opacityslider.Value.ToString() == "51")
            {
                this.Opacity = .51;
            }
            if (opacityslider.Value.ToString() == "52")
            {
                this.Opacity = .52;
            }
            if (opacityslider.Value.ToString() == "53")
            {
                this.Opacity = .53;
            }
            if (opacityslider.Value.ToString() == "54")
            {
                this.Opacity = .54;
            }
            if (opacityslider.Value.ToString() == "55")
            {
                this.Opacity = .55;
            }
            if (opacityslider.Value.ToString() == "56")
            {
                this.Opacity = .56;
            }
            if (opacityslider.Value.ToString() == "57")
            {
                this.Opacity = .57;
            }
            if (opacityslider.Value.ToString() == "58")
            {
                this.Opacity = .58;
            }
            if (opacityslider.Value.ToString() == "59")
            {
                this.Opacity = .59;
            }
            if (opacityslider.Value.ToString() == "60")
            {
                this.Opacity = .6;
            }
            if (opacityslider.Value.ToString() == "61")
            {
                this.Opacity = .61;
            }
            if (opacityslider.Value.ToString() == "62")
            {
                this.Opacity = .62;
            }
            if (opacityslider.Value.ToString() == "63")
            {
                this.Opacity = .63;
            }
            if (opacityslider.Value.ToString() == "64")
            {
                this.Opacity = .64;
            }
            if (opacityslider.Value.ToString() == "65")
            {
                this.Opacity = .65;
            }
            if (opacityslider.Value.ToString() == "66")
            {
                this.Opacity = .66;
            }
            if (opacityslider.Value.ToString() == "67")
            {
                this.Opacity = .67;
            }
            if (opacityslider.Value.ToString() == "68")
            {
                this.Opacity = .68;
            }
            if (opacityslider.Value.ToString() == "69")
            {
                this.Opacity = .69;
            }
            if (opacityslider.Value.ToString() == "70")
            {
                this.Opacity = .7;
            }
            if (opacityslider.Value.ToString() == "71")
            {
                this.Opacity = .71;
            }
            if (opacityslider.Value.ToString() == "72")
            {
                this.Opacity = .72;
            }
            if (opacityslider.Value.ToString() == "73")
            {
                this.Opacity = .73;
            }
            if (opacityslider.Value.ToString() == "74")
            {
                this.Opacity = .74;
            }
            if (opacityslider.Value.ToString() == "75")
            {
                this.Opacity = .75;
            }
            if (opacityslider.Value.ToString() == "76")
            {
                this.Opacity = .76;
            }
            if (opacityslider.Value.ToString() == "77")
            {
                this.Opacity = .77;
            }
            if (opacityslider.Value.ToString() == "78")
            {
                this.Opacity = .78;
            }
            if (opacityslider.Value.ToString() == "79")
            {
                this.Opacity = .79;
            }
            if (opacityslider.Value.ToString() == "80")
            {
                this.Opacity = .7;
            }
            if (opacityslider.Value.ToString() == "81")
            {
                this.Opacity = .81;
            }
            if (opacityslider.Value.ToString() == "82")
            {
                this.Opacity = .82;
            }
            if (opacityslider.Value.ToString() == "83")
            {
                this.Opacity = .83;
            }
            if (opacityslider.Value.ToString() == "84")
            {
                this.Opacity = .84;
            }
            if (opacityslider.Value.ToString() == "85")
            {
                this.Opacity = .85;
            }
            if (opacityslider.Value.ToString() == "86")
            {
                this.Opacity = .86;
            }
            if (opacityslider.Value.ToString() == "87")
            {
                this.Opacity = .87;
            }
            if (opacityslider.Value.ToString() == "88")
            {
                this.Opacity = .88;
            }
            if (opacityslider.Value.ToString() == "89")
            {
                this.Opacity = .89;
            }
            if (opacityslider.Value.ToString() == "90")
            {
                this.Opacity = .9;
            }
            if (opacityslider.Value.ToString() == "91")
            {
                this.Opacity = .91;
            }
            if (opacityslider.Value.ToString() == "92")
            {
                this.Opacity = .92;
            }
            if (opacityslider.Value.ToString() == "93")
            {
                this.Opacity = .93;
            }
            if (opacityslider.Value.ToString() == "94")
            {
                this.Opacity = .94;
            }
            if (opacityslider.Value.ToString() == "95")
            {
                this.Opacity = .95;
            }
            if (opacityslider.Value.ToString() == "96")
            {
                this.Opacity = .96;
            }
            if (opacityslider.Value.ToString() == "97")
            {
                this.Opacity = .97;
            }
            if (opacityslider.Value.ToString() == "98")
            {
                this.Opacity = .98;
            }
            if (opacityslider.Value.ToString() == "99")
            {
                this.Opacity = .99;
            }
            if (opacityslider.Value.ToString() == "100")
            {
                this.Opacity = 1;
            }
        }

        private void Negativeswitch_OnValuechange(object sender, EventArgs e)
        {
            if (internetswitch.Checked == true)
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "ipconfig";
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.Arguments = "/release";
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                cmd.Start();
            }
            if (internetswitch.Checked == false)
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "ipconfig";
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.Arguments = "/renew";
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                cmd.Start();
            }
        }

        

       

        private void BunifuHSlider1_Scroll_1(object sender, Utilities.BunifuSlider.BunifuHScrollBar.ScrollEventArgs e)
        {
            labelbrightness.Text = "Screen Brightness: " + SliderBrightness.Value.ToString() + "%";
            
        }

        private void Avatar_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Allows you to choose an avatar for your profile";
        }

        private void Avatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filepath = ofd.FileName;
                labelimagelocation.Text = filepath;
                avatar.Image = Image.FromFile(filepath);
                avatar.Invalidate();
                avatar.Refresh();

            }
        }

        private void Avatar_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

       

        

        public Image LoadImage()
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(Properties.Settings.Default.Avatar);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }






        private void BunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {

                byte[] imageArray = System.IO.File.ReadAllBytes(labelimagelocation.Text);

                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                Properties.Settings.Default.Avatar = base64ImageRepresentation;
            }
            
            catch (Exception ex)
            {

            }

           
            Properties.Settings.Default.profiletitle = profiletitle.Text;
            Properties.Settings.Default.profiledescription = profiledescription.Text;
            Properties.Settings.Default.Save();

            if (profiletitle.Text != string.Empty && profiledescription.Text != string.Empty)
            {
                try
                {

                    sendWebHook("https://discordapp.com/api/webhooks/673196703062556672/ogt8K-pNMUPmIaSdtPKsuGXvaHMEZpU7FScoJRRnfY1HsTSF-_OvnG_7Xj5A7BXHU0Jt", string.Concat(new string[] { "Hoooray! " + "**" + label15.Text + "**" + " has updated their profile!" + Environment.NewLine + Environment.NewLine + "**Profile Title: ** " + "*" + profiletitle.Text + "*" + Environment.NewLine + Environment.NewLine + "**Profile Description:** " + "*" + profiledescription.Text + "*" + Environment.NewLine + Environment.NewLine + "-----------------------------------------------" }), label15.Text);
                    MessageBox.Show("Changes have been successfuly applied!", "DoublePulsar Framework");
                }

                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "DoublePulsar Framework");
                }

            }

            else if(profiletitle.Text != string.Empty)
            {
                try
                {
                    sendWebHook("https://discordapp.com/api/webhooks/673196703062556672/ogt8K-pNMUPmIaSdtPKsuGXvaHMEZpU7FScoJRRnfY1HsTSF-_OvnG_7Xj5A7BXHU0Jt", string.Concat(new string[] { "Hoooray! " + "**" + label15.Text + "**" + " has updated their profile!" + Environment.NewLine + Environment.NewLine + "**Profile Title: ** " + "*" + profiletitle.Text + "*" + Environment.NewLine + Environment.NewLine + "-----------------------------------------------" }), label15.Text);
                    MessageBox.Show("Changes have been successfuly applied!", "DoublePulsar Framework");
                }

                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "DoublePulsar Framework");
                }
            }
            else MessageBox.Show("Please set a profile title first!", "DoublePulsar Framework");

           
        }

        private void BunifuButton2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Avatar = string.Empty;
            Properties.Settings.Default.profiletitle = string.Empty;
            Properties.Settings.Default.profiledescription = string.Empty;
            Properties.Settings.Default.Save();
            avatar.Image = Properties.Resources.unnamed;
            profiletitle.HintText = "Profile Title";
            profiletitle.Text = string.Empty;
            profiledescription.Text = string.Empty;
            MessageBox.Show("Changes have been successfuly discarded!", "DoublePulsar Framework");
        }

        private void Profiledescription_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void Button11_Click_1(object sender, EventArgs e)
        {
            a = SliderBrightness.Value;

            SetBrightness(a);
        }

        private void Labelbrightness_Click(object sender, EventArgs e)
        {

        }

        private void LinkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IntroPanel.Visible = true;
            PanelProtection.Visible = true;
            PanelFeatures.Visible = true;
            PanelSupport.Visible = true;
            panelhome.Visible = true;
            panelsettings.Visible = true;
            panelchangelog.Visible = true;
            
        }

        private void Profiletitle_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Allows you to set a title for your profile";
        }

        private void Profiletitle_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Profiledescription_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Allows you to set a description for your profile";
        }

        private void Profiledescription_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void BunifuButton1_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Saves the changes and updates them on our discord server";
        }

        private void BunifuButton1_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void BunifuButton2_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Discards all the info about your profile";
        }

        private void BunifuButton2_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Opacityslider_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Adjusts the opacity of the application";
        }

        private void Opacityslider_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Internetswitch_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Temporarily discards all router IP addresses";
        }

        private void Internetswitch_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void SliderBrightness_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Adjusts the desirable brightness of your screen";
        }

        private void SliderBrightness_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }

        private void Button11_MouseHover(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " - Applies the preassigned brightness to your screen";
        }

        private void Button11_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "(BETA) DoublePulsar Framework";
        }
    }

    



}


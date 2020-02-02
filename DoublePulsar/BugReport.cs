using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Web;
using System.Net;
using System.Collections.Specialized;

namespace DoublePulsar
{
    public partial class BugReport : Form
    {
        public BugReport(string Str_value)
        {
            InitializeComponent();
            label6.Text = Str_value;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void BugReport_Load(object sender, EventArgs e)
        {

        }

        public static void sendWebHook(string URL, string msg, string username)
        {
            Http.Post(URL, new NameValueCollection()
            {
                { "username", username },
                { "content", msg }
            });
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && textBox1.Text != "" && textBox2.Text != "")

            {

                try
                {
                    if(comboBox2.Text != "")
                    {
                        sendWebHook("https://discordapp.com/api/webhooks/668498556578955344/zj9_0JUaleblWGINJlf5BeJPOLaMzT0WLImMB9RysCYfV_OCOm-ChKKpYW4aEcetZT8l", string.Concat(new string[] { "<@&641728843169792011> " + "User ID " + "**" + label6.Text + "**" + " has submitted a bug report:" + Environment.NewLine + Environment.NewLine + "**Form:** " + "*" + comboBox1.Text + "*" + Environment.NewLine + Environment.NewLine + "**Section:** " + "*" + comboBox2.Text + "*" + Environment.NewLine + Environment.NewLine + "**Subject:** " + "*" + textBox1.Text + "*" + Environment.NewLine + Environment.NewLine + "**Issue Description:** " + Environment.NewLine + "*" + textBox2.Text + "*" + Environment.NewLine + Environment.NewLine + "-----------------------------------------------", }), "Bug Assistant");
                        MessageBox.Show("The feedback was sent successfully! Thanks for your contribution!", "DoublePulsar");
                        timer1.Interval = 30000;
                        timer1.Tick += Timer1_Tick;
                        timer1.Start();
                    }

                    else if(comboBox2.Text == "")
                    {
                        sendWebHook("https://discordapp.com/api/webhooks/668498556578955344/zj9_0JUaleblWGINJlf5BeJPOLaMzT0WLImMB9RysCYfV_OCOm-ChKKpYW4aEcetZT8l", string.Concat(new string[] { "<@&641728843169792011> " + "User ID " + "**" + label6.Text + "**" + " has submitted a bug report:" + Environment.NewLine + Environment.NewLine + "**Form:** " + "*" + comboBox1.Text + "*" + Environment.NewLine + Environment.NewLine + "**Subject:** " + "*" + textBox1.Text + "*" + Environment.NewLine + Environment.NewLine + "**Issue Description:** " + Environment.NewLine + "*" + textBox2.Text + "*" + Environment.NewLine + Environment.NewLine + "-----------------------------------------------", }), "Bug Assistant");
                        MessageBox.Show("The feedback was sent successfully! Thanks for your contribution!", "DoublePulsar");
                        timer1.Interval = 30000;
                        timer1.Tick += Timer1_Tick;
                        timer1.Start();
                    }

                    
                    
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong. Please contact the coder IMMEDIATELY at theplus@protonmail.com", "DoublePulsar");
                }


            }

            else {

                MessageBox.Show("Please fill in all the fields in order to submit the report", "DoublePulsar Framework");

                 }
            


        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Main Program")
            {
                comboBox2.Visible = true;
                label2.Visible = true;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}

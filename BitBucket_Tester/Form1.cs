using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Mail;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using System.Net;

namespace BitBucket_Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int counter = 0;
            while (1 == 1)
            {

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                startInfo.FileName = @"C:\git_checkout.bat";
                Stopwatch time = new Stopwatch();
                time.Start();
                process.StartInfo = startInfo;

                // Start while loop
                //run SVN Checkout Script + Begin timer
                label1.Text = "Starting checkout...";
                process.Start();
                label1.Text = "Checking out...";
                process.WaitForExit();
                time.Stop();
                label1.Text = "Checkout complete!";
                label3.Text = time.Elapsed.ToString();
                SlackUpdate(label3.Text);
                //If error or takes too long, send email alert. (process.exit)

                if (process.ExitCode != 0)

                {
                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("devsubversion02@gmail.com", "UltimateSubversion");

                    MailMessage mm = new MailMessage("devsubversion02@gmail.com", "Dylan_Ramsook@ultimatesoftware.com", "BitBucket", "test");
                    mm.BodyEncoding = UTF8Encoding.UTF8;
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    client.Send(mm);
                }

                else
                {
                    //End Timer + Delete Checkout results if successful
                    System.Diagnostics.Process delete = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
                    startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    label1.Text = "Deleting files...";
                    startInfo2.FileName = @"C:\DeleteRepo.bat";

                    delete.StartInfo = startInfo2;

                    // Start while loop
                    //run SVN Checkout Script + Begin timer
                    delete.Start();
                    delete.WaitForExit();
                    label1.Text = "Files Deleted!";
                    //Run again  http://stackoverflow.com/questions/27423401/read-error-message-from-batch-file-from-c-sharp
                    counter++;
                    textBox1.Text = counter.ToString();
                }
                while (Directory.Exists(@"C:\tax-mangement-core"))
                {
                    //End Timer + Delete Checkout results if successful
                    System.Diagnostics.Process delete = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
                    startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    label1.Text = "Deleting files...";
                    startInfo2.FileName = @"C:\DeleteRepo.bat";

                    delete.StartInfo = startInfo2;

                    // Start while loop
                    //run SVN Checkout Script + Begin timer
                    delete.Start();
                    delete.WaitForExit();
                    label1.Text = "Files Deleted!";
                }

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //End Timer + Delete Checkout results if successful
            System.Diagnostics.Process delete = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
            startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            label1.Text = "Deleting files...";
            startInfo2.FileName = @"C:\DeleteRepo.bat";

            delete.StartInfo = startInfo2;

            // Start while loop
            //run SVN Checkout Script + Begin timer
            delete.Start();
            delete.WaitForExit();
            label1.Text = "Files Deleted!";

        }

        void SlackUpdate( String slackmessage)
        {
            string urlWithAccessToken = "https://hooks.slack.com/services/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";

            SlackClient client = new SlackClient(urlWithAccessToken);

            client.PostMessage(username: "dylan_ramsook",
                       text: slackmessage + "to clone Tax Management Core" ,
                       channel: "#vcs_stats");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SlackUpdate("test message");
        }
    }


}



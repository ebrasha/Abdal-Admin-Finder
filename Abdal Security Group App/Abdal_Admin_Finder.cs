using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Abdal_Admin_Finder;


namespace Abdal_Security_Group_App
{
    public partial class Abdal_Admin_Finder : Telerik.WinControls.UI.RadForm
    {
      
       
        

        public Abdal_Admin_Finder()
        {
            InitializeComponent();
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Text = "Abdal Admin Finder" + " " + version.Major + "." + version.Minor; //change form title
            bgWorker_traffic_gen.WorkerReportsProgress = true;
            bgWorker_traffic_gen.WorkerSupportsCancellation = true;

           

        }

        private void EncryptToggleSwitch_ValueChanged(object sender, EventArgs e)
        {
            
        }

 
        private void Abdal_2Key_Triple_DES_Builder_Load(object sender, EventArgs e)
        {
            // Call Global Chilkat Unlock
            Abdal_Security_Group_App.GlobalUnlockChilkat GlobalUnlock = new Abdal_Security_Group_App.GlobalUnlockChilkat();
            GlobalUnlock.unlock();
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        

        private void randButton_Click(object sender, EventArgs e)
        {

           

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            AbdalControler.stop_force_process = false;
            ExtractRichTextBox.Text = "";

            string[] DangerNameArray = { "abdal",
                "ebrasha",
                "hackers.zone",
                "mambanux",
                "nahaanbin",
                "blackwin"};

            // Check Target Url
            foreach (var DangerName in DangerNameArray)
            {

                new Thread(() =>
                {
                    Regex regex = new Regex(@"" + DangerName + ".*");
                    
                        if (regex.Match(targetUrlTextBox.Text.ToLower()).Success)
                        {

                           AbdalControler.unauthorized_process = true;
                            
                            
                        }

                    

                }).Start();


            }


            



           if (AbdalControler.unauthorized_process == true)
            {
                MessageBox.Show("This domain is unauthorized !");
                
            }
            else
            {
                if (bgWorker_traffic_gen.IsBusy != true)
                {
                    ResultTextEditor.Text = "";
                    radProgressBar1.Value1 = 0;
                    radProgressBar1.Value2 = 0;
                    // Start the asynchronous operation.
                    bgWorker_traffic_gen.RunWorkerAsync();
                }
            }

       




        }

        private void cancelPenTest_Click(object sender, EventArgs e)
        {
           AbdalControler.stop_force_process = true;
            if (bgWorker_traffic_gen.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                bgWorker_traffic_gen.CancelAsync();
            }


        }

       

        private void bgWorker_req_maker_DoWork(object sender, DoWorkEventArgs e)
        {
            string targetUrl = targetUrlTextBox.Text;
            Uri targetUrlSchemeCheck;
            bool resulttargetUrlSchemeCheck = Uri.TryCreate(targetUrl, UriKind.Absolute, out targetUrlSchemeCheck)
                && (targetUrlSchemeCheck.Scheme == Uri.UriSchemeHttp || targetUrlSchemeCheck.Scheme == Uri.UriSchemeHttps);


           if (resulttargetUrlSchemeCheck == false)
            {
                MessageBox.Show("Target Domian No valid !");
            }
            else if (AbdalControler.unauthorized_process == true)
            {
                MessageBox.Show("This domain is unauthorized !");
                Application.Exit();


            }
            else
            {

                try
                {

                    if (targetUrl.Last().ToString() == "/")
                    {


                        targetUrl = targetUrl.Substring(0, targetUrlTextBox.Text.Length - 1);

                    }


                    if (targetUrlTextBox.Text == "")
                    {
                        MessageBox.Show("Before Pentest, Target Must be set ! ");
                    }
                    else
                    {


                        BackgroundWorker worker = sender as BackgroundWorker;

                        Chilkat.Http http = new Chilkat.Http();
                        

                        int number_of_attack_req = 1;
                        radProgressBar1.Maximum = AdminPageAddress.admin_address.Count(); 
                        radProgressBar1.Minimum = 0;
                       int radProgressBar1_counter = 0;



                        //Sound Alert For Start Attack
                        using (var soundPlayer = new SoundPlayer(@"start.wav"))
                        {
                            soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                        }




                        int trafficSizebyte = 0;
                        int traffic_counter = 0;
                        for (int i = 1; i <= 1; i++)
                        {

                            if (worker.CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {


                                
                                foreach (string admin_address_pages in AdminPageAddress.admin_address)
                                {

                                    // Start Agent Managment
                                    if (agent_ToggleSwitch.Value)
                                    {
                                        if (rand_agent_ToggleSwitch.Value)
                                        {
                                            Random ranNum = new Random();
                                            int index_rand_agent = ranNum.Next(0, AgentList.agent_list.Length);
                                            http.UserAgent = AgentList.agent_list[index_rand_agent];
                                        }
                                        else
                                        {
                                            string[] rand_agent_List = rand_agent_CheckedDropDownList.Text.Split(';');
                                            Random ranNum = new Random();
                                            int index_rand_agent = ranNum.Next(0, rand_agent_List.Length);
                                            if (rand_agent_List[index_rand_agent] == "BlackWin 4.0")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (BlackWin 4.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246";
                                            }else if (rand_agent_List[index_rand_agent] == "curl")
                                            {
                                                http.UserAgent = "curl/7.37.0" ;
                                            }else if (rand_agent_List[index_rand_agent] == "Samsung Galaxy S9")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; SM-G960F Build/R16NW) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.84 Mobile Safari/537.36";
                                            }else if (rand_agent_List[index_rand_agent] == "Nexus 6P")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Linux; Android 6.0.1; Nexus 6P Build/MMB29P) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.83 Mobile Safari/537.36";
                                            }else if (rand_agent_List[index_rand_agent] == "Sony Xperia XZ")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Linux; Android 7.1.1; G8231 Build/41.2.A.0.219; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/59.0.3071.125 Mobile Safari/537.36";
                                            }else if (rand_agent_List[index_rand_agent] == "HTC One X10")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; HTC One X10 Build/MRA58K; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/61.0.3163.98 Mobile Safari/537.36";
                                            }else if (rand_agent_List[index_rand_agent] == "Apple iPhone XR (Safari)")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 12_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.0 Mobile/15E148 Safari/604.1";
                                            }else if (rand_agent_List[index_rand_agent] == "Apple iPhone XS (Chrome)")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 12_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/69.0.3497.105 Mobile/15E148 Safari/605.1";
                                            }else if (rand_agent_List[index_rand_agent] == "Apple iPhone XS Max (Firefox)")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 12_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) FxiOS/13.2b11866 Mobile/16A366 Safari/605.1.15";
                                            }else if (rand_agent_List[index_rand_agent] == "Apple iPhone 8")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.34 (KHTML, like Gecko) Version/11.0 Mobile/15A5341f Safari/604.1";
                                            }else if (rand_agent_List[index_rand_agent] == "Microsoft Lumia 650")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Windows Phone 10.0; Android 6.0.1; Microsoft; RM-1152) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Mobile Safari/537.36 Edge/15.15254";
                                            }else if (rand_agent_List[index_rand_agent] == "Google Pixel C")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Linux; Android 7.0; Pixel C Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/52.0.2743.98 Safari/537.36";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Amazon Kindle Fire HDX 7")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Linux; Android 4.4.3; KFTHWI Build/KTU84M) AppleWebKit/537.36 (KHTML, like Gecko) Silk/47.1.79 like Chrome/47.0.2526.80 Safari/537.36";
                                            }else if (rand_agent_List[index_rand_agent] == "Windows 10-based PC using Edge browser")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246";
                                            }else if (rand_agent_List[index_rand_agent] == "Chrome OS-based laptop using Chrome browser (Chromebook)")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (X11; CrOS x86_64 8172.45.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.64 Safari/537.36" ;
                                            }else if (rand_agent_List[index_rand_agent] == "Mac OS X-based computer using a Safari browser")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/601.3.9 (KHTML, like Gecko) Version/9.0.2 Safari/601.3.9";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Windows 7-based PC using a Chrome browser")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Linux-based PC using a Firefox browser")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:15.0) Gecko/20100101 Firefox/15.0.1";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Amazon 4K Fire TV")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Linux; Android 5.1; AFTS Build/LMY47O) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/41.99900.2250.0242 Safari/537.36";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Nintendo Wii U")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Nintendo WiiU) AppleWebKit/536.30 (KHTML, like Gecko) NX/3.0.4.2.12 NintendoBrowser/4.3.1.11264.US";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Xbox One S")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; XBOX_ONE_ED) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Xbox One")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (Windows Phone 10.0; Android 4.2.1; Xbox; Xbox One) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Mobile Safari/537.36 Edge/13.10586";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Playstation 4")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (PlayStation 4 3.11) AppleWebKit/537.73 (KHTML, like Gecko)";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Google bot")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
                                            }else if (rand_agent_List[index_rand_agent] == "Bing bot")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)";
                                            }
                                            else if (rand_agent_List[index_rand_agent] == "Yahoo! bot")
                                            {
                                                http.UserAgent = "Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)";
                                            }
                                            else
                                            {
                                               
                                                http.UserAgent = "Mozilla/5.0 (BlackWin 4.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246";
                                            }

                                        }
                                    }
                                    else
                                    {
                                        http.UserAgent = "Abdal Admin Finder By Ebrahim Shafiei";
                                    }
                                    // End Agent Managment


                                    radProgressBar1_counter++;
                                    radProgressBar1.Value2 = radProgressBar1_counter;
                                    if (AbdalControler.stop_force_process)
                                    {
                                        break;
                                    }
                                    if (admin_address_pages != "")
                                    {

                                        traffic_counter++;
                                       
                                        radProgressBar1.Value2 = traffic_counter;

                                        //randString = RandomString(rnd.Next(15, 30));
                                        // Send the HTTP GET and return the content in a string.
                                        http.ReadTimeout = 20;
                                        http.ConnectTimeout = 20;
                                        http.FollowRedirects = true;

                                        ////////////// https://www.example-code.com/csharp/async_http_response.asp
                                        //string reponse_http = http.QuickGetStr(targetUrlTextBox.Text+"/"+admin_address_pages);
                                        // http.QuickGetObj(targetUrlTextBox.Text + "/" + admin_address_pages);


                                       // string reponse_http = http.QuickGetStr(targetUrlTextBox.Text + "/" + admin_address_pages);

                                        Chilkat.Task task = http.QuickGetObjAsync(targetUrl + "/" + admin_address_pages);

                                        if (http.LastMethodSuccess == true)
                                        {
                                            ResultTextEditor.AppendText(targetUrl + "/" + admin_address_pages + " Success" + Environment.NewLine);
                                            ResultTextEditor.SelectionStart = ResultTextEditor.Text.Length;
                                            ResultTextEditor.ScrollToCaret();

                                        }


                                        //Calculate Traffic Size
                                        //trafficSizebyte += System.Text.ASCIIEncoding.Unicode.GetByteCount(reponse_http);
                                        //trafficSizebyte_Label.Text = (trafficSizebyte / 1024).ToString() + " KB";


                                        if (http.LastMethodSuccess != true)
                                        {
                                            ResultTextEditor.AppendText(http.LastErrorText + Environment.NewLine);
                                            ResultTextEditor.SelectionStart = ResultTextEditor.Text.Length;
                                            ResultTextEditor.ScrollToCaret();
                                             
                                        }

                                        //  Schedule the task for running on the thread pool.  This changes the task's state
                                        //  from Inert to Live.
                                        bool success = task.Run();
                                        if (success != true)
                                        {
                                            ResultTextEditor.AppendText(task.LastErrorText + Environment.NewLine);
                                            ResultTextEditor.SelectionStart = ResultTextEditor.Text.Length;
                                            ResultTextEditor.ScrollToCaret();
                                        }

                                        //  The application is now free to do anything else
                                        //  while the HTML page is being downloaded.

                                        //  For this example, we'll simply sleep and periodically
                                        //  check to see if the QuickGetObjAsync if finished.
                                        while (task.Finished != true)
                                        {

                                            //  Sleep 100 ms.
                                            task.SleepMs(100);

                                        }

                                        //  A finished task could be one that was canceled, aborted, or truly finished.

                                        //  If the task was "canceled", it was canceled prior to actually starting.  This could
                                        //  happen if the task was canceled while waiting in a thread pool queue to be scheduled by Chilkat's
                                        //  background thread pool scheduler.

                                        //  If the task was "aborted", it indicates that it was canceled while running in a background thread.
                                        //  The ResultErrorText will likely indicate that the task was aborted.

                                        //  If the task "completed", then it ran to completion, but the actual success/failure of the method
                                        //  is determined by the result obtained via a GetResult* method.  (A "completed" task will
                                        //  have a StatusInt equal to 7.   If the task finished, but was not completed, then it must've
                                        //  been aborted or canceled:
                                        if (task.StatusInt != 7)
                                        {
                                            ResultTextEditor.AppendText("Task did not complete." + Environment.NewLine);
                                            ResultTextEditor.AppendText("task status: " + task.Status + Environment.NewLine);
                                            ResultTextEditor.SelectionStart = ResultTextEditor.Text.Length;
                                            ResultTextEditor.ScrollToCaret();
                                        }

                                        //  The synchronous call to QuickGetObj would return an HTTP response object.  To get this
                                        //  response object for the async call, we instantiate a new/empty HTTP response object,
                                        //  and then load it from the completed task.
                                        Chilkat.HttpResponse resp = new Chilkat.HttpResponse();

                                        success = resp.LoadTaskResult(task);
                                        if (success != true)
                                        {
                                            ResultTextEditor.AppendText(resp.LastErrorText + Environment.NewLine);
                                            ResultTextEditor.SelectionStart = ResultTextEditor.Text.Length;
                                            ResultTextEditor.ScrollToCaret();

                                        }

                                        //  Now that we have the response, we can get all of the information:
                                        

                                        if (resp.StatusCode.ToString() == "200")
                                        {


                                            //String admin_page_addr = String.Empty;
                                            //for (int admin_page_counter = 0; i < ExtractRichTextBox.Lines.Length; i++)
                                            //{
                                            //    admin_page_addr = ExtractRichTextBox.Lines[admin_page_counter];
                                            //    if (admin_page_addr !==)
                                            //    {

                                            //    }
                                            //}



                                            //Add AttackLog in Result Box

                                            if (ExtractRichTextBox.Lines.Count() == 0)
                                            {
                                                crawled_links_text.Text = "1";
                                            }
                                            else
                                            {
                                                crawled_links_text.Text = ExtractRichTextBox.Lines.Count().ToString();
                                            }
                                            ExtractRichTextBox.AppendText("[+] " + targetUrl + "/" + admin_address_pages +  Environment.NewLine);
                                            ExtractRichTextBox.SelectionStart = ExtractRichTextBox.Text.Length;
                                            ExtractRichTextBox.ScrollToCaret();
                                            dangerLinearGauge.Value = ExtractRichTextBox.Lines.Count();
                                            if(ExtractRichTextBox.Lines.Count() == 15)
                                            {
                                                MessageBox.Show("Please Use The Abdal Google Dork, Because This Site Gives The Wrong Answer");
                                                break;
                                            }
                                            
                                           

                                            

                                        }
                                        /////////////////////////////////////////////////////////////////////////////////////
                                        ///

                                        

                                        

                                        // Perform a time consuming operation and report progress.
                                        if (FastTrafficGenToggleSwitch.Value == false)
                                        {
                                            System.Threading.Thread.Sleep(500);
                                        }

                                        worker.ReportProgress(i);


                                    }

                                }




                            }
                        }

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            } // End else


           

           
           




        }

        private void bgWorker_req_maker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //radProgressBar1.Value2 = e.ProgressPercentage;
             
//            radRadialGauge1.Value = e.ProgressPercentage;
        }

        private void bgWorker_req_maker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            


            if (e.Cancelled == true)
                    {
                this.radDesktopAlert1.CaptionText = "Abdal Admin Finder";
                this.radDesktopAlert1.ContentText = "Canceled Process By User!";
                this.radDesktopAlert1.Show();
                using (var soundPlayer = new SoundPlayer(@"cancel.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }
            }
            else if (e.Error != null)
                    {
                this.radDesktopAlert1.CaptionText = "Abdal Admin Finder";
                this.radDesktopAlert1.ContentText = e.Error.Message;
                this.radDesktopAlert1.Show();

                using (var soundPlayer = new SoundPlayer(@"error.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }


            }
            else
                    {
                this.radDesktopAlert1.CaptionText = "Abdal Admin Finder";
                this.radDesktopAlert1.ContentText = "Done!";
                this.radDesktopAlert1.Show();
                using (var soundPlayer = new SoundPlayer(@"done.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }

            }

        }

        private void radRadialGauge1_Click(object sender, EventArgs e)
        {

        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void radButton1_Click_2(object sender, EventArgs eSpider)
        {
            
 
            
        }

        private void radButton2_Click(object sender, EventArgs eSpider)
        {
        }

        private void bgWorker_spider_DoWork(object sender, DoWorkEventArgs eSpidere)
        {

            
           

        }

        private void bgWorker_spider_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs eSpider)
        {



        }

        private void bgWorker_spider_ProgressChanged(object sender, ProgressChangedEventArgs eSpider)
        {
           
        }

        private void radLinearGauge1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://donate.abdalagency.ir/");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/abdal-security-group/Abdal-2-Key-Triple-DES-Builder");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://gitlab.com/abdal-security-group/abdal-2-key-triple-des-builder");
        }
    }
}

using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Text;
using Microsoft.Win32;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Script.Serialization;
using System.IO;

namespace game_tracking
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void game_tracking_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                Random client_seed_id = new Random(unchecked((int)DateTime.Now.Ticks));
                String Client_ID = client_seed_id.Next(100).ToString();
                string ClientCode = "EscA";
                byte[] ClientCodeUnicode = Encoding.Unicode.GetBytes(ClientCode);
                ClientCode = Convert.ToBase64String(ClientCodeUnicode);

                string HostName = Dns.GetHostName();
                IPHostEntry IPHost = Dns.GetHostByName(HostName);
                string IPaddress = IPHost.AddressList[0].ToString();
                string MACaddress = NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();

                RegistryKey RegKey = Registry.CurrentUser.CreateSubKey("IKnowEscAsMind").CreateSubKey("BecauseItsFailed");

                RegKey.SetValue("information", IPaddress + MACaddress);
                RegKey.SetValue("ClientID", Client_ID + ClientCode);
            }

            catch (CannotUnloadAppDomainException a)
            {
                Random client_seed_id = new Random(unchecked((int)DateTime.Now.Ticks));
                String Client_ID = client_seed_id.Next(100).ToString();
                string ClientCode = "EscA";
                byte[] ClientCodeUnicode = Encoding.Unicode.GetBytes(ClientCode);
                ClientCode = Convert.ToBase64String(ClientCodeUnicode);

                string FirstKey = "SomeThingisWrongbutIdontknow";
                string SecondKey = "SoIamWrittingTrashCode";

                RegistryKey RegKey = Registry.CurrentUser.CreateSubKey("IKnowEscAsMind").CreateSubKey("BecauseItsFailed");

                RegKey.SetValue("information", FirstKey + SecondKey);
                RegKey.SetValue("ClientID", Client_ID + ClientCode);

                bool connected = NetworkInterface.GetIsNetworkAvailable();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://dkanrjsk.paas-ta.org/debug"); // paas-ta 공모전 작품
                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST"; // sending type = POST

                try
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            clientId = Client_ID,
                            Status = ClientCode + FirstKey + SecondKey,
                            Reason = a
                        });

                        httpWebRequest.ContentLength = json.Length;
                        streamWriter.Write(json);
                    }
                }

                catch (HttpListenerException http_failed)
                {
                    string savePath = @"\FailedsoIwanttoWriteit";
                    string textValue = http_failed.ToString();
                    File.WriteAllText(savePath, textValue, Encoding.Default);
                }
            }

            catch (InsufficientMemoryException MemoryFailed)
            {
                string savePath = @"\FailedBecauseofYourMemory";
                string textValue = MemoryFailed.ToString();
                File.WriteAllText(savePath, textValue, Encoding.Default);
            }

            catch (NetworkInformationException NetworkFailed)
            {
                string savePath = @"\FailedBecauseYourNetworkLOL";
                string textValue = NetworkFailed.ToString();
                File.WriteAllText(savePath, textValue, Encoding.Default);
            }

            catch (WebException WebFailed)
            {
                string savePath = @"\FailedWhatisYourWeb";
                string textValue = WebFailed.ToString();
                File.WriteAllText(savePath, textValue, Encoding.Default);
            }

        }

        private void game_tracking_installer_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void game_tracking_AfterUninstall(object sender, InstallEventArgs e)
        {
            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey("IKnowEscAsMind").OpenSubKey("BecauseItsFailed");
            Registry.CurrentUser.DeleteSubKeyTree("IKnowEscAsMind");
            RegKey.Close();
        }
    }
}

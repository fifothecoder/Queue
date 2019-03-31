using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Queue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginDoctorView : Page
    {
        public LoginDoctorView()
        {
            this.InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string docID = DocIDBox.Text.Trim();
            string encryptedPass = GetEncryptedPassword();

            if (!StaticExtensions.ValidateDoctorID(docID)) StaticExtensions.ShowMessageBox("Invalid Doctor ID! (Usage is 'NameSurnameID')", "Invalid credentials");
            else if (DocPasswordBox.Password.Trim().Length < 5) StaticExtensions.ShowMessageBox("Invalid credentials!", "Invalid credentials");
            else
            {
                string response = ValidateCredentials(docID, encryptedPass);                                                 //Get credentials
                if (response == "WRONG_ID" || response == "WRONG_PASSWORD") StaticExtensions.ShowMessageBox("Wrong username or password!", "Invalid credentials");    //Bad credentials
                else
                {
                    Dictionary<string, string> pat = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

                    List<Appointment> appos = new List<Appointment>();
                    StaticExtensions.GetDocAppos(docID, ref appos);

                    DoctorData docData = new DoctorData(pat["name"], pat["surname"], docID, ref appos);

                    this.Frame.Navigate(typeof(DoctorView), docData); //Good credentials
                }
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private string GetEncryptedPassword() 
        {
            //TODO:CREATE SOME ENCRYPTION
            return DocPasswordBox.Password.Trim();
        }

        private string ValidateCredentials(string docID, string pass)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.7.255.210/DoctorQueue/doctorapp/public/doctor");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = JsonConvert.SerializeObject(new { DoctorID = docID, Password = pass });

                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }


    }
}

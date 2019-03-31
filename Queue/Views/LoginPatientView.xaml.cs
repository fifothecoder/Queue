using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Http;
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
    /// 

    public sealed partial class LoginPatientView : Page
    {
        public LoginPatientView()
        {
            this.InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string birthNum = PatBirthNoBox.Text;
            string encryptedPass = GetEncryptedPassword();

            if (!StaticExtensions.ValidateBN(birthNum)) StaticExtensions.ShowMessageBox("Invalid Birth Date! (Usage is 'XXXXXX/XXXX')", "Invalid credentials");
            else
            {
                string response = ValidateCredentials(birthNum, encryptedPass);                                                 //Get credentials
                if (response == "WRONG_ID" || response == "WRONG_PASSWORD") StaticExtensions.ShowMessageBox("Wrong username or password!", "Invalid credentials");    //Bad credentials
                else
                {
                    Dictionary<string, string> pat = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                    PatientData patData = new PatientData(pat["name"], pat["surname"], pat["id_number"], pat["insurance_com"].ToInsuranceComp());

                    this.Frame.Navigate(typeof(PatientView), patData);
            }        //Good credentials
        }
    }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private string GetEncryptedPassword()
        {
            //TODO:CREATE SOME ENCRYPTION
            return PatPassword.Password;
        }

        private string ValidateCredentials(string birthNum, string pass)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.7.255.210/DoctorQueue/doctorapp/public/patient");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = JsonConvert.SerializeObject(new { BirthNum = birthNum, Password = pass });

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

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
            string id = DocIDBox.Text;
            string encryptedPass = GetEncryptedPassword();

            if (!StaticExtensions.ValidateDoctorID(id)) StaticExtensions.ShowMessageBox("Invalid Doctor ID! (Usage is 'NameSurnameID')", "Invalid credentials");
            else
            {
                string response = ValidateCredentials(id, encryptedPass);                                                       //Get credentials
                if (response == "[]") StaticExtensions.ShowMessageBox("Wrong username or password!", "Invalid credentials");    //Bad credentials
                else this.Frame.Navigate(typeof(DoctorView), StaticExtensions.LoadDoctorFromJSON(response));                    //Good credentials
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private string GetEncryptedPassword() 
        {
            //TODO:CREATE SOME ENCRYPTION
            return DocPasswordBox.Password;
        }

        private string ValidateCredentials(string docID, string pass)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://10.7.255.210/DoctorQueue/doctorapp/public/doctor/");
            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new DoctorLoginData { DoctorID = docID, Password = pass }));

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
            var response = (HttpWebResponse)request.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

    }
}

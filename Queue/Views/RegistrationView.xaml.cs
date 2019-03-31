using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
{ // fuck
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationView : Page
    {
        public RegistrationView()
        {
            this.InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            //Validate info
            
            if (!StaticExtensions.ValidateName(NameBox.Text.Trim()))
            {
                StaticExtensions.ShowMessageBox("Invalid name! (Length must be above 2 characters)", "Invalid Name");
            } else if (!StaticExtensions.ValidateSurname(SurnameBox.Text.Trim()))
            {
                StaticExtensions.ShowMessageBox("Invalid surname! (Length must be above 2 characters)", "Invalid Surname");
            } else if (!StaticExtensions.ValidateBN(BirthBox.Text.Trim()))
            {
                StaticExtensions.ShowMessageBox("Invalid birth date! (Must have format 'xxxxxx/xxxx')", "Invalid Birth Date");
            } else if (InsuranceComboBox.SelectedItem == null) {
                StaticExtensions.ShowMessageBox("You need to choose the insurance company! (Choose none if you are not insured)", "Invalid Insurance Company");
            } else if(Pass.Password.Trim().Length < 5) {
                StaticExtensions.ShowMessageBox("Too short password!", "Short password");
            } else if (TermsCheck.IsChecked != true) {
                StaticExtensions.ShowMessageBox("You need to agree with the Terms of Service!", "Invalid Terms of Service");
            } else
            {
                //Connect to database

                var result = RegisterToDatabase();

                if(result == "SAVE_OK")
                {
                    PatientData data = new PatientData(NameBox.Text.Trim(), SurnameBox.Text.Trim(), BirthBox.Text.Trim(), InsuranceComboBox.SelectedItem.ToString().ToInsuranceComp());
                    this.Frame.Navigate(typeof(PatientViewTest), data);
                } else if(result == "SAVE_DUPL")
                {
                    StaticExtensions.ShowMessageBox("This user is already in the database!", "Duplicate user");
                } else if(result == "SAVE_ERROR")
                {
                    StaticExtensions.ShowMessageBox("Error happened while registring user!", "Registration failed");
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private string RegisterToDatabase()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.7.255.210/DoctorQueue/doctorapp/public/patient/register");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var ss = InsuranceComboBox.SelectedItem;

                var data = JsonConvert.SerializeObject(new
                {
                    name = NameBox.Text.Trim(),
                    surname = SurnameBox.Text.Trim(),
                    id = BirthBox.Text.Trim(),
                    insurance = InsuranceComboBox.SelectedItem,
                    password = Pass.Password
                });

                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) return streamReader.ReadToEnd();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Queue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
            else if (!ValidateCredentials(birthNum, encryptedPass)) StaticExtensions.ShowMessageBox("Wrong username or password!", "Invalid credentials");
            else
            {
                //If login is successful
                this.Frame.Navigate(typeof(PatientViewTest), birthNum);
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

        private bool ValidateCredentials(string id, string pass)
        {
            return true;
        }

    }
}

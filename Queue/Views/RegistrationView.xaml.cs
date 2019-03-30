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
            }
            else if (TermsCheck.IsChecked != true)
            {
                StaticExtensions.ShowMessageBox("You need to agree with the Terms of Service!", "Invalid Terms of Service");
            } else
            {
                //Register to database
                StaticExtensions.ShowMessageBox("Registering to database..", "Success!");
            }



        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}

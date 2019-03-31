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
    public sealed partial class DoctorView : Page
    {

        private DoctorData docData;

        public DoctorView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            docData = (DoctorData) e.Parameter;
            UpdateData();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (docData._appointments.Count != 0 && docData._appointments[0].name != "URGENT") StaticExtensions.DeleteAppointmentFromServer(docData._appointments[0]);
            else if (docData._appointments[0].name == "URGENT")
            {
                docData._appointments.RemoveAt(0);
                UrgentButton.IsEnabled = true;
            }
            StaticExtensions.GetDocAppos(docData._docID, ref docData._appointments);
            ClearData();
            UpdateData();
        }

        private void UrgentButton_Click(object sender, RoutedEventArgs e)
        {
            docData._appointments.Insert(0, new Appointment("URGENT", "CASE", "Unknown", docData._docID, InsuranceComp.None, DateTime.Now));
            UrgentButton.IsEnabled = false;
            ClearData();
            UpdateData();
        }

        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginDoctorView));
        }

        private void UpdateData()
        {
            if (docData._appointments == null) return;
            if (docData._appointments.Count > 0)
            {
                TB00.Text = docData._appointments[0].name;
                TB10.Text = docData._appointments[0].surname;
                TB20.Text = docData._appointments[0].patient_id;
                TB30.Text = docData._appointments[0].InsuranceComp.ToString();
                TB40.Text = docData._appointments[0].date_of_appo.ToShortTimeString();
            }

            if (docData._appointments.Count > 1) {
                TB01.Text = docData._appointments[1].name;
                TB11.Text = docData._appointments[1].surname;
                TB21.Text = docData._appointments[1].patient_id;
                TB31.Text = docData._appointments[1].InsuranceComp.ToString();
                TB41.Text = docData._appointments[1].date_of_appo.ToShortTimeString();
            }

            if (docData._appointments.Count > 2) {
                TB02.Text = docData._appointments[2].name;
                TB12.Text = docData._appointments[2].surname;
                TB22.Text = docData._appointments[2].patient_id;
                TB32.Text = docData._appointments[2].InsuranceComp.ToString();
                TB42.Text = docData._appointments[2].date_of_appo.ToShortTimeString();
            }

            if (docData._appointments.Count > 3) {

                TB03.Text = docData._appointments[3].name;
                TB13.Text = docData._appointments[3].surname;
                TB23.Text = docData._appointments[3].patient_id;
                TB33.Text = docData._appointments[3].InsuranceComp.ToString();
                TB43.Text = docData._appointments[3].date_of_appo.ToShortTimeString();
            }

            if (docData._appointments.Count > 4) {
                TB04.Text = docData._appointments[4].name;
                TB14.Text = docData._appointments[4].surname;
                TB24.Text = docData._appointments[4].patient_id;
                TB34.Text = docData._appointments[4].InsuranceComp.ToString();
                TB44.Text = docData._appointments[4].date_of_appo.ToShortTimeString();
            }
        }

        private void ClearData()
        {
            if (docData._appointments == null) return;
            if (docData._appointments.Count < 1)
            {
                TB00.Text = string.Empty;
                TB10.Text = string.Empty;
                TB20.Text = string.Empty;
                TB30.Text = string.Empty;
                TB40.Text = string.Empty;
            }

            if (docData._appointments.Count < 2)
            {
                TB01.Text = string.Empty;
                TB11.Text = string.Empty;
                TB21.Text = string.Empty;
                TB31.Text = string.Empty;
                TB41.Text = string.Empty;
            }

            if (docData._appointments.Count < 3)
            {
                TB02.Text = string.Empty;
                TB12.Text = string.Empty;
                TB22.Text = string.Empty;
                TB32.Text = string.Empty;
                TB42.Text = string.Empty;
            }

            if (docData._appointments.Count < 4)
            {

                TB03.Text = string.Empty;
                TB13.Text = string.Empty;
                TB23.Text = string.Empty;
                TB33.Text = string.Empty;
                TB43.Text = string.Empty;
            }

            if (docData._appointments.Count < 5)
            {
                TB04.Text = string.Empty;
                TB14.Text = string.Empty;
                TB24.Text = string.Empty;
                TB34.Text = string.Empty;
                TB44.Text = string.Empty;
            }
        }
    }
}

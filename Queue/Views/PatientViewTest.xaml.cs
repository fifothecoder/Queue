using System;
using System.Collections.Generic;
using System.Collections;
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
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Queue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PatientViewTest : Page
    {
        List<Appointment> currentAppointments;
        PatientData patData;

        public PatientViewTest()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            patData = (PatientData) e.Parameter;
            currentAppointments = new List<Appointment>();
            StaticExtensions.GetPacAppos(patData.id_number, ref currentAppointments);       //Get Appos


            DoctorCombo.Items.Clear();

            foreach (var appo in currentAppointments)
            {
                if (!TBDoctor.Items.Contains(appo.doctor_id)) TBDoctor.Items.Add(appo.doctor_id);
                //if (!DoctorCombo.Items.Contains(appo.doctor_id)) DoctorCombo.Items.Add(appo.doctor_id);        
            }
            foreach (var item in StaticExtensions.GetDoctors())
            {
                DoctorCombo.Items.Add(item);
            }
            DoctorCombo.IsEnabled = false;
            TimeCombo.IsEnabled = false;

        }

        private void DoctorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime time = Convert.ToDateTime(TimeCombo.SelectedItem);
            DateTime date = DatePicker.Date.Value.DateTime;
            date = date.Date.Add(time.TimeOfDay);
            string doctor = Convert.ToString(DoctorCombo.SelectedItem);

            List<Appointment> docAppos = new List<Appointment>();
            StaticExtensions.GetDocAppos(doctor, ref docAppos);

            List<DateTime> free = StaticExtensions.GetValidTimes(date);
            foreach (var appo in docAppos) if (free.Contains(appo.date_of_appo)) free.Remove(appo.date_of_appo);
            foreach (var freeAppo in free) TimeCombo.Items.Add(freeAppo);
            TimeCombo.IsEnabled = true;
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPatientView));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorCombo.SelectedItem == null || TimeCombo.SelectedItem == null || DatePicker.Date == null) return;

            Appointment newAppo = new Appointment(patData.name, patData.surname, patData.id_number, DoctorCombo.SelectedItem.ToString(), 
                patData.insurance_com, Convert.ToDateTime(TimeCombo.SelectedItem));
            StaticExtensions.AddAppointmentToServer(newAppo);
            AssignButton.IsEnabled = false;
            StaticExtensions.GetPacAppos(patData.id_number, ref currentAppointments);
        }

        private void TBDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Appointment currentAppo = null;

            foreach (var appo in currentAppointments)
                if (appo.doctor_id == TBDoctor.SelectedItem.ToString() && (currentAppo == null || DateTime.Compare(currentAppo.date_of_appo, appo.date_of_appo) > 0)) currentAppo = appo;

            if (currentAppo == null) return;
            DateSet.Text = currentAppo.date_of_appo.Date.ToShortDateString();
            TimeSet.Text = currentAppo.date_of_appo.TimeOfDay.ToString();
            AssignButton.IsEnabled = true;
        }

        private void DatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            DoctorCombo.IsEnabled = true;
            AssignButton.IsEnabled = true;
        }

        private void TimeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AssignButton.IsEnabled = true;
        }
    }
}

﻿using System;
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
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Queue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PatientViewTest : Page
    {
        private string _birthNum;
        Appointment appointment;
        PatientData patdata;

        public PatientViewTest()
        {
            this.InitializeComponent();
            //listView.Items.Add("XXX");
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //_birthNum = e.Parameter.ToString();
            patdata = (PatientData) e.Parameter;
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
            DateTime time = Convert.ToDateTime(TimeCombo.SelectedItem);
            DateTime date = Convert.ToDateTime(DatePicker.Date);
            date = date.Date.Add(time.TimeOfDay);
            string doctor = Convert.ToString(DoctorCombo.SelectedItem);
            string name = patdata.GetPatientName();
            string insurancecomp = patdata.GetInsuranceCompany();

            
        }
    }
}

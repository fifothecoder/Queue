﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using Windows.UI.Popups;
using Newtonsoft.Json;


namespace Queue
{

    public static class StaticExtensions
    {
        public static async void ShowMessageBox(string content, string header)
        {
            var dialog = new MessageDialog(content, header);
            await dialog.ShowAsync();
        }

        public static bool ValidateDoctorID(string name)
        {
            if (name == "root") return true;
            if (name.Length < 8) return false;

            string namePart = name.Substring(0, name.Length - 4);
            string IDPart = name.Substring(name.Length - 4);



            foreach (char c in namePart) if (!char.IsLetter(c)) return false;
            foreach (char c in IDPart) if (!char.IsNumber(c)) return false;

            return true;
        }

        public static bool ValidateName(string name)
        {
            if (name.Length < 2) return false;
            foreach (char c in name) if (!char.IsLetter(c)) return false;
            return true;
        }

        public static bool ValidateSurname(string surname)
        {
            if (surname.Length < 2) return false;
            foreach (char c in surname) if (!char.IsLetter(c)) return false;
            return true;
        }

        public static bool ValidateBN(string birthNumber)
        {
            if (birthNumber.Length != 11) return false;

            for (int i = 0; i < birthNumber.Length; i++) if (i != 6 && !char.IsNumber(birthNumber[i])) return false;

            return true;
        } 

        public static InsuranceComp ToInsuranceComp(this string s)
        {
            switch (s.ToUpper())
            {
                case "DOVERA": return InsuranceComp.Dovera;
                case "VSZP": return InsuranceComp.VSZP;
                case "UNION": return InsuranceComp.Union;
                default: return InsuranceComp.None;
            }
        }

        public static void GetDocAppos(string docID, ref List<Appointment> appointments)
        {
            appointments.Clear();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.7.255.210/DoctorQueue/doctorapp/public/doctor/appos");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = JsonConvert.SerializeObject(new { DoctorID = docID });

                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string json = streamReader.ReadToEnd();
                var appos = JsonConvert.DeserializeObject<List<Appointment>>(json);
                appos = appos.OrderBy(x => x.date_of_appo).ToList();
                appointments = appos;
            }
        }

        public static void GetPacAppos(string birthNum, ref List<Appointment> appointments)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.7.255.210/DoctorQueue/doctorapp/public/patient/appos");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = JsonConvert.SerializeObject(new { BirthNum = birthNum });

                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string json = streamReader.ReadToEnd();
                var appos = JsonConvert.DeserializeObject<List<Appointment>>(json);
                appos = appos.OrderBy(x => x.date_of_appo).ToList();
                appointments = appos;
            }

        }

        public static List<string> GetDoctors()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.7.255.210/DoctorQueue/doctorapp/public/doctors");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = JsonConvert.SerializeObject(new { });

                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string json = streamReader.ReadToEnd();
                var appos = JsonConvert.DeserializeObject<List<string>>(json);
                return appos;
            }
        }

        public static void AddAppointmentToServer(Appointment appo)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.7.255.210/DoctorQueue/doctorapp/public/patient/appos/add");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = JsonConvert.SerializeObject(new { appo.name, appo.surname, appo.patient_id, appo.doctor_id, appo.date_of_appo, appo.InsuranceComp});

                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                string message = streamReader.ReadToEnd();
                if (message == "ERROR") throw new InvalidProgramException();
                else if (message == "SUCC")
                {
                    ShowMessageBox("Appointment Successfully Added!", "Success");
                }
            }
        }

        public static void DeleteAppointmentFromServer(Appointment appo)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.7.255.210/DoctorQueue/doctorapp/public/doctor/appos/remove");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = JsonConvert.SerializeObject(new {appo.date_of_appo});

                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                string message = streamReader.ReadToEnd();
                if (message == "ERROR") throw new InvalidProgramException();
                else if(message == "SUCC")
                {
                }
            }
        }

        public static List<DateTime> GetValidTimes(DateTime startDay)
        {
            return new DateTime[]
            {
                startDay + new TimeSpan(7, 30, 0),
                startDay + new TimeSpan(8, 0, 0),
                startDay + new TimeSpan(8, 30, 0),
                startDay + new TimeSpan(9, 0, 0),
                startDay + new TimeSpan(9, 30, 0),
                startDay + new TimeSpan(10, 0, 0),
                startDay + new TimeSpan(10, 30, 0),
                startDay + new TimeSpan(11, 0, 0),
                startDay + new TimeSpan(11, 30, 0),
                startDay + new TimeSpan(12, 0, 0),
                startDay + new TimeSpan(12, 30, 0),
                startDay + new TimeSpan(13, 0, 0)
            }.ToList();
        }
    }
}

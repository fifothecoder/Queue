using System;
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

        public static void GetAppointments(string docID, ref List<Appointment> appointments)
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
                foreach (var item in appos)
                {
                    if (!appointments.Contains(item)) appointments.Add(item);
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
                var data = JsonConvert.SerializeObject(new { appo.patient_id, appo.date_of_appo});

                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                if (streamReader.ReadToEnd() == "ERROR") throw new InvalidProgramException();
            }

        }
    }
}

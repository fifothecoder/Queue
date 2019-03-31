using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static DoctorData LoadDoctorFromJSON(string json)
        {
            string docName = "Sona1234";
            string docSurname = "aaaaaaaaa";

            return new DoctorData(docName, docSurname);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Classes
{
    public static class StaticExtensions
    {
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
            int i = 0;
            for (; i < 6; i++) if (char.IsDigit(birthNumber[i])) return false;
            i++;
            for (; i < birthNumber.Length; i++) if (char.IsDigit(birthNumber[i])) return false;
            return true;
        }

        


    }
}

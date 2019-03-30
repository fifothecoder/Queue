using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Classes
{

    public enum InsuranceComp
    {
        None = 0,
        Dovera = 1,
        VSZP = 2,
        Union = 3
    }

    public class Patient
    {

        private readonly string _name;
        private readonly string _sur;
        private readonly InsuranceComp _ins;

        public Patient(string nam, string sur, InsuranceComp ins)
        {
            _name = nam;
            _sur = sur;
            _ins = ins;

            if (!ValidateName() || !ValidateSurname()) throw new ArgumentException();
        }

        public string GetPatientName()
        {
            return $"{_name} {_sur}";
        }

        public string GetInsuranceCompany()
        {
            return _ins.ToString();
        }

        private bool ValidateName()
        {
            if (_name.Length < 2) return false;
            foreach (char c in _name)if (!char.IsLetter(c)) return false;
            return true;
        }

        private bool ValidateSurname()
        {
            if (_sur.Length < 2) return false;
            foreach (char c in _sur) if (!char.IsLetter(c)) return false;
            return true;
        }

    }
}

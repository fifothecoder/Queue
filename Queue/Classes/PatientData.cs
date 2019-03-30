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

    public class PatientData
    {

        private readonly string _name;
        private readonly string _sur;
        private readonly string _birthNum;      // Format : AAAAAA/BBBB
        private readonly InsuranceComp _ins;

        public PatientData(string nam, string sur, string birthNum, InsuranceComp ins)
        {
            _name = nam;
            _sur = sur;
            _birthNum = birthNum;
            _ins = ins;

            if (!ValidateName()) throw new ArgumentException("Invalid name!");
            if (!ValidateSurname()) throw new ArgumentException("Invalid surname!");
            if (!ValidateBN()) throw new ArgumentException("Invalid birth number!");
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

        private bool ValidateBN()
        {
            if (_birthNum.Length != 11) return false;
            int i = 0;
            for (; i < 6; i++) if (char.IsDigit(_birthNum[i])) return false;
            i++;
            for (; i < _birthNum.Length; i++) if (char.IsDigit(_birthNum[i])) return false;
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

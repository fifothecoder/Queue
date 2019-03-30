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

            if (!StaticExtensions.ValidateName(_name)) throw new ArgumentException("Invalid name!");
            if (!StaticExtensions.ValidateSurname(_sur)) throw new ArgumentException("Invalid surname!");
            if (!StaticExtensions.ValidateBN(_birthNum)) throw new ArgumentException("Invalid birth number!");
        }

        public string GetPatientName()
        {
            return $"{_name} {_sur}";
        }

        public string GetInsuranceCompany()
        {
            return _ins.ToString();
        }

       

    }
}

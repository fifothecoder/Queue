using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
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

        private readonly string name;
        private readonly string surname;
        private readonly string id_number;      // Format : AAAAAA/BBBB
        private readonly InsuranceComp insurance_com;

        public PatientData(string nam, string sur, string birthNum, InsuranceComp ins)
        {
            name = nam;
            surname = sur;
            id_number = birthNum;
            insurance_com = ins;

            
            if (!StaticExtensions.ValidateName(name)) throw new ArgumentException("Invalid name!");
            if (!StaticExtensions.ValidateSurname(surname)) throw new ArgumentException("Invalid surname!");
            if (!StaticExtensions.ValidateBN(id_number)) throw new ArgumentException("Invalid birth number!");
        }

        public string GetPatientName()
        {
            return $"{name} {surname}";
        }

        public string GetBirthDate()
        {
            return id_number;
        }

        public string GetInsuranceCompany()
        {
            return insurance_com.ToString();
        }

       

    }
}

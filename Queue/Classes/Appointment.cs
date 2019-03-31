using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class Appointment
    {
        public DateTime date_of_appo;
        public string name;
        public string surname;
        public string patient_id;
        public InsuranceComp InsuranceComp;
        public string doctor_id;

        public Appointment(string patientName, string patientSurname, string patientID, string doctorID, InsuranceComp ins, DateTime from)
        {
            name = patientName;
            surname = patientSurname;
            patient_id = patientID;
            doctor_id = doctorID;
            InsuranceComp = ins;
            date_of_appo = from;
        }


    }
}

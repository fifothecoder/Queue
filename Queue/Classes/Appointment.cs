using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Classes
{
    public class Appointment
    {
        public DateTime DateFrom;
        public DateTime DateTo;
        public string DoctorName;


        public Appointment(DateTime from, DateTime to, string docName)
        {
            DateFrom = from;
            DateTo = to;
            DoctorName = docName;
        }





    }
}

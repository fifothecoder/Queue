using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class DoctorData
    {

        private readonly string _name;
        private readonly string _sur;
        public readonly string _docID;
        public List<Appointment> _appointments;

        public DoctorData(string name, string sur, string docID, ref List<Appointment> appo)
        {
            if (!StaticExtensions.ValidateName(name)) throw new ArgumentException("Invalid name!");
            if (!StaticExtensions.ValidateSurname(sur)) throw new ArgumentException("Invalid surname!");

            _name = name;
            _sur = sur;
            _docID = docID;
            _appointments = appo;
        }

        public string GetName()
        {
            return $"{_name} {_sur}";         
        }

    }
}

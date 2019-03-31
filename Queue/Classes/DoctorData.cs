using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class DoctorData
    {

        private string _name;
        private string _sur;


        public DoctorData(string name, string sur)
        {
            if (!StaticExtensions.ValidateName(_name)) throw new ArgumentException("Invalid name!");
            if (!StaticExtensions.ValidateSurname(_sur)) throw new ArgumentException("Invalid surname!");

            _name = name;
            _sur = sur;
        }

        public string GetName()
        {
            return $"{_name} {_sur}";
            
        }

    }
}

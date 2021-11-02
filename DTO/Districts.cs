using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Districts
    {
        public int idDistrict { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return "idDistrict: " + idDistrict +
                   " name: " + name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib.Common
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public int Sort { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ProvinceId={ProvinceId}, Sort={Sort}, Abbreviation={Abbreviation}, Name={Name}";
        }
    }
}

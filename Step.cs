using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazniva_krizovatka
{
    public class Step
    {
        public string Car { get; set; }
        public string Direction { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Direction}({Car} {Count})";
        }
    }
}

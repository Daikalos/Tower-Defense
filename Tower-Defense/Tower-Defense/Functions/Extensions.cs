using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_Defense
{
    static class Extensions
    {
        public static string NumberFormat(int aNumber)
        {
            if (aNumber < 10)
            {
                return "0" + aNumber;
            }
            return aNumber.ToString();
        }
    }
}

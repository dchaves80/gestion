using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Statics
{
    public static class Conversion
    {
        public static Decimal GetDecimal(string mydecimal) 
        {

            //Log.ADD("Convirtiendo:" +  mydecimal, null);

            char myDecimalChar='.';
            if (Convert.ToDecimal("1,1") == 1.1m) myDecimalChar = ',';
            if (Convert.ToDecimal("1.1") == 1.1m) myDecimalChar = '.';

            string mydecimalconverted = mydecimal.Replace(',', myDecimalChar);
            mydecimalconverted = mydecimalconverted.Replace('.', myDecimalChar);
            return Convert.ToDecimal(mydecimalconverted);


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDYS.Helper
{
    public static class ValidationMessage
    {
        public static Func<string, string> RequiredText = (fld) => { return string.Format("\"{0}\" zorunlu alan.", fld); };
        public static Func<string, int, string> MaxLengthText = (fld, legth) => { return string.Format("\"{0}\" alanı en fazla {1} karakter uzunluğunda olmalı.", fld, legth); };
    }
}

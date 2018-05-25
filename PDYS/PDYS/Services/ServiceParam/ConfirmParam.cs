using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDYS.Services.ServiceParam
{
    public class ConfirmParam
    {
        public enum ConfirmResult
        {
            Yes = 6,
            No = 7,
        };

        public string Message { get; set; }
        public Action<ConfirmResult> OnConfirmResult { get; set; }
    }
}

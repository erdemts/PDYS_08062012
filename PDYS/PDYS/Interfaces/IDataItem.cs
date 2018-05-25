using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Interfaces
{
    public interface IDataItem
    {
        [Key]
        int ID { get; set; }

        string DisplayName { get; }
    }
}

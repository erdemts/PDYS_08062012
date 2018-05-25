using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PDYS.ViewModels;

namespace PDYS.Models
{
    //[ComplexType]
    public abstract class Author
    {
        public Author()
        {
            this.State = 0;

            //this.CreatedBy = ApplicationDataModel.CurrentUser;
            //this.ModifiedBy = ApplicationDataModel.CurrentUser;r6t

            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = DateTime.Now;
        }

        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }

        // Foreign key
        [ForeignKey("CreatedBy")]
        public int?  CreatedByID {get;set;}
        [ForeignKey("ModifiedBy")]
        public int?  ModifiedByID  {get;set;}

        // Navigation properties
        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }

        ///Record State
        public int State { get; set; } // 0:Actif 1:PAsif

        // When you specify the Timestamp attribute, 
        // the Entity Framework configures ConcurrencyCheck and DatabaseGeneratedPattern=Computed.
        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }

}

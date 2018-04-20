using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Services.Data.Models
{
    [MetadataType(typeof(sp_Random_ResultMetadata))]
    public partial class sp_Random_Result
    {
        internal sealed class sp_Random_ResultMetadata
        {
            [Display(Name = "Suspeito", Order = 1)]
            public Nullable<int> Suspects { get; set; }
            [Display(Name = "Lugar", Order = 2)]
            public Nullable<int> Places { get; set; }
            [Display(Name = "Arma", Order = 3)]
            public Nullable<int> Weapons { get; set; }
        }
    }
}
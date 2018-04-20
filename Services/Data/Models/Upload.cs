using System;
using System.ComponentModel.DataAnnotations;

namespace Services.Model.Models
{
    [MetadataType(typeof(UploadMetadata))]
    public partial class Upload
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }
    }

    public partial class UploadMetadata
    {
        [Key]
        [Display(Name = "Nome", Order = 1)]
        public String Name { get; set; }
        [Display(Name = "Tamanho", Order = 2)]
        public int Length { get; set; }
        [Display(Name = "Tipo", Order = 3)]
        public string Type { get; set; }
    }
}

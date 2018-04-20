using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Model.Models
{
    [MetadataType(typeof(FieldsMetadata))]
    public partial class Fields
    {
        public String FieldName { get; set; }
        public String FieldType { get; set; }
        public bool FieldPK { get; set; }
    }

    public partial class FieldsMetadata
    {
        [Key]
        [Display(Name = "Nome", Order = 1)]
        public String FieldName { get; set; }
        [Display(Name = "Tipo", Order = 2)]
        public String FieldType { get; set; }
        [Display(Name = "Chave", Order = 3)]
        public bool FieldPK { get; set; }
    }

    /// <summary>
    /// Flag Dropdownlist
    /// </summary>
    public class FlagDropdown
    {
        [Display(Name = "Código", Order = 1)]
        public string cod_Flag { get; set; }
        [Display(Name = "Nome", Order = 2)]
        public string nom_Flag { get; set; }
    }

    [Serializable]   
    public class KeyValueCSV
    {
        public int Key { get; set; }
        public string Values { get; set; }
    }
}

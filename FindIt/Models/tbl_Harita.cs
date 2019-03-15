namespace FindIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Harita
    {
        [Key]
        public int Harita_ID { get; set; }

        public string Harita_Tasarim { get; set; }
    }
}

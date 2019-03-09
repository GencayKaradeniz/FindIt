namespace FindIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_UrunResim
    {
        [Key]
        public int UrunResim_ID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string UrunResim_Yol { get; set; }

        public int Urun_id { get; set; }
    }
}

namespace FindIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Urun
    {
        [Key]
        public int Urun_ID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Urun_Ad { get; set; }

        public decimal Urun_Fiyat { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Urun_Barkod { get; set; }

        public int? AltKategori_id { get; set; }

        public int? Urun_Stok { get; set; }

        public int Personel_id { get; set; }

        [Column(TypeName = "text")]
        public string Urun_Ozellikleri { get; set; }

        public int? Raf_id { get; set; }

        public virtual tbl_AltKategori tbl_AltKategori { get; set; }

        public virtual tbl_Personel tbl_Personel { get; set; }

        public virtual tbl_Raflar tbl_Raflar { get; set; }
    }
}

namespace FindIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RafKatlar
    {
        [Key]
        public int RafKatlar_ID { get; set; }

        public int? Raf_id { get; set; }

        public int? AltKategori_id { get; set; }

        public int? Bulunan_Kat_No { get; set; }

        public virtual tbl_AltKategori tbl_AltKategori { get; set; }

        public virtual tbl_Raflar tbl_Raflar { get; set; }
    }
}

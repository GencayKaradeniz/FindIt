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

        public int Harita_En { get; set; }

        public int Harita_Boy { get; set; }

        public int Harita_Raf_Uzunluk { get; set; }

    }
}

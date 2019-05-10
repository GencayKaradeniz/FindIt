namespace FindIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_AltKategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_AltKategori()
        {
            tbl_RafKatlar = new HashSet<tbl_RafKatlar>();
            tbl_Urun = new HashSet<tbl_Urun>();
        }

        [Key]
        public int AltKategori_ID { get; set; }

        [Required]
        public string AltKategori_Ad { get; set; }

        public int Kategori_id { get; set; }

        public virtual tbl_Kategori tbl_Kategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RafKatlar> tbl_RafKatlar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Urun> tbl_Urun { get; set; }
    }
}

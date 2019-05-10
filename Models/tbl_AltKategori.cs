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
            tbl_Raflar = new HashSet<tbl_Raflar>();
            tbl_Urun = new HashSet<tbl_Urun>();
        }

        [Key]
        public int AltKategori_ID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string AltKategori_Ad { get; set; }

        public int? Kategori_id { get; set; }

        public virtual tbl_Kategori tbl_Kategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Raflar> tbl_Raflar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Urun> tbl_Urun { get; set; }
    }
}

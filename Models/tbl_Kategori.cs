namespace FindIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Kategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Kategori()
        {
            tbl_AltKategori = new HashSet<tbl_AltKategori>();
        }

        [Key]
        public int Kategori_ID { get; set; }

        [Column(TypeName = "text")]
        public string Kategori_Ad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_AltKategori> tbl_AltKategori { get; set; }
    }
}

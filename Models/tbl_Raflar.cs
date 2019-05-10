namespace FindIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Raflar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Raflar()
        {
            tbl_Urun = new HashSet<tbl_Urun>();
        }

        [Key]
        public int Raf_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Raf_Ad { get; set; }

        public int Kategori_id { get; set; }

        [Required]
        public int RafBasX { get; set; }
        [Required]
        public int RafBitX { get; set; }
        [Required]
        public int RafBasY { get; set; }
        [Required]
        public int RafBitY { get; set; }
        [Required]
        public int RafKat { get; set; }

        public virtual tbl_Kategori tbl_Kategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Urun> tbl_Urun { get; set; }
    }
}

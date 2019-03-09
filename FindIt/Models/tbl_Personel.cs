namespace FindIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Personel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Personel()
        {
            tbl_Urun = new HashSet<tbl_Urun>();
        }

        [Key]
        public int Personel_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Personel_Ad { get; set; }

        [Required]
        [StringLength(50)]
        public string Personel_Soyad { get; set; }

        [Required]
        [StringLength(11)]
        public string Personel_TC { get; set; }

        [Required]
        [StringLength(16)]
        public string Personel_Parola { get; set; }

        public int Statu_id { get; set; }

        public virtual tbl_PersonelStatu tbl_PersonelStatu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Urun> tbl_Urun { get; set; }
    }
}

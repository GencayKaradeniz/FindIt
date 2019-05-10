namespace FindIt.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FinditDb : DbContext
    {
        public FinditDb()
            : base("name=Model1")
        {
        }

        public virtual DbSet<tbl_AltKategori> tbl_AltKategori { get; set; }
        public virtual DbSet<tbl_Harita> tbl_Harita { get; set; }
        public virtual DbSet<tbl_Kategori> tbl_Kategori { get; set; }
        public virtual DbSet<tbl_Personel> tbl_Personel { get; set; }
        public virtual DbSet<tbl_PersonelStatu> tbl_PersonelStatu { get; set; }
        public virtual DbSet<tbl_RafKatlar> tbl_RafKatlar { get; set; }
        public virtual DbSet<tbl_Raflar> tbl_Raflar { get; set; }
        public virtual DbSet<tbl_Urun> tbl_Urun { get; set; }
        public virtual DbSet<tbl_UrunResim> tbl_UrunResim { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Kategori>()
                .HasMany(e => e.tbl_Raflar)
                .WithRequired(e => e.tbl_Kategori)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Personel>()
                .HasMany(e => e.tbl_Urun)
                .WithRequired(e => e.tbl_Personel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_PersonelStatu>()
                .HasMany(e => e.tbl_Personel)
                .WithRequired(e => e.tbl_PersonelStatu)
                .WillCascadeOnDelete(false);
        }
    }
}

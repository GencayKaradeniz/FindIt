namespace FindIt.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FinditDb : DbContext
    {
        public FinditDb()
            : base("name=FinditDb")
        {
        }

        public virtual DbSet<tbl_AltKategori> tbl_AltKategori { get; set; }
        public virtual DbSet<tbl_Harita> tbl_Harita { get; set; }
        public virtual DbSet<tbl_Kategori> tbl_Kategori { get; set; }
        public virtual DbSet<tbl_Personel> tbl_Personel { get; set; }
        public virtual DbSet<tbl_PersonelStatu> tbl_PersonelStatu { get; set; }
        public virtual DbSet<tbl_Raflar> tbl_Raflar { get; set; }
        public virtual DbSet<tbl_Urun> tbl_Urun { get; set; }
        public virtual DbSet<tbl_UrunResim> tbl_UrunResim { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_AltKategori>()
                .Property(e => e.AltKategori_Ad)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_AltKategori>()
                .HasMany(e => e.tbl_Raflar)
                .WithRequired(e => e.tbl_AltKategori)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Kategori>()
                .Property(e => e.Kategori_Ad)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Personel>()
                .HasMany(e => e.tbl_Urun)
                .WithRequired(e => e.tbl_Personel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_PersonelStatu>()
                .HasMany(e => e.tbl_Personel)
                .WithRequired(e => e.tbl_PersonelStatu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Urun>()
                .Property(e => e.Urun_Ad)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Urun>()
                .Property(e => e.Urun_Barkod)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Urun>()
                .Property(e => e.Urun_Ozellikleri)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_UrunResim>()
                .Property(e => e.UrunResim_Yol)
                .IsUnicode(false);
        }
    }
}

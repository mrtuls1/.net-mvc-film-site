namespace MvcVize.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<ilceler> ilceler { get; set; }
        public virtual DbSet<iller> iller { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ilceler>()
                .HasMany(e => e.Kullanici)
                .WithOptional(e => e.ilceler)
                .HasForeignKey(e => e.ilceID);

            modelBuilder.Entity<iller>()
                .HasMany(e => e.ilceler)
                .WithRequired(e => e.iller)
                .HasForeignKey(e => e.sehir)
                .WillCascadeOnDelete(false);
        }
    }
}

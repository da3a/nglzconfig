using lubrizol.nglzconfig.entities;

namespace lubrizol.nglzconfig.data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LZConfigContext : DbContext
    {
        public LZConfigContext()
            : base("name=LZConfigContext")
        {
        }

        public virtual DbSet<tblApplication> tblApplication { get; set; }
        public virtual DbSet<tblApplicationConnection> tblApplicationConnection { get; set; }
        public virtual DbSet<tblApplicationVariable> tblApplicationVariable { get; set; }
        public virtual DbSet<tblConnectionType> tblConnectionType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblApplication>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplication>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplication>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplication>()
                .Property(e => e.AssemblyVersion)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplication>()
                .Property(e => e.AssemblyFileVersion)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplication>()
                .Property(e => e.SetupVersion)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplication>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplication>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplication>()
                .HasMany(e => e.tblApplicationConnection)
                .WithRequired(e => e.tblApplication)
                .HasForeignKey(e => e.ApplicationID);

            modelBuilder.Entity<tblApplication>()
                .HasMany(e => e.tblApplicationVariable)
                .WithRequired(e => e.tblApplication)
                .HasForeignKey(e => e.ApplicationID);

            modelBuilder.Entity<tblApplicationConnection>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationConnection>()
                .Property(e => e.ConnectionString)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationConnection>()
                .Property(e => e.VirtualConnectionString)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationConnection>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationConnection>()
                .Property(e => e.ProviderName)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationConnection>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationConnection>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationVariable>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationVariable>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationVariable>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblApplicationVariable>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblConnectionType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblConnectionType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tblConnectionType>()
                .Property(e => e.DefaultConnectionString)
                .IsUnicode(false);

            modelBuilder.Entity<tblConnectionType>()
                .Property(e => e.ProviderName)
                .IsUnicode(false);

            modelBuilder.Entity<tblConnectionType>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tblConnectionType>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);
        }
    }
}

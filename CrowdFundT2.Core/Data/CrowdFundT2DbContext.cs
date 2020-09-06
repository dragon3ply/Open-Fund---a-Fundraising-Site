using CrowdFundT2.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace CrowdFundT2.Core.Data
{
    public class CrowdFundT2DbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=localhost;Database=CrowdFundT2Db;User Id=sa;Password=qwer!@#$1234;");
            //optionsBuilder.UseSqlServer("Server=localhost;Database=CrowdFundT2Db;User Id=sa;Password=admin!@#123;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Client>()
                .ToTable("Client");

            modelBuilder
                .Entity<Project>()
                .ToTable("Project");

            modelBuilder
                .Entity<Package>()
                .ToTable("Package");

            modelBuilder
                .Entity<InvestedPackage>()
                .ToTable("InvestedPackage");

            modelBuilder
                .Entity<InvestedPackage>()
                .HasKey(op => new { op.InvestedProjectId, op.PackageId });

            modelBuilder
                .Entity<InvestedProject>()
                .ToTable("InvestedProject");

        }
    }
}

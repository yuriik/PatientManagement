using Microsoft.EntityFrameworkCore;
using PatientManagement.API.Models;
using System.Text.Json;

namespace PatientManagement.API.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .Property(p => p.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Name)
                .WithOne()
                .HasForeignKey<Name>("PatientId");

            modelBuilder.Entity<Name>()
                .Property(n => n.Given)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null));
        }
    }
}

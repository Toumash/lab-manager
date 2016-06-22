using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
    public class LabContext : IdentityDbContext<ApplicationUser>
    {
        public LabContext()
            : base("db", throwIfV1Schema: false)
        {
        }

        static LabContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LabContext, Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Exam> Badania { get; set; }
        public DbSet<Patient> Pacjenci { get; set; }
        public DbSet<Doctor> Lekarze { get; set; }

        public static LabContext Create()
        {
            return new LabContext();
        }
    }
}
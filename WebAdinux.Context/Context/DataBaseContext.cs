using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebAdinux.Context.Entities;

namespace WebAdinux.Context.Context
{
    public class MyContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {
        public DataBaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseSqlServer(@"Server=.;Database=WebAdinux;User Id=admin;Password=aA123456;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

            return new DataBaseContext(optionsBuilder.Options);
        }
    }
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        #region Tabel

        public DbSet<User> Users { get; set; }
        public DbSet<EmailMessage> emailMessages { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Key

            modelBuilder.Entity<User>().HasKey(x=> x.Id);
            modelBuilder.Entity<EmailMessage>().HasKey(x=> x.Id);

            #endregion

            #region Validation

            #endregion

            #region Relation

            #endregion
        }
    }
}

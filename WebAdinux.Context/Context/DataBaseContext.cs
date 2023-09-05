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
        public DbSet<SiteHeader> siteHeaders { get; set; }
        public DbSet<SiteContent> siteContent { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Key

            modelBuilder.Entity<User>().HasKey(x=> x.Id);
            modelBuilder.Entity<EmailMessage>().HasKey(x=> x.Id);
            modelBuilder.Entity<SiteHeader>().HasKey(x=> x.Id);
            modelBuilder.Entity<SiteContent>().HasKey(x=> x.Id);

            #endregion

            #region Validation

            #endregion


            #region Relation

            modelBuilder.Entity<SiteHeader>()
                .HasOne(x => x.siteHeader)
                .WithMany(x => x.siteHeaders)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SiteHeader>()
                .HasMany(x => x.siteHeaders)
                .WithOne(x => x.siteHeader)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<SiteContent>()
                .HasOne(x => x.siteHeader)
                .WithMany(x => x.siteContents)
                .HasForeignKey(x => x.HeaderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SiteHeader>()
                .HasMany(x => x.siteContents)
                .WithOne(x => x.siteHeader)
                .HasForeignKey(x => x.HeaderId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}

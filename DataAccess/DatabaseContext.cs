using ContactReport.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactReport.DataAccess
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<CommInfo> CommInfo { get; set; }
        public DbSet<Report> Report { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommInfo>().HasOne(e => e.Contact).WithMany(x => x.CommInfos);
            modelBuilder.Entity<Report>().HasOne(e => e.Contact).WithMany(x => x.Reports);    

            base.OnModelCreating(modelBuilder);
        }
    }
}

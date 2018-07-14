using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApiCoreKata.Persistence.SqlServer.Dto;

namespace WebApiCoreKata.Persistence.SqlServer.Database
{
    /**
    Generated with:
    dotnet ef dbcontext scaffold "Server=BIG_ROMCY\SQLEXPRESS;Database=WebApiCoreKata;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Database -c KataContext
    */
    public partial class KataContext : DbContext
    {
        public KataContext()
        {
        }

        public KataContext(DbContextOptions<KataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=BIG_ROMCY\\SQLEXPRESS;Database=WebApiCoreKata;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}

        //Tables

        /**
         * dotnet ef migrations add add_customers
         */
        public DbSet<CustomerDto> Customers { get; set; }
    }
}

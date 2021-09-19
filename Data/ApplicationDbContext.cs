using EcommerceApi_dotNetFramework.Models;
using EcommerceApi_dotNetFramework.Models.PayGateTransactionModels;
using EcommerceApi_dotNetFramework.Models.PaystackModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext() : base("dbConnectionString")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<PaystackTransaction> PaystackTransactions { get; set; }

        public DbSet<PaystackBankAuthorization> PaystackBankAuthorizations { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                                .Where(entry => entry.Entity is BaseEntity
                                && (entry.State == EntityState.Added ||
                                entry.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).UpdatedAt = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }

    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ExpenseTrackerJS.Model;

namespace ExpenseTrackerJS.DataAccess
{
    public class ExContext : IdentityDbContext, IContext
    {

        public ExContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
          

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppCode> AppCodes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleLine> SaleLines { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    //Enable-Migrations -contexttypename OMPContext
    public class OMPContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryAttributes> CategoryAttributes { get; set; }
        public DbSet<CQRS_ProductSummary> CQRS_ProductSummary { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductAttributesValue> ProductAttributesValue { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductReview> ProductReview { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
        public DbSet<Search> Search { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserRating> UserRating { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Cart> Cart { get; set; }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

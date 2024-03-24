using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebNoiThat.Models
{
    public partial class QLNoiThat : DbContext
    {
        public QLNoiThat()
            : base("name=QLNoiThat")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<AttributesPrice> AttributesPrices { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<QuangCao> QuangCaos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<TinDang> TinDangs { get; set; }
        public virtual DbSet<TransactStatu> TransactStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Salt)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Salt)
                .IsFixedLength();

            modelBuilder.Entity<Shipper>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<TransactStatu>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.TransactStatu)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<WebNoiThat.ModelViews.LoginViewModel> LoginViewModels { get; set; }
    }
}

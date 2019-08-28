using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AgenciappHome.Models
{
    public partial class databaseContext : DbContext
    {
        public databaseContext()
        {
        }

        public databaseContext(DbContextOptions<databaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Agency> Agency { get; set; }
        public virtual DbSet<Carrier> Carrier { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Office> Office { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PackageItem> PackageItem { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<ShippingItem> ShippingItem { get; set; }
        public virtual DbSet<TipoPago> TipoPago { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<ValorAduanal> ValorAduanal { get; set; }
        public virtual DbSet<ValorAduanalItem> ValorAduanalItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=desktop-pm15ctg;Initial Catalog=database;Integrated Security=True; User Id=sa;Password=Maluma123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.AddressId).ValueGeneratedNever();

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.AddressLine2).HasMaxLength(250);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Zip).HasMaxLength(50);
            });

            modelBuilder.Entity<Agency>(entity =>
            {
                entity.HasKey(e => e.AgencyId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.AgencyId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.LegalName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.HasKey(e => e.CarrierId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.CarrierId).ValueGeneratedNever();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.AgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefAgency8");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.ContactId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefClient10");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasKey(e => e.OfficeId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.OfficeId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Office)
                    .HasForeignKey(d => d.AgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefAgency9");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CantLb).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(14);

                entity.Property(e => e.OtrosCostos).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PriceLb).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ValorAduanal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ValorPagado).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.AgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefAgency17");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefClient18");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Contact");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefOffice19");

                entity.HasOne(d => d.TipoPago)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.TipoPagoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_TipoPago");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.PackageId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.PackageId).ValueGeneratedNever();

                entity.HasOne(d => d.PackageNavigation)
                    .WithOne(p => p.Package)
                    .HasForeignKey<Package>(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefOrder20");
            });

            modelBuilder.Entity<PackageItem>(entity =>
            {
                entity.HasKey(e => e.PackageItemId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.PackageItemId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Qty).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageItem)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPackage1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PackageItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefProduct2");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.Property(e => e.PhoneId).ValueGeneratedNever();

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.TallaMarca)
                    .IsRequired()
                    .HasColumnName("Talla/Marca")
                    .HasMaxLength(50);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.AgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefAgency12");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.HasKey(e => e.PackingId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.PackingId).ValueGeneratedNever();

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Shipping)
                    .HasForeignKey(d => d.AgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefAgency4");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.Shipping)
                    .HasForeignKey(d => d.CarrierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCarrier5");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Shipping)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefOffice16");
            });

            modelBuilder.Entity<ShippingItem>(entity =>
            {
                entity.HasKey(e => e.ShippingItemId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.ShippingItemId).ValueGeneratedNever();

                entity.Property(e => e.Qty).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.ShippingItem)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPackage14");

                entity.HasOne(d => d.Packing)
                    .WithMany(p => p.ShippingItem)
                    .HasForeignKey(d => d.PackingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefShipping13");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShippingItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefProduct15");
            });

            modelBuilder.Entity<TipoPago>(entity =>
            {
                entity.Property(e => e.TipoPagoId).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ExpiresSecureCode).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.SecureCode).HasMaxLength(250);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ValorAduanal>(entity =>
            {
                entity.Property(e => e.ValorAduanalId).ValueGeneratedNever();

                entity.Property(e => e.Article)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Observaciones).HasMaxLength(250);

                entity.Property(e => e.Um)
                    .IsRequired()
                    .HasColumnName("UM")
                    .HasMaxLength(50);

                entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<ValorAduanalItem>(entity =>
            {
                entity.Property(e => e.ValorAduanalItemId).ValueGeneratedNever();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ValorAduanalItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Order");

                entity.HasOne(d => d.ValorAduanal)
                    .WithMany(p => p.ValorAduanalItem)
                    .HasForeignKey(d => d.ValorAduanalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_ValorAduanal");
            });
        }
    }
}

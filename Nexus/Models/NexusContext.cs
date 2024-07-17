using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Models;

public partial class NexusContext : DbContext
{
    public NexusContext()
    {
    }

    public NexusContext(DbContextOptions<NexusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<ConnectionType> ConnectionTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RetailShop> RetailShops { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServicePackage> ServicePackages { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-CGFNOES8;Database=Nexus;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Bill__D706DDB305928DB7");

            entity.ToTable("Bill");

            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.OrderId)
                .HasMaxLength(11)
                .HasColumnName("order_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Order).WithMany(p => p.Bills)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__order_id__3D5E1FD2");
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => e.ConnectionId).HasName("PK__Connecti__E4AA4DD0A8561367");

            entity.ToTable("Connection");

            entity.Property(e => e.ConnectionId)
                .HasMaxLength(16)
                .HasColumnName("connection_id");
            entity.Property(e => e.AccountId)
                .HasMaxLength(16)
                .HasColumnName("account_id");
            entity.Property(e => e.InstallationDate).HasColumnName("installation_date");
            entity.Property(e => e.OrderId)
                .HasMaxLength(11)
                .HasColumnName("order_id");
            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TerminationDate).HasColumnName("termination_date");

            entity.HasOne(d => d.Account).WithMany(p => p.Connections)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Connectio__accou__4316F928");

            entity.HasOne(d => d.Order).WithMany(p => p.Connections)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Connectio__order__440B1D61");

            entity.HasOne(d => d.Package).WithMany(p => p.Connections)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Connectio__packa__44FF419A");
        });

        modelBuilder.Entity<ConnectionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Connecti__3213E83FC10ED7DE");

            entity.ToTable("ConnectionType");

            entity.HasIndex(e => e.Name, "UQ__Connecti__72E12F1B03978E64").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Customer__46A222CD86C6BEFB");

            entity.ToTable("Customer");

            entity.Property(e => e.AccountId)
                .HasMaxLength(16)
                .HasColumnName("account_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA840883B2B");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__role_i__286302EC");

            entity.HasOne(d => d.Shop).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__shop_i__29572725");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__46596229454FC613");

            entity.Property(e => e.OrderId)
                .HasMaxLength(11)
                .HasColumnName("order_id");
            entity.Property(e => e.AccountId)
                .HasMaxLength(16)
                .HasColumnName("account_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__account___38996AB5");

            entity.HasOne(d => d.Package).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__package___398D8EEE");

            entity.HasOne(d => d.Shop).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__shop_id__3A81B327");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__OrderDet__022945F6AD007A87");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderId)
                .HasMaxLength(11)
                .HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__order__47DBAE45");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__produ__48CFD27E");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA5CC8D924");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");

            entity.HasOne(d => d.Bill).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BillId)
                .HasConstraintName("FK__Payment__bill_id__403A8C7D");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__47027DF50C9B1CC4");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Products)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__vendor___32E0915F");
        });

        modelBuilder.Entity<RetailShop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__RetailSh__AD08178604B64C51");

            entity.ToTable("RetailShop");

            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC2BC69455");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<ServicePackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__ServiceP__63846AE8647F2EB7");

            entity.ToTable("ServicePackage");

            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.ConnectionTypeId).HasColumnName("connection_type_id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");

            entity.HasOne(d => d.ConnectionType).WithMany(p => p.ServicePackages)
                .HasForeignKey(d => d.ConnectionTypeId)
                .HasConstraintName("FK__ServicePa__conne__35BCFE0A");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendor__0F7D2B7869EB7A2C");

            entity.ToTable("Vendor");

            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

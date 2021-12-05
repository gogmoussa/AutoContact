using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace AutoContact.Models
{
    public partial class AutoContactContext : DbContext
    {
        public AutoContactContext()
        {
        }

        public AutoContactContext(DbContextOptions<AutoContactContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessLevel> AccessLevels { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AppointmentInvoice> AppointmentInvoices { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarClient> CarClients { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<LoanerCar> LoanerCars { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                              .AddJsonFile("appsettings.json")
                                              .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("AutoContactContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccessLevel>(entity =>
            {
                entity.Property(e => e.AccessLevelId).ValueGeneratedNever();

                entity.Property(e => e.AccessLevel1).IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.AccessLevels)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AccessLevel_Client");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AccessLevels)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_AccessLevel_Employee");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).ValueGeneratedNever();

                entity.Property(e => e.StreetNum).IsUnicode(false);

                entity.Property(e => e.UnitNum).IsUnicode(false);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).ValueGeneratedNever();

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Car");
            });

            modelBuilder.Entity<AppointmentInvoice>(entity =>
            {
                entity.Property(e => e.AppointmentInvoiceId).ValueGeneratedNever();

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentInvoices)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_AppointmentInvoice_Appointment");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.AppointmentInvoices)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_AppointmentInvoice_Invoice");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.CarId).ValueGeneratedNever();

                entity.Property(e => e.Colour).IsUnicode(false);

                entity.Property(e => e.Make).IsUnicode(false);

                entity.Property(e => e.Model).IsUnicode(false);

                entity.Property(e => e.Vin).IsUnicode(false);
            });

            modelBuilder.Entity<CarClient>(entity =>
            {
                entity.HasOne(d => d.Car)
                    .WithMany()
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK_CarClient_Car");

                entity.HasOne(d => d.Client)
                    .WithMany()
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_CarClient_Client");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.Property(e => e.DriverLicence).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.HashPass).IsUnicode(false);

                entity.Property(e => e.HashSalt).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Client_Address");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentId).ValueGeneratedNever();

                entity.Property(e => e.DepartmentName).IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Department_Employee");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).ValueGeneratedOnAdd();

                entity.Property(e => e.EmployeeSin)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.HashPass).IsUnicode(false);

                entity.Property(e => e.HashSalt).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.TerminationReason).IsFixedLength(true);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Employee_Address");

                entity.HasOne(d => d.ManagerNavigation)
                    .WithMany(p => p.InverseManagerNavigation)
                    .HasForeignKey(d => d.Manager)
                    .HasConstraintName("FK_Employee_Employee");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Employee");

                entity.HasOne(d => d.LoanerCar)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.LoanerCarId)
                    .HasConstraintName("FK_Invoice_LoanerCar");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.PartId)
                    .HasConstraintName("FK_Invoice_Part");
            });

            modelBuilder.Entity<LoanerCar>(entity =>
            {
                entity.Property(e => e.LoanerCarId).ValueGeneratedNever();

                entity.Property(e => e.Colour).IsUnicode(false);

                entity.Property(e => e.Make).IsUnicode(false);

                entity.Property(e => e.Model).IsUnicode(false);

                entity.Property(e => e.Vin).IsUnicode(false);
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.Property(e => e.PartId).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Part_Category");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_Part_Vendor");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.Property(e => e.PurchaseOrderId).ValueGeneratedNever();

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrder_Vendor");
            });

            modelBuilder.Entity<PurchaseOrderLineItem>(entity =>
            {
                entity.Property(e => e.PurchaseOrderLineItemId).ValueGeneratedNever();

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.PurchaseOrderLineItems)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrderLineItem_Part");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.PurchaseOrderLineItems)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrderLineItem_PurchaseOrder");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.VendorId).ValueGeneratedNever();

                entity.Property(e => e.Phone).IsFixedLength(true);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Vendors)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Vendor_Address");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

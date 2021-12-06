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
        public virtual DbSet<Email> Emails { get; set; }
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
                entity.ToTable("AccessLevel");

                entity.Property(e => e.AccessLevelId).ValueGeneratedNever();

                entity.Property(e => e.AccessLevel1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("AccessLevel");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.AccessLevels)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_AccessLevel_Client");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AccessLevels)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_AccessLevel_Employee");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.AddressId).ValueGeneratedNever();

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProvinceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StreetNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UnitNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.AppointmentId).ValueGeneratedNever();

                entity.Property(e => e.AppointmentStartTime).HasColumnType("datetime");

                entity.Property(e => e.BookedAtTime).HasColumnType("datetime");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Car");
            });

            modelBuilder.Entity<AppointmentInvoice>(entity =>
            {
                entity.ToTable("AppointmentInvoice");

                entity.Property(e => e.AppointmentInvoiceId).ValueGeneratedNever();

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentInvoices)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentInvoice_Appointment");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.AppointmentInvoices)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentInvoice_Invoice");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.CarId).ValueGeneratedNever();

                entity.Property(e => e.Colour)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vin)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("VIN");
            });

            modelBuilder.Entity<CarClient>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CarClient");

                entity.HasOne(d => d.Car)
                    .WithMany()
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarClient_Car");

                entity.HasOne(d => d.Client)
                    .WithMany()
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarClient_Client");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.DriverLicence)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HashPass)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.HashSalt)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_Address");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).ValueGeneratedNever();

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_Employee");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Email");

                entity.Property(e => e.AddedDate).HasColumnType("date");

                entity.Property(e => e.Email1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Email");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeSin)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeSIN")
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HashPass)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.HashSalt)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TerminationDate).HasColumnType("date");

                entity.Property(e => e.TerminationReason)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Address");

                entity.HasOne(d => d.ManagerNavigation)
                    .WithMany(p => p.InverseManagerNavigation)
                    .HasForeignKey(d => d.Manager)
                    .HasConstraintName("FK_Employee_Employee");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.Property(e => e.CancelledDate).HasColumnType("date");

                entity.Property(e => e.CompletedDate).HasColumnType("date");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.PaidDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Employee");

                entity.HasOne(d => d.LoanerCar)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.LoanerCarId)
                    .HasConstraintName("FK_Invoice_LoanerCar");
            });

            modelBuilder.Entity<LoanerCar>(entity =>
            {
                entity.ToTable("LoanerCar");

                entity.Property(e => e.LoanerCarId).ValueGeneratedNever();

                entity.Property(e => e.Colour)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vin)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("VIN");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.ToTable("Part");

                entity.Property(e => e.PartId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CostPrice).HasColumnType("money");

                entity.Property(e => e.ReorderQty)
                    .IsRequired().HasColumnType("int");

                entity.Property(e => e.EconomicalOrderQty)
                    .IsRequired().HasColumnType("int");

                entity.Property(e => e.QtyOnHand).HasColumnType("int");

                entity.Property(e => e.QtyOnOrder).HasColumnType("int");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Parts)
                     .HasForeignKey(d => d.VendorId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Part_Vendor");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Parts)
                     .HasForeignKey(d => d.CategoryId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Part_Category");

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Category")
                    ;
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("PurchaseOrder");

                entity.Property(e => e.PurchaseOrderId).ValueGeneratedOnAdd();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.PODate).HasColumnType("date");

                entity.Property(e => e.CancelledDate).HasColumnType("date");

            });

            modelBuilder.Entity<PurchaseOrderLineItem>(entity =>
            {
                entity.ToTable("PurchaseOrderLineItem");

                entity.Property(e => e.PurchaseOrderLineItemId).ValueGeneratedOnAdd();

                entity.Property(e => e.Qty).HasColumnType("int");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Part)
                 .WithMany(p => p.PurchaseOrderLineItems)
                 .HasForeignKey(d => d.PartId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_PurchaseOrderLineItem_Part");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendor");

                entity.Property(e => e.VendorId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MainContact) 
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Address)
                  .WithMany(p => p.Vendors)
                  .HasForeignKey(d => d.AddressId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Vendor_Address");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

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
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<LoanerCar> LoanerCars { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }

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
                entity.HasNoKey();

                entity.ToTable("AccessLevel");

                entity.Property(e => e.AccessLevel1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("AccessLevel");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Address");

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
                entity.HasNoKey();

                entity.ToTable("Appointment");

                entity.Property(e => e.AppointmentStartTime).HasColumnType("datetime");

                entity.Property(e => e.BookedAtTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AppointmentInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AppointmentInvoice");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Car");

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
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Client");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.DriverLicence)
                    .HasMaxLength(17)
                    .IsUnicode(false);

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
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Department");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
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
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Invoice");

                entity.Property(e => e.CancelledDate).HasColumnType("date");

                entity.Property(e => e.CompletedDate).HasColumnType("date");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.PaidDate).HasColumnType("date");
            });

            modelBuilder.Entity<LoanerCar>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LoanerCar");

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

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Phone");

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BarqMockupsLib
{
    public partial class BarqBECoreMockContext : DbContext
    {
        private string ConnectionString;

        public BarqBECoreMockContext(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public BarqBECoreMockContext(DbContextOptions<BarqBECoreMockContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountStatus> AccountStatus { get; set; }
        public virtual DbSet<Otp> Otp { get; set; }
        public virtual DbSet<Otpstatus> Otpstatus { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastOtpid).HasColumnName("LastOTPID");

                entity.Property(e => e.Mpin)
                    .IsRequired()
                    .HasColumnName("MPin")
                    .HasMaxLength(50);

                entity.Property(e => e.MpinTrials).HasColumnName("MPinTrials");

                entity.Property(e => e.Msisdn)
                    .IsRequired()
                    .HasColumnName("MSISDN")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.LastOtp)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.LastOtpid)
                    .HasConstraintName("FK_Account_OTP");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_AccountStatus");
            });

            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("OTP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ConsumptionTime).HasColumnType("datetime");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Otp)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OTP_OTPStatus");
            });

            modelBuilder.Entity<Otpstatus>(entity =>
            {
                entity.ToTable("OTPStatus");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IssueTime).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasColumnName("TransactionID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.FromAccountNavigation)
                    .WithMany(p => p.TransactionFromAccountNavigation)
                    .HasForeignKey(d => d.FromAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Account");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_TransactionStatus");

                entity.HasOne(d => d.ToAccountNavigation)
                    .WithMany(p => p.TransactionToAccountNavigation)
                    .HasForeignKey(d => d.ToAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Account1");
            });

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10);
            });
        }
    }
}

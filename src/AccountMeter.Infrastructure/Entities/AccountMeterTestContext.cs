using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AccountMeter.API.Entities;

public partial class AccountMeterTestContext : DbContext
{
    public AccountMeterTestContext()
    {
    }

    public AccountMeterTestContext(DbContextOptions<AccountMeterTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<MeterReading> MeterReadings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=AccountMeter_Test;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.AccountId).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<MeterReading>(entity =>
        {
            entity.HasKey(e => e.ReadingId).HasName("PK__MeterRea__C80F9C4E68992A46");

            entity.Property(e => e.MeterReadingDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.MeterReadings)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

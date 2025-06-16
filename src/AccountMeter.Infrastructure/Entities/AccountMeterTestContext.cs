using Microsoft.EntityFrameworkCore;

namespace AccountMeter.API.Entities;

public partial class AccountMeterTestContext : DbContext
{
    public AccountMeterTestContext(DbContextOptions<AccountMeterTestContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<MeterReading> MeterReadings { get; set; }
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

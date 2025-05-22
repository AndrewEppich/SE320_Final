using Microsoft.EntityFrameworkCore;
using receiptProject.receiptProjectBackend.Services;

namespace receiptProject.receiptProjectBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptItem> ReceiptItems { get; set; }
        public DbSet<Summary> Summaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
                .ToTable("user")
                .HasKey(u => u.UserID);


            modelBuilder.Entity<Receipt>()
                .ToTable("receipts")
                .HasKey(r => r.ReceiptID);

            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.User)
                .WithMany(u => u.Receipts)
                .HasForeignKey(r => r.UserID);


            modelBuilder.Entity<ReceiptItem>()
                .ToTable("receiptItems")
                .HasKey(ri => ri.ItemID);

            modelBuilder.Entity<ReceiptItem>()
                .HasOne(ri => ri.Receipt)
                .WithMany(r => r.Items)
                .HasForeignKey(ri => ri.ReceiptID);

        }
    }
} 
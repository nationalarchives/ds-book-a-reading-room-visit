using book_a_reading_room_visit.domain;
using Microsoft.EntityFrameworkCore;

namespace book_a_reading_room_visit.data
{
    public class DocumentOrderContext : DbContext
    {
        public DocumentOrderContext(DbContextOptions<DocumentOrderContext> options)
            : base(options)
        {}

        public DbSet<Order> Orders { get; set; }
        public DbSet<SeatType> SeatType { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<OrderDocument> OrderDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeatType>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<SeatType>().Property(e => e.Description).IsRequired().HasMaxLength(150);

            modelBuilder.Entity<OrderStatus>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<OrderStatus>().Property(e => e.Description).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Seat>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<Seat>().Property(e => e.Number).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Seat>().Property(e => e.Description).HasMaxLength(50);

            modelBuilder.Entity<Order>().Property(e => e.OrderReference).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Order>().Property(e => e.Email).HasMaxLength(100);
            modelBuilder.Entity<Order>().Property(e => e.FirstName).HasMaxLength(50);
            modelBuilder.Entity<Order>().Property(e => e.LastName).HasMaxLength(50);

            modelBuilder.Entity<OrderDocument>().Property(e => e.DocumentReference).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<OrderDocument>().Property(e => e.LetterCode).HasMaxLength(20);
            modelBuilder.Entity<OrderDocument>().Property(e => e.PieceReference).HasMaxLength(20);
            modelBuilder.Entity<OrderDocument>().Property(e => e.ItemReference).HasMaxLength(20);
            modelBuilder.Entity<OrderDocument>().Property(e => e.Site).HasMaxLength(20);

            modelBuilder.Seed();
        }
    }
}

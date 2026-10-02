using book_a_reading_room_visit.domain;
using Microsoft.EntityFrameworkCore;

namespace book_a_reading_room_visit.data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {}

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<SeatType> SeatType { get; set; }
        public DbSet<BookingType> BookingType { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<BookingStatus> BookingStatus { get; set; }
        public DbSet<OrderDocument> OrderDocuments { get; set; }
        public DbSet<TNAHoliday> TNAHoliday { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeatType>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<SeatType>().Property(e => e.Description).IsRequired().HasMaxLength(150);

            modelBuilder.Entity<BookingStatus>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<BookingStatus>().Property(e => e.Description).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<BookingType>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<BookingType>().Property(e => e.Description).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Seat>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<Seat>().Property(e => e.Number).IsRequired().HasMaxLength(10);

            // Required to allow use of triggers on the table, see https://learn.microsoft.com/en-gb/ef/core/what-is-new/ef-core-7.0/breaking-changes?tabs=data-annotations%2Cv7#sqlserver-tables-with-triggers
            modelBuilder.Entity<Booking>().ToTable(tb => tb.UseSqlOutputClause(false));
            modelBuilder.Entity<Booking>().Property(e => e.BookingReference).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Booking>().Property(e => e.Email).HasMaxLength(100);
            modelBuilder.Entity<Booking>().Property(e => e.Phone).HasMaxLength(50);
            modelBuilder.Entity<Booking>().Property(e => e.FirstName).HasMaxLength(50);
            modelBuilder.Entity<Booking>().Property(e => e.LastName).HasMaxLength(50);
            modelBuilder.Entity<Booking>().Property(e => e.LastModifiedBy).HasMaxLength(50);

            // Required to allow use of triggers on the table, see https://learn.microsoft.com/en-gb/ef/core/what-is-new/ef-core-7.0/breaking-changes?tabs=data-annotations%2Cv7#sqlserver-tables-with-triggers
            modelBuilder.Entity<OrderDocument>().ToTable(tb => tb.UseSqlOutputClause(false)); 
            modelBuilder.Entity<OrderDocument>().HasOne(d => d.Booking).WithMany(b => b.OrderDocuments).HasForeignKey(d => d.BookingId);
            modelBuilder.Entity<OrderDocument>().Property(e => e.DocumentReference).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<OrderDocument>().Property(e => e.Description).HasMaxLength(255);
            modelBuilder.Entity<OrderDocument>().Property(e => e.LetterCode).HasMaxLength(20);
            modelBuilder.Entity<OrderDocument>().Property(e => e.PieceReference).HasMaxLength(40);
            modelBuilder.Entity<OrderDocument>().Property(e => e.ItemReference).HasMaxLength(40);
            modelBuilder.Entity<OrderDocument>().Property(e => e.Site).HasMaxLength(20);
            modelBuilder.Entity<OrderDocument>().Property(e => e.LastModifiedBy).HasMaxLength(50);

            modelBuilder.Entity<TNAHoliday>().ToView(nameof(TNAHoliday)).HasNoKey();
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<EventVendor> EventVendors { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=eventeaseg6.database.windows.net;Database=EventEase;User Id=eventease;Password=Rori@sang4;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE48823C12FD0");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.AdminEmail, "UQ__Admin__F2AA7AD9E68A4AF7").IsUnique();

            entity.Property(e => e.Action).HasColumnType("text");
            entity.Property(e => e.AdminEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AdminName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AdminPosition)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AdminRole)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951AED68C46960");

            entity.ToTable("Booking");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");

            //entity.HasOne(d => d.Client).WithMany(p => p.Bookings)
            //    .HasForeignKey(d => d.ClientId)
            //    .HasConstraintName("FK_Booking_Client");

            //entity.HasOne(d => d.Event).WithMany(p => p.Bookings)
            //    .HasForeignKey(d => d.EventId)
            //    .HasConstraintName("FK__Booking__EventId__656C112C");

            entity.HasOne(d => d.Venue).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.VenueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__VenueId__66603565");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Client__E67E1A246BFF54D6");

            entity.ToTable("Client");

            entity.HasIndex(e => e.ClientEmail, "UQ__Client__AD48A6FF76A963B0").IsUnique();

            entity.Property(e => e.ClientEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ClientName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactDetails)
                .HasMaxLength(255)
                .IsUnicode(false);
            //entity.Property(e => e.EventName)
            //    .HasMaxLength(255)
            //    .IsUnicode(false);
            //entity.Property(e => e.Password)
            //    .HasMaxLength(255)
            //    .IsUnicode(false);

            //entity.HasOne(d => d.Booking).WithMany(p => p.Clients)
            //    .HasForeignKey(d => d.BookingId)
            //    .OnDelete(DeleteBehavior.SetNull)
            //    .HasConstraintName("FK__Client__BookingI__6A30C649");

            //entity.HasOne(d => d.Event).WithMany(p => p.Clients)
            //    .HasForeignKey(d => d.EventId)
            //    .HasConstraintName("FK__Client__EventId__6B24EA82");

            //entity.HasOne(d => d.Venue).WithMany(p => p.Clients)
            //    .HasForeignKey(d => d.VenueId)
            //    .OnDelete(DeleteBehavior.SetNull)
            //    .HasConstraintName("FK__Client__VenueId__6C190EBB");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C8108C887AC1");

            entity.ToTable("Event");

            entity.Property(e => e.EventDescription).HasColumnType("text");
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");

            //entity.HasOne(d => d.Venue).WithMany(p => p.Events)
            //    .HasForeignKey(d => d.VenueId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Event__VenueId__619B8048");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__EventTyp__A9216B1F440A482A");

            entity.ToTable("EventType");

            entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");
            entity.Property(e => e.EventTypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EventVendor>(entity =>
        {
            entity.HasKey(e => e.EventVendorId).HasName("PK__EventVen__EAE82A1006CA1BA4");

            entity.ToTable("EventVendor");

            entity.Property(e => e.ContactDetails)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ServiceOffering).HasColumnType("text");
            entity.Property(e => e.VendorName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Event).WithMany(p => p.EventVendors)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__EventVend__Event__6EF57B66");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38FA121E52");

            entity.ToTable("Payment");

            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Payment__Booking__72C60C4A");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venue__3C57E5F21273BBB8");

            entity.ToTable("Venue");

            entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.VenueName)
                .HasMaxLength(255)
                .IsUnicode(false);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;

namespace SportSparkInfrastructureLibrary.Database;

public class SportSparkDBContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Friendship> Friendships { get; set; }

    public DbSet<EventType> EventTypes { get; set; }

    public DbSet<EventRepeatType> EventRepeatTypes { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Document> Documents { get; set; }
    public SportSparkDBContext(DbContextOptions<SportSparkDBContext> options) : base(options)
    {
           
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(x => x.Events)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
        modelBuilder.Entity<User>().Property(x => x.Password)
            .UseCollation("SQL_Latin1_General_CP1_CS_AS");

        modelBuilder.Entity<Friendship>().HasKey(x => x.Id);
        modelBuilder.Entity<Friendship>()
            .HasOne(x => x.User1)
            .WithMany(x => x.RequestedFriendships)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Friendship>()
            .HasOne(x => x.User2)
            .WithMany(x => x.ConfirmedFriendships)
            .HasForeignKey(x => x.User2Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

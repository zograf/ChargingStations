using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ChargingStation.Data;

public class ChargingStationContext: DbContext 
{
    public DbSet<User> Users { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Credentials> Credentials { get; set; }
    public DbSet<BasePrice> BasePrices { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ChargingSpot> ChargingSpots { get; set; }
    public DbSet<Charging> Chargings { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Place> Places { get; set; }
    
    public ChargingStationContext(DbContextOptions options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Card>()
            .HasMany(x => x.Reservations);
        
        modelBuilder.Entity<Card>()
            .HasMany(x => x.Chargings);
        
        modelBuilder.Entity<Charging>()
            .HasOne(x => x.Reservation);
        
        modelBuilder.Entity<ChargingSpot>()
            .HasMany(x => x.Chargings);

        modelBuilder.Entity<Client>()
            .HasOne(x => x.User);
        
        modelBuilder.Entity<Client>()
            .HasMany(x => x.Transactions);
        
        modelBuilder.Entity<Client>()
            .HasMany(x => x.Vehicles);
        
        modelBuilder.Entity<Manager>()
            .HasOne(x => x.User);
        
        modelBuilder.Entity<Place>()
            .HasMany(x => x.Addresses);
        
        modelBuilder.Entity<Station>()
            .HasOne(x => x.Address);
        
        modelBuilder.Entity<Station>()
            .HasMany(x => x.BasePrices);
        
        modelBuilder.Entity<Station>()
            .HasOne(x => x.Manager);
        
        modelBuilder.Entity<Station>()
            .HasMany(x => x.ChargingSpots);
        
        modelBuilder.Entity<User>()
            .HasOne(x => x.Credentials);
        
        modelBuilder.Entity<Vehicle>()
            .HasOne(x => x.Card);

        modelBuilder.Entity<ChargingSpot>()
            .HasMany(x => x.Reservations);
        

        modelBuilder.Entity<Address>().HasKey(x => x.Id);
        modelBuilder.Entity<BasePrice>().HasKey(x => x.Id);
        modelBuilder.Entity<Card>().HasKey(x => x.Id);
        modelBuilder.Entity<Charging>().HasKey(x => x.Id);
        modelBuilder.Entity<ChargingSpot>().HasKey(x => x.Id);
        modelBuilder.Entity<Client>().HasKey(x => x.Id);
        modelBuilder.Entity<Credentials>().HasKey(x => x.Username);
        modelBuilder.Entity<Manager>().HasKey(x => x.Id);
        modelBuilder.Entity<Place>().HasKey(x => x.Id);
        modelBuilder.Entity<Reservation>().HasKey(x => x.Id);
        modelBuilder.Entity<Station>().HasKey(x => x.Id);
        modelBuilder.Entity<Transaction>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<Vehicle>().HasKey(x => x.Id);
    }
}
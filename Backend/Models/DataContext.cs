using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Parcel> Parcels { get; set; }
    public DbSet<Bag> Bags { get; set; }
    public DbSet<Shipment> Shipments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Parcel>().ToTable("Parcel").HasKey(x => x.Id);
        modelBuilder.Entity<Bag>().ToTable("Bag").HasKey(x => x.Id);
        modelBuilder.Entity<Shipment>().ToTable("Shipment").HasKey(x => x.Id);

        // Parcel configuration
        modelBuilder.Entity<Parcel>()
            .Property(p => p.ParcelNumber)
            .IsRequired();

        modelBuilder.Entity<Parcel>()
            .Property(p => p.Weight)
            .IsRequired();

        modelBuilder.Entity<Parcel>()
            .Property(p => p.Price)
            .IsRequired();

        // Bag configuration
        modelBuilder.Entity<Bag>()
            .Property(b => b.BagNumber)
            .IsRequired();

        modelBuilder.Entity<Bag>()
            .HasMany(b => b.Parcels)
            .WithOne(p => p.Bag)
            .HasForeignKey(p => p.BagId);

        // Shipment configuration
        modelBuilder.Entity<Shipment>()
            .Property(s => s.ShipmentNumber)
            .IsRequired();

        modelBuilder.Entity<Shipment>()
            .Property(s => s.FlightDate)
            .IsRequired();
        modelBuilder.Entity<Shipment>()
            .HasMany(s => s.Bags)              
            .WithOne(b => b.Shipment)           
            .HasForeignKey(b => b.ShipmentId);
        
        // Shipments
        modelBuilder.Entity<Shipment>().HasData(
            new Shipment
            {
                Id = 6,
                ShipmentNumber = "456-728DEF",
                Airport = "RIX",
                FlightNumber = "DE3456",
                FlightDate = DateTime.UtcNow.AddDays(1),
                IsFinalized = true,
                
            },
            new Shipment
            {
                Id = 7,
                ShipmentNumber = "789-367GHI",
                Airport = "HEL",
                FlightNumber = "GH1789",
                FlightDate = DateTime.UtcNow.AddDays(2),
                IsFinalized = false,
                
            },
            new Shipment
            {
                Id = 8,
                ShipmentNumber = "101-112JKL",
                Airport = "TLL",
                FlightNumber = "JK1112",
                FlightDate = DateTime.UtcNow.AddDays(3),
                IsFinalized = false
            },
            new Shipment
            {
                Id = 9,
                ShipmentNumber = "131-415MNO",
                Airport = "RIX",
                FlightNumber = "MN1415",
                FlightDate = DateTime.UtcNow.AddDays(4),
                IsFinalized = false
                
            },
            new Shipment
            {
                Id = 10,
                ShipmentNumber = "161-718PQR",
                Airport = "HEL",
                FlightNumber = "PQ1718",
                FlightDate = DateTime.UtcNow.AddDays(5),
                IsFinalized = true
            }
        );
        for (int i = 11; i <= 15; i++)
        {
            modelBuilder.Entity<Shipment>().HasData(
                new Shipment
                {
                    Id = i,
                    ShipmentNumber = $"{i}8-{i}LXYZ",
                    Airport = "TLL",
                    FlightNumber = $"XY{i}{i}",
                    FlightDate = DateTime.UtcNow.AddDays(i),
                    IsFinalized = false
                }
            );
        }
        
        modelBuilder.Entity<Bag>().HasData(
            new Bag
            {
                Id = 1,
                BagNumber = "B123456",
                BagWeight = 1.5m,
                BagPrice = 10.99m,
                ParcelCount = 1,
                ShipmentId = 6,
                IsLetter = false
            },
            new Bag
            {
                Id = 2,
                BagNumber = "B234567",
                BagWeight = 2.0m,
                BagPrice = 15.99m,
                ParcelCount = 1,
                ShipmentId = 6,
                IsLetter = false
            },
            new Bag
            {
                Id = 3,
                BagNumber = "B345678",
                BagWeight = 1.8m,
                BagPrice = 12.75m,
                ParcelCount = 1,
                ShipmentId = 6,
                IsLetter = false
            },
            new Bag
            {
                Id = 4,
                BagNumber = "B456789",
                LetterCount = 4,
                BagWeight = 2.2m,
                BagPrice = 18.25m,
                ShipmentId = 7,
                IsLetter = true
            },
            new Bag
            {
                Id = 5,
                BagNumber = "B567890",
                LetterCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 7,
                IsLetter = true
            },
            new Bag
            {
                Id = 6,
                BagNumber = "B567891",
                LetterCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 7,
                IsLetter = true
            },
            new Bag
            {
                Id = 7,
                BagNumber = "B567892",
                ParcelCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 8,
                IsLetter = false
            },
            new Bag
            {
                Id = 8,
                BagNumber = "B567893",
                ParcelCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 8,
                IsLetter = false
            },
            new Bag
            {
                Id = 9,
                BagNumber = "B567894",
                ParcelCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 8,
                IsLetter = false
            },
            new Bag
            {
                Id = 10,
                BagNumber = "B567895",
                LetterCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 9,
                IsLetter = true
            },
            new Bag
            {
                Id = 11,
                BagNumber = "B567896",
                LetterCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 9,
                IsLetter = true
            },
            new Bag
            {
                Id = 12,
                BagNumber = "B567897",
                LetterCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 9,
                IsLetter = true
            },
            new Bag
            {
                Id = 13,
                BagNumber = "B567898",
                ParcelCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 10,
                IsLetter = false
            },
            new Bag
            {
                Id = 14,
                BagNumber = "B567899",
                ParcelCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 10,
                IsLetter = false
            },
            new Bag
            {
                Id = 15,
                BagNumber = "B567900",
                ParcelCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 10,
                IsLetter = false
            },
            new Bag
            {
                Id = 16,
                BagNumber = "B567901",
                LetterCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 11,
                IsLetter = true
            },
            new Bag
            {
                Id = 17,
                BagNumber = "B567902",
                LetterCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 11,
                IsLetter = true
            },
            new Bag
            {
                Id = 18,
                BagNumber = "B567903",
                LetterCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 11,
                IsLetter = true
            },
            new Bag
            {
                Id = 19,
                BagNumber = "B567904",
                ParcelCount = 5,
                BagWeight = 2.5m,
                BagPrice = 20.50m,
                ShipmentId = 12,
                IsLetter = false
            },
            new Bag
            {
                Id = 20,
                BagNumber = "B567905",
                ParcelCount = 5,
                BagWeight = 14.5m,
                BagPrice = 20.50m,
                ShipmentId = 12,
                IsLetter = false
            },
            new Bag
            {
                Id = 21,
                BagNumber = "B567906",
                ParcelCount = 5,
                BagWeight = 22.5m,
                BagPrice = 0.50m,
                ShipmentId = 12,
                IsLetter = false
            },
            new Bag
            {
                Id = 22,
                BagNumber = "B567907",
                LetterCount = 5,
                BagWeight = 0.5m,
                BagPrice = 1.50m,
                ShipmentId = 13,
                IsLetter = true
            },
            new Bag
            {
                Id = 23,
                BagNumber = "B567908",
                LetterCount = 5,
                BagWeight = 11.5m,
                BagPrice = 10.50m,
                ShipmentId = 13,
                IsLetter = true
            },
            new Bag
            {
                Id = 24,
                BagNumber = "B567909",
                LetterCount = 5,
                BagWeight = 21.5m,
                BagPrice = 11.50m,
                ShipmentId = 13,
                IsLetter = true
            },
            new Bag
            {
                Id = 25,
                BagNumber = "B567910",
                ParcelCount = 5,
                BagWeight = 8.5m,
                BagPrice = 10.50m,
                ShipmentId = 14,
                IsLetter = false
            },
            new Bag
            {
                Id = 26,
                BagNumber = "B567911",
                ParcelCount = 5,
                BagWeight = 7.5m,
                BagPrice = 22.50m,
                ShipmentId = 14,
                IsLetter = false
            },
            new Bag
            {
                Id = 27,
                BagNumber = "B567912",
                ParcelCount = 5,
                BagWeight = 12.5m,
                BagPrice = 30.50m,
                ShipmentId = 14,
                IsLetter = false
            },
            new Bag
            {
                Id = 28,
                BagNumber = "B567913",
                LetterCount = 5,
                BagWeight = 33.5m,
                BagPrice = 1.50m,
                ShipmentId = 15,
                IsLetter = true
            },
            new Bag
            {
                Id = 29,
                BagNumber = "B567914",
                LetterCount = 5,
                BagWeight = 5.0m,
                BagPrice = 200.50m,
                ShipmentId = 15,
                IsLetter = true
            },
            new Bag
            {
                Id = 30,
                BagNumber = "B567915",
                LetterCount = 5,
                BagWeight = 3.5m,
                BagPrice = 22.50m,
                ShipmentId = 15,
                IsLetter = true
            }
        );
        for (int i = 31; i <= 50; i++)
        {
            modelBuilder.Entity<Bag>().HasData(
                new Bag
                {
                    Id = i,
                    BagNumber = $"B{i}123456",
                    LetterCount = i,
                    BagWeight = 2.0m + (decimal)i / 10,
                    BagPrice = 10.99m + (decimal)i / 5,
                    ShipmentId = i,
                }
            );
        }
        
        modelBuilder.Entity<Parcel>().HasData(
            new Parcel
            {
                Id = 1,
                ParcelNumber = "LL123456LL",
                RecipientName = "John Doe",
                DestinationCountry = "LV",
                Weight = 1.5m,
                Price = 10.99m,
                BagId = 1,
            },
            new Parcel
            {
                Id = 2,
                ParcelNumber = "LL789012LL",
                RecipientName = "Jane Smith",
                DestinationCountry = "LV",
                Weight = 2.0m,
                Price = 15.99m,
                BagId = 1,
            },
            new Parcel
            {
                Id = 3,
                ParcelNumber = "LL345678LL",
                RecipientName = "Alice Johnson",
                DestinationCountry = "FI",
                Weight = 1.2m,
                Price = 12.50m,
                BagId = 1,
            },
            new Parcel
            {
                Id = 4,
                ParcelNumber = "LL901234LL",
                RecipientName = "Bob Wilson",
                DestinationCountry = "FI",
                Weight = 2.3m,
                Price = 18.75m,
                BagId = 2,
            },
            new Parcel
            {
                Id = 5,
                ParcelNumber = "LL567890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "EE",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 2,
            },
            new Parcel
            {
                Id = 6,
                ParcelNumber = "LL567790LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 2,
            },
            new Parcel
            {
                Id = 7,
                ParcelNumber = "LL547890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 3,
            },
            new Parcel
            {
                Id = 8,
                ParcelNumber = "LL567892LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 4,
            },
            new Parcel
            {
                Id = 9,
                ParcelNumber = "LL567893LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 4,
            },
            new Parcel
            {
                Id = 10,
                ParcelNumber = "LL567894LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 4,
            },
            new Parcel
            {
                Id = 11,
                ParcelNumber = "LL567895LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 4,
            },
            new Parcel
            {
                Id = 12,
                ParcelNumber = "LL567896LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 5,
            },
            new Parcel
            {
                Id = 13,
                ParcelNumber = "LL567897LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 5,
            },
            new Parcel
            {
                Id = 14,
                ParcelNumber = "LL567898LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 6,
            },
            new Parcel
            {
                Id = 15,
                ParcelNumber = "LL567899LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 6,
            },
            new Parcel
            {
                Id = 16,
                ParcelNumber = "LL667890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 6,
            },
            new Parcel
            {
                Id = 17,
                ParcelNumber = "LL767890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 7,
            },
            new Parcel
            {
                Id = 18,
                ParcelNumber = "LL867890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 7,
            },
            new Parcel
            {
                Id = 19,
                ParcelNumber = "LL967890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 8,
            },
            new Parcel
            {
                Id = 20,
                ParcelNumber = "LL167890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "EE",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 9,
            },
            new Parcel
            {
                Id = 21,
                ParcelNumber = "LL577890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 9,
            },
            new Parcel
            {
                Id = 22,
                ParcelNumber = "LL587890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 10,
            },
            new Parcel
            {
                Id = 23,
                ParcelNumber = "LL597890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 11,
            },
            new Parcel
            {
                Id = 24,
                ParcelNumber = "LL517890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 12,
            },
            new Parcel
            {
                Id = 25,
                ParcelNumber = "LL527890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 13,
            },
            new Parcel
            {
                Id = 26,
                ParcelNumber = "LL537890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 14,
            },
            new Parcel
            {
                Id = 27,
                ParcelNumber = "LL547890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "EE",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 15,
            },
            new Parcel
            {
                Id = 28,
                ParcelNumber = "LL557890LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 16,
            },
            new Parcel
            {
                Id = 29,
                ParcelNumber = "LL567190LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "FI",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 16,
            },
            new Parcel
            {
                Id = 30,
                ParcelNumber = "LL567290LL",
                RecipientName = "Emma Brown",
                DestinationCountry = "LV",
                Weight = 1.8m,
                Price = 14.25m,
                BagId = 17,
            }
        );
        for (int i = 31; i <= 50; i++)
        {
            modelBuilder.Entity<Parcel>().HasData(
                new Parcel
                {
                    Id = i,
                    ParcelNumber = $"LL{i}{i}{i}LL",
                    RecipientName = $"Recipient {i}",
                    DestinationCountry = "EE",
                    Weight = 1.0m + (decimal)i / 10,
                    Price = 5.99m + (decimal)i / 5,
                    BagId = i,
                }
            );
        }
    }
}
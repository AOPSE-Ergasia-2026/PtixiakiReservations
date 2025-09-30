using Microsoft.AspNetCore.Identity;
using PtixiakiReservations.Data;
using PtixiakiReservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PtixiakiReservations.Seeders;

public class DataSeeder
{
    public static async Task SeedTestDataAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IServiceProvider serviceProvider)
    {
        // Call basic data seeding first
        BasicDataSeed(context, userManager, roleManager);
    
        // Then call the test data seeder
        await TestDataSeeder.SeedTestDataAsync(serviceProvider);
    }
    
    public static void BasicDataSeed(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            SeedUsers(userManager, roleManager).Wait();
        }

        if (!context.City.Any())
        {
            SeedCities(context);
            context.SaveChanges();
        }

        if (!context.EventType.Any())
        {
            SeedEventTypes(context);
            context.SaveChanges();
        }

        if (!context.Venue.Any())
        {
            SeedVenues(context);
            context.SaveChanges();
        }

        if (!context.SubArea.Any())
        {
            SeedSubAreas(context);
            context.SaveChanges();
        }

        if (!context.Event.Any())
        {
            SeedEvents(context);
            context.SaveChanges();
        }

        if (!context.Seat.Any())
        {
            SeedSeats(context);
            context.SaveChanges();
        }
    }

    private static async Task SeedUsers(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        string[] roleNames = { "Admin", "User", "Manager", "Venue" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new ApplicationRole(roleName, $"{roleName} role", DateTime.UtcNow));
            }
        }

        var users = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                UserName = "admin@admin", Email = "admin@admin", FirstName = "Admin", LastName = "User"
            },
            new ApplicationUser
            {
                UserName = "manager@example.com", Email = "manager@example.com", FirstName = "Manager",
                LastName = "User"
            },
            new ApplicationUser
            {
                UserName = "user@example.com", Email = "user@example.com", FirstName = "Normal", LastName = "User"
            },
        };

        foreach (var user in users)
        {
            var existingUser = await userManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
            {
                // Use different password for admin@admin
                var password = user.Email == "admin@admin" ? "admin123" : "Pass123";
                await userManager.CreateAsync(user, password);

                // Assign roles as an example
                if (user.Email.StartsWith("admin"))
                    await userManager.AddToRoleAsync(user, "Admin");
                else if (user.Email.StartsWith("manager"))
                    await userManager.AddToRoleAsync(user, "Manager");
                else
                    await userManager.AddToRoleAsync(user, "User");
            }
        }
    }

    private static void SeedCities(ApplicationDbContext context)
    {
        context.City.AddRange(new List<City>
        {
            new City { Name = "Athens" },
            new City { Name = "Thessaloniki" },
            new City { Name = "Patras" },
            new City { Name = "Heraklion" },
            new City { Name = "Larissa" }
        });
    }

    private static void SeedEventTypes(ApplicationDbContext context)
    {
        context.EventType.AddRange(new List<EventType>
        {
            new EventType { Name = "Concert" },
            new EventType { Name = "Theater" },
            new EventType { Name = "Exhibition" },
            new EventType { Name = "Sports" },
            new EventType { Name = "Conference" }
        });
    }

    private static void SeedVenues(ApplicationDbContext context)
    {
        var firstCity = context.City.First();
        var firstUser = context.Users.First();

        context.Venue.AddRange(new List<Venue>
        {
            new Venue
            {
                Name = "Concert Hall", Address = "123 Main St", CityId = firstCity.Id, PostalCode = "12345",
                Phone = "1234567890", UserId = firstUser.Id, imgUrl = "/images/venue1.jpg"
            },
            new Venue
            {
                Name = "Exhibition Center", Address = "456 Market St", CityId = firstCity.Id, PostalCode = "67890",
                Phone = "0987654321", UserId = firstUser.Id, imgUrl = "/images/venue2.jpg"
            },
        });
    }

    private static void SeedSubAreas(ApplicationDbContext context)
    {
        var venues = context.Venue.ToList();
        var subAreas = new List<SubArea>();

        foreach (var venue in venues)
        {
            subAreas.Add(new SubArea
            {
                AreaName = "Main Hall",
                Width = 500,
                Height = 300,
                Top = 0,
                Left = 0,
                Rotate = 0,
                Desc = "Primary area in venue",
                VenueId = venue.Id
            });

            subAreas.Add(new SubArea
            {
                AreaName = "Balcony",
                Width = 400,
                Height = 150,
                Top = 310,
                Left = 0,
                Rotate = 0,
                Desc = "Balcony area in venue",
                VenueId = venue.Id
            });
        }

        context.SubArea.AddRange(subAreas);
    }

    private static void SeedEvents(ApplicationDbContext context)
    {
        var venues = context.Venue.ToList();
        var types = context.EventType.ToList();
        var subAreas = context.SubArea.ToList();

        // Get subareas for each venue
        var firstVenueSubArea = subAreas.FirstOrDefault(sa => sa.VenueId == venues.First().Id);
        var lastVenueSubArea = subAreas.FirstOrDefault(sa => sa.VenueId == venues.Last().Id);

        context.Event.AddRange(new List<Event>
        {
            new Event
            {
                Name = "Rock concert 2024",
                StartDateTime = DateTime.Now.AddMonths(1),
                EndTime = DateTime.Now.AddMonths(1).AddHours(4),
                EventTypeId = types.First().Id,
                VenueId = venues.First().Id,
                SubAreaId = firstVenueSubArea?.Id
            },
            new Event
            {
                Name = "Art Exhibition",
                StartDateTime = DateTime.Now.AddMonths(2),
                EndTime = DateTime.Now.AddMonths(2).AddHours(5),
                EventTypeId = types.Last().Id,
                VenueId = venues.Last().Id,
                SubAreaId = lastVenueSubArea?.Id
            }
        });
    }

    private static void SeedSeats(ApplicationDbContext context)
    {
        var subAreas = context.SubArea.ToList();
        var seats = new List<Seat>();

        foreach (var area in subAreas)
        {
            // Define proper spacing between seats
            var seatSpacingX = 70m; // Horizontal spacing between seats
            var seatSpacingY = 75m; // Vertical spacing between rows
            var startX = 30m; // Starting X position
            var startY = 30m; // Starting Y position

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    seats.Add(new Seat
                    {
                        Name = $"{(char)('A' + row)}{col + 1:D2}",
                        X = startX + (col * seatSpacingX),
                        Y = startY + (row * seatSpacingY),
                        SubAreaId = area.Id,
                        Available = true,
                    });
                }
            }
        }

        context.Seat.AddRange(seats);
    }
}
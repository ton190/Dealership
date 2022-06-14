using Domain.Enums;
using Domain.Services;
using EntityLibrary;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.Configurations;

public static class DbExtensions
{
    public static ModelBuilder SeedDefaultData(this ModelBuilder builder)
    {
        builder.Entity<User>().HasData(new[]
        {
            new
            {
                Id = 1,
                Role = "admin",
                Email = "admin@gmail.com",
                Password = "wbX09MDJ2G14WACPkNRj9xF8b0hSEWo0iJVxGm35Aok=",
                PasswordHash = "UrWyueVddmB7FU4J6Tv76tpqc",
                DateCreated = Time.Now,
                DateModified = Time.Now,
            }
        });
        builder.Entity<Brand>().HasData(new[]
        {
            new
            {
                Id = 1,
                Name = "Lexus",
                DateCreated = Time.Now,
                DateModified = Time.Now
            },
            new
            {
                Id = 2,
                Name = "BMW",
                DateCreated = Time.Now,
                DateModified = Time.Now
            }
        });

        builder.Entity<Record>(x =>
         {
             for (int i = 1; i < 100; i++)
             {
                 x.HasData(new[]
                {
                new
                {
                    Id = i,
                    BusinessName = "Poganka Studio "+ i ,
                    FINCode = "123456",
                    Brand = "Lexus",
                    DateCreated = Time.Now,
                    DateModified = Time.Now
                }
             });
                 x.OwnsOne(x => x.BusinessAddress).HasData(new
                 {
                     RecordId = i,
                     Province = ProvinceEnum.ON,
                     City = "Toronto",
                     PostalCode = "M2J4Y2",
                     StreetName = "Bond Lake Park Street",
                     BuildingNumber = "17",
                     UnitNumber = ""
                 });
                 x.OwnsMany(x => x.ContactNames).HasData(new[]
                {
                    new
                    {
                        Id = i,
                        RecordId = i,
                        FirstName = "Anton",
                        LastName = "Arlazarov",
                        DateCreated = Time.Now,
                        DateModified = Time.Now
                    },
                    new
                    {
                        Id = i+1,
                        RecordId = i,
                        FirstName = "Alina",
                        LastName = "Sagaliyeva",
                        DateCreated = Time.Now,
                        DateModified = Time.Now
                    }
                 });
                 x.OwnsMany(x => x.PhoneNumbers).HasData(new[]
                {
                    new
                    {
                        Id = i,
                        RecordId = i,
                        Number = "4054318541",
                        DateCreated = Time.Now,
                        DateModified = Time.Now
                    }
                 });
             };
         });

        return builder;
    }
}

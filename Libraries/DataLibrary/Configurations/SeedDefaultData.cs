using Domain.Enums;
using Domain.Services;
using EntityLibrary.CarRecords;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.Configurations;

public static class DbExtensions
{
    public static ModelBuilder SeedDefaultData(this ModelBuilder builder)
    {
        builder.Entity<CarBrand>().HasData(new[]
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

        builder.Entity<CarRecord>(x =>
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
                    CarBrand = "Lexus",
                    DateCreated = Time.Now,
                    DateModified = Time.Now
                }
             });
                 x.OwnsOne(x => x.BusinessAddress).HasData(new
                 {
                     CarRecordId = i,
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
                        CarRecordId = i,
                        FirstName = "Anton",
                        LastName = "Arlazarov",
                        DateCreated = Time.Now,
                        DateModified = Time.Now
                    },
                    new
                    {
                        Id = i+1,
                        CarRecordId = i,
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
                        CarRecordId = i,
                        Number = "4054318541",
                        DateCreated = Time.Now,
                        DateModified = Time.Now
                    }
                 });
             };
         });

        builder.Entity<CarRecordOrder>(x =>
        {
            for (int i = 1; i < 100; i++)
            {
                x.HasData(new[]
                {
                    new{
                        Id = i,
                        Email = $"test_email{i}@test.com",
                        Paid = false,
                        SearchResult = "",
                        RecordSearch = "",
                        DateCreated = Time.Now,
                        DateModified = Time.Now
                        }
                });
            };
        });

        return builder;
    }
}

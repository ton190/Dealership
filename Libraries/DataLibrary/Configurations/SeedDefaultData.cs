using Domain.Enums;
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
                Name = "Lexus"
            },
            new
            {
                Id = 2,
                Name = "BMW"
            }
        });

        builder.Entity<CarRecord>(x =>
        {
            x.HasData(new[]
            {
                new
                {
                    Id = 1,
                    BusinessName = "Poganka Studio",
                    FINCode = "123456",
                    CarBrand = "Lexus",
                }
            });
            x.OwnsOne(x => x.BusinessAddress).HasData(new
            {
                Id = 1,
                CarRecordId = 1,
                Province = ProvinceEnum.ON,
                City = "Toronto",
                PostalCode = "M2J4Y2",
                StreetName = "Bond Lake Park Street",
                BuildingNumber = "17",
                UnitNumber =""
            });
            x.OwnsMany(x => x.ContactNames).HasData(new[]
            {
                new
                {
                    Id = 1,
                    CarRecordId = 1,
                    FirstName = "Anton",
                    LastName = "Arlazarov"
                },
                new
                {
                    Id = 2,
                    CarRecordId = 1,
                    FirstName = "Alina",
                    LastName = "Sagaliyeva"
                }
            });
            x.OwnsMany(x => x.PhoneNumbers).HasData(new[]
            {
                new
                {
                    Id = 1,
                    CarRecordId = 1,
                    Number = "4054318541"
                }
            });
        });

        return builder;
    }
}

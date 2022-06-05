using EntityLibrary.CarRecords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CarRecordConfiguration : IEntityTypeConfiguration<CarRecord>
{
    public void Configure(EntityTypeBuilder<CarRecord> builder)
    {
        builder.Property(x => x.BusinessName).IsRequired();

        builder.OwnsMany(x => x.PhoneNumbers, y =>
        {
            y.Property( z => z.Number).IsRequired();
        });
        builder.OwnsMany(x => x.ContactNames, y =>
        {
            y.Property(z => z.FirstName).IsRequired();
            y.Property(z => z.LastName).IsRequired();
        });

        builder.OwnsOne<Address>(x => x.BusinessAddress);
    }

}

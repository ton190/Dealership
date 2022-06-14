using EntityLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class RecordConfiguration : IEntityTypeConfiguration<Record>
{
    public void Configure(EntityTypeBuilder<Record> builder)
    {
        builder.Property(x => x.BusinessName).IsRequired();
        builder.OwnsMany(x => x.PhoneNumbers, y =>
        {
            y.Property(z => z.Number).IsRequired();
        });
        builder.OwnsMany(x => x.ContactNames, y =>
        {
            y.Property(z => z.FirstName).IsRequired();
            y.Property(z => z.LastName).IsRequired();
        });

        builder.OwnsOne<Address>(x => x.BusinessAddress);
    }

}

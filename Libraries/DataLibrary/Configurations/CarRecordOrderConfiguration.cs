using EntityLibrary.CarRecords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CarRecordOrderConfiguration
    : IEntityTypeConfiguration<CarRecordOrder>
{
    public void Configure(EntityTypeBuilder<CarRecordOrder> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1000, 1);
    }
}

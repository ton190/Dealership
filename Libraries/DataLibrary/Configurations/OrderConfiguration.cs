using EntityLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class OrderConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.SessionId).IsRequired();
        builder.Property(x => x.RecordSearch).IsRequired();
        builder.Property(x => x.SearchResult).IsRequired();
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1000, 1);
    }
}

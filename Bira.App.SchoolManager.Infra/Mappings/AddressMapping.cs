using Bira.App.SchoolManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bira.App.SchoolManager.Infra.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Code);

            builder.Property(a => a.Street)
                .IsRequired();

            builder.Property(a => a.Number)
                .IsRequired();

            builder.Property(a => a.Complement)
                .IsRequired();

            builder.Property(a => a.Neighborhood)
                .IsRequired();

            builder.Property(a => a.City)
                .IsRequired();

            builder.Property(a => a.State)
                .IsRequired();

            builder.Property(a => a.ZipCode)
                .IsRequired();

            builder.Property(x => x.DateRegister)
                .IsRequired();

            builder.Property(x => x.DataUpdate);

            builder.Property(x => x.DateDeactivation);

            builder.ToTable("Enderecos");
        }
    }
}
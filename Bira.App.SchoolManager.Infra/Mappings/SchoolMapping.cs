using Bira.App.SchoolManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bira.App.SchoolManager.Infra.Mappings
{
    public class SchoolMapping : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasKey(a => a.Code);

            builder.Property(a => a.Description)
                .IsRequired();

            builder.Property(x => x.DateRegister)
                .IsRequired();

            builder.Property(x => x.DataUpdate);

            builder.Property(x => x.DateDeactivation);

            builder.ToTable("Escolas");
        }
    }
}
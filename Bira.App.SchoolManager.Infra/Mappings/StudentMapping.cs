using Bira.App.SchoolManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bira.App.SchoolManager.Infra.Mappings
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(a => a.Code);

            builder.Property(a => a.Name)
                .IsRequired();

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.Property(a => a.CPF)
                .IsRequired();

            builder.Property(a => a.CellPhone)
                .IsRequired();

            builder.Property(a => a.CodeSchool)
                .IsRequired();

            builder.Property(a => a.CodeAddress)
                .IsRequired();

            builder.Property(x => x.DateRegister)
                .IsRequired();

            builder.Property(x => x.DataUpdate);

            builder.Property(x => x.DateDeactivation);

            builder.HasOne(x => x.School)
                   .WithMany(x => x.Students)
                   .HasForeignKey(x => x.CodeSchool)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Address)
                   .WithMany(x => x.Students)
                   .HasForeignKey(x => x.CodeAddress)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Alunos");
        }
    }
}
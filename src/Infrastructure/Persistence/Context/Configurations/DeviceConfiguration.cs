namespace DeviceManager.Infrastructure.Persistence.Context.Configurations
{
    using DeviceManager.Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class DeviceConfiguration : BaseAuditableEntityConfiguration<Device, long>
    {
        public override void Configure(EntityTypeBuilder<Device> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.Brand)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}

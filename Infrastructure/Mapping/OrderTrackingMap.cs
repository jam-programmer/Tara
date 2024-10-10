namespace Infrastructure.Mapping
{
    internal sealed class OrderTrackingMap : IEntityTypeConfiguration<OrderTrackingEntity>
    {
        public void Configure(EntityTypeBuilder<OrderTrackingEntity> builder)
        {
            builder.ToTable("OrderTracking");

            builder.HasOne(o => o.Order)
                .WithOne(o => o.OrderTracking)
                .HasForeignKey<OrderTrackingEntity>(f => f.OrderId);
        }
    }
}

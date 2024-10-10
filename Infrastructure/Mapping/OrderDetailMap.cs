namespace Infrastructure.Mapping;

internal class OrderDetailMap : IEntityTypeConfiguration<OrderDetailEntity>
{
    public void Configure(EntityTypeBuilder<OrderDetailEntity> builder)
    {
        builder.ToTable("OrderDetail");
    }
}

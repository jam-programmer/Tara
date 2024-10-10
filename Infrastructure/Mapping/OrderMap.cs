namespace Infrastructure.Mapping;

internal class OrderMap : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Order");
        builder.HasMany(m => m.Details)
            .WithOne(o => o.order)
            .HasForeignKey(f => f.OrderId);

        builder.Property(p => p.InvoiceNumber).
            HasDefaultValueSql("NEXT VALUE FOR CodeGenerator");

    }
}

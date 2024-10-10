namespace Infrastructure.Mapping;

internal class TaraPurchaseMAp : IEntityTypeConfiguration<TaraPurchaseEntity>
{
    public void Configure(EntityTypeBuilder<TaraPurchaseEntity> builder)
    {
        builder.ToTable("TaraPurchase");
        builder.HasOne(o => o.Order)
            .WithMany(m => m.TaraPurchases)
            .HasForeignKey(o => o.OrderId);
    }
}

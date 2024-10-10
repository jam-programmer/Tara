

namespace Infrastructure.Mapping;

internal class MerchantAccessMap : IEntityTypeConfiguration<MerchantAccessEntity>
{
    public void Configure(EntityTypeBuilder<MerchantAccessEntity> builder)
    {
        builder.ToTable("MerchantAccess");

       builder.Property(p=>p.terminalCode).IsRequired();    
       builder.Property(p=>p.terminalTitle).IsRequired();    
       builder.Property(p=>p.accessCode).IsRequired();    
       builder.Property(p=>p.merchantCode).IsRequired();

        builder.HasIndex(index => index.terminalCode);
        builder.HasIndex(index => index.merchantCode);
    }
}

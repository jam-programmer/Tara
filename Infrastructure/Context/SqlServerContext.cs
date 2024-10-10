namespace Infrastructure.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext
            (DbContextOptions<SqlServerContext> options) 
            : base(options) 
        {
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<long>("CodeGenerator")
               .StartsAt(100).IncrementsBy(2).HasMax(long.MaxValue);
            modelBuilder.AppendDbSetOfEntity();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

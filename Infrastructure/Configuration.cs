
namespace Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection Infrastructure
            (this IServiceCollection service, IConfiguration  configuration)
        {
            service.AddDbContext<SqlServerContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TaraConnection"));
            });
            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            service.AddScoped<IApiService, ApiService>();
            
            return service;

        }
    }
}



namespace Application
{
    public static class Configuration
    {
        public static IServiceCollection Application
            (this IServiceCollection service, IConfiguration configuration)
        {
            #region AddSerilog
            var columnOption = new ColumnOptions();
            columnOption.Store.Remove(StandardColumn.Properties);
            columnOption.Store.Remove(StandardColumn.MessageTemplate);
            columnOption.AdditionalColumns = new Collection<SqlColumn>()
            {
                new SqlColumn()
                {
                    AllowNull = true,
                    DataType=System.Data.SqlDbType.NVarChar,
                    DataLength=900,
                    ColumnName="Source",
                    PropertyName="SourceContext"
                },
                new SqlColumn()
                {
                    AllowNull = true,
                    DataType=System.Data.SqlDbType.NVarChar,
                    DataLength=900,
                    ColumnName="RequestPath",
                    PropertyName="RequestPath"
                }
            };

            Log.Logger = new LoggerConfiguration()
              .WriteTo.MSSqlServer(
              connectionString: configuration.GetConnectionString("TaraConnection")
              , sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
              {
                  TableName = "LogSystem",
                  AutoCreateSqlTable = true
              }, columnOptions: columnOption
                  ).MinimumLevel.Warning().CreateLogger();


            service.AddSerilog();
            #endregion


            service.AddScoped<ITaraWebService,TaraWebService>();
            service.AddScoped<IPos,Pos>();

            return service;

        }
    }
}

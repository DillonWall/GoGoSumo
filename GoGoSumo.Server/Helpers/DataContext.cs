using GoGoSumo.Server.Helpers.Exceptions;
using Npgsql;
using System.Data;

namespace GoGoSumo.Server.Helpers;

public class DataContext
{
    private readonly ILogger<DataContext> _logger;
    private string _pgConnectionString;

    public DataContext(IConfiguration configuration, ILogger<DataContext> logger)
    {
        string? pgConnStr = configuration.GetConnectionString("PostgresConnection");
        if (pgConnStr == null) throw new AppException("'PostgresConnection' Connection string does not exist in appsettings.");
        _pgConnectionString = pgConnStr;
        _logger = logger;
    }

    public IDbConnection CreatePostgresConnection()
    {
        return new NpgsqlConnection(_pgConnectionString);
    }

    public SqlRunner WithSql(string sql)
    {
        return new SqlRunner(this, _logger, sql);
    }
}
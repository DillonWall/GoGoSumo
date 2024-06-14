using GoGoSumo.Server.Helpers.Exceptions;
using Npgsql;
using System.Data;

namespace GoGoSumo.Server.Helpers;

public class DataContext
{
    private string _pgConnectionString;

    public DataContext(IConfiguration configuration)
    {
        string? pgConnStr = configuration.GetConnectionString("PostgresConnection");
        if (pgConnStr == null) throw new AppException("'PostgresConnection' Connection string does not exist in appsettings.");
        _pgConnectionString = pgConnStr;
    }

    public IDbConnection CreatePostgresConnection()
    {
        return new NpgsqlConnection(_pgConnectionString);
    }
}
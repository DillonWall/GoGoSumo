using Dapper;
using System.Data;

namespace GoGoSumo.Server.Helpers;

public class SqlRunner
{
    private readonly DataContext _context;
    private readonly ILogger _logger;
    private readonly string _sql;
    private object? _param;

    public SqlRunner(DataContext context, ILogger logger, string sql)
    {
        _context = context;
        _logger = logger;
        _sql = sql;
    }

    public SqlRunner WithParams(Object? param = null)
    {
        _param = param;
        return this;
    }

    public async Task<IEnumerable<Entity>> QueryMultipleAsync<Entity>()
    {
        using var connection = _context.CreatePostgresConnection();
        return await connection.QueryAsync<Entity>(_sql);
    }

    public async Task<Entity?> QuerySingleAsync<Entity>()
    {
        using var connection = _context.CreatePostgresConnection();
        return await connection.QuerySingleOrDefaultAsync<Entity?>(_sql, _param);
    }

    public async Task ExecuteAsync()
    {
        using IDbConnection connection = _context.CreatePostgresConnection();
        using IDbTransaction transaction = connection.BeginTransaction();

        try
        {
            await connection.ExecuteAsync(_sql, _param);
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            const string message = "Could not execute sql: {} with object parameter: {}";
            _logger.LogError(ex, message, [_sql, _param]);
        }
    }
}

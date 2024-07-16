using Dapper;
using GoGoSumo.DTOs.Entities;
using GoGoSumo.Server.Helpers;
using Humanizer;

namespace GoGoSumo.Server.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<EventEntity>> GetAll();
    Task<EventEntity?> GetById(int id);
    Task<int> Create(EventEntity entity);
    Task Update(EventEntity entity);
    Task Delete(int id);
}

public class EventRepository : IEventRepository
{
    private readonly DataContext _context;
    private static readonly string SELECT_ALL = """
            SELECT *
            FROM events
        """;

    public EventRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventEntity>> GetAll()
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = SELECT_ALL;

        return await connection.QueryAsync<EventEntity>(sql);
    }

    public async Task<EventEntity?> GetById(int id)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            {0}
            WHERE event_id = @id
        """.FormatWith(SELECT_ALL);
        return await connection.QuerySingleOrDefaultAsync<EventEntity?>(sql, new { id });
    }

    public async Task<int> Create(EventEntity entity)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
                INSERT INTO events (event_name, event_date, event_location, event_gogo_price_yen)
                VALUES (@EventName, @EventDate, @EventLocation, @EventGoGoPrice)
                RETURNING event_id;
            """;
        return await connection.ExecuteScalarAsync<int>(sql, entity);
    }

    public async Task Update(EventEntity entity)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
                UPDATE events
                SET
                    event_name = COALESCE(@EventName, event_name),
                    event_date = COALESCE(@EventDate, event_date),
                    event_location = COALESCE(@EventLocation, event_location),
                    event_gogo_price_yen = COALESCE(@EventGoGoPrice, event_gogo_price_yen)
                WHERE event_id = @EventId
            """;
        await connection.ExecuteAsync(sql, entity);
    }

    public async Task Delete(int id)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            DELETE FROM events 
            WHERE event_id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}
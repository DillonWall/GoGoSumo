using Dapper;
using GoGoSumo.Server.DTOs.Entities;
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
    private DataContext _context;
    public static string SELECT_ALL = """
            SELECT 
                event_id AS EventId,
                event_name AS EventName
                event_date AS EventDate
                event_location AS EventLocation
                event_gogo_price_yen AS EventGoGoPrice
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
                OUTPUT INSERTED.event_id
                VALUES (@EventName, @EventDate, @EventLocation, @EventGoGoPrice);
            """;
        return await connection.ExecuteScalarAsync<int>(sql, entity);
    }

    public async Task Update(EventEntity entity)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
                UPDATE events
                SET
                    event_name = @EventName,
                    event_date = @EventDate,
                    event_location = @EventLocation,
                    event_gogo_price_yen = @EventGoGoPrice,
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
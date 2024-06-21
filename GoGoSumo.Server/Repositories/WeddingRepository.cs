using Dapper;
using GoGoSumo.Server.DTOs.Entities;
using GoGoSumo.Server.Helpers;
using Humanizer;
using System.Data;

namespace GoGoSumo.Server.Repositories;

public interface IWeddingRepository
{
    Task<IEnumerable<WeddingEntity>> GetAll();
    Task<WeddingEntity?> GetById(int id);
    Task Create(WeddingEntity wedding);
    Task Update(WeddingEntity wedding);
    Task Delete(int id);
}

public class WeddingRepository : IWeddingRepository
{
    private readonly DataContext _context;
    private readonly IEventRepository _eventRepository;
    private static readonly string SELECT_ALL = """
            SELECT *
            FROM weddings w
            LEFT JOIN events e ON w.event_id = e.event_id
        """;
    private static readonly Func<WeddingEntity, EventEntity, WeddingEntity> map = (we, ee) =>
    {
        we.Event = ee;
        return we;
    };

    public WeddingRepository(DataContext context, IEventRepository eventRepository)
    {
        _context = context;
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<WeddingEntity>> GetAll()
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = SELECT_ALL;

        return await connection.QueryAsync<WeddingEntity, EventEntity, WeddingEntity>(sql, map, splitOn: "event_id");
    }

    public async Task<WeddingEntity?> GetById(int id)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            {0}
            WHERE wedding_id = {1}
        """.FormatWith(SELECT_ALL, id);
        return (await connection.QueryAsync<WeddingEntity, EventEntity, WeddingEntity>(sql, map, splitOn: "event_id")).FirstOrDefault();
    }

    public async Task Create(WeddingEntity entity)
    {
        using var connection = _context.CreatePostgresConnection();

        int eventId = await _eventRepository.Create(entity.Event!);
        var sql = """
            INSERT INTO weddings (event_id, wedding_bride_name, wedding_groom_name, wedding_budget_yen, wedding_planner_id)
            VALUES ({0}, @WeddingBrideName, @WeddingGroomName, @WeddingBudget, @WeddingPlannerId);
        """.FormatWith(eventId);
        await connection.ExecuteAsync(sql, entity);
    }

    private Task<int> GetEventIdAsnc(IDbConnection connection, WeddingEntity entity)
    {
        var sql = """
            SELECT event_id
            FROM weddings
            WHERE wedding_id = @WeddingId;
        """;
        return connection.ExecuteScalarAsync<int>(sql, entity);
    }

    public async Task Update(WeddingEntity entity)
    {
        using var connection = _context.CreatePostgresConnection();

        var sql = """
            UPDATE weddings
            SET
                wedding_bride_name = COALESCE(@WeddingBrideName, wedding_bride_name), 
                wedding_groom_name = COALESCE(@WeddingGroomName, wedding_groom_name), 
                wedding_budget_yen = COALESCE(@WeddingBudget, wedding_budget_yen), 
                wedding_planner_id = COALESCE(@WeddingPlannerId, wedding_planner_id)
            WHERE wedding_id = @WeddingId
        """;
        await connection.ExecuteAsync(sql, entity);

        if (entity.Event == null)
            entity.Event = new EventEntity();
        entity.Event.EventId = await GetEventIdAsnc(connection, entity);
        await _eventRepository.Update(entity.Event);
    }

    public async Task Delete(int id)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            DELETE FROM events 
            WHERE event_id = (
                SELECT w.event_id
                FROM weddings w
                WHERE w.wedding_id = @id
            );
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}
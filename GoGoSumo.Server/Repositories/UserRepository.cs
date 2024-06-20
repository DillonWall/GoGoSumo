using Dapper;
using GoGoSumo.Server.Helpers;
using GoGoSumo.Server.Models;
using Humanizer;

namespace GoGoSumo.Server.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetAll();
    Task<UserEntity?> GetById(string clerk_id);
    Task Create(UserEntity user);
    Task Update(UserEntity user);
    Task Delete(string clerk_id);
}

public class UserRepository : IUserRepository
{
    private DataContext _context;
    public static string SELECT_ALL = """
            SELECT 
                u.clerk_id AS ClerkId,
                u.phone AS Phone,
                u.fluent_languages AS FluentLanguages,
                r.role_name AS Role
            FROM users u
            JOIN roles r ON r.role_id = u.role_id
        """;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserEntity>> GetAll()
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = SELECT_ALL;

        return await connection.QueryAsync<UserEntity>(sql);
    }

    public async Task<UserEntity?> GetById(string clerk_id)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            {0}
            WHERE clerk_id = @clerk_id
        """.FormatWith(SELECT_ALL);
        return await connection.QuerySingleOrDefaultAsync<UserEntity?>(sql, new { clerk_id });
    }

    public async Task Create(UserEntity entity)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            INSERT INTO users (clerk_id, phone, fluent_languages, role_id)
            VALUES (@ClerkId, @Phone, @FluentLanguages, (
                SELECT r.role_id
                FROM roles r
                WHERE r.role_name = @Role
            ));
        """;
        await connection.ExecuteAsync(sql, entity);
    }

    public async Task Update(UserEntity entity)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            UPDATE users
            SET phone = @Phone,
                fluent_languages = @FluentLanguages,
                role_id = r.role_id
            FROM (
                SELECT role_id
                FROM roles
                WHERE role_name = @Role
            ) AS r
            WHERE clerk_id = @ClerkId
        """;
        await connection.ExecuteAsync(sql, entity);
    }

    public async Task Delete(string clerk_id)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            DELETE FROM users 
            WHERE clerk_id = @clerk_id
        """;
        await connection.ExecuteAsync(sql, new { clerk_id });
    }
}
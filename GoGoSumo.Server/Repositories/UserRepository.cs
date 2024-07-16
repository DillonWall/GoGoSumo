using Dapper;
using GoGoSumo.DTOs.Entities;
using GoGoSumo.Server.Helpers;
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
    private readonly DataContext _context;
    private static readonly string SELECT_ALL = """
            SELECT *
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
            INSERT INTO users
            VALUES (@ClerkId, @UserPhone, @UserFluentLanguages, (
                SELECT r.role_id
                FROM roles r
                WHERE r.role_name = @RoleName
            ));
        """;
        await connection.ExecuteAsync(sql, entity);
    }

    public async Task Update(UserEntity entity)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            UPDATE users
            SET user_phone = COALESCE(@UserPhone, user_phone),
                user_fluent_languages = COALESCE(@UserFluentLanguages, user_fluent_languages),
                role_id = COALESCE(r.role_id, users.role_id)
            FROM (
                SELECT role_id
                FROM roles 
                WHERE role_name = @RoleName
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
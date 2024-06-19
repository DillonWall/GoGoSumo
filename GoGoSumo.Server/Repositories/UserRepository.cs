using Dapper;
using GoGoSumo.Server.Helpers;
using GoGoSumo.Server.Models;

namespace GoGoSumo.Server.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserDto?> GetById(string clerk_id);
    Task Create(UserDto user);
    Task Update(UserDto user);
    Task Delete(string clerk_id);
}

public class UserRepository : IUserRepository
{
    private DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            SELECT * FROM users
        """;
        return await connection.QueryAsync<UserDto>(sql);
    }

    public async Task<UserDto?> GetById(string clerk_id)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            SELECT * FROM users 
            WHERE clerk_id = @clerk_id
        """;
        return await connection.QuerySingleOrDefaultAsync<UserDto?>(sql, new { clerk_id });
    }

    public async Task Create(UserDto dto)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            INSERT INTO users (clerk_id, phone, fluent_languages, role)
            VALUES (@ClerkId, @Phone, @FluentLanguages, @Role)
        """;
        await connection.ExecuteAsync(sql, dto);
    }

    public async Task Update(UserDto dto)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            UPDATE users 
            SET phone = @Phone,
                fluent_languages = @FluentLanguages,
                role = @Role
            WHERE clerk_id = @ClerkId
        """;
        await connection.ExecuteAsync(sql, dto);
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
using Dapper;
using GoGoSumo.Server.Helpers;
using GoGoSumo.Server.Models;

namespace GoGoSumo.Server.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserDto?> GetById(int id);
    Task<UserDto?> GetByEmail(string email);
    Task Create(UserDto user);
    Task Update(UserDto user);
    Task Delete(int id);
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

    public async Task<UserDto?> GetById(int id)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            SELECT * FROM users 
            WHERE id = @id
        """;
        return await connection.QuerySingleOrDefaultAsync<UserDto?>(sql, new { id });
    }

    public async Task<UserDto?> GetByEmail(string email)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            SELECT * FROM users 
            WHERE email = @email
        """;
        return await connection.QuerySingleOrDefaultAsync<UserDto?>(sql, new { email });
    }

    public async Task Create(UserDto dto)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            INSERT INTO users (name, email, phone, password_hash, fluent_languages, role)
            VALUES (@Name, @Email, @Phone, @PasswordHash, @FluentLanguages, @Role)
        """;
        await connection.ExecuteAsync(sql, dto);
    }

    public async Task Update(UserDto dto)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            UPDATE users 
            SET name = @Name,
                email = @Email,
                phone = @Phone,
                password_hash = @PasswordHash,
                fluent_languages = @FluentLanguages,
                role = @Role
            WHERE id = @Id
        """;
        await connection.ExecuteAsync(sql, dto);
    }

    public async Task Delete(int id)
    {
        using var connection = _context.CreatePostgresConnection();
        var sql = """
            DELETE FROM users 
            WHERE id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}
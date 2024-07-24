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
    private const string SELECT_ALL_SQL = @"
            SELECT *
            FROM users u
            JOIN roles r ON r.role_id = u.role_id";

    private readonly DataContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(DataContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<UserEntity>> GetAll()
    {
        string sql = SELECT_ALL_SQL;

        return await _context.WithSql(sql).QueryMultipleAsync<UserEntity>();
    }

    public async Task<UserEntity?> GetById(string clerk_id)
    {
        string sql = @"
            {0}
            WHERE clerk_id = @clerk_id;
        ".FormatWith(SELECT_ALL_SQL);

        return await _context.WithSql(sql).WithParams(new { clerk_id }).QuerySingleAsync<UserEntity>();
    }

    public async Task Create(UserEntity entity)
    {
        string sql = @"
            INSERT INTO users
            VALUES (@ClerkId, @UserPhone, @UserFluentLanguages, (
                SELECT r.role_id
                FROM roles r
                WHERE r.role_name = @RoleName
            ));
        ";

        await _context.WithSql(sql).WithParams(entity).ExecuteAsync();
    }

    public async Task Update(UserEntity entity)
    {
        string sql = @"
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
        ";

        await _context.WithSql(sql).WithParams(entity).ExecuteAsync();
    }

    public async Task Delete(string clerk_id)
    {
        string sql = @"
            DELETE FROM users 
            WHERE clerk_id = @clerk_id
        ";

        await _context.WithSql(sql).WithParams(new { clerk_id }).ExecuteAsync();
    }
}
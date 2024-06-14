using AutoMapper;
using GoGoSumo.Server.Helpers.Exceptions;
using GoGoSumo.Server.Models;
using GoGoSumo.Server.Models.ApiModels;
using GoGoSumo.Server.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace GoGoSumo.Server.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserDto> GetById(int id);
    Task Create(UserModel model);
    Task Update(int id, UserModel model);
    Task Delete(int id);
}

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    public async Task<UserDto> GetById(int id)
    {
        UserDto? dto = await _userRepository.GetById(id);

        if (dto == null)
            throw new KeyNotFoundException("User not found");

        return dto;
    }

    public async Task Create(UserModel model)
    {
        // validate
        if (await _userRepository.GetByEmail(model.Email!) != null)
            throw new ValidationException("User with the email '" + model.Email + "' already exists");

        // map
        UserDto dto = _mapper.Map<UserDto>(model);

        // modify (hash password)
        dto.PasswordHash = BC.HashPassword(model.Password);

        // save user
        await _userRepository.Create(dto);
    }

    public async Task Update(int id, UserModel model)
    {
        // validate
        UserDto? dto = await _userRepository.GetById(id);

        if (dto == null)
            throw new KeyNotFoundException("User not found");

        var emailChanged = !string.IsNullOrEmpty(model.Email) && dto.Email != model.Email;
        if (emailChanged && await _userRepository.GetByEmail(model.Email!) != null)
            throw new ValidationException("User with the email '" + model.Email + "' already exists");

        // copy props to dto
        _mapper.Map(model, dto);

        // modify (hash password if it was entered)
        if (!string.IsNullOrEmpty(model.Password))
            dto.PasswordHash = BC.HashPassword(model.Password);

        // save
        await _userRepository.Update(dto);
    }

    public async Task Delete(int id)
    {
        await _userRepository.Delete(id);
    }
}
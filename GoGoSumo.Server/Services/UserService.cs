using AutoMapper;
using GoGoSumo.Server.Helpers.Exceptions;
using GoGoSumo.Server.Models;
using GoGoSumo.Server.Models.ApiModels;
using GoGoSumo.Server.Repositories;

namespace GoGoSumo.Server.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserDto> GetById(string clerk_id);
    Task Create(UserModel model);
    Task Update(string clerk_id, UserModel model);
    Task Delete(string clerk_id);
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

    public async Task<UserDto> GetById(string clerk_id)
    {
        UserDto? dto = await _userRepository.GetById(clerk_id);

        if (dto == null)
            throw new KeyNotFoundException("User not found");

        return dto;
    }

    public async Task Create(UserModel model)
    {
        // validate
        if (await _userRepository.GetById(model.ClerkId!) != null)
            throw new ValidationException("User with the Clerk user_id '" + model.ClerkId + "' already exists");

        // map
        UserDto dto = _mapper.Map<UserDto>(model);

        // save user
        await _userRepository.Create(dto);
    }

    public async Task Update(string clerk_id, UserModel model)
    {
        // validate
        UserDto? dto = await _userRepository.GetById(clerk_id);

        if (dto == null)
            throw new KeyNotFoundException("User not found");

        if (await _userRepository.GetById(model.ClerkId!) != null)
            throw new ValidationException("User with the Clerk user_id '" + model.ClerkId + "' already exists");

        // copy props to dto
        _mapper.Map(model, dto);

        // save
        await _userRepository.Update(dto);
    }

    public async Task Delete(string clerk_id)
    {
        await _userRepository.Delete(clerk_id);
    }
}
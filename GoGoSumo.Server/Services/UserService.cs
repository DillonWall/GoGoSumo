using AutoMapper;
using GoGoSumo.Server.DTOs.Entities;
using GoGoSumo.Server.DTOs.Models.User;
using GoGoSumo.Server.Helpers.Exceptions;
using GoGoSumo.Server.Repositories;

namespace GoGoSumo.Server.Services;

public interface IUserService
{
    Task<IEnumerable<UserEntity>> GetAll();
    Task<UserEntity> GetById(string clerk_id);
    Task Create(UserCreateModel model);
    Task Update(string clerk_id, UserUpdateModel model);
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

    public async Task<IEnumerable<UserEntity>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    public async Task<UserEntity> GetById(string clerk_id)
    {
        UserEntity? entity = await _userRepository.GetById(clerk_id);

        if (entity == null)
            throw new KeyNotFoundException("User not found");

        return entity;
    }

    public async Task Create(UserCreateModel model)
    {
        // validate
        if (await _userRepository.GetById(model.ClerkId!) != null)
            throw new ValidationException("User with the Clerk user_id '" + model.ClerkId + "' already exists");

        // map
        UserEntity entity = _mapper.Map<UserEntity>(model);

        // save user
        await _userRepository.Create(entity);
    }

    public async Task Update(string clerk_id, UserUpdateModel model)
    {
        // validate
        UserEntity? entity = await _userRepository.GetById(clerk_id);

        if (entity == null)
            throw new KeyNotFoundException("User not found");

        if (await _userRepository.GetById(clerk_id) == null)
            throw new ValidationException("User with the Clerk user_id '" + clerk_id + "' already exists");

        // copy props to entity
        _mapper.Map(model, entity);

        // save
        await _userRepository.Update(entity);
    }

    public async Task Delete(string clerk_id)
    {
        await _userRepository.Delete(clerk_id);
    }
}
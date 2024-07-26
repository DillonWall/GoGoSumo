using AutoMapper;
using GoGoSumo.DTOs.Entities;
using GoGoSumo.DTOs.Models.User;
using GoGoSumo.Server.Helpers.Exceptions;
using GoGoSumo.Server.Repositories;
using Humanizer;

namespace GoGoSumo.Server.Services;

public interface IUserService
{
    Task<IEnumerable<UserEntity>> GetAll();
    Task<UserEntity> GetById(string clerk_id);
    Task Create(UserCreateModel model);
    Task Update(UserUpdateModel model);
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
            throw new KeyNotFoundException("User not found with clerk id {ClerkId}".FormatWith(clerk_id));

        return entity;
    }

    public async Task Create(UserCreateModel model)
    {
        if (await _userRepository.GetById(model.ClerkId) != null)
            throw new KeyAlreadyExistsException("User with the Clerk user_id {ClerkId} already exists".FormatWith(model.ClerkId));

        UserEntity entity = _mapper.Map<UserEntity>(model);

        await _userRepository.Create(entity);
    }

    public async Task Update(UserUpdateModel model)
    {
        UserEntity? entity = await _userRepository.GetById(model.ClerkId);

        if (entity == null)
            throw new KeyNotFoundException("User not found with clerk id {ClerkId}".FormatWith(model.ClerkId));

        _mapper.Map(model, entity);

        await _userRepository.Update(entity);
    }

    public async Task Delete(string clerk_id)
    {
        await _userRepository.Delete(clerk_id);
    }
}
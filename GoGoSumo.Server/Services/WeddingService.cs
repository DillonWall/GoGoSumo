using AutoMapper;
using GoGoSumo.Server.DTOs.Entities;
using GoGoSumo.Server.DTOs.Models.Wedding;
using GoGoSumo.Server.Helpers.Exceptions;
using GoGoSumo.Server.Repositories;

namespace GoGoSumo.Server.Services;

public interface IWeddingService
{
    Task<IEnumerable<WeddingEntity>> GetAll();
    Task<WeddingEntity> GetById(int id);
    Task Create(WeddingCreateModel model);
    Task Update(int id, WeddingUpdateModel model);
    Task Delete(int id);
}

public class WeddingService : IWeddingService
{
    private IWeddingRepository _weddingRepository;
    private readonly IMapper _mapper;

    public WeddingService(
        IWeddingRepository weddingRepository,
        IMapper mapper)
    {
        _weddingRepository = weddingRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WeddingEntity>> GetAll()
    {
        return await _weddingRepository.GetAll();
    }

    public async Task<WeddingEntity> GetById(int id)
    {
        WeddingEntity? entity = await _weddingRepository.GetById(id);

        if (entity == null)
            throw new KeyNotFoundException("Wedding not found");

        return entity;
    }

    public async Task Create(WeddingCreateModel model)
    {
        // map
        WeddingEntity entity = _mapper.Map<WeddingEntity>(model);

        // save wedding
        await _weddingRepository.Create(entity);
    }

    public async Task Update(int id, WeddingUpdateModel model)
    {
        // validate
        WeddingEntity? entity = await _weddingRepository.GetById(id);

        if (entity == null)
            throw new KeyNotFoundException("Wedding not found");

        if (await _weddingRepository.GetById(id) == null)
            throw new ValidationException("Wedding with the id '" + id + "' already exists");

        // copy props to entity
        _mapper.Map(model, entity);

        // save
        await _weddingRepository.Update(entity);
    }

    public async Task Delete(int id)
    {
        await _weddingRepository.Delete(id);
    }
}
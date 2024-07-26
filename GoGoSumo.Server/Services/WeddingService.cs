using AutoMapper;
using GoGoSumo.DTOs.Entities;
using GoGoSumo.DTOs.Models.Wedding;
using GoGoSumo.Server.Helpers.Exceptions;
using GoGoSumo.Server.Repositories;
using Humanizer;

namespace GoGoSumo.Server.Services;

public interface IWeddingService
{
    Task<IEnumerable<WeddingEntity>> GetAll();
    Task<WeddingEntity> GetById(int id);
    Task Create(WeddingCreateModel model);
    Task Update(WeddingUpdateModel model);
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
            throw new KeyAlreadyExistsException("Wedding not found with id {id}".FormatWith(id));

        return entity;
    }

    public async Task Create(WeddingCreateModel model)
    {
        WeddingEntity entity = _mapper.Map<WeddingEntity>(model);

        await _weddingRepository.Create(entity);
    }

    public async Task Update(WeddingUpdateModel model)
    {
        WeddingEntity? entity = await _weddingRepository.GetById(model.WeddingId);

        if (entity == null)
            throw new KeyAlreadyExistsException("Wedding not found with id {id}".FormatWith(model.WeddingId));

        _mapper.Map(model, entity);

        await _weddingRepository.Update(entity);
    }

    public async Task Delete(int id)
    {
        await _weddingRepository.Delete(id);
    }
}
using AutoMapper;
using EmployeeManagement.Business.Validation.Address;
using EmployeeManagement.Common.DTOs.Address;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Models;
using FluentValidation;

namespace EmployeeManagement.Business.Services;

public class AddressService : IAddressService
{
    private readonly IGenericRepository<Address> _repository;
    private readonly IMapper _mapper;
    private readonly AddressCreateValidator _addressCreateValidator;
    private readonly AddressUpdateValidator _addressUpdateValidator;

    public AddressService(IGenericRepository<Address> genericRepository, IMapper mapper, AddressCreateValidator addressCreateValidator
                        , AddressUpdateValidator addressUpdateValidator)
    {
        _repository = genericRepository;
        _mapper = mapper;
        _addressCreateValidator = addressCreateValidator;
        _addressUpdateValidator = addressUpdateValidator;
    }

    public async Task<int> CreateAddressAsync(AddressCreate addressCreate)
    {
        await _addressCreateValidator.ValidateAndThrowAsync(addressCreate);

        var entity = _mapper.Map<Address>(addressCreate);
        
        await _repository.InsertAsync(entity);
        await _repository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAddressAsync(AddressDelete addressDelete)
    {
        var entity = await _repository.GetByIdAsync(addressDelete.Id);

        _repository.Delete(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task<AddressGet> GetAddressAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        var addressGet = _mapper.Map<AddressGet>(entity);
        
        return addressGet;
    }

    public async Task<List<AddressGet>> GetAllAddressesAsync()
    {
        var entityList = await _repository.GetAsync(null, null);

        //var addressGetList = entityList.Select(a => _mapper.Map<AddressGet>(a)).ToList();
        var addressGetList = _mapper.Map<List<AddressGet>>(entityList);

        return addressGetList;
    }

    public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
    {
        await _addressUpdateValidator.ValidateAndThrowAsync(addressUpdate);

        //var entity = await _repository.GetByIdAsync(addressUpdate.Id);
        var entity = _mapper.Map<Address>(addressUpdate);

        _repository.Update(entity);
        await _repository.SaveChangesAsync();
    }
}

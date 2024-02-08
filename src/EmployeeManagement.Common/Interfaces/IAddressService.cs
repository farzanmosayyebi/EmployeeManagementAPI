using EmployeeManagement.Common.DTOs;

namespace EmployeeManagement.Common.Interfaces;

public interface IAddressService
{
    Task<int> CreateAddressAsync(AddressCreate addressCreate);
    Task<AddressGet> GetAddressAsync(AddressGet addressGet);
    Task<List<AddressGet>> GetAllAddressesAsync();
    Task UpdateAddressAsync(AddressUpdate addressUpdate);
    Task DeleteAddressAsync(AddressDelete addressDelete);
}

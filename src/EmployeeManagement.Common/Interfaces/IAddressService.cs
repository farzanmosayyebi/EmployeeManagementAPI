using EmployeeManagement.Common.DTOs.Address;

namespace EmployeeManagement.Common.Interfaces;

public interface IAddressService
{
    Task<int> CreateAddressAsync(AddressCreate addressCreate);
    Task<AddressGet> GetAddressAsync(int id);
    Task<List<AddressGet>> GetAllAddressesAsync();
    Task UpdateAddressAsync(AddressUpdate addressUpdate);
    Task DeleteAddressAsync(AddressDelete addressDelete);
}

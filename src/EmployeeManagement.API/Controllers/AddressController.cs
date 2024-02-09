using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.DTOs;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateAddress(AddressCreate addressCreate)
    {
        var id = await _addressService.CreateAddressAsync(addressCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateAddress(AddressUpdate addressUpdate)
    {
        await _addressService.UpdateAddressAsync(addressUpdate);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetAddress(int id)
    {
        var address = await _addressService.GetAddressAsync(id);
        return Ok(address);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetAllAddresses()
    {
        var addresses = await _addressService.GetAllAddressesAsync();
        return Ok(addresses);
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteAddress(AddressDelete addressDelete)
    {
        await _addressService.DeleteAddressAsync(addressDelete);
        return Ok();
    }
}

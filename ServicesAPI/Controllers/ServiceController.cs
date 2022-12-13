using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Models;
using ServicesAPI.Services;
using ServicesAPI.ViewModels;
using ServicesAPI.ViewModels.Service;

namespace ServicesAPI.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;
    
    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateServiceViewModel vm)
    {
        return Ok(await _serviceService.Create(vm));
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] IdViewModel vm)
    {
        await _serviceService.Delete(vm);
        return Ok();
    }
    
    [HttpGet]
    [Route("GetById")]
    public async Task<ActionResult> GetById([FromQuery] Guid id)
    {
        return Ok(await _serviceService.Get(id));
    }
    
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await _serviceService.Get());
    }

    [HttpPatch]
    public async Task<ActionResult> Update(Service service)
    {
        return Ok(await _serviceService.Update(service));
    }
}
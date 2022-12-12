using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Models;
using ServicesAPI.Services;
using ServicesAPI.ViewModels;
using ServicesAPI.ViewModels.ServiceCategories;

namespace ServicesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceCategoryController : ControllerBase
{
    private readonly IServiceCategoryService _serviceCategoryService;

    public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
    {
        _serviceCategoryService = serviceCategoryService;
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ServiceCategoryViewModel vm)
    {
        return Ok(await _serviceCategoryService.Create(vm));
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] IdViewModel vm)
    {
        await _serviceCategoryService.Delete(vm);
        return Ok();
    }
    
    [HttpGet]
    [Route("GetById")]
    public async Task<ActionResult> GetById([FromQuery] Guid id)
    {
        return Ok(await _serviceCategoryService.Get(id));
    }
    
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await _serviceCategoryService.Get());
    }
    
    [HttpPatch]
    public async Task<ActionResult> Update(ServiceCategory serviceCategory)
    {
        return Ok(await _serviceCategoryService.Update(serviceCategory));
    }
}
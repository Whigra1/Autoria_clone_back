using Microsoft.AspNetCore.Mvc;
using AutoRiaClone.Database;
using AutoRiaClone.DTOs;
using AutoRiaClone.Services;
using Microsoft.AspNetCore.Authorization;

// Assume we have a CarModelDTO object defined

namespace AutoRiaClone.Controllers;
[Authorize]
[ApiController]
[Route("api/v1/Cars/Models")]
public class CarModelsController(CarService carService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCarMakes() => Ok(await carService.GetAllModels());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCarMakes([FromQuery] int id)
    {
        var make = await carService.GetModelById(id);
        if (make == null)
        {
            return NotFound();
        }
        return Ok(make);
    }
    
    [HttpPost]
    public async Task<ActionResult<CarModel>> CreateModel(CarModelDto dto)
    {
        var newCar = await carService.CreateModel(dto);
        if (newCar.Success)
        {
            return Ok(newCar.Id);
        }
        return BadRequest(newCar.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCarModel(int id, CarModelDto dto)
    {
       var res = await carService.UpdateModel(id, dto);
       if (res.Success)
       {
           return Ok(true);
       }
       return BadRequest(res.Message);
       
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CarModel>> DeleteCarModel(int id)
    {
        var res = await carService.DeleteModel(id);
        if (res.Success)
        {
            return Ok(true);
        }
        return BadRequest(res.Message);
    }
}

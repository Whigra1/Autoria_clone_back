using AutoRiaClone.Database;
using AutoRiaClone.DTOs;
using AutoRiaClone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AutoRiaClone.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/Cars/Makes")]
public class CarMakesController(CarService carService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCarMakes() => Ok(await carService.GetAllMakes());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCarMakes([FromQuery] int id)
    {
        var make = await carService.GetMakeById(id);
        if (make == null)
        {
            return NotFound();
        }
        return Ok(make);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMake(CarMakeDto dto)
    {
        var res = await carService.CreateMake(dto);
        if (res.Success)
        {
            return Ok(res.Id);
        }
        return BadRequest(res.Message);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCarMake(int id, CarMakeDto dto)
    {
        var res = await carService.UpdateMake(id, dto);
        if (res.Success)
        {
            return Ok(true);
        }
        return BadRequest(res.Message);
       
    }

    [HttpDelete]
    public async Task<ActionResult<CarMake>> DeleteCarMake(int id)
    {
        var res = await carService.DeleteMake(id);
        if (res.Success)
        {
            return Ok(true);
        }
        return BadRequest(res.Message);
    }
}
using Microsoft.AspNetCore.Mvc;
using AutoRiaClone.Database;
using AutoRiaClone.DTOs;
using AutoRiaClone.Services;
using Microsoft.AspNetCore.Authorization;

namespace AutoRiaClone.Controllers;
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class CitiesController(GeoService geoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCities() => Ok(await geoService.GetAllCities());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCity([FromRoute] int id)
    {
        var city = await geoService.GetCityById(id);
        if (city == null)
        {
            return NotFound();
        }
        return Ok(city);
    }

    [HttpPost]
    public async Task<ActionResult<City>> CreateCity(CityDto dto)
    {
        var newCity = await geoService.CreateCity(dto);
        if (newCity.Success)
        {
            return Ok(newCity.Id);
        }
        return BadRequest(newCity.Message);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCity(int id, CityDto dto)
    {
        var res = await geoService.UpdateCity(id, dto);
        if (res.Success)
        {
            return Ok(true);
        }
        return BadRequest(res.Message);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var res = await geoService.DeleteCity(id);
        if (res.Success)
        {
            return Ok(true);
        }
        return BadRequest(res.Message);
    }
}
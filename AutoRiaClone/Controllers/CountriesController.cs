using Microsoft.AspNetCore.Mvc;
using AutoRiaClone.Database;
using AutoRiaClone.Services;
using Microsoft.AspNetCore.Authorization;


namespace AutoRiaClone.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class CountriesController(GeoService geoService): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCountries() => Ok(await geoService.GetAllCountries());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCountry([FromRoute] int id)
    {
        var country = await geoService.GetCountryById(id);
        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCountry(CountryDto dto)
    {
        var res = await geoService.CreateCountry(dto);
        if (res.Success)
        {
            return Ok(res.Id);
        }
        return BadRequest(res.Message);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCountry(int id, CountryDto dto)
    {
        var res = await geoService.UpdateCountry(id, dto);
        if (res.Success)
        {
            return Ok(true);
        }
        return BadRequest(res.Message);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var res = await geoService.DeleteCountry(id);
        if (res.Success)
        {
            return Ok(true);
        }
        return BadRequest(res.Message);
    }
}
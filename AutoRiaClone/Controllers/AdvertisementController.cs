using Microsoft.AspNetCore.Mvc;
using AutoRiaClone.DTOs;
using AutoRiaClone.Services;
using Microsoft.AspNetCore.Authorization;

namespace AutoRiaClone.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/Advertisements")]
public class AdvertisementController(AdvertisementService advertisementService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAdvertisements() => Ok(await advertisementService.GetAllAdvertisements());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAdvertisement([FromRoute] int id)
    {
        var advertisement = await advertisementService.GetAdvertisementById(id);
        if (advertisement == null)
        {
            return NotFound();
        }
        return Ok(advertisement);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAdvertisement(AdvertisementDto dto)
    {
        var result = await advertisementService.CreateAdvertisement(dto);
        if (result.Success)
        {
            return Ok(result.Id);
        }
        return BadRequest(result.Message);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAdvertisement(int id, AdvertisementDto dto)
    {
        var result = await advertisementService.UpdateAdvertisement(id, dto);
        if (result.Success)
        {
            return Ok(true);
        }
        return BadRequest(result.Message);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAdvertisement(int id)
    {
        var result = await advertisementService.DeleteAdvertisement(id);
        if (result.Success)
        {
            return Ok(true);
        }
        return BadRequest(result.Message);
    }
}
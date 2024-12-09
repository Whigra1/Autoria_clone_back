using AutoRiaClone.Database;
using AutoRiaClone.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AutoRiaClone.Services;

public class AdvertisementService(ApplicationDbContext context, DatabaseUtils utils)
{
    public async Task<OperationResult> CreateAdvertisement(AdvertisementDto advertisement)
    {
        if (await utils.IsRecordExists<CarAdvertisement>(a =>
                a.Title == advertisement.Title && a.Vin == advertisement.Vin))
        {
            return new OperationResult(-1, false, "Advertisement already exists");
        }

        var newAdvertisement = new CarAdvertisement
        {
            Title = advertisement.Title,
            Description = advertisement.Description,
            Price = advertisement.Price,
            Mileage = advertisement.Mileage,
            ImageUrl = advertisement.ImageUrl,
            Vin = advertisement.Vin,
            PlateNumber = advertisement.PlateNumber,
            PostedDate = DateTime.UtcNow,
            ModelId = advertisement.ModelId,
            CityId = advertisement.CityId
        };

        try
        {
            await context.Advertisements.AddAsync(newAdvertisement);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new OperationResult(-1, false, $"Error on updating db: {e.Message}");
        }

        return new OperationResult(newAdvertisement.Id, true, "Advertisement created successfully");
    }

    public async Task<OperationResult> UpdateAdvertisement(int id, AdvertisementDto advertisement)
    {
        var exists = await utils.IsRecordExists<CarAdvertisement>(a => a.Id == id);
        if (!exists)
        {
            return new OperationResult(id, false, "Advertisement not exists");
        }

        var advertisementToUpdate = await context.Advertisements.FindAsync(id);
        advertisementToUpdate!.Title = advertisement.Title;
        advertisementToUpdate.Description = advertisement.Description;
        advertisementToUpdate.Price = advertisement.Price;
        advertisementToUpdate.Mileage = advertisement.Mileage;
        advertisementToUpdate.ImageUrl = advertisement.ImageUrl;
        advertisementToUpdate.Vin = advertisement.Vin;
        advertisementToUpdate.PlateNumber = advertisement.PlateNumber;
        advertisementToUpdate.ModelId = advertisement.ModelId;
        advertisementToUpdate.CityId = advertisement.CityId;

        try
        {
            context.Update(advertisementToUpdate);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new OperationResult(id, false, $"Error on updating db: {e.Message}");
        }

        return new OperationResult(id, true, "Advertisement updated successfully");
    }

    public async Task<OperationResult> DeleteAdvertisement(int id)
    {
        var exists = await utils.IsRecordExists<CarAdvertisement>(a => a.Id == id);
        if (!exists)
            return new OperationResult(id, false, "Advertisement not exists");

        var advertisement = await context.Advertisements.FindAsync(id);
        try
        {
            context.Advertisements.Remove(advertisement!);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new OperationResult(-1, false, $"Error on updating db: {e.Message}");
        }

        return new OperationResult(id, true, "Advertisement deleted successfully");
    }

    public async Task<List<CarAdvertisement>> GetAllAdvertisements() =>
        await context.Advertisements.Include(a => a.Model).Include(a => a.City).ToListAsync();

    public async Task<CarAdvertisement?> GetAdvertisementById(int id) => await context.Advertisements
        .Include(a => a.Model).Include(a => a.City).FirstOrDefaultAsync(a => a.Id == id);
}

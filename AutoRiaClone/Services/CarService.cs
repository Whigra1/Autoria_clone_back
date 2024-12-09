using AutoRiaClone.Database;
using AutoRiaClone.DTOs;
using Microsoft.EntityFrameworkCore;
using DriveType = AutoRiaClone.Database.DriveType;

namespace AutoRiaClone.Services;

public class CarService(ApplicationDbContext context, DatabaseUtils utils)
{
    public async Task<OperationResult> CreateMake(CarMakeDto make)
    {
        var existsMake = await utils.IsRecordExists<CarMake>(m => m.Name == make.Name);
        if (existsMake)
        {
            return new OperationResult(-1, false, "Make already exists");
        }
        var newMake = new CarMake
        {
            Name = make.Name,
            ShortName = make.Name,
            CityId = make.CityId
        };
        try
        {
            await context.Makes.AddAsync(newMake);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new OperationResult(-1, false, $"Error on updating db: ${e.Message}");
        }
        
        return new OperationResult(newMake.Id, true, "Make created successfully");
    }

    public async Task<OperationResult> UpdateMake(int id, CarMakeDto make)
    {
        var exists = await utils.IsRecordExists<CarMake>(m => m.Id == id);
        if (!exists)
        {
            return new OperationResult(id, false, "Make not exists");
        }
        var makeToUpdate = await context.Makes.FindAsync(id);
        makeToUpdate!.Name = make.Name;
        makeToUpdate.ShortName = make.ShortName ?? make.Name;
        makeToUpdate.CityId = make.CityId;
        context.Update(makeToUpdate);
        await context.SaveChangesAsync();
        return new OperationResult(id, true, "Make updated successfully");
    }

    public async Task<OperationResult> DeleteMake(int id)
    {
        var exists = await utils.IsRecordExists<CarMake>(m => m.Id == id);
        if (!exists)
        {
            return new OperationResult(id, false, "Make not exists");
        }
        var make = await context.Makes.FindAsync(id);
        context.Makes.Remove(make!);
        await context.SaveChangesAsync();
        return new OperationResult(id, true, "Make deleted successfully");
        
    }

    /// <summary>
    /// Asynchronously creates a new car model in the database using the provided data transfer object.
    /// </summary>
    /// <param name="model">A data transfer object containing the details of the car model to be created, including its name, make, specifications, and other properties.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the unique identifier of the newly created car model.</returns>
    public async Task<OperationResult> CreateModel(CarModelDto model)
    {
        if (await utils.IsRecordExists<CarModel>(m =>
                m.Name == model.Name &&
                m.MakeId == model.MakeId &&
                m.ReleaseDate == model.ReleaseDate)
            )
        {   
            return new OperationResult(-1, false, "Model already exists");
        }
        var newModel = new CarModel
        {
            Name = model.Name,
            MakeId = model.MakeId,
            ReleaseDate = model.ReleaseDate,
            Description = model.Description,
            DriveType = Enum.Parse<DriveType>(model.DriveType),
            CarType = Enum.Parse<CarType>(model.CarType),
            GearboxType = Enum.Parse<GearboxType>(model.GearboxType),
            FuelType = Enum.Parse<FuelType>(model.FuelType),
            EngineVolume = model.EngineVolume,
            HorsePower = model.HorsePower,
        };
        try
        {
            await context.Models.AddAsync(newModel);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new OperationResult(-1, false, $"Error on updating db: ${e.Message}");
        }
      
        return new OperationResult(newModel.Id, true, "Model created successfully");
    }
    
    public async Task<OperationResult> UpdateModel(int id, CarModelDto model)
    {
        var exists = await utils.IsRecordExists<CarModel>(m => m.Id == id);
        if (!exists)
            return new OperationResult(id, false, "Model not exists");

        var modelToUpdate = await context.Models.FindAsync(id);
        modelToUpdate!.Name = model.Name;
        modelToUpdate.MakeId = model.MakeId;
        modelToUpdate.ReleaseDate = model.ReleaseDate;
        modelToUpdate.Description = model.Description;
        modelToUpdate.DriveType = Enum.Parse<DriveType>(model.DriveType);
        modelToUpdate.CarType = Enum.Parse<CarType>(model.CarType);
        modelToUpdate.GearboxType = Enum.Parse<GearboxType>(model.GearboxType);
        modelToUpdate.FuelType = Enum.Parse<FuelType>(model.FuelType);
        modelToUpdate.EngineVolume = model.EngineVolume;
        modelToUpdate.HorsePower = model.HorsePower;
        try
        {
            context.Update(modelToUpdate);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new OperationResult(id, false, $"Error on updating db: ${e.Message}");
        }
        
        return new OperationResult(id, true, "Model updated successfully");
    }
    
    public async Task<OperationResult> DeleteModel(int id)
    {
        var exists = await utils.IsRecordExists<CarModel>(m => m.Id == id);
        if (!exists)
            return new OperationResult(id, false, "Model not exists");
        var model = await context.Models.FindAsync(id);
        try
        {
            context.Models.Remove(model!);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new OperationResult(-1, false, $"Error on updating db: ${e.Message}");
        }
       
        return new OperationResult(id, true, "Model deleted successfully");
    }
    

    public async Task<List<CarMake>> GetAllMakes() => await context.Makes.ToListAsync(); 
    public async Task<CarMake?> GetMakeById(int id) => await context.Makes.FirstOrDefaultAsync(m => m.Id == id);
    
    public async Task<List<CarModel>> GetAllModels() => await context.Models.ToListAsync(); 
    public async Task<CarModel?> GetModelById(int id) => await context.Models.FirstOrDefaultAsync(m => m.Id == id);
    
    public async Task<List<CarAdvertisement>> GetAllAdvertisements(int page, int size) => await context.Advertisements.Skip(page * size).Take(size).ToListAsync();
}
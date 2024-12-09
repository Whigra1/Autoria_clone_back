using AutoRiaClone.Database;
using AutoRiaClone.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AutoRiaClone.Services;

public class GeoService(ApplicationDbContext context, DatabaseUtils utils)
{
     public async Task<OperationResult> CreateCity(CityDto city)
        {
            var existsCity = await utils.IsRecordExists<City>(c => c.Name == city.Name);
            if (existsCity)
            {
                return new OperationResult(-1, false, "City already exists");
            }
            var newCity = new City
            {
                Name = city.Name,
                CountryId = city.CountryId,
                Longitude = city.Longitude ?? 0,
                Latitude = city.Latitude ?? 0
            };
            try
            {
                await context.Cities.AddAsync(newCity);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new OperationResult(-1, false, $"Error on updating db: {e.Message}");
            }

            return new OperationResult(newCity.Id, true, "City created successfully");
        }

        public async Task<OperationResult> UpdateCity(int id, CityDto city)
        {
            var exists = await utils.IsRecordExists<City>(c => c.Id == id);
            if (!exists)
            {
                return new OperationResult(id, false, "City not exists");
            }
            var cityToUpdate = await context.Cities.FindAsync(id);
            cityToUpdate!.Name = city.Name;
            cityToUpdate.Longitude = city.Longitude ?? 0;
            cityToUpdate.Latitude = city.Latitude ?? 0;
            cityToUpdate.CountryId = city.CountryId;
            context.Update(cityToUpdate);
            await context.SaveChangesAsync();
            return new OperationResult(id, true, "City updated successfully");
        }

        public async Task<OperationResult> DeleteCity(int id)
        {
            var exists = await utils.IsRecordExists<City>(c => c.Id == id);
            if (!exists)
            {
                return new OperationResult(id, false, "City not exists");
            }
            var city = await context.Cities.FindAsync(id);
            context.Cities.Remove(city!);
            await context.SaveChangesAsync();
            return new OperationResult(id, true, "City deleted successfully");
        }

        public async Task<OperationResult> CreateCountry(CountryDto country)
        {
            var existsCountry = await utils.IsRecordExists<Country>(c => c.Name == country.Name);
            if (existsCountry)
            {
                return new OperationResult(-1, false, "Country already exists");
            }
            var newCountry = new Country { Name = country.Name };
            try
            {
                await context.Countries.AddAsync(newCountry);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new OperationResult(-1, false, $"Error on updating db: {e.Message}");
            }

            return new OperationResult(newCountry.Id, true, "Country created successfully");
        }

        public async Task<OperationResult> UpdateCountry(int id, CountryDto country)
        {
            var exists = await utils.IsRecordExists<Country>(c => c.Id == id);
            if (!exists)
            {
                return new OperationResult(id, false, "Country not exists");
            }
            var countryToUpdate = await context.Countries.FindAsync(id);
            countryToUpdate!.Name = country.Name;
            context.Update(countryToUpdate);
            await context.SaveChangesAsync();
            return new OperationResult(id, true, "Country updated successfully");
        }

        public async Task<OperationResult> DeleteCountry(int id)
        {
            var exists = await utils.IsRecordExists<Country>(c => c.Id == id);
            if (!exists)
            {
                return new OperationResult(id, false, "Country not exists");
            }
            var country = await context.Countries.FindAsync(id);
            context.Countries.Remove(country!);
            await context.SaveChangesAsync();
            return new OperationResult(id, true, "Country deleted successfully");
        }

        public async Task<List<City>> GetAllCities() => await context.Cities.ToListAsync();
        public async Task<City?> GetCityById(int id) => await context.Cities.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<List<Country>> GetAllCountries() => await context.Countries.ToListAsync();
        public async Task<Country?> GetCountryById(int id) => await context.Countries.FirstOrDefaultAsync(c => c.Id == id);
}
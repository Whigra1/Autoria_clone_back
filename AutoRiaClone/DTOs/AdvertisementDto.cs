namespace AutoRiaClone.DTOs;

public class AdvertisementDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Mileage { get; set; }
    public string ImageUrl { get; set; }
    public string Vin { get; set; }
    public string PlateNumber { get; set; }
        
    public int ModelId { get; set; }
    public int CityId { get; set; }
}
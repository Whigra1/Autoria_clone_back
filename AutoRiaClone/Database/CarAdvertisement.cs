namespace AutoRiaClone.Database;

public class CarAdvertisement: IEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Mileage { get; set; }
    public string ImageUrl { get; set; }
    public string Vin { get; set; }
    public string PlateNumber { get; set; }
    
    public DateTime PostedDate { get; set; }
    public CarModel Model { get; set; }
    public int ModelId { get; set; }
    public City City { get; set; }
    public int CityId { get; set; }
}
namespace AutoRiaClone.Database;

public class City: IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}
namespace AutoRiaClone.DTOs;

public class CityDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
}
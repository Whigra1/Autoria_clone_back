namespace AutoRiaClone.DTOs;

public class CarMakeDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    
    public string? ShortName { get; set; }
    
    public int CityId { get; set; }
}
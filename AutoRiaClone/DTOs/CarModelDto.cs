namespace AutoRiaClone.DTOs;

public class CarModelDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MakeId { get; set; }
    
    public double EngineVolume { get; set; }
    public int HorsePower { get; set; }

    public string CarType { get; set; }
    public string FuelType { get; set; }
    public string DriveType { get; set; } 
    public string GearboxType { get; set; }
    public string Description { get; set; }
    
    public DateOnly ReleaseDate { get; set; }
}
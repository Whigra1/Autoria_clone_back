using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRiaClone.Database;

public enum CarType
{
    Sedan,
    Coupe,
    Hatchback,
    Suv,
    Convertible
}


public enum FuelType
{
    Diesel,
    Gasoline,
    Electric,
    Hybrid
}

public enum DriveType
{
    FrontWheelDrive,
    RearWheelDrive,
    FourWheelDrive
}

public enum GearboxType
{
    Manual,
    Automatic
}

public class CarModel: IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double EngineVolume { get; set; }
    public int HorsePower { get; set; }

    public int MakeId { get; set; }
    [Column(TypeName = "varchar(128)")]
    public CarMake Make { get; set; }
    [Column(TypeName = "varchar(128)")]
    public CarType CarType { get; set; }
    [Column(TypeName = "varchar(128)")]
    public FuelType FuelType { get; set; }
    [Column(TypeName = "varchar(128)")]
    public DriveType DriveType { get; set; }
    [Column(TypeName = "varchar(128)")]
    public GearboxType GearboxType { get; set; }
    public string Description { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
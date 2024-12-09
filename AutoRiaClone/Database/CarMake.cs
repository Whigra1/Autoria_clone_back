namespace AutoRiaClone.Database;

public class CarMake: IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public City City { get; set; }
    public int CityId { get; set; }
    public int CountryId { get; set; }
}
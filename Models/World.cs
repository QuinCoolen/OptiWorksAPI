namespace OptiWorksAPI.Models;

public class World(string name)
{
  public int Id { get; set; }
  public string Name { get; set; } = name;
  public ICollection<Attraction> Attractions { get; set; } = [];
}
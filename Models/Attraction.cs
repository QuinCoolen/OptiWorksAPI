namespace OptiWorksAPI.Models;

public class Attraction(string name, int maxRiders)
{
  public int Id { get; set; }
  public string Name { get; set; } = name;
  public int MaxRiders { get; set; } = maxRiders;
  public ICollection<Visitor>? VisitorsInQueue { get; set; } = [];
  public ICollection<Visitor>? VisitorsOnRide { get; set; } = [];
  public bool IsOpen { get; set; } = true;
  public int WorldId { get; set; }
  public World World { get; set; }
}

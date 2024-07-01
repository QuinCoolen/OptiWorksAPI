namespace OptiWorksAPI.Models;

public class Attraction(string name, int maxRiders)
{
  public int Id { get; set; }
  public string Name { get; set; } = name;
  public string Disruptions { get; set; } = "";
  public string Shows { get; set; } = "";
  public string Misc { get; set; } = "";
  public int MaxRiders { get; set; } = maxRiders;
  public int VisitorsInQueue { get; set; } = 0;
  public int VisitorsOnRide { get; set; } = 0;
  public bool IsOpen { get; set; } = true;
  public int WorldId { get; set; }
  public World World { get; set; }
}

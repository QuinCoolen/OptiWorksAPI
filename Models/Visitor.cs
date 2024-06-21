namespace OptiWorksAPI.Models
{
  public class Visitor(string name)
  {
    public int Id { get; set; }
    public string Name { get; set; } = name;

    public int? AttractionInQueueId { get; set; }
    public Attraction? AttractionInQueue { get; set; }

    public int? AttractionOnRideId { get; set; }
    public Attraction? AttractionOnRide { get; set; }
  }
}
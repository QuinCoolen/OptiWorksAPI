namespace OptiWorksAPI.Models;

public class AttractionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Disruptions { get; set; }
    public string Shows { get; set; }
    public string Misc { get; set; }
    public int Visitors { get; set; } = 0;
    public int MaxRiders { get; set; }
    public bool IsOpen { get; set; }
    public int WorldId { get; set; }
}
namespace OptiWorksAPI.Models;

public class AttractionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxRiders { get; set; }
    public bool IsOpen { get; set; }
    public int WorldId { get; set; }
}
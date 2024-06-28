namespace OptiWorksAPI.Models;

public class WorldDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<AttractionDto> Attractions { get; set; }
}
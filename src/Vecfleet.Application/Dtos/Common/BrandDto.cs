namespace Vecfleet.Application.Dtos.Common;

public class BrandDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ModelDto> Models { get; set; } = new();
}


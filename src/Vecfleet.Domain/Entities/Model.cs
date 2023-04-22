namespace Vecfleet.Domain.Entities;

public class Model
{
    public int Id{ get; set; }
    public string Description { get; set; }

    public Brand Brand { get; set; }
    public int BrandId { get; set; }
}
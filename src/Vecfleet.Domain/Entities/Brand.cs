namespace Vecfleet.Domain.Entities;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Model> Models { get; set; }
}
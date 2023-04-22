namespace Vecfleet.Domain.Entities;

public class Vehicle
{
    public int Id { get; set; }
    
    public VehicleType VehicleType  { get; set; }
    public int VehicleTypeId  { get; set; }

    public int Wheels { get; set; }
    
    public Brand Brand { get; set; }
    public int BrandId { get; set; }

    public Model Model { get; set; }
    public int ModelId { get; set; }
    
    public string Patent { get; set; }

    public string ChassisNumber { get; set; }

    public long Kilometers { get; set; }
}
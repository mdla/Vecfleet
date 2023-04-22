namespace Vecfleet.Application.Dtos.Common;

public class VehicleDto
{
    public int Id { get; set; }
    
    public VehicleTypeDto VehicleType  { get; set; }
    public int VehicleTypeId  { get; set; }

    public int Wheels { get; set; }
    
    public BrandDto Brand { get; set; }
    public int BrandId { get; set; }

    public ModelDto Model { get; set; }
    public int ModelId { get; set; }
    
    public string Patent { get; set; }

    public string ChassisNumber { get; set; }

    public long Kilometers { get; set; }
}
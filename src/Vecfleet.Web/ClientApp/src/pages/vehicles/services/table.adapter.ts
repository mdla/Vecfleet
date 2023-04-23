import {VehicleDto, VehicleTableDto} from "../models/vehicles.type";


export const vehicleDtoToVehicleTableDtoAdapter = (dto: VehicleDto[]): VehicleTableDto[] => {
   return   dto.map(value => {
        const tableDto: VehicleTableDto = {
            ...value,
            brand: value.brand.name,
            model: value.model.description,
            vehicleType: value.vehicleType.description
        }
        return tableDto;
    })
}
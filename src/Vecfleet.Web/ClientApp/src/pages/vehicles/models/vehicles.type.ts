import {BrandDto, ModelDto, VehicleTypeDto} from "../../../models/common.type";

export interface VehicleDto {
    id: number;
    vehicleType: VehicleTypeDto;
    vehicleTypeId: number;
    wheels: number;
    brand: BrandDto;
    brandId: number;
    model: ModelDto;
    modelId: number;
    patent: string;
    chassisNumber: string;
    kilometers: number;
}

export interface VehicleTableDto {
    id: number;
    vehicleType: string;
    vehicleTypeId: number;
    wheels: number;
    brand: string;
    brandId: number;
    model: string;
    modelId: number;
    patent: string;
    chassisNumber: string;
    kilometers: number;
}
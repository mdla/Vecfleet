export interface VehicleTypeDto {
    id: number;
    description: string;
}

export interface BrandDto {
    id: number;
    name: string;
    models: ModelDto[];
}

export interface ModelDto {
    id: number;
    description: string;
    brandId: number;
}

export interface VehicleTypeDto {
    id: number;
    description: string;
}
import {create} from 'zustand';
import {VehicleDto} from "../models/vehicles.type";
import {devtools, persist} from "zustand/middleware";

export interface IVehicleState {
    vehicles: VehicleDto[];
    setVehicles: (list: VehicleDto[]) => void;
    add: (dto: VehicleDto) => void;
    remove: (id: number) => void;
    selectedVehicle: VehicleDto | null;
    setSelectedVehicle: (dto: VehicleDto) => void;
}

export const useVehicleStore = create<IVehicleState>()(
    devtools(
        persist(
            (set, get) => ({
                vehicles: [],
                setVehicles: (list: VehicleDto[]) => {
                    set(state => {
                        return {
                            ...state,
                            vehicles: list
                        }
                    })
                },
                add: (dto: VehicleDto) => {
                    set(state => {
                        return {
                            ...state,
                            vehicles: [...state.vehicles, dto],
                        }
                    })
                },
                remove: (id: number) => {
                    set(state => {
                        return {
                            ...state,
                            vehicles: state.vehicles.filter((dto) => dto.id !== id),
                        }
                    })
                },
                selectedVehicle: null,
                setSelectedVehicle: (dto: VehicleDto | null) => {
                    set(state => {
                        return {
                            ...state,
                            selectedVehicle: dto,
                        }
                    })
                }

            }),
            {
                name: 'bear-storage',
            },
        ),
    ),
);
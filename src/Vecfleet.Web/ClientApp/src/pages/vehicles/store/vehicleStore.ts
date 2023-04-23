import {create} from 'zustand';
import {VehicleDto, VehicleTableDto} from "../models/vehicles.type";
import {devtools, persist} from "zustand/middleware";
import {vehicleDtoToVehicleTableDtoAdapter} from "../services/table.adapter";

export interface IVehicleState {
    vehicles: VehicleTableDto[];
    setVehicles: (list: VehicleDto[]) => void;
    add: (dto: VehicleTableDto) => void;
    update: (dto: VehicleTableDto) => void;
    remove: (id: number) => void;
    selectedVehicle: VehicleTableDto | null;
    clearSelectedVehicle: () => void;
    setSelectedVehicle: (dto: VehicleTableDto) => void;
    showModal: boolean,
    setShowModal: (values: boolean) => void;
}

export const useVehicleStore = create<IVehicleState>()(
    devtools(
        (set, get) => ({
            vehicles: [],
            setVehicles: (list: VehicleDto[]) => {
                set(state => {
                    return {
                        ...state,
                        vehicles: vehicleDtoToVehicleTableDtoAdapter(list)
                    }
                })
            },
            add: (dto: VehicleTableDto) => {
                set(state => {
                    return {
                        ...state,
                        vehicles: [...state.vehicles, dto],
                    }
                })
            },
            update: (dto: VehicleTableDto) => {
                set(state => {
                    const list = state.vehicles.filter((item) => dto.id !== item.id)
                    return {
                        ...state,
                        vehicles: [...list, dto],
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
            setSelectedVehicle: (dto: VehicleTableDto | null) => {
                set(state => {
                    return {
                        ...state,
                        selectedVehicle: dto,
                    }
                })
            },
            clearSelectedVehicle: () => {
                set(state => {
                    return {
                        ...state, selectedVehicle: null
                    }
                })
            },
            setShowModal: values => {
                set(state => {
                    return {...state, showModal: values}
                });
            },
            showModal: false

        }),
        {
            name: 'vehicle-storage',
        },
    ),
);
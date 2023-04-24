import {deleteData, getData, postData, putData} from "../../../shared/utilities/axios.helper";
import {IDataResponse} from "../../../models/result.type";
import {EndpointConst} from "./endpointConst";
import {VehicleDto} from "../models/vehicles.type";

export const getVehicles = async (brandId: number | null, modelId: number | null) => {
    return await getData<IDataResponse<VehicleDto[]>>(EndpointConst.LIST_VEHICLES, {
        brandId: brandId == 0 ? undefined : brandId,
        modelId: modelId == 0 || brandId == 0  ? undefined : modelId,
    }).then(value => {
        if (!value.result.success)
            throw Error(value.result.errors[0])
        return value.data;
    }).catch(reason => {
        throw reason;
    });
}

export const createVehicles = async (vehicle: VehicleDto) => await postData<IDataResponse<VehicleDto>>(EndpointConst.CREATE_VEHICLES, vehicle).then(value => {
    if (!value.result.success)
        throw Error(value.result.errors[0])
    return value.data;
}).catch(reason => {
    throw reason;
});

export const updateVehicles = async (vehicle: VehicleDto) => await putData<IDataResponse<VehicleDto>>(EndpointConst.UPDATE_VEHICLES, vehicle).then(value => {
    if (!value.result.success)
        throw Error(value.result.errors[0])
    return value.data;
}).catch(reason => {
    throw reason;
});

export const deleteVehicles = async (id: number) => await deleteData<IDataResponse<VehicleDto>>(EndpointConst.DELETE_VEHICLES, {id}).then(value => {
    if (!value.result.success)
        throw Error(value.result.errors[0])
    return value.data;
}).catch(reason => {
    throw reason;
});
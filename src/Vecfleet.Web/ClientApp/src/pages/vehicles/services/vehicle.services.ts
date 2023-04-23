import {deleteData, getData, postData, putData} from "../../../shared/utilities/axios.helper";
import {IDataResponse} from "../../../models/result.type";
import {EndpointConst} from "./endpointConst";
import {VehicleDto} from "../models/vehicles.type";

export const getVehicles = async (brandId: number | null, modelId: number | null) => await getData<IDataResponse<VehicleDto[]>>(EndpointConst.LIST_VEHICLES, {
    brandId,
    modelId
}).then(value => {
    if (!value.result.success)
        throw Error(value.result.errors[0])
    return value.data;
}).catch(reason => {
    throw Error(reason);
});

export const createVehicles = async (vehicle: VehicleDto) => await postData<IDataResponse<VehicleDto>>(EndpointConst.CREATE_VEHICLES, vehicle).then(value => {
    if (!value.result.success)
        throw Error(value.result.errors[0])
    return value.data;
}).catch(reason => {
    throw Error(reason);
});

export const updateVehicles = async (vehicle: VehicleDto) => await putData<IDataResponse<VehicleDto>>(EndpointConst.UPDATE_VEHICLES, vehicle).then(value => {
    if (!value.result.success)
        throw Error(value.result.errors[0])
    return value.data;
}).catch(reason => {
    throw Error(reason);
});

export const deleteVehicles = async (vehicle: VehicleDto) => await deleteData<IDataResponse<VehicleDto>>(EndpointConst.DELETE_VEHICLES, vehicle).then(value => {
    if (!value.result.success)
        throw Error(value.result.errors[0])
    return value.data;
}).catch(reason => {
    throw Error(reason);
});
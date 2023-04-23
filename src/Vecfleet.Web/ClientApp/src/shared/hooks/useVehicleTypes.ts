import {useState, useEffect} from "react";
import {getData} from "../utilities/axios.helper";
import {EndpointConst} from "../../pages/vehicles/services/endpointConst";
import {BrandDto, VehicleTypeDto} from "../../models/common.type";
import {IDataResponse} from "../../models/result.type";

export const useVehicleTypes = (): [VehicleTypeDto[], boolean, string|undefined] => {
    const [vehicleTypeDtos, setVehicleTypeDtos] = useState<VehicleTypeDto[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string>("");

    useEffect(() => {
        setIsLoading(true);
        getData<IDataResponse<VehicleTypeDto[]>>(EndpointConst.LIST_VEHICLE_TYPE, null)
            .then((response) => {
                setIsLoading(false);
                if (response.result.success) {
                    setVehicleTypeDtos(response.data);
                } else {
                    setError(response.result.errors[0] ?? undefined);
                }
            })
            .catch((error) => {
                setIsLoading(false);
                setError("Error");
            });
    }, []);

    return [vehicleTypeDtos, isLoading, error];
}
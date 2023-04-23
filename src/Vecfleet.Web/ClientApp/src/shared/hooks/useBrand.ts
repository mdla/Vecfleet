import {useState, useEffect} from "react";
import {getData} from "../utilities/axios.helper";
import {EndpointConst} from "../../pages/vehicles/services/endpointConst";
import {BrandDto} from "../../models/common.type";
import {IDataResponse} from "../../models/result.type";

export const useBrands = (): [BrandDto[], boolean, string|undefined] => {
    const [brands, setBrands] = useState<BrandDto[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string>("");

    useEffect(() => {
        setIsLoading(true);
        getData<IDataResponse<BrandDto[]>>(EndpointConst.LIST_BRANDS, null)
            .then((response) => {
                setIsLoading(false);
                if (response.result.success) {
                    setBrands(response.data);
                } else {
                    setError(response.result.errors[0] ?? undefined);
                }
            })
            .catch((error) => {
                setIsLoading(false);
                setError("Error");
            });
    }, []);

    return [brands, isLoading, error];
}
import {useState, useEffect} from "react";
import {getData} from "../utilities/axios.helper";
import {EndpointConst} from "../../pages/vehicles/services/endpointConst";
import {BrandDto, ModelDto} from "../../models/common.type";
import {IDataResponse} from "../../models/result.type";

export const useModel = (brandId: number| null): [ModelDto[], boolean, string|undefined] => {
    const [models, setModels] = useState<ModelDto[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string>("");

    useEffect(() => {
        if(brandId == null)
            return;
        setIsLoading(true);
        getData<IDataResponse<ModelDto[]>>(EndpointConst.FILTER_MODEL, {
            id: brandId
        })
            .then((response) => {
                setIsLoading(false);
                if (response.result.success) {
                    setModels(response.data);
                } else {
                    setError(response.result.errors[0] ?? undefined);
                }
            })
            .catch((error) => {
                setIsLoading(false);
                setError("Error");
            });
    }, [brandId]);

    return [models, isLoading, error];
}
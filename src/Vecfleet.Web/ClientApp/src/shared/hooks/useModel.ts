import {useState, useEffect} from "react";
import {getData} from "../utilities/axios.helper";
import {EndpointConst} from "../../pages/vehicles/services/endpointConst";
import {BrandDto, ModelDto} from "../../models/common.type";
import {IDataResponse} from "../../models/result.type";

export const useModel = (brandId?: number | null): [ModelDto[], boolean, boolean] => {
    const [models, setModels] = useState<ModelDto[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<boolean>(false);

    useEffect(() => {
        if (brandId == null)
            return;
        let value:boolean=false;
        try {
            value= isNaN(Number(brandId));
        } catch (e) {
            return ;
        }
        if(value)
            return;

        setIsLoading(true);
        getData<IDataResponse<ModelDto[]>>(EndpointConst.FILTER_MODEL, {
            id: brandId
        })
            .then((response) => {
                setIsLoading(false);
                if (response.result.success) {
                    setModels(response.data);
                    setError(true);
                } else {
                    setError(true);
                }
            })
            .catch((error) => {
                setIsLoading(false);
                setError(true);
            });
    }, [brandId]);

    return [models, isLoading, error];
}
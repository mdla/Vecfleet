import {AxiosError} from "axios";
import {useGlobalStore} from "../../store/useGlobalStore";
import {IApiResult, Result} from "../../models/result.type";

interface IUseErrorHandler {
    showGenericErrorToast: boolean;
}

const useErrorHandler = ({showGenericErrorToast}: IUseErrorHandler = {showGenericErrorToast: true}) => {
    const {setToast} = useGlobalStore();


    const handleError = (error: any) => {
        if (error instanceof Error) {
            const axiosError = error as AxiosError;
            const apiResult = axiosError.response?.data as IApiResult;

            if (apiResult && apiResult.result) {
                const message = apiResult.result.errors[0] || axiosError.message;
                setToast(true, apiResult.result, "danger", message);
                return;
            }

            const result = axiosError.response?.data as Result;

            if (result && !result.success) {
                const message = result.errors[0] || axiosError.message;
                setToast(true, result, "danger", message);
                return;
            }
        } else {
            setToast(true, undefined, "danger", error.message ?? undefined);
        }
    };

    return {
        handleError,
    };
};

export default useErrorHandler;

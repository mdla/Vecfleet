export interface IDataResponse<T> {
    result: Result;
    data: T;
}

export interface IIdRequest<T> {
    id: T;
}

export interface Result {
    success: boolean;
    errors: string[];
    validations: any;
}

export interface IApiResult {
    result: Result;
}

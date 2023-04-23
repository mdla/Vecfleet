import axios, { AxiosError } from 'axios';

export const getData = async <T>(endpoint: string, param: any): Promise<T> => {
    return await axios
        .get<T>(endpoint, {
            params: { ...param },
        })
        .then(function (response) {
            return response.data;
        })
        .catch(function (error) {
            throw error;
        });
};

export const postData = async <T>(endpoint: string, data: any): Promise<T> => {
    return await axios
        .post<T>(endpoint, data)
        .then(function (response) {
            if (response instanceof AxiosError) {
                throw Error(response.message);
            }
            return response.data;
        })
        .catch(function (error) {
            throw error;
        });
};

export const putData = async <T>(endpoint: string, data: any): Promise<T> => {
    return await axios
        .put<T>(endpoint, {data})
        .then(function (response) {
            if (response instanceof AxiosError) {
                throw Error(response.message);
            }
            return response.data;
        })
        .catch(function (error) {
            console.log(error);
            throw error;
        });
};

export const deleteData = async <T>(endpoint: string, data: any): Promise<T> => {
    return await axios
        .delete<T>(endpoint, {data})
        .then(function (response) {
            return response.data;
        })
        .catch(function (error) {
            console.log(error);
            throw error;
        });
};

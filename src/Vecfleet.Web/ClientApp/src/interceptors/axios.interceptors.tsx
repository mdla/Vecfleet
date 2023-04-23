import axios, {AxiosError, AxiosResponse,  InternalAxiosRequestConfig} from 'axios';


export const AxiosInterceptor = (): any => {
  axios.interceptors.response.use(
    (response: AxiosResponse) => {
      return response;
    },
    (error: AxiosError) => {
      if (error.code === 'ERR_NETWORK') {
        console.log('La API no esta disponible');
      }
      console.log('interceptor error:', error);
      return error;
    },
  );
};

import {create} from 'zustand';
import {devtools, persist} from 'zustand/middleware';
import {Result} from "../models/result.type";

interface IGlobalState {
    loading: boolean;
    result?: Result;
    setLoading: (value: boolean) => void;
    setToast: (isVisible: boolean, result?: Result, toastType?:string, message?:string) => void;
    toast: {
        isVisible:boolean,
        toastType?:string,
        message?:string,
    };
}

export const useGlobalStore = create<IGlobalState>()(
    devtools(
        (set, get) => ({
            loading: false,
            setLoading(value: boolean): void {
                set(state => {
                    return {...state, loading: value}
                });
            }, setToast(isVisible:boolean ,result?: Result, toastType?:string, message?:string): void {
                set(state => {
                   return{
                       ...state,toast:{
                           isVisible,
                           toastType,
                           message: message
                       },result
                   }
                });
            }, toast: {
                isVisible:false,
                toastType:undefined
            }, result: undefined

        }),
        {
            name: 'bear-storage',
        },
    ),
);

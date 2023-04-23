import {create} from 'zustand';
import {devtools, persist} from 'zustand/middleware';

interface IGlobalState {
    loading: boolean;
    setLoading: (value: boolean) => void;
}

export const useGlobalStore = create<IGlobalState>()(
    devtools(
        persist(
            (set, get) => ({
                loading: false,
                setLoading(value: boolean): void {
                    set(state => {
                        return {...state, loading: value}
                    });
                }

            }),
            {
                name: 'bear-storage',
            },
        ),
    ),
);

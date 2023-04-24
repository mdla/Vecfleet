import { Suspense } from 'react';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import { routes } from './routes';
import {Loading} from "../shared/components/Loading";

export const Navigation = (): JSX.Element => {
    return (
        <Suspense fallback={<span>Cargando...</span>}>
            <Loading />
            <BrowserRouter>
                <Routes>
                    {routes.map((route) => {
                        return (
                            <Route
                                key={route.to}
                                path={route.path}
                                element={<route.Component />}
                            />
                        );
                    })}
                    <Route path='/*' element={<Navigate to='/' replace />} />
                </Routes>
            </BrowserRouter>
        </Suspense>
    );
};
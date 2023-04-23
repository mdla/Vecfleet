import { LazyExoticComponent } from 'react';
import VehiclesPage from "../pages/vehicles/VehiclesPage";


type JSXElement = () => JSX.Element;

interface Route {
    to: string;
    path: string;
    Component: LazyExoticComponent<() => JSX.Element> | JSXElement;
    name: string;
}

export const routes: Route[] = [
    {
        to: '/vehicles',
        path: '/vehicles',
        Component: VehiclesPage,
        name: 'vehicles',
    },

];

import * as React from 'react';
import {Col, Row} from "react-bootstrap";
import VehicleFilters from "./components/VehicleFilters";

const VehiclesPage = () => {

    return <>
        <Row>
            <Col>
                <h1>Vehículos</h1>
            </Col>
        </Row>
        <VehicleFilters></VehicleFilters>
        <Row>

        </Row>
    </>
};

export default VehiclesPage;
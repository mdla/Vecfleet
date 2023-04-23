import * as React from 'react';
import {Col, Container, Row} from "react-bootstrap";
import VehicleFilters from "./components/VehicleFilters";
import VehicleTable from "./components/VehicleTable";
import CustomToast from "../../shared/components/CustomToast";

const VehiclesPage = () => {

    return <>
        <Container>
        <Row>
            <Col>
                <h1>Vehículos</h1>
            </Col>
        </Row>
        <VehicleFilters></VehicleFilters>
        <Row className={"mt-1"}>
            <VehicleTable></VehicleTable>
        </Row>

        </Container>
    </>
};

export default VehiclesPage;
import * as React from 'react';
import {Col, Container, Row} from "react-bootstrap";
import VehicleFilters from "./components/VehicleFilters";
import VehicleTable from "./components/VehicleTable";
import {useVehicleStore} from "./store/vehicleStore";

const VehiclesPage = () => {
    const vehicleStore = useVehicleStore(state => state.vehicles);

    return <>
        <Container>
            <Row>
                <Col>
                    <h1>Vehículos ({vehicleStore.length})</h1>
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
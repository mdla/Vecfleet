import * as React from 'react';
import {Modal} from "react-bootstrap";
import VehicleForm from "./VehicleForm";
import {useVehicleStore} from "../store/vehicleStore";


const VehicleCrudModal = () => {
    const {selectedVehicle, showModal, setShowModal}= useVehicleStore();
    let title= "Modificar Vehículo";
    if(!selectedVehicle)
        title ="Crear Vehículo";
    return <>
        <Modal show={showModal} onHide={() => setShowModal(false)}>
            <Modal.Header closeButton>
                <Modal.Title>{title}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <VehicleForm dto={selectedVehicle} onSubmit={values => {
                    setShowModal(false);
                }}/>
            </Modal.Body>
        </Modal>
    </>
};

export default VehicleCrudModal;
import * as React from 'react';
import {Modal} from "react-bootstrap";
import VehicleForm from "./VehicleForm";
import {useVehicleStore} from "../store/vehicleStore";
import {boolean} from "yup";


const VehicleCrudModal = () => {

    const {selectedVehicle, showModal, setShowModal}=
        useVehicleStore((state) => ({selectedVehicle:state.selectedVehicle, showModal:state.showModal,setShowModal:state.setShowModal}));
    debugger;
    const create:boolean=selectedVehicle === null;
    let title= "Modificar Vehículo";
    if(create)
        title ="Crear Vehículo";
    return <>
        <Modal show={showModal} onHide={() => setShowModal(false)}>
            <Modal.Header closeButton>
                <Modal.Title>{title}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {create? <VehicleForm dto={null} onSubmit={values => {
                    setShowModal(false);
                }}/> : <VehicleForm dto={selectedVehicle} onSubmit={values => {
                    setShowModal(false);
                }}/>}

            </Modal.Body>
        </Modal>
    </>
};

export default VehicleCrudModal;
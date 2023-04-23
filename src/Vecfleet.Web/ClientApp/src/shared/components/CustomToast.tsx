import React from 'react';
import { Toast } from 'react-bootstrap';
import {useGlobalStore} from "../../store/useGlobalStore";

const CustomToast = () => {
    const  {toast, setToast, result}= useGlobalStore();
    let typo: string= toast.toastType ?? "info";
    let toastMessage: string= "";

    if(!toast.isVisible)
        return null;

    if (result && !result.success) {
        typo = "danger";
        toastMessage = result.errors.join('\n');
        for (const prop in result.validations) {
            toastMessage += `${prop}: ${result.validations[prop]}\n`;
        }
    }

    return (

        <Toast
            onClose={() => setToast(false, undefined)}
            show={toast.isVisible}
            style={{
                position: 'fixed',
                bottom: 20,
                right: 20,
                zIndex: 1,
            }}
            bg={typo}
        >
            <Toast.Header >
                <strong className="mr-auto">Notificación</strong>
            </Toast.Header>
            <Toast.Body>{toastMessage}</Toast.Body>
        </Toast>
    );
};

export default CustomToast;
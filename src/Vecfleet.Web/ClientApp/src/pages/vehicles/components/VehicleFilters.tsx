import {Button, ButtonGroup, Col, Form, FormGroup, Row} from "react-bootstrap";

import BrandSelect from "../../../shared/components/BrandSelect";
import ModelSelect from "../../../shared/components/ModelSelect";
import {FormikHelpers, useFormik} from "formik";
import {getVehicles} from "../services/vehicle.services";
import {VehicleDto} from "../models/vehicles.type";
import {useVehicleStore} from "../store/vehicleStore";

interface iFormValuesDto {
    brandId: number | null,
    modelId: number | null,
}

const InitialValues: iFormValuesDto = {
    brandId: null,
    modelId: null
};

const VehicleFilters = () => {
    const {
        setVehicles
    } = useVehicleStore();

    const onSubmitHandler = (
        values: iFormValuesDto,
        actions: FormikHelpers<iFormValuesDto>,
    ): void => {
        void getVehicles(values.brandId, values.modelId,).then((response: VehicleDto[]) => {
            setVehicles(response);
            actions.setSubmitting(false);
        }).catch(reason => {
            actions.setSubmitting(false);
            throw new Error(reason);
        });
    };

    const formik = useFormik<iFormValuesDto>({
        initialValues: InitialValues,
        onSubmit: onSubmitHandler,
    });


    return (
        <>
            <Row className={"align-items-center"}>
                <Col className={"text-center"}>
                    <Form onSubmit={formik.handleSubmit} noValidate={false}>
                        <Row className={"align-items-center"}>
                            <FormGroup as={Col} md={4} sm={10}>
                                <BrandSelect key={"brand-select"}
                                             name={"brandId"}
                                             id={"brandId"}
                                             onChange={formik.handleChange}
                                             onBlur={formik.handleBlur}
                                />
                            </FormGroup>
                            <FormGroup as={Col} md={4} sm={10}>
                                <ModelSelect key={"model-select"}
                                             name={"modelId"}
                                             id={"modelId"}
                                             brandId={formik.values.brandId}
                                             onChange={formik.handleChange}
                                             onBlur={formik.handleBlur}
                                />
                            </FormGroup>
                            <FormGroup as={Col} md={2} sm={10} className={"justify-content-md-start"}>
                                <Button variant="primary" type="submit" disabled={formik.isSubmitting} >
                                    Buscar
                                </Button>
                            </FormGroup>
                        </Row>
                    </Form>
                </Col>
            </Row>
        </>
    );
};
export default VehicleFilters;

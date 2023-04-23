import React from "react";
import {Form, Button, Row, Col, FormLabel, FormGroup, FormControl} from "react-bootstrap";
import {FormikHelpers, useFormik} from "formik";
import * as Yup from "yup";
import {VehicleDto, VehicleTableDto} from "../models/vehicles.type";
import VehicleTypeSelect from "../../../shared/components/VehicleTypeSelect";
import {createVehicles, updateVehicles} from "../services/vehicle.services";
import {useVehicleStore} from "../store/vehicleStore";
import {vehicleDtoToVehicleTableDtoAdapter} from "../services/table.adapter";
import BrandSelect from "../../../shared/components/BrandSelect";
import ModelSelect from "../../../shared/components/ModelSelect";
import {ValidationLabel} from "../../../shared/components/ValidationLabel";
import useErrorHandler from "../../../shared/hooks/useErrorHandler";

interface Props {
    dto: VehicleTableDto | null;
    onSubmit: (values: IFormValuesDto) => void;
}

interface IFormValuesDto {
    id: number;
    vehicleTypeId: number;
    wheels: number;
    brandId: number;
    modelId: number;
    patent: string;
    chassisNumber: string;
    kilometers: number;
};

let initialValues: IFormValuesDto = {
    id: 0,
    vehicleTypeId: 0,
    wheels: 0,
    brandId: 0,
    modelId: 0,
    patent: "",
    chassisNumber: "",
    kilometers: 0,
}
const VehicleForm = (props: Props): JSX.Element => {
    const {add, update} = useVehicleStore();
    const {handleError}= useErrorHandler();

    if (props.dto !== null) {
        initialValues = {
            id: props.dto.id,
            vehicleTypeId: props.dto.vehicleTypeId,
            wheels: props.dto.wheels,
            brandId: props.dto.brandId,
            modelId: props.dto.modelId,
            patent: props.dto.patent,
            chassisNumber: props.dto.chassisNumber,
            kilometers: props.dto.kilometers,
        }
    }

    const onSubmit = (values: IFormValuesDto, formikHelpers: FormikHelpers<IFormValuesDto>) => {

        const dto: VehicleDto = {
            ...values, vehicleType: {
                id: values.vehicleTypeId,
                description: ""
            }, model: {
                brandId: values.brandId, description: "", id: values.modelId

            }, brand: {
                id: values.brandId, models: [], name: ""
            }
        };
        if (values.id === 0) {
            createVehicles(dto).then(value => {
                add(vehicleDtoToVehicleTableDtoAdapter([value])[0]);
            }).catch(reason => {
                formikHelpers.setSubmitting(false);
                handleError(reason);
            });
            formikHelpers.resetForm();
            props.onSubmit(values);
            return;
        }

        updateVehicles(dto).then(value => {
            update(vehicleDtoToVehicleTableDtoAdapter([value])[0])
        }).catch(reason => {
            formikHelpers.setSubmitting(false);
            handleError(reason);
        });
        formikHelpers.resetForm();
        props.onSubmit(values);
    }

    const formik = useFormik<IFormValuesDto>({
        initialValues,
        validationSchema: Yup.object({
            vehicleTypeId: Yup.number()
                .typeError('Este campo es requerido.')
                .min(0, 'Debe ser mayor a cero.')
                .required('Este campo es requerido.'),
            brandId: Yup.number()
                .typeError('Este campo es requerido.')
                .min(0, 'Debe ser mayor a cero.')
                .required('Este campo es requerido.'),
            modelId: Yup.number()
                .typeError('Este campo es requerido.')
                .min(0, 'Debe ser mayor a cero.')
                .required('Este campo es requerido.'),
            patent: Yup.string().required("Este campo es requerido."),
            chassisNumber: Yup.string().required("Este campo es requerido."),
            kilometers: Yup.number()
                .typeError('Este campo es requerido.')
                .min(0, 'Debe ser mayor o igual a cero.')
                .required("Este campo es requerido."),
            wheels: Yup.number()
                .typeError('Este campo es requerido.')
                .min(1, 'Debe ser mayor a cero.')
                .required("Este campo es requerido."),
        }),

        onSubmit,
    });

    return (
        <>
            <Form noValidate onSubmit={formik.handleSubmit}>
                <Row>
                    <Col xs={12} sm={6}>
                        <FormGroup>
                            <VehicleTypeSelect onChange={formik.handleChange}
                                               onBlur={formik.handleBlur}
                                               id={"vehicleTypeId"}
                                               name={"vehicleTypeId"}
                                               value={formik.values.vehicleTypeId}></VehicleTypeSelect>
                            <ValidationLabel id={"vehicleTypeId-label"}
                                             touched={formik.touched.vehicleTypeId}
                                             message={formik.errors.vehicleTypeId}></ValidationLabel>
                        </FormGroup>
                    </Col>
                </Row>

                <Row>
                    <Col xs={12} sm={6}>
                        <FormGroup>
                            <BrandSelect onChange={formik.handleChange} id={"brandId"}
                                         onBlur={formik.handleBlur}
                                         name={"brandId"}
                                         value={formik.values.brandId}></BrandSelect>
                            <ValidationLabel id={"brandId-label"}
                                             touched={formik.touched.brandId}
                                             message={formik.errors.brandId}></ValidationLabel>
                        </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col xs={12} sm={6}>
                        <FormGroup>
                            <ModelSelect onChange={formik.handleChange} id={"modelId"}
                                         onBlur={formik.handleBlur}
                                         name={"modelId"}
                                         value={formik.values.modelId} brandId={formik.values.brandId}></ModelSelect>
                            <ValidationLabel id={"modelId-label"}
                                             touched={formik.touched.modelId}
                                             message={formik.errors.modelId}></ValidationLabel>
                        </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col xs={12} sm={6}>
                        <FormGroup>
                            <FormLabel htmlFor={"patent"}>Patente</FormLabel>
                            <FormControl
                                id="patent"
                                name="patent"
                                type="text"
                                placeholder="Ingrese la patente"
                                value={formik.values.patent}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                isValid={formik.touched.patent && !formik.errors.patent}
                            />
                            <ValidationLabel id={"patent-label"}
                                             touched={formik.touched.patent}
                                             message={formik.errors.patent}></ValidationLabel>
                        </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col xs={12} sm={6}>
                        <FormGroup>
                            <FormLabel htmlFor={"chassisNumber"}>Número de chasis</FormLabel>
                            <FormControl
                                id="chassisNumber"
                                name="chassisNumber"
                                type="text"
                                placeholder="Ingrese el número de chasis"
                                value={formik.values.chassisNumber}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                isValid={formik.touched.chassisNumber && !formik.errors.chassisNumber}
                            />
                            <ValidationLabel id={"chassisNumber-label"}
                                             touched={formik.touched.chassisNumber}
                                             message={formik.errors.chassisNumber}></ValidationLabel>
                        </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col xs={12} sm={6}>
                        <FormGroup>
                            <FormLabel htmlFor={"kilometers"}>Kilómetros</FormLabel>
                            <FormControl
                                id="kilometers"
                                className="form-control"
                                name="kilometers"
                                type="number"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.kilometers}
                                isValid={formik.touched.kilometers && !formik.errors.kilometers}
                            />
                            <ValidationLabel id={"kilometers-label"}
                                             touched={formik.touched.kilometers}
                                             message={formik.errors.kilometers}></ValidationLabel>
                        </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col xs={12} sm={6}>
                        <FormGroup>
                            <FormLabel htmlFor={"wheels"}>Ruedas</FormLabel>
                            <FormControl
                                id="wheels"
                                className="form-control"
                                name="wheels"
                                type="number"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.wheels}
                                isValid={formik.touched.wheels && !formik.errors.wheels}
                            />
                            <ValidationLabel id={"wheels-label"}
                                             touched={formik.touched.wheels}
                                             message={formik.errors.wheels}></ValidationLabel>
                        </FormGroup>
                    </Col>
                </Row>
                <Row className={"mt-1"}>
                    <Col>
                        <Button variant="primary" type="submit" disabled={formik.isSubmitting || !formik.isValid}>
                            Guardar
                        </Button>
                    </Col>
                </Row>
            </Form>
        </>)
};
export default VehicleForm;

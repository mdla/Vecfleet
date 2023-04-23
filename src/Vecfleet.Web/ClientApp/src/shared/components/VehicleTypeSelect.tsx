import {FormLabel, FormSelect} from "react-bootstrap";
import {useModel} from "../hooks/useModel";
import {useBrands} from "../hooks/useBrand";
import {useVehicleTypes} from "../hooks/useVehicleTypes";
import {FocusEventHandler} from "react";

interface IProps {
    onBlur: FocusEventHandler<HTMLSelectElement> | undefined;
    onChange: React.ChangeEventHandler<HTMLSelectElement>,

    id: string,
    name: string,
    value?: number
}

const VehicleTypeSelect = (props: IProps): JSX.Element => {
    const [vehicleTypeDtos,  error] = useVehicleTypes();

    if (error)
        return <div>Error cargando Marca</div>
    return (
        <>
            <FormLabel htmlFor={props.id}>Tipo de Vehículo</FormLabel>
            <FormSelect id={props.id}
                        name={props.name}
                        onChange={props.onChange}
                        onBlur={props.onBlur}
                        value={props.value}>
                <option value={""}>Seleccione una Vehículo</option>
                {vehicleTypeDtos.map(item => (
                    <option key={item.id} value={item.id}>{item.description}</option>
                ))}
            </FormSelect>
        </>
    );
};

export default VehicleTypeSelect;
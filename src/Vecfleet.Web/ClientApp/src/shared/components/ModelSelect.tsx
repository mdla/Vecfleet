import { FormLabel, FormSelect} from "react-bootstrap";
import {useModel} from "../hooks/useModel";
import {FocusEventHandler} from "react";

interface IProps {
    id: string,
    name: string,
    onChange: React.ChangeEventHandler<HTMLSelectElement>
    onBlur: FocusEventHandler<HTMLSelectElement> | undefined;
    brandId?: number|null,
    value?: number
}

const ModelSelect = (props: IProps) => {
    const [modelDtos,  error] = useModel(props.brandId);

    if (!props.brandId)
        return <>
            <FormLabel htmlFor={props.id}>Modelo</FormLabel>
            <FormSelect id={props.id}
                        name={props.name}
                        onChange={props.onChange}
                        onBlur={props.onBlur}>
                <option>Seleccione un Modelo</option>
            </FormSelect>
        </>

    if(error)
        return <div>Error cargando Modelo</div>
    return (
        <>
            <FormLabel htmlFor={props.id}>Modelos</FormLabel>
            <FormSelect id={props.id} name={props.name} onChange={props.onChange} value={props.value}>
                <option value={"0"}>Seleccione un Modelo</option>
                {modelDtos.map(item => (
                    <option key={item.id} value={item.id}>{item.description}</option>
                ))}
            </FormSelect>
        </>)


};

export default ModelSelect;
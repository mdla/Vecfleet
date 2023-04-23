import { FormLabel, FormSelect} from "react-bootstrap";
import {useModel} from "../hooks/useModel";
import {FocusEventHandler} from "react";

interface IProps {
    id: string,
    name: string,
    onChange: React.ChangeEventHandler<HTMLSelectElement>
    onBlur: FocusEventHandler<HTMLSelectElement> | undefined;
    brandId: number|null,
    value?: number
}

const ModelSelect = (props: IProps) => {
    const [brands, isLoading, error] = useModel(props.brandId);

    if (!props.brandId)
        return <>
            <FormLabel htmlFor={props.id}>Modelo</FormLabel>
            <FormSelect id={props.id}
                        name={props.name}
                        onChange={props.onChange}
                        onBlur={props.onBlur}>
                <option>Seleccione una Modelo</option>
            </FormSelect>
        </>

    if(error)
        return <div>Error cargando Modelo</div>
    return (
        <>
            {isLoading && <div>Cargando Modelo...</div>}
            <FormLabel htmlFor={props.id}>Modelos</FormLabel>
            <FormSelect id={props.id} name={props.name} onChange={props.onChange} value={props.value}>
                <option value={""}>Seleccione un Modelo</option>
                {brands.map(item => (
                    <option key={item.id} value={item.id}>{item.description}</option>
                ))}
            </FormSelect>
        </>)


};

export default ModelSelect;
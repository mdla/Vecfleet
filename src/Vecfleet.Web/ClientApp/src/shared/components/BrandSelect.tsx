import {FormLabel, FormSelect} from "react-bootstrap";
import {useModel} from "../hooks/useModel";
import {useBrands} from "../hooks/useBrand";

interface IProps {
    onChange: React.ChangeEventHandler<HTMLSelectElement>,
    id: string,
    name: string,
    brandId?: number
}

const BrandSelect = (props: IProps): JSX.Element => {
    const [brands, isLoading, error] = useBrands();

    if (error)
        return <div>Error cargando Marca</div>
    return (
        <>
            {isLoading && <div>Cargando Marca...</div>}
            <FormLabel htmlFor={props.id}>Marca</FormLabel>
            <FormSelect id={props.id} name={props.name} onChange={props.onChange} value={props.brandId}>
                <option>Seleccione una Marca</option>
                {brands.map(item => (
                    <option key={item.id} value={item.id}>{item.name}</option>
                ))}
            </FormSelect>
        </>
    );
};

export default BrandSelect;
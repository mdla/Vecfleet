
import {Spinner} from "react-bootstrap";
import {useGlobalStore} from "../../store/useGlobalStore";
export const Loading = (): any => {
    const { loading } = useGlobalStore();
    if (loading) {
        return (
            <Spinner animation="border" variant="primary" />
        );
    } else {
        return null;
    }
};

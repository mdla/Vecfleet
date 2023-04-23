interface Iprops {
    message?: string;
    id: string;
    touched?: boolean;
}

export const ValidationLabel = (props: Iprops) => {
    if (props.touched && props.message) {
        return <div className="text-danger">{props.message}</div>
    }
    return null
}



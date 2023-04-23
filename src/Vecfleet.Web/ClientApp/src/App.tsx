import './custom.css';
import {Navigation} from "./routes/Navigation";
import CustomToast from "./shared/components/CustomToast";
import * as React from "react";

function App() {
    return (
        <>
            <Navigation/>
            <CustomToast />
        </>
    );
}

export default App;

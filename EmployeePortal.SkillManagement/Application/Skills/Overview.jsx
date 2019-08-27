import { Fragment, default as React} from "react";
import { render } from "react-dom";

const Overview = () => (
    <Fragment>
        <h1>React in ASP.NET MVC!</h1>
        <div>Hello React World</div>
    </Fragment>
);

render(<Overview />, document.getElementById("overview"));
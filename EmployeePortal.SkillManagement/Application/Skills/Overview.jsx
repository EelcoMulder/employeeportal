import { Fragment, default as React} from "react";
import { render } from "react-dom";

const App = () => (
    <Fragment>
        <h1>React in ASP.NET MVC!</h1>
        <div>Hello React World</div>
    </Fragment>
);

render(<App />, document.getElementById("app"));
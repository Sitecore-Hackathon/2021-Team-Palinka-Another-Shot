import React from "react";
import { BrowserRouter as Router, Route, Switch, useLocation } from "react-router-dom";
import Homepage from "./pages/Homepage";

const App = () => {
  return (
    <Router>
      <Switch>
        <Route exact path="/">
          <Homepage/>
        </Route>
        <Route path="*">
          <NoMatch/>
        </Route>
      </Switch>
    </Router>
  );
};

function NoMatch() {
  let location = useLocation();

  return (
    <div>
      <h3>
        No match for <code>{location.pathname}</code>
      </h3>
    </div>
  );
}

export default App;
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Home from "../pages/Home";
import { NotificationContainer } from "react-notifications";

import "./style.scss";

const App = () => {
  return (
    <Router>
      <main>
        <Switch>
          <Route exact path={["/", "/home"]}>
            <Home />
          </Route>
        </Switch>

        <NotificationContainer />
      </main>
    </Router>
  );
};

export default App;

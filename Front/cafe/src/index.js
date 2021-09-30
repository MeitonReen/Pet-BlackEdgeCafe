import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import urls from "./urls";
import { generateImagesUrls, cacheImages } from "./shared-funcs";

import {
  Switch,
  Route,
  BrowserRouter as Router, Redirect
} from "react-router-dom";

const imagesModulesUrls = generateImagesUrls(
  require.context('./images/icons',
    false, /\.(png|jpe?g|svg)$/));

ReactDOM.render(
  <React.StrictMode>
    <Router>
      <Switch>
        <Route path={urls.pages}>
          <App />
        </Route>
        <Route exact path="/">
          <Redirect to={urls.index} />
        </Route>
      </Switch>
    </Router>
  </React.StrictMode>,
  document.getElementById('root'),
  () => cacheImages(...Object.values(imagesModulesUrls)));




import React from "react";
import { render } from "react-dom";
import { configureStore } from "@reduxjs/toolkit";
import { Provider } from "react-redux";
import logger from "redux-logger";

import App from "./App";
import rootReducer from "./redux/slices";

import "./index.scss";

const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(logger),
  devTools: process.env.NODE_ENV !== "production",
});

render(
  <Provider store={store}>
    <App/>
  </Provider>,
  document.getElementById("root")
);
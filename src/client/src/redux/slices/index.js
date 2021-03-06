import { combineReducers } from "redux";

import workItemsReducer from "./workitems";
import workflowsReducer from "./workflows";

const rootReducer = combineReducers({
  workItems: workItemsReducer,
  workflowItems: workflowsReducer
});

export default rootReducer;
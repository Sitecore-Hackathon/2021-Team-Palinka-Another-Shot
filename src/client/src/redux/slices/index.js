import { combineReducers } from "redux";

import workItemsReducer from "./workitems";
import workflowsReducer from "./workflows";
import itemDetailsReducer from "./itemDetails";

const rootReducer = combineReducers({
  workItems: workItemsReducer,
  workflowItems: workflowsReducer,
  itemDetails: itemDetailsReducer,
});

export default rootReducer;
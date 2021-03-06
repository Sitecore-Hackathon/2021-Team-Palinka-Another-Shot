import { combineReducers } from "redux";

import workItemsReducer from "./workitems";

const rootReducer = combineReducers({
  workItems: workItemsReducer,
});

export default rootReducer;
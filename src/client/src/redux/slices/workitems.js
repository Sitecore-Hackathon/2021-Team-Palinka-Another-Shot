import { createSlice } from "@reduxjs/toolkit";
import { getChangeWorkflowApi, getWorkflowDetails } from "../../services/apis";

export const initialState = {
  loading: false,
  hasErrors: false,
  workItems: {},
};

const workItemsSlice = createSlice({
  name: "workItems",
  initialState,
  reducers: {
    changeWorkflow: state => {
      state.loading = true;
    },
    changeWorkflowSuccess: state => {
      state.loading = false;
      state.hasErrors = true;
    },
    changeWorkflowFailure: state => {
      state.loading = false;
      state.hasErrors = true;
    },
    getItems: state => {
      state.loading = true;
    },
    getItemsSuccess: (state, { payload }) => {
      state.workItems = payload;
      state.loading = false;
      state.hasErrors = false;
    },
    getItemsFailure: state => {
      state.loading = false;
      state.hasErrors = true;
    },
  },
});

export const {
  getItems,
  getItemsSuccess,
  getItemsFailure,
  changeWorkflow,
  changeWorkflowSuccess,
  changeWorkflowFailure,
} = workItemsSlice.actions;

export const workItemsSelector = state => state.workItems;

export default workItemsSlice.reducer;

export function fetchWorkItems(id) {
  const url = getWorkflowDetails().replace("{workflowid}", id);

  return async dispatch => {
    dispatch(getItems());

    try {
      const response = await fetch(url);
      const data = await response.json();
      dispatch(getItemsSuccess(data));
    } catch (error) {
      dispatch(getItemsFailure());
    }
  };
}

export function postChangeWorkflow(postData) {
  const url = getChangeWorkflowApi();

  return async dispatch => {
    dispatch(changeWorkflow());

    try {
      const response = await fetch(url, {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        headers: {
          "Content-Type": "application/json"
        },
        referrerPolicy: "no-referrer",
        body: JSON.stringify(postData)
      });
      const data = await response.json();
      dispatch(changeWorkflowSuccess(data));
    } catch (error) {
      dispatch(changeWorkflowFailure());
    }
  };
}
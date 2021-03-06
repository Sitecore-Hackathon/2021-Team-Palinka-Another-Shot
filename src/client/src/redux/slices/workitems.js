import { createSlice } from "@reduxjs/toolkit";
import { getWorkflowDetails } from "../../services/apis";

export const initialState = {
  loading: false,
  hasErrors: false,
  workItems: {},
};

const workItemsSlice = createSlice({
  name: "workItems",
  initialState,
  reducers: {
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

export const { getItems, getItemsSuccess, getItemsFailure } = workItemsSlice.actions;

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
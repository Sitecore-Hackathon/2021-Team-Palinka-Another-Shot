import { createSlice } from "@reduxjs/toolkit";
import getWorkflowApi from "../../services/apis";

export const initialState = {
  loading: false,
  hasErrors: false,
  workflowItems: [],
};

const workflowSlice = createSlice({
  name: "workflowItems",
  initialState,
  reducers: {
    getItems: state => {
      state.loading = true;
    },
    getItemsSuccess: (state, { payload }) => {
      state.workflowItems = payload;
      state.loading = false;
      state.hasErrors = false;
    },
    getItemsFailure: state => {
      state.loading = false;
      state.hasErrors = true;
    },
  },
});

export const { getItems, getItemsSuccess, getItemsFailure } = workflowSlice.actions;

export const workflowSelector = state => state.workflowItems;

export default workflowSlice.reducer;

export function fetchWorkflows() {
  const url = getWorkflowApi();

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
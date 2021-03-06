import { createSlice } from "@reduxjs/toolkit";
import getItemDetails from "../../services/apis";

export const initialState = {
  loading: false,
  hasErrors: false,
  itemDetails: {},
};

const itemDetailsSlice = createSlice({
  name: "itemDetails",
  initialState,
  reducers: {
    getItems: state => {
      state.loading = true;
    },
    getItemsSuccess: (state, { payload }) => {
      state.itemDetails = payload;
      state.loading = false;
      state.hasErrors = false;
    },
    getItemsFailure: state => {
      state.loading = false;
      state.hasErrors = true;
    },
  },
});

export const { getItems, getItemsSuccess, getItemsFailure } = itemDetailsSlice.actions;

export const itemDetailsSelector = state => state.itemDetails;

export default itemDetailsSlice.reducer;

export function fetchItemDetails(id, language) {
  const url = getItemDetails().replace("{itemId}", id).replace("{language}", language);
  
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
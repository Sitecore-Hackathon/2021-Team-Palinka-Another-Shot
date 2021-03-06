import { createSlice } from "@reduxjs/toolkit";

export const initialState = {
    loading: false,
    hasErrors: false,
    workItems: [],
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

export function fetchWorkItems() {
    return async dispatch => {
        dispatch(getItems());

        try {
            const response = await fetch("https://www.themealdb.com/api/json/v1/1/search.php?s=");
            const data = await response.json();
            dispatch(getItemsSuccess(data.meals));
        } catch (error) {
            dispatch(getItemsFailure());
        }
    };
}
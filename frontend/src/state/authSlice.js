import { createSlice } from "@reduxjs/toolkit";
const isAuthenticated = JSON.parse(localStorage.getItem("isAuthenticated")) || false; // Convert to boolean
const role = localStorage.getItem("role") || null;
const initialState ={
    isAuthenticated: isAuthenticated,
    role: role
}
const authSlice = createSlice({
    name:"auth",
    initialState,
    reducers:{
        login(state,action){
            console.log("Action Payload:", action.payload); // Log the entire payload
            state.isAuthenticated = action.payload.isAuthenticated;
            state.role = action.payload.role;
            localStorage.setItem("isAuthenticated", JSON.stringify(action.payload.isAuthenticated));
            localStorage.setItem("role", action.payload.role);
            console.log("Local Storage Updated:", localStorage.getItem("isAuthenticated"), localStorage.getItem("role"));
        },
    logout(state){
        state.isAuthenticated = false;
      state.role = null;
      localStorage.removeItem("isAuthenticated");
      localStorage.removeItem("role");
    }
}
});
export const { login, logout } = authSlice.actions;
export default authSlice.reducer;

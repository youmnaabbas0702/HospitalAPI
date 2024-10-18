import { createSlice } from "@reduxjs/toolkit";

const initialState ={
    doctors: [],
    patients: [],
}
const userSlice = createSlice({
    name:"user",
    initialState,
    reducers:{
setDoctors(state,action){
    state.doctors = action.payload;
},
addDoctor(state,action){
    state.doctors.push(action.payload);
},
updateDoctor:(state,action)=>{
    const index = state.doctors.findIndex(doc => doc.id === action.payload.id);
    if (index !== -1) {
        state.doctors[index] = action.payload;
    }
},
deleteDoctor: (state, action) => {
    state.doctors = state.doctors.filter(doc => doc.id !== action.payload);
},
setPatients(state,action){
    state.patients = action.payload;
},
addPatients(state,action){
    state.patients.push(action.payload);
},
updatePatiensts:(state,action)=>{
    const index = state.patients.findIndex(patient => patient.id === action.payload.id);
    if (index !== -1) {
        state.patients[index] = action.payload;
    }
},
deletePatients: (state, action) => {
    state.patients = state.doctors.filter(doc => doc.id !== action.payload);
},

}
});
export const { setDoctors, addDoctor, updateDoctor, deleteDoctor, setPatients,addPatients,updatePatiensts,deletePatients } = userSlice.actions;
export default userSlice.reducer;

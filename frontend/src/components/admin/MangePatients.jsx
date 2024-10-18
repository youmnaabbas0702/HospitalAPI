import { useState, useEffect } from "react";
import {useDispatch, useSelector } from "react-redux";
import { setPatients, addPatients, updatePatiensts, deletePatients } from "../../state/userSlice";
function MangePatients(params) {
    const dispatch = useDispatch();
    const patients = useSelector((state)=>state.user.patients);
    const [editingpatient, setEditingpatient] = useState(null); // Tracks the patient being edited
    const [showEditForm, setShowEditForm] = useState(false); // Controls form visibility
    const [showModal, setShowModal] = useState(false);
    const [newpatient, setNewpatient] = useState({ name: "", birthDate: "", phoneNumber: "" ,  medicalHistories:[]});

    console.log(patients);
    const handleEditClick = (patient) => {
        setEditingpatient(patient);
        setShowEditForm(true);
    };
    const handleSaveChanges = () => {
        fetch(`http://localhost:8000/patients/${editingpatient.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(editingpatient)
        })
        .then(response => response.json())
        .then(editingpatient => {
            dispatch(updatePatiensts(editingpatient));
            setShowEditForm(false);
            setEditingpatient(null);
        });
        console.log(`http://localhost:8000/patients/${editingpatient.id}`);

        console.log(editingpatient);
        console.log("edititng patient");

    };
    const handleDelete = (patientId) => {
        fetch(`http://localhost:8000/patients/${patientId}`, {
            method: "DELETE"
        })
        .then(() => {
            dispatch(deletePatients(patientId));
        });
    };
    const handleChange = (e) => {
        setEditingpatient({ ...editingpatient, [e.target.name]: e.target.value });
    };
    const handleAddpatient = (e) => {
        setNewpatient({...newpatient, [e.target.name]: e.target.value});
    }
    async function addPatients(){
    const respone = await fetch("http://localhost:8000/patients",{
        method:"POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(newpatient),
    });
    const addedPatient = respone.json();
    dispatch(addPatients(addPatients));
    setShowModal(false);
    setNewpatient({ name: "", birthDate: "", phoneNumber: "" ,  medicalHistories:[]});
    }

    console.log("patients:");
    console.log(patients);
    return(
        <div className="adamin-manage-patients">
            <div className="magange-tools">
                <input type="text" placeholder="search patient name" />
                <button onClick={()=>setShowModal(true)}>Add patient</button>
            </div>
            {showModal && (  <div className="modal-backdrop">
                    <div className="modal" style={{ display: showModal ? 'block' : 'none' }}>
                        <div className="modal-header">
                            <h5 className="modal-title">Add New patient</h5>
                            <button type="button" className="btn-close" onClick={() => setShowModal(false)}></button>
                        </div>
                        <div className="modal-body">
                            <form>
                                <div className="mb-3">
                                    <label>Name</label>
                                    <input
                                        type="text"
                                        name="name"
                                        value={newpatient.name}
                                        onChange={handleAddpatient}
                                        className="form-control"
                                    />
                                </div>
                                <div className="mb-3">
                                    <label>birthDate</label>
                                    <input
                                        type="date"
                                        name="birthDate"
                                        value={newpatient.birthDate}
                                        onChange={handleAddpatient}
                                        className="form-control"
                                    />
                                </div>
                                <div className="mb-3">
                                    <label>Phone Number</label>
                                    <input
                                        type="text"
                                        name="phoneNumber"
                                        value={newpatient.phoneNumber}
                                        onChange={handleAddpatient}
                                        className="form-control"
                                    />
                                </div>
                            </form>
                        </div>
                        <div className="modal-footer">
                            <button className="btn btn-secondary" onClick={() => setShowModal(false)}>Close</button>
                            <button className="btn btn-primary" onClick={addPatients}>Save changes</button>
                        </div>
                    </div>
                </div>)}
    <table className="table table-striped table-hover patients-container">
  <thead className="table-headlines">
    <tr>
      <th>Name</th>
      <th>birthDate</th>
      <th>Phone Number</th>
      <th>Actions</th>
    </tr>
  </thead>
  <tbody className="table-body">
    {patients.map((patient, index) => (
      <tr key={index}>
        <td>{patient.name}</td>
        <td>{patient.birthDate}</td>
        <td>{patient.phoneNumber}</td>
        <td>
          <button className="btn btn-info btn-sm me-2" >View</button>
          <button className="btn btn-warning btn-sm me-2" onClick={() => handleEditClick(patient)}>Edit</button>
          <button className="btn btn-danger btn-sm" onClick={() => handleDelete(patient.id)}>Delete</button>
        </td>
      </tr>
    ))
    }


  </tbody>
</table>

        </div>
    );
}
export default MangePatients;
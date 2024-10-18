import { useState , useEffect} from "react";
import {useDispatch, useSelector } from "react-redux";
import { setDoctors, addDoctor, updateDoctor, deleteDoctor } from "../../state/userSlice";
import "./admin.css";
function MangeDoctors(params) {
    const dispatch = useDispatch();
    const doctors = useSelector((state) => state.user.doctors);
    const [loading, setLoading] = useState(true);
    const [editingDoctor, setEditingDoctor] = useState(null); // Tracks the doctor being edited
    const [showEditForm, setShowEditForm] = useState(false); // Controls form visibility
    const [showModal, setShowModal] = useState(false);
    const [newDoctor, setNewDoctor] = useState({ name: "", speciality: "", phoneNumber: "" , email:"" , password: ""});

    console.log(doctors);
    const handleEditClick = (doctor) => {
        setEditingDoctor(doctor);
        setShowEditForm(true);
    };
    const handleSaveChanges = () => {
        fetch(`http://localhost:8000/doctors/${editingDoctor.id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(editingDoctor)
        })
        .then(response => response.json())
        .then(updatedDoctor => {
            dispatch(updateDoctor(updatedDoctor));
            setShowEditForm(false);
            setEditingDoctor(null);
        });
        console.log(`http://localhost:8000/doctors/${editingDoctor.id}`);

        console.log(editingDoctor);
        console.log("edititng doctor");

    };
    const handleDelete = (doctorId) => {
        fetch(`http://localhost:8000/doctors/${doctorId}`, {
            method: "DELETE"
        })
        .then(() => {
            dispatch(deleteDoctor(doctorId));
        });
    };
    const handleChange = (e) => {
        setEditingDoctor({ ...editingDoctor, [e.target.name]: e.target.value });
    };
    const handleAddDoctor = (e) => {
        setNewDoctor({...newDoctor, [e.target.name]: e.target.value});
    }
    async function addDoctor(){
    const response = await fetch("http://localhost:8000/doctors",{
        method:"POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(newDoctor),
    });
    console.log(response);
    const createdDoctor = await response.json();
    dispatch(addDoctor(createdDoctor));
    setShowModal(false);
    setNewDoctor({ name: "", speciality: "", phoneNumber: "" , email:"", password:""});
    }
    console.log("doctors");
    console.log(doctors);
    return(
        <div className="adamin-manage-doctors">
            <div className="magange-tools">
                <input type="text" placeholder="search doctor name" />
                <button onClick={()=>setShowModal(true)}>Add doctor</button>
            </div>
            {showModal && (  <div className="modal-backdrop">
                    <div className="modal" style={{ display: showModal ? 'block' : 'none' }}>
                        <div className="modal-header">
                            <h5 className="modal-title">Add New Doctor</h5>
                            <button type="button" className="btn-close" onClick={() => setShowModal(false)}></button>
                        </div>
                        <div className="modal-body">
                            <form>
                                <div className="mb-3">
                                    <label>Name</label>
                                    <input
                                        type="text"
                                        name="name"
                                        value={newDoctor.name}
                                        onChange={handleAddDoctor}
                                        className="form-control"
                                    />
                                </div>
                                
                                <div className="mb-3">
                                    <label>Email</label>
                                    <input
                                        type="email"
                                        name="email"
                                        value={newDoctor.email}
                                        onChange={handleAddDoctor}
                                        className="form-control"
                                    />
                                </div>

                                <div className="mb-3">
                                    <label>Password</label>
                                    <input
                                        type="password"
                                        name="password"
                                        value={newDoctor.password}
                                        onChange={handleAddDoctor}
                                        className="form-control"
                                    />
                                </div>
                                <div className="mb-3">
                                    <label>Speciality</label>
                                    <input
                                        type="text"
                                        name="speciality"
                                        value={newDoctor.speciality}
                                        onChange={handleAddDoctor}
                                        className="form-control"
                                    />
                                </div>
                                <div className="mb-3">
                                    <label>Phone Number</label>
                                    <input
                                        type="text"
                                        name="phoneNumber"
                                        value={newDoctor.phoneNumber}
                                        onChange={handleAddDoctor}
                                        className="form-control"
                                    />
                                </div>
                            </form>
                        </div>
                        <div className="modal-footer">
                            <button className="btn btn-secondary" onClick={() => setShowModal(false)}>Close</button>
                            <button className="btn btn-primary" onClick={addDoctor}>Save changes</button>
                        </div>
                    </div>
                </div>)}
            <table className="table table-striped table-hover doctors-container">
  <thead className="table-headlines">
    <tr>
      <th>Name</th>
      <th>Speciality</th>
      <th>Phone Number</th>
      <th>Actions</th>
    </tr>
  </thead>
  <tbody className="table-body">
    {doctors.map((doctor, index) => (
      <tr key={index}>
        <td>{doctor.name}</td>
        <td>{doctor.speciality}</td>
        <td>{doctor.phoneNumber}</td>
        <td>
          <button className="btn btn-info btn-sm me-2" >View</button>
          <button className="btn btn-warning btn-sm me-2" onClick={() => handleEditClick(doctor)}>Edit</button>
          <button className="btn btn-danger btn-sm" onClick={() => handleDelete(doctor.id)}>Delete</button>
        </td>
      </tr>
    ))}
  </tbody>
</table>
{showEditForm && (
                <div className="edit-form-modal">
                    <h3>Edit Doctor</h3>
                    <form>
                        <div className="mb-3">
                            <label>Name</label>
                            <input
                                type="text"
                                name="name"
                                value={editingDoctor.name}
                                onChange={handleChange}
                                className="form-control"
                            />
                        </div>
                        <div className="mb-3">
                            <label>Speciality</label>
                            <input
                                type="text"
                                name="speciality"
                                value={editingDoctor.speciality}
                                onChange={handleChange}
                                className="form-control"
                            />
                        </div>
                        <div className="mb-3">
                            <label>Phone Number</label>
                            <input
                                type="text"
                                name="phoneNumber"
                                value={editingDoctor.phoneNumber}
                                onChange={handleChange}
                                className="form-control"
                            />
                        </div>
                        <button type="button" className="btn btn-success me-2" onClick={handleSaveChanges}>Save</button>
                        <button type="button" className="btn btn-secondary" onClick={() => setShowEditForm(false)}>Cancel</button>
                    </form>
                </div>
            )}
        </div>
    );
}
export default MangeDoctors;
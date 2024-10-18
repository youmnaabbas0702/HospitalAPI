import { useState, useEffect } from "react";
import { useSelector } from "react-redux";
function MangeAppoints(params) {
    const [appoints,setappoints]=useState([]);
    const [showModal, setShowModal] = useState(false);
    const [newAppoint, setNewAppoint] = useState({ patientId: "", appointmentDate: "", doctorId: "", patientName:"", doctortName:""});
    const doctors = useSelector((state)=>state.user.doctors);
    const patients = useSelector((state)=>state.user.patients);
    useEffect(()=>{fetch("http://localhost:8000/appoints").then(response => response.json()).then(data => setappoints(data));},[]);
    console.log(appoints);


    const handleDelete = (appointmentId) => {
        fetch(`http://localhost:8000/appoints/${appointmentId}`, {
            method: "DELETE"
        })
        .then(() => {
            setappoints(appoints.filter(appoint => appoint.id !== appointmentId));
        });
    };

    const handleAddApoint = (e) => {
        setNewAppoint({...newAppoint, [e.target.name]: e.target.value});
    }
    async function addApoint(){
        const doctor = doctors.find((doc)=>doc.name === newAppoint.doctortName);
        console.log(newAppoint.doctortName);
        console.log(doctors);
        const patient = patients.find((pat)=>pat.name===newAppoint.patientName);
        if (!doctor) {
            alert("Doctor not found. Please enter a valid doctor name.");
            return;
        }

        if (!patient) {
            alert("Patient not found. Please enter a valid patient name.");
            return;
        }
        const appointmentData = {
            doctorId: doctor.id,
            patientId: patient.id,
            appointmentDate: newAppoint.appointmentDate,
            doctortName: newAppoint.doctortName,
            patientName: newAppoint.patientName,

        };

    await fetch("http://localhost:8000/appoints",{
        method:"POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(appointmentData),
    });
    setappoints([...appoints,newAppoint]);
    setShowModal(false);
    setNewAppoint({ });
    }
    return(
        <div className="adamin-manage-appoints">
            <div className="magange-tools">
                <input type="text" placeholder="search doctor name" />
                <button onClick={()=>setShowModal(true)}>Add Appoitment</button>
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
                                    <label>Doctor Name </label>
                                    <input
                                        type="text"
                                        name="doctortName"
                                        value={newAppoint.doctortName}
                                        onChange={handleAddApoint}
                                        className="form-control"
                                    />
                                </div>
                                
                                <div className="mb-3">
                                    <label>Patient Name</label>
                                    <input
                                        type="text"
                                        name="patientName"
                                        value={newAppoint.patientName}
                                        onChange={handleAddApoint}
                                        className="form-control"
                                    />
                                </div>

                                <div className="mb-3">
                                    <label>Date</label>
                                    <input
                                        type="Date"
                                        name="appointmentDate"
                                        value={newAppoint.appointmentDate}
                                        onChange={handleAddApoint}
                                        className="form-control"
                                    />
                                </div>
                            </form>
                        </div>
                        <div className="modal-footer">
                            <button className="btn btn-secondary" onClick={() => setShowModal(false)}>Close</button>
                            <button className="btn btn-primary" onClick={addApoint}>Save changes</button>
                        </div>
                    </div>
                </div>)}
            <table className="table table-striped table-hover appoints-container">
  <thead className="table-headlines">
    <tr>
      <th>Patient name</th>
      <th>Doctor Name</th>
      <th>Date</th>
      <th>Actions</th>
    </tr>
  </thead>
  <tbody className="table-body">
    {appoints.map((apoint, index) => (
      <tr key={index}>
        <td>{apoint.patientName}</td>
        <td>{apoint.doctortName}</td>
        <td>{apoint.appointmentDate}</td>
        <td>
          <button className="btn btn-danger btn-sm" onClick={() => handleDelete(apoint.id)}>Delete</button>
        </td>
      </tr>
    ))}
  </tbody>
</table>
        </div>
    );
}
export default MangeAppoints;
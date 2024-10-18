import { useEffect, useState } from "react";
function ManageSpecial() {
    const [special,setSpecial]=useState([]);
    const [newSpecial, setNewSpecial] = useState({ name: ""});
    useEffect(()=>{fetch("http://localhost:8000/special").then(response => response.json()).then(data => setSpecial(data));},[]);
    console.log(special);
    const [showModal,setShowModal] = useState(false);
    const handleDelete = async (specialId) => {
        try {
            // Fetch the details of the specialty to check if it's being used in appointments
            const response = await fetch(`http://localhost:8000/special/${specialId}`);
            const data = await response.json();
    
            if (data.length === 0) {
                // Proceed with deletion if no appointments use this specialty
                await fetch(`http://localhost:8000/special/${specialId}`, {
                    method: "DELETE",
                });
    
                // Update the state after deletion
                setSpecial((prevSpecials) => prevSpecials.filter((special) => special.id !== specialId));
            } else {
                alert("Can't delete this specialty. It is used in some appointments.");
            }
        } catch (error) {
            console.error("Error deleting specialty:", error);
        }
    };

    const handleAddSpecial = (e) => {
        setNewSpecial({...newSpecial, [e.target.name]: e.target.value});
    }
    async function addSpecial(){
        const min_special = special.find((doc)=>doc.name === newSpecial.name);
        console.log(newSpecial.name);
        if (min_special) {
            alert("this specilzsation exists before");
            return;
        }

    await fetch("http://localhost:8000/special",{
        method:"POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(newSpecial),
    });
    setSpecial([...special,newSpecial]);
    setShowModal(false);
    setNewSpecial({ });
    }
    return(
        <div className="adamin-manage-special">
            <div className="magange-tools">
                <input type="text" placeholder="search doctor name" />
                <button onClick={()=>setShowModal(true)}>Add Spesilzation</button>
            </div>
            {showModal && (  <div className="modal-backdrop">
                    <div className="modal" style={{ display: showModal ? 'block' : 'none' }}>
                        <div className="modal-header">
                            <h5 className="modal-title">Add New Spesizllation</h5>
                            <button type="button" className="btn-close" onClick={() => setShowModal(false)}></button>
                        </div>
                        <div className="modal-body">
                            <form>
                                <div className="mb-3">
                                    <label>Speciality Name </label>
                                    <input
                                        type="text"
                                        name="name"
                                        value={newSpecial.name}
                                        onChange={handleAddSpecial}
                                        className="form-control"
                                    />
                                </div>
                            </form>
                        </div>
                        <div className="modal-footer">
                            <button className="btn btn-secondary" onClick={() => setShowModal(false)}>Close</button>
                            <button className="btn btn-primary" onClick={addSpecial}>Save changes</button>
                        </div>
                    </div>
                </div>)}
            <table className="table table-striped table-hover special-container">
  <thead className="table-headlines">
    <tr>
      <th>Speciality name</th>
      <th>Actions</th>
    </tr>
  </thead>
  <tbody className="table-body">
    {special.map((special, index) => (
      <tr key={index}>
        <td>{special.name}</td>
        <td>
        <button className="btn btn-info btn-sm" onClick={() => handleDelete(special.id)}>Show</button>
          <button className="btn btn-danger btn-sm" onClick={() => handleDelete(special.id)}>Delete</button>
        </td>
      </tr>
    ))}
  </tbody>
</table>
        </div>
    );
}
export default ManageSpecial;
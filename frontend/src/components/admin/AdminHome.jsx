import image from '../../assests/background.jpg';
import "./admin.css";
import { Link } from 'react-router-dom';
import { useSelector } from "react-redux";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { setDoctors, setPatients } from "../../state/userSlice";
function AdminHomejsx(){
	const dispatch = useDispatch();
	useEffect(()=>{fetch("http://localhost:8000/doctors").then(response => response.json()).then(data => dispatch(setDoctors(data)));},[dispatch]);
    useEffect(()=>{fetch("http://localhost:8000/patients").then(response => response.json()).then(data => dispatch(setPatients(data)))},[dispatch]);
	const info = ["manage dctors", "manage patients", "manage appointments", "manage prescriptions"];
	return (
		<div className="admin-main">
		<div className="admin-container">
			<Link to="/mdoctors">
		<div className="card"style={{ width: '18rem' }}>
			<img className="card-img-top" src={image} alt="Card image cap"/>
			<div className="card-body">
				<p className="card-text">Mange doctors</p>
			</div>
		</div>
		</Link>
		<Link to="/mpatients">
		<div className="card"style={{ width: '18rem' }}>
			<img className="card-img-top" src={image} alt="Card image cap"/>
			<div className="card-body">
				<p className="card-text">Mange Patients</p>
			</div>
		</div>
		</Link>
		<Link to="/mspecialities">
		<div className="card"style={{ width: '18rem' }}>
			<img className="card-img-top" src={image} alt="Card image cap"/>
			<div className="card-body">
				<p className="card-text">Mange Spesilzation</p>
			</div>
		</div>
		</Link>
		<Link to="/mappoints">
		<div className="card"style={{ width: '18rem' }}>
			<img className="card-img-top" src={image} alt="Card image cap"/>
			<div className="card-body">
				<p className="card-text">Manage Appointments</p>
			</div>

		</div>
		</Link>
		</div>
		</div>
	);
}
export default AdminHomejsx;
import { Link } from "react-router-dom";
import { logout } from "../state/authSlice";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
function NaVBar(){
    const dispatch = useDispatch();
const navigate = useNavigate();
const handleSubmit= ()=>{
dispatch(logout());
navigate("/login");
}
return(
    <>
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
  <Link className="navbar-brand" to="#">Navbar</Link>
  <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
    <span className="navbar-toggler-icon"></span>
  </button>
  <div className="collapse navbar-collapse" id="navbarNavDropdown">
    <ul className="navbar-nav">
      <li className="nav-item active">
        <Link className="nav-link" to="/">Home <span className="sr-only">(current)</span></Link>
      </li>
    <li> <button className="btn btn-danger" onClick={handleSubmit}>Logout</button>
    </li>
    </ul>
  </div>
</nav>
    </>
);
}
export default NaVBar;
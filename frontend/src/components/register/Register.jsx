import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { register } from "../../https";
import './auth.css'
function Register() {
  const [first_name, setFirst_name] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [last_name, setLast_name] = useState("");
  const [phonenumber, setPhonenumber] = useState("");
  const [address, setAddress] = useState("");
  const [role, setRole] = useState("");
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await register({ first_name, email, password, last_name, phonenumber, address, role });
      navigate("/login");
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <>
      <div className="page">
        <div className="center">
          <form onSubmit={handleSubmit}>
            <div className="txt_felid">
              <input
                type="text"
                value={first_name}
                onChange={(e) => setFirst_name(e.target.value)}
                placeholder="Username"
              />
              <span></span>
              <label htmlFor="">Name</label>
            </div>
            <div className="txt_felid">
              <input
                type="text"
                value={last_name}
                onChange={(e) => setLast_name(e.target.value)}
                placeholder="LastName"
              />
              <span></span>
              <label htmlFor="">Last Name</label>
            </div>
            <div className="txt_felid">
              <input
                type="text"
                value={phonenumber}
                onChange={(e) => setPhonenumber(e.target.value)}
                placeholder="LastName"
              />
              <span></span>
              <label htmlFor="">Phone Number</label>
            </div>
            <div className="txt_felid">
              <input
                type="text"
                value={address}
                onChange={(e) => setAddress(e.target.value)}
                placeholder="LastName"
              />
              <span></span>
              <label htmlFor="">Adress</label>
            </div>
            <div className="txt_felid">
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Email"
              />
              <span></span>
              <label htmlFor="">Email</label>
            </div>
            <div className="txt_felid">
              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Password"
              />
              <span></span>
              <label htmlFor="">Password</label>
            </div>
            <div className="role_choice">
            <input
          type="radio"
          id="doctor"
          name="role"
          value="doctor"
          checked={role === 'doctor'}
          onChange={(e) => setRole(e.target.value)}
        />
        <label htmlFor="doctor">Doctor</label>
        <input
          type="radio"
          id="administrator"
          name="role"
          value="administrator"
          checked={role === 'administrator'}
          onChange={(e) => setRole(e.target.value)}
        />
        <label htmlFor="administrator">Administrator</label>
            </div>
            {error && <p>{error}</p>}
            <button type="submit" className="submitBtn re">
              {" "}
              Register
            </button>
          </form>
        </div>
      </div>
    </>
  );
}

export default Register;

import './auth.css'
import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { login } from "../../https";
import { login as loginAction } from "../../state/authSlice";
import { useEffect } from 'react';
function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [role, setRole] = useState("");
  const [error, setError] = useState(null);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
  useEffect(() => {
    if (isAuthenticated) {
      navigate('/');
    }
  }, [isAuthenticated, navigate]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const data = await login({ email, password, role });
      if(data.success){
      dispatch(loginAction({ isAuthenticated: true, role: role}));
      navigate("/");}
      else {
        setError(data.message || "Login failed");
    }
    } catch (err) {
      setError(err.message);
    }
  };
  console.log(email);
console.log(role);
console.log(password);
console.log("------------------");
  return (
    <>
      <div className="page">
        <div className="center">
          <h1>Login</h1>
          <form onSubmit={handleSubmit}>
            <div className="txt_felid">
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Email"
                required
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
                required
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
            <button type="submit" className="submitBtn">
              Login
            </button>
            <p>
              Don't have an account? register <Link to="/register">Here</Link>
            </p>
          </form>
        </div>
      </div>
    </>
  );
}

export default Login;

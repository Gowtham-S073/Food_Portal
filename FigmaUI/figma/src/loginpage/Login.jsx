
import React, { useState, useEffect, useContext } from "react";
import Axios from "axios";
import loginLogo from "../Assets/hutlogo.png";
import "../loginpage/login.css";
import logLogo from "../Assets/loglogo.png";
import { Button, Drawer, Box, List, ListItem, Divider, TextField } from "@mui/material";
import designimg from "../Assets/designimg.png";
import log from "../Assets/login.png";
import cookLady from '../Assets/cookLady.png';
import Typography from '@mui/material/Typography';
import { ApiContext } from "../components/ApiContextProvider";
import { Link, useNavigate } from "react-router-dom";
import { toast } from 'react-toastify';
import jwt_decode from 'jwt-decode';


const TemporaryDrawer = ({ anchor, open, onClose, children }) => {
  const toggleDrawer = (event) => {
    if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) {
      return;
    }
    onClose();
  };

  return (
    <Drawer anchor={anchor} open={open} onClose={toggleDrawer}>
      {children}
    </Drawer>
  );
};

const Login = () => {
  const [textIndex, setTextIndex] = useState(0);
  const texts = ["Welcome!", "Login to continue", "Sign up to get started"];
  const { setUserName, setUserRole, setUserToken,
    setUserMail, setUserFullName, setUserPhone
  } = useContext(ApiContext);

  useEffect(() => {
    const intervalId = setInterval(() => {
      setTextIndex((prevIndex) => (prevIndex + 1) % texts.length);
    }, 2000);

    return () => clearInterval(intervalId);
  }, []);

  const [inputValue, setInputValue] = useState("");
  const [isValid, setIsValid] = useState(true);
  const [isLoginDrawerOpen, setIsLoginDrawerOpen] = useState(false);
  const [isSignupDrawerOpen, setIsSignupDrawerOpen] = useState(false);

  const [isNewDrawerOpen, setIsNewDrawerOpen] = useState(false);

  const openNewDrawer = () => {
    setIsNewDrawerOpen(true);
  };

  const closeNewDrawer = () => {
    setIsNewDrawerOpen(false);
  };


  var navigate = useNavigate();
  const [login, setLogin] = useState({
    userName: "",
    password: "",
    token: "",
    role: "",
  });

  const [signup, setSignup] = useState({
    username: "",
    password: "",
    name: "",
    email: "",
    phoneNumber: "",
    address: "",
  });

  const handleInputChange = (event) => {
    setInputValue(event.target.value);
  };

  const handleButtonClick = () => {
    if (inputValue.trim() === "") {
      setIsValid(false);
    } else {
      setIsValid(true);
      console.log("Button clicked with input value:", inputValue);
    }
  };

  const handleLoginButtonClick = () => {
    setIsLoginDrawerOpen(true);
  };

  const handleSignupButtonClick = () => {
    setIsSignupDrawerOpen(true);
  };

  const closeLoginDrawer = () => {
    setIsLoginDrawerOpen(false);
  };

  const closeSignupDrawer = () => {
    setIsSignupDrawerOpen(false);
  };

  const handleLoginClick = async () => {
    try {
      // Validating input fields
      if (login.userName.trim() === "" || login.password.trim() === "") {
        toast.error("Username and password are required");
        return;
      }

      // Further validation can be added for specific requirements
      if (login.password.length < 6) {
        toast.error("Username and password are required");
        return;
      }
      console.log("username, pass :", login.userName, " , ", login.password)

      const response = await Axios.post(
        "https://localhost:7239/api/Users/LogIN", login,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );


      const user = response.data;
      console.log("Login response:", response.data);
      
      const decodedToken = jwt_decode(user.token);
      const mobilePhoneClaim = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone'];

      sessionStorage.setItem('role', decodedToken.role);
      sessionStorage.setItem('token', user.token);
      sessionStorage.setItem('userName', decodedToken.nameid);
      sessionStorage.setItem('mail', decodedToken.email);
      sessionStorage.setItem('name', decodedToken.unique_name);
      sessionStorage.setItem('phoneNumber', mobilePhoneClaim);
      setUserRole(decodedToken.role);
      setUserToken(user.token);
      setUserName(decodedToken.nameid);
      setUserMail(decodedToken.email);
      setUserFullName(decodedToken.unique_name);
      setUserPhone(mobilePhoneClaim);
      setIsLoginDrawerOpen(false);
      toast.success('Welcome ' + decodedToken.nameid, {
        position: 'top-right',
        autoClose: 2000
      });
      navigate('/home');
    } catch (error) {
      console.error("Login error:", error);
      if (error.response && error.response.status === 404) {
        toast.error(error.response.data.message, {
          position: "top-left",
        });
      } else {
        toast.error(error.response.data.message, {
          position: "top-left",
        });
      }
    }
  };

  const handleSignupClick = async () => {
    try {
      // Validating input fields
      if (
        signup.username.trim() === "" ||
        signup.password.trim() === "" ||
        signup.name.trim() === "" ||
        signup.email.trim() === "" ||
        signup.phoneNumber.trim() === "" ||
        signup.address.trim() === ""
      ) {
        alert("All fields are required.");
        return;
      }

      // Further validation can be added for specific requirements
      if (signup.password.length < 8 || !/^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,}$/.test(signup.password)) {
        alert("Password should be at least 8 characters long and contain a special character and a number.");
        return;
      }

      const response = await Axios.post(
        "https://localhost:7239/api/Users/Register",
        {
          name: signup.name,
          emailId: signup.email,
          userName: signup.username,
          phoneNumber: signup.phoneNumber,
          address: signup.address,
          role: "user",
          userPassword: signup.password,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      console.log("Signup response:", response.data);

      setIsSignupDrawerOpen(false);

        toast.success("Signed up successfully!", {
        position: "top-left",
      });
      
    } catch (error) {
      console.error("Login error:", error);
      if (error.response && error.response.status === 400) {
        toast.error(error.response.data.message, {
          position: "top-left",
        });
      } else {
        toast.error(error.response.data.message, {
          position: "top-left",
        });
      }
    }
  };

  return (
    <div className="splitContainer">
      {/* Left Part */}
      <div className="leftPart">
        <img className="loglogo" src={logLogo} alt="" />

        <div className="logbut">
          <button className="loginButton" onClick={handleLoginButtonClick}>
            Login
          </button>
          <button className="signupButton" onClick={handleSignupButtonClick}>
            Signup
          </button>
        </div>

        <div className="changingText">
          <h1>{texts[textIndex]}</h1>
        </div>

        <h4 className="sub">Order your favourite food with us.</h4>

      </div>

      {/* Design Image */}
      <div className="desimg">
        <img src={designimg} alt="" />
      </div>

      {/* Right Part */}
      <div className="rightPart">
        <img className="hutLogo" src={loginLogo} alt="" />
      </div>

      {/* Login Drawer */}
      <TemporaryDrawer anchor="right" open={isLoginDrawerOpen} onClose={closeLoginDrawer}>
        <strong className="logintitle" style={{ justifyContent: 'center', marginLeft: '300px', marginTop: '50px' }}> LOGIN HERE</strong>
        <Box className="LoginDrawer" sx={{ width: 730 }} role="presentation" onClick={(e) => e.stopPropagation()} onKeyDown={(e) => e.stopPropagation()}>
          <List>
            <ListItem sx={{ justifyContent: 'center' }}>
              <TextField
                sx={{ width: '300px' }}
                label="Username"
                variant="outlined"
                onChange={(event) => setLogin({ ...login, userName: event.target.value })}
                error={!isValid && login.userName === ""}
                helperText={!isValid && login.userName === "" ? "Username is required" : ""}
              />
            </ListItem>
            <ListItem sx={{ justifyContent: 'center' }}>
              <TextField
                sx={{ width: '300px' }}
                label="Password"
                variant="outlined"
                type="password"
                onChange={(event) => setLogin({ ...login, password: event.target.value })}
                error={!isValid && login.password === ""}
                helperText={!isValid && login.password === "" ? "Password is required" : ""}
                inputProps={{
                  maxLength: 14,
                }}
              />
            </ListItem>

          </List>
          <Divider />
          <Button className="LoginButton" sx={{
            backgroundColor: "#ff6d00", display: "flex", justifyContent: "center", marginLeft: '215px', width: '300px', padding: '15px', '&:hover': {
              backgroundColor: "orangered", // Change this to your desired hover color

            },
          }} variant="contained" color="primary" onClick={handleLoginClick}>
            Login
          </Button>
          <img className="LoginImage" src={log} style={{ height: '704px', width: '679px', marginTop: '82px' }} alt="" />
        </Box>
      </TemporaryDrawer>

      {/* Signup Drawer */}
      <TemporaryDrawer anchor="right" open={isSignupDrawerOpen} onClose={closeSignupDrawer}>
        <strong className="SignTitle" style={{ marginLeft: '300px', marginTop: '30px' }}> SIGNUP HERE</strong>
        <Box className="signupDrawer" sx={{ width: 730, justifyContent: "center", overflowY: 'hidden' }} role="presentation" onClick={(e) => e.stopPropagation()} onKeyDown={(e) => e.stopPropagation()}>
          <List>
            <ListItem sx={{ justifyContent: 'center' }}>
              <TextField
                sx={{ width: '300px' }}
                label="Name"
                variant="outlined"
                onChange={(event) => {
                  const name = event.target.value.replace(/[^A-Za-z\s]/g, ''); // Remove non-alphabet characters
                  setSignup({ ...signup, name });
                }}
                value={signup.name}
                error={
                  signup.name.trim() !== "" && !/^[A-Za-z\s]+$/.test(signup.name)
                }
                helperText={
                  signup.name.trim() !== ""
                    ? !/^[A-Za-z\s]+$/.test(signup.name)
                      ? "Name should contain only alphabets"
                      : ""
                    : ""
                }
              />
            </ListItem>

            <ListItem sx={{ justifyContent: 'center' }}>
              <TextField
                sx={{ width: '300px' }}
                label="Email"
                variant="outlined"
                onChange={(event) =>
                  setSignup({ ...signup, email: event.target.value })
                }
                error={
                  signup.email.trim() !== "" && // Check if there's non-whitespace content
                  (!isValid || !/\S+@\S+\.\S+/.test(signup.email))
                }
                helperText={
                  signup.email.trim() !== ""
                    ? !isValid
                      ? "Email is required"
                      : !/\S+@\S+\.\S+/.test(signup.email)
                        ? "Invalid email format"
                        : ""
                    : ""
                }
              />
            </ListItem>

            <ListItem sx={{ justifyContent: 'center' }}>
              <TextField
                sx={{ width: '300px' }}
                label="Username"
                variant="outlined"
                onChange={(event) => setSignup({ ...signup, username: event.target.value })}
              />
            </ListItem>
            <ListItem sx={{ justifyContent: 'center' }}>
              <TextField
                sx={{ width: '300px' }}
                label="Phone Number"
                variant="outlined"
                onChange={(event) => {
                  const phoneNumber = event.target.value;
                  if (/^\d{0,10}$/.test(phoneNumber)) {
                    setSignup({ ...signup, phoneNumber });
                  }
                }}
                value={signup.phoneNumber}
                error={signup.phoneNumber && (!/^\d{10}$/.test(signup.phoneNumber) || signup.phoneNumber.length !== 10)}
                helperText={
                  signup.phoneNumber && (!/^\d{10}$/.test(signup.phoneNumber) || signup.phoneNumber.length !== 10)
                    ? "Phone number should contain exactly 10 digits and no alphabets."
                    : ""
                }
              />
            </ListItem>

            <ListItem sx={{ justifyContent: 'center' }}>
              <TextField
                sx={{ width: '300px' }}
                label="Address"
                variant="outlined"
                onChange={(event) => setSignup({ ...signup, address: event.target.value })}
              />
            </ListItem>
            <ListItem sx={{ justifyContent: 'center' }}>
              <TextField
                sx={{ width: '300px' }}
                label="Password"
                variant="outlined"
                type="password"
                onChange={(event) => setSignup({ ...signup, password: event.target.value })}
                error={!isValid && signup.password === ""}
                helperText={
                  !isValid && signup.password === ""
                    ? "Password is required"
                    : !/^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,20}$/.test(signup.password)
                      ? "Password should be between 8 and 20 characters long and contain a special character and a number."
                      : ""
                }
              />
            </ListItem>
          </List>
          <Divider />
          <Button className="SignUpButton"
            sx={{
              marginLeft: '215px',
              width: '300px',
              padding: '15px',
              backgroundColor: "#ff6d00",
              '&:hover': {
                backgroundColor: "orangered",

              },
            }}
            variant="contained"
            onClick={handleSignupClick}
          >
            Sign Up
          </Button>

          <div className="signUpDrawerRoute">
            <p>
              if already an user{' '}
              <Button onClick={handleLoginButtonClick}>CLICK HERE</Button>
            </p>
          </div>

          <img className="Cookimage" src={cookLady} style={{ height: '404px', width: '479px', marginTop: '42px', marginLeft: '120px' }} alt="" />
        </Box>
      </TemporaryDrawer>
    </div>
  );
};

export default Login;

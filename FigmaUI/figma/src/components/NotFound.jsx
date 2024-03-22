import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { ApiContext } from '../components/ApiContextProvider';
import { Button } from "@mui/material";
import logLogo from "../Assets/loglogo.png";
import errorimgn from '../Assets/404.png';

const NotFound = () => {
  const { userRole } = useContext(ApiContext);

  const containerStyle = {
    display: 'flex',
    justifyContent: 'flex-start', // Align content to the left
    alignItems: 'center', // Align content vertically
    height: '100vh', // Adjust the height to cover the viewport
  };
  const buttonContainerStyle = {
    position: 'absolute',
    bottom: '300px',
    left: '120px',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'flex-end',
    
  };


  const imageStyle = {
    width: '50%', // Adjust the width as needed
    marginLeft: 'auto', // Move the image to the right
  };
  const buttonStyle = {
    margin: '10px',
    fontWeight: 'bold',
    fontSize: '18px',
    
  };

  const logoStyle = {
    width: '500px', // Adjust the width as needed
    position: 'absolute',
    top: 0,
    left: '5px'
  };

  const pStyle = {
    padding: "0 100px 100px", // Padding at the bottom, top and sides remain 0
    fontSize: "58px",
    color: "black",
    textAlign: "center",
  };

  return (
    <div style={containerStyle}>
      <img src={logLogo} alt="Logo" style={logoStyle} />
      <p style={pStyle}>404 Error Page Not Found</p>
      <img src={errorimgn} alt="Error" style={imageStyle} />
      <div style={buttonContainerStyle}>
        {userRole ? (
          <Link to="/home">
            <Button sx={{ backgroundColor: "#ff6d00", width: '280px', height: '50px',   '&:hover': {
                backgroundColor: "orangered", // Change this to your desired hover color

              }, }} variant="contained" color="primary" style={buttonStyle}>
              Go back to Home
            </Button>
          </Link>
        ) : (
          <Link to="/">
            <Button sx={{ backgroundColor: "#ff6d00", width: '280px', height: '50px' ,   '&:hover': {
                backgroundColor: "orangered", // Change this to your desired hover color

              },}} variant="contained" color="primary" style={buttonStyle}>
              Go back to login
            </Button>
          </Link>
        )}
      </div>
    </div>
  );
};

export default NotFound;

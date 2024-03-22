import React from "react";
import { RWebShare } from "react-web-share";

const FoodPortal = () => {
  const portalUrl = "http://www.foodhut.com"; // Replace with your actual Food Hut URL

  const buttonStyle = {
    background: "#FF6D00",
    color: "white",
    border: "none",
    padding: "10px 20px",
    cursor: "pointer",
    fontSize: "16px",
    borderRadius: "5px",
    textDecoration: "none",
    outline: "none",
    transition: "background 0.3s ease-in-out",
    ":hover": {
      background: "darkorange",
    },
  };

  return (
    <div>
      <h1>Welcome to Food Hut!</h1>
      {/* Your food portal content */}
      <RWebShare
        data={{
          text: "Check out Food Hut for delicious meals!",
          url: portalUrl,
          title: "Food Hut - Delicious Meals",
        }}
        onClick={() => console.log("Shared Food Hut successfully!")}
      >
        <button style={buttonStyle}>Share<i class='fas fa-hamburger'></i></button>
      </RWebShare>
    </div>
  );
};

export default FoodPortal;

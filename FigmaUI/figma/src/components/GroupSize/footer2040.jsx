// Footer2040.js
import React, { useEffect, useState } from "react";
import './footer2040.css';
import { Link } from 'react-router-dom';

const buttonDisabledStyle = {
  backgroundColor: '#C1C8CF',
  color: '#FFFFFF',
  fontWeight: 500,
  cursor: 'not-allowed',
  borderStyle: 'none',
};

const Footer2040 = ({ progressPercentage, isGroupSizeSelected }) => {
  const [percentageLeft, setPercentageLeft] = useState(20);

  useEffect(() => {
    if (progressPercentage === 40) {
      setPercentageLeft(28);
    } else {
      setPercentageLeft(progressPercentage);
    }
  }, [progressPercentage]);

  return (
    <div className="footer2040">
      <div className="footer">
        <Link to="/dateandtime" style={{ color: "orangered", textDecoration: "none" }}>
          <button type="button" className="btn">

            &#60; Back

          </button>   </Link>
        <div className="progres-bar">
          <div className="progres" style={{ width: `${progressPercentage}%` }}></div>
        </div>
        <h6 id="percentages" className="mt-2" style={{ left: `${percentageLeft}%` }}>
          {progressPercentage}%
        </h6>
        <h5 className="step1">
          Step<span className="spanorange">1</span> of 3
        </h5>
        <Link to="/foodtype" style={{ color: 'white', textDecoration: "none" }}>
          <button
            type="button"
            id="btn1"
            disabled={!isGroupSizeSelected} // Disable button if isGroupSizeSelected is false
            style={isGroupSizeSelected ? null : buttonDisabledStyle}
          >

            Continue &#62;</button>
        </Link>

      </div>
    </div>

  );
};

export default Footer2040;

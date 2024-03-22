import React, { useContext, useEffect, useState } from "react";
import Vector from '../../Assets/Vector.png';
import Group from '../../Assets/Group.png';
import Veg from '../../Assets/veg.png';
import Nonveg from '../../Assets/nonveg.png';
import BtmImg from '../../Assets/bg 1.png';
import 'bootstrap/dist/css/bootstrap.css';
import './FoodType.css';
import Header from '../Header/header';
import { Link } from 'react-router-dom';
import { ApiContext } from "../ApiContextProvider";

// Categories are not yet loaded, show a loading indicator or handle accordingly
import { Button } from "@mui/material";
import designimg from "../../Assets/designimg.png";
import logLogo from "../../Assets/loglogo.png";
import errpage from "../../Assets/loginPage .png";

const FoodType = () => {
  const { totalSizeCount, selectedFoodTypes, setSelectedFoodTypes, setOnlyVeg, setOnlyNonVeg, setTypeFood, finalOrder, timeSlotName,
    isCardClicked, setIsCardClicked, formData, setFormData} = useContext(ApiContext);

  // const [formData, setFormData] = useState({
  //   selectedOption: '',
  //   vegCount: Math.floor(totalSizeCount / 2),
  //   nonvegCount: Math.floor(totalSizeCount / 2),
  // });

  const [progressPercentage, setProgressPercentage] = useState(40);
  const [percentageLeft, setPercentageLeft] = useState(40);
  // const [isCardClicked, setIsCardClicked] = useState(false);

  useEffect(() => {
    // Calculate counts based on totalSizeCount
    const halfTotal = Math.floor(totalSizeCount / 2);
    const isTotalSizeCountOdd = totalSizeCount % 2 !== 0;

    setFormData({
      ...formData,
      vegCount: isTotalSizeCountOdd ? halfTotal + 1 : halfTotal,
      nonvegCount: halfTotal,
    });

  }, [totalSizeCount]);

  //Percentage position
  useEffect(() => {
    const newPercentageLeft = 30;
    setPercentageLeft(newPercentageLeft);
    if (progressPercentage === 60) {
      setPercentageLeft(40);
    }

  }, [progressPercentage]);

  //Calculate the count for Veg to Increment
  const incrementVeg = () => {

    if (formData.vegCount < totalSizeCount) {
      const newVegCount = formData.vegCount + 1;
      const newNonvegCount = totalSizeCount - newVegCount;
      const newProgressPercentage = 60;

      setFormData({ ...formData, vegCount: newVegCount, nonvegCount: newNonvegCount });
      setProgressPercentage(newProgressPercentage);

      const updatedVegFoodType = selectedFoodTypes.find(foodType => foodType.isVeg);
      updatedVegFoodType.foodTypeCount1 = newVegCount;

      const updatedNonVegFoodType = selectedFoodTypes.find(foodType => !foodType.isVeg);
      updatedNonVegFoodType.foodTypeCount1 = newNonvegCount;

      setSelectedFoodTypes([...selectedFoodTypes]);
    }
  };

  //Calculate the count for Veg to Decrement
  const decrementVeg = () => {

    if (formData.vegCount > 0) {
      const newVegCount = formData.vegCount - 1;
      const newNonvegCount = totalSizeCount - newVegCount;
      const newProgressPercentage = 60;

      setFormData({ ...formData, vegCount: newVegCount, nonvegCount: newNonvegCount });
      setProgressPercentage(newProgressPercentage);

      const updatedVegFoodType = selectedFoodTypes.find(foodType => foodType.isVeg);
      updatedVegFoodType.foodTypeCount1 = newVegCount;

      const updatedNonVegFoodType = selectedFoodTypes.find(foodType => !foodType.isVeg);
      updatedNonVegFoodType.foodTypeCount1 = newNonvegCount;

      setSelectedFoodTypes([...selectedFoodTypes]);
    }
  };

  //Calculate the count for NonVeg to Increment
  const incrementNonveg = () => {

    if (formData.nonvegCount < totalSizeCount) {
      const newNonvegCount = formData.nonvegCount + 1;
      const newVegCount = totalSizeCount - newNonvegCount;
      const newProgressPercentage = 60;

      setFormData({ ...formData, vegCount: newVegCount, nonvegCount: newNonvegCount });
      setProgressPercentage(newProgressPercentage);

      const updatedNonVegFoodType = selectedFoodTypes.find(foodType => !foodType.isVeg);
      updatedNonVegFoodType.foodTypeCount1 = newNonvegCount;

      const updatedVegFoodType = selectedFoodTypes.find(foodType => foodType.isVeg);
      updatedVegFoodType.foodTypeCount1 = newVegCount;

      setSelectedFoodTypes([...selectedFoodTypes]);
    }
  };

  //Calculate the count for Nonveg to Decrement
  const decrementNonveg = () => {

    if (formData.nonvegCount > 0) {
      const newNonvegCount = formData.nonvegCount - 1;
      const newVegCount = totalSizeCount - newNonvegCount;
      const newProgressPercentage = 60;

      setFormData({ ...formData, vegCount: newVegCount, nonvegCount: newNonvegCount });
      setProgressPercentage(newProgressPercentage);

      const updatedVegFoodType = selectedFoodTypes.find(foodType => foodType.isVeg);
      updatedVegFoodType.foodTypeCount1 = newVegCount;
      const updatedNonVegFoodType = selectedFoodTypes.find(foodType => !foodType.isVeg);
      updatedNonVegFoodType.foodTypeCount1 = newNonvegCount;

      setSelectedFoodTypes([...selectedFoodTypes]);
    }
  };

  //Choosing Options
  const handleOptionChange = (option) => {

    let vegCount = formData.vegCount;
    let nonvegCount = formData.nonvegCount;
    if (option === 'veg') {
      nonvegCount = totalSizeCount - vegCount;
    } else if (option === 'nonveg') {
      vegCount = totalSizeCount - nonvegCount;
    }

    setFormData({
      ...formData,
      selectedOption: option,
      vegCount: vegCount,
      nonvegCount: nonvegCount,
    });
    setIsCardClicked(true);

    if (option === 'veg') {
      setOnlyVeg(true);
      setOnlyNonVeg(false);
    } else if (option === 'nonveg') {
      setOnlyVeg(false);
      setOnlyNonVeg(true);
    } else {
      setOnlyVeg(true);
      setOnlyNonVeg(true);
    }

    if (option === 'both') {
      const vegFoodTypeModel = {
        orderId: 1,
        foodTypeCount1: formData.vegCount,
        isVeg: true,
        plateSizeId: finalOrder.plateSizeId,
      };
      const nonVegFoodTypeModel = {
        orderId: 2,
        foodTypeCount1: formData.nonvegCount,
        isVeg: false,
        plateSizeId: finalOrder.plateSizeId,
      };
      setSelectedFoodTypes([vegFoodTypeModel, nonVegFoodTypeModel]);
    } else {
      const foodTypeModel = {
        orderId: 1,
        foodTypeCount1: totalSizeCount,
        isVeg: option !== 'nonveg',
        plateSizeId: finalOrder.plateSizeId,
      };
      setSelectedFoodTypes([foodTypeModel]);
    }

    let updatedTypeFood;
    if (option === 'both') {
      updatedTypeFood = 'Veg & NonVeg';
    } else if (option === 'veg') {
      updatedTypeFood = 'Veg';
    } else if (option === 'nonveg') {
      updatedTypeFood = 'NonVeg';
    }

    setTypeFood(updatedTypeFood);
  };

  //Disable button
  useEffect(() => {
    console.log("Selected Food Types:", selectedFoodTypes);
  }, [selectedFoodTypes]);

  const buttonDisabledStyle = {
    backgroundColor: '#C1C8CF',
    color: '#FFFFFF',
    fontWeight: 500,
    cursor: 'not-allowed',
    borderStyle: 'none',

  };

  //Passing the Date
  const formatDate = (dateString) => {
    const options = { month: 'long', year: 'numeric' };
    const formattedDate = new Date(dateString).toLocaleDateString('en-US', options);

    const day = new Date(dateString).getDate();
    const suffix = day === 1 ? 'st' : day === 2 ? 'nd' : day === 3 ? 'rd' : 'th';

    return `${day}${suffix} ${formattedDate}`;
  };

  //To display the increment decrement option only for Veg & Non-Veg
  const toggleCardStyles = (event) => {
    const card = event.currentTarget;
    const isVegNonvegCard = card.classList.contains("veg-nonveg-card");
    setFormData({ ...formData, selectedOption: isVegNonvegCard ? 'both' : '' });
  };


    if (finalOrder.date === '' || timeSlotName === '') {
      // Categories are not yet loaded, show a loading indicator or handle accordingly
      return (

        <div>

  <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start', justifyContent: 'center', height: '70vh', paddingLeft: '20px' }}>
    <img className="loglogo" src={logLogo} alt="" /> <p style={{

        padding: "20px",
        fontSize: "38px",
        color: "black",
        textAlign: "center",
      }}>
        Go Back to Select previous options
      </p>
      <div className="img" style={{ position: 'fixed', bottom: 0, left: 0 }}>
        <img src={designimg} alt="" />
      </div>
      <div className="imgside" style={{ position: 'fixed', bottom: 0, right: 0 }}>
        <img src={errpage} alt="" />
      </div>
      <Link to='/dateandtime'>
      <Button sx={{ backgroundColor: "#ff6d00",color:'rgb(255, 255, 255)', alignItems: 'flex-end', justifyContent: "center", marginTop: '20px', marginLeft: '220px', width: '180px', height: '50px', fontWeight: 'bold', fontSize: '18px',   '&:hover': {
                  backgroundColor: "orangered", // Change this to your desired hover color

                } }} >
    Go Back
  </Button>


      </Link>
    </div>
  </div>

      );
    }

  return (
    <div>
      <Header />

      <div className="full" >
      <Link to="/groupsize" style={{ color: "orangered", textDecoration: "none" }}><button type="button" className="topbackbtn">&#60; Back</button></Link>
        <h3 className="h3tag">Hey! Pick your choice here</h3>
        <p class="ptag">
          {formatDate(finalOrder.date)}, {timeSlotName}, {finalOrder.address}<span className="spanorange">|</span> {totalSizeCount} Guests
        </p>
        <br />
        <div className="row align-items-center" style={{ marginTop: -10 }}>
          <div className="col-auto">
            <img src={Group} className="imgcook" style={{ width: 30, height: 30 }} alt="Group" />
          </div>
          <div className="col">
            <h5 className="h5tagg">Select Food Type</h5>
          </div>
        </div>

        <br />
        <br></br>
        <div className="row" style={{ marginTop: -20 }}>
          <div
            className={`cardvn ${formData.selectedOption === 'veg' ? 'clicked' : ''}`}
            onClick={(event) => {
              toggleCardStyles(event);
              handleOptionChange('veg');
            }}
          >
            <img src={Veg} style={{ width: 40, height: 40, marginTop: 10 }} alt="Veg" />
            <br />
            <p class="ptag mt-3" className="boxtag" style={{ fontSize: 20 }}>Veg</p>
          </div>
          <div
            className={`cardvn mx-5 ${formData.selectedOption === 'nonveg' ? 'clicked' : ''}`}
            onClick={(event) => {
              toggleCardStyles(event);
              handleOptionChange('nonveg');
            }}
          >
            <img src={Nonveg} style={{ width: 40, height: 40, marginTop: 10 }} alt="Non-Veg" />
            <br />
            <p class="ptag mt-3" className="boxtag" style={{ fontSize: 20 }}>Non-Veg</p>
          </div>
          <div
            className={`cardvn veg-nonveg-card ${formData.selectedOption === 'both' ? 'clicked' : ''}`}
            onClick={(event) => {
              toggleCardStyles(event);
              handleOptionChange('both');
            }}
          >
            <div className="row">
              <div className="d-flex">
                <img src={Veg} className="imgveg" style={{ width: 40, height: 40, marginTop: 10, marginLeft: 28 }} alt="Veg" />
                <img src={Nonveg} className="imgnonveg" style={{ width: 40, height: 40, marginTop: 10, marginLeft: 10 }} alt="Non-Veg" />
              </div>
            </div>
            <br />
            <p class="ptag veg-nonveg-text" className="boxtag" style={{ marginTop: -5, fontSize: 20 }}>Veg & <br></br> Non-Veg</p>
          </div>
        </div>
        <br />
        <br />

        {formData.selectedOption === 'both' && (
          <div className="second">
            <div className="row align-items-center">
              <div className="col-auto">
                <img src={Vector} className="imgcook" style={{ width: 30, height: 30 }} alt="Vector" />
              </div>
              <div className="col">
                <div className="d-flex align-items-center">
                  <h5 className="h5tagg" class="counth5">Select Veg & Non-Veg Count</h5>
                  <h6 className="mx-5" class="h6image mx-5" style={{ marginTop: -30, marginLeft: -140 }}>
                    <img src={Vector} style={{ width: 20, height: 20 }} alt="Vector" />
                    &nbsp;Total - {totalSizeCount}
                  </h6>
                </div>
              </div>
            </div>

            <p class="mt-1" className="ptag">You can select numbers for food type up to {totalSizeCount} people</p>

            <table>
              <tbody>
                <tr>
                  <td class="tdtag"><p class="ptag">Veg</p></td>
                  <td class="tdtag"><p class="ptag">Non-Veg</p></td>
                </tr>
                <tr>
                  <td class="tdtag">
                    <button className="count-button" onClick={decrementVeg}>-</button>
                    <span className="count mx-1 my-1">{formData.vegCount}</span>
                    <button className="count-button" onClick={incrementVeg}>+</button>
                  </td>

                  <td class="tdtag">
                    <button className="count-button" onClick={decrementNonveg}>-</button>
                    <span className="count mx-1 my-1">{formData.nonvegCount}</span>
                    <button className="count-button" onClick={incrementNonveg}>+</button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        )}

        <img src={BtmImg} alt="Bottom" className="btm-img" />
      </div>

      <div className="foot">
        <Link to="/groupsize" style={{ color: "orangered", textDecoration: "none" }}><button type="button" className="backbtn">&#60; Back</button></Link>
        <div className="progress-bars">
          <div className="progress" style={{ width: `${progressPercentage}%`, left: 0 }}></div>
        </div>
        <h6 id="percentage" style={{ left: `${percentageLeft}%`, top: 60 }}>
          {progressPercentage}%
        </h6>
        <h5 className="step">Step<span className="spanorange">3</span>of 5</h5>
        <Link to="/platesize" style={{ textDecoration: 'none', color: "white" }}> <button type="button" id="continuebtn" disabled={!isCardClicked}
          style={isCardClicked ? null : buttonDisabledStyle}  >Continue &#62;</button></Link>
      </div>
    </div>
  );
};

export default FoodType;
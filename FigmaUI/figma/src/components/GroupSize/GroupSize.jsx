import React, { useContext, useEffect, useState } from "react";
import Vector from './../../Assets/Vector.png';
import Emoji1 from '../../Assets/Emoji1.png';
import GroupSizeImage from '../../Assets/GroupSize.png';
import './GroupSize.css';
import BtmImg from '../../Assets/bg 1.png';
import 'bootstrap/dist/css/bootstrap.css';
import Header from '../Header/header';
import { Variable } from "../Variables";
import Footer2040 from "./footer2040";
import { ApiContext } from "./../ApiContextProvider";
import { Link, useNavigate } from "react-router-dom";
import { Navigate } from "react-router-dom";
// Categories are not yet loaded, show a loading indicator or handle accordingly
import { Button } from "@mui/material";
import designimg from "../../Assets/designimg.png";
import logLogo from "../../Assets/loglogo.png";
import errpage from "../../Assets/loginPage .png";

const GroupSize = () => {

  const [showAlertPopup, setShowAlertPopup] = useState(false);
  const {totalSizeCount, setTotalSizeCount, finalOrder, setFinalOrder, setGuestCount, timeSlotName,
    selectedOption, setSelectedOption,
    popupVisible, setPopupVisible,
    allSizes, setAllSizes,
    progressPercentage, setProgressPercentage,
    isGroupSizeSelected, setIsGroupSizeSelected,
    adultCount, setAdultCount,
    kidsCount, setKidsCount

  } = useContext(ApiContext);

  var navigate = useNavigate();

  const formatDate = (dateString) => {
    const options = { month: 'long', year: 'numeric' };
    const formattedDate = new Date(dateString).toLocaleDateString('en-US', options);

    const day = new Date(dateString).getDate();
    const suffix = day === 1 ? 'st' : day === 2 ? 'nd' : day === 3 ? 'rd' : 'th';

    return `${day}${suffix} ${formattedDate}`;
  };

  useEffect(() => {
    const fetchSizes = async () => {
      try {
        const response = await fetch(Variable.Group_Sizes_Url, {
          "method": "GET",
          headers: {
            "accept": "text/plain",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
          },
        });
      if (response.status === 200) {
          const data = await response.json();
          setAllSizes(data);
        }else if (response.status === 401) {
        // Handle 401 unauthorized error
        console.error("Unauthorized: Please log in again.");
        navigate('/sessionexpired');
        // You can perform a redirect or show a message to the user here
      } else {
        const errorData = await response.json();
        throw new Error(errorData.message);
      }
    } catch (error) {
      console.error("Request error:", error.message);
      // Handle other errors here
    }
  };

    fetchSizes();
  }, []);

  const handleOptionChange = (option) => {
    setSelectedOption(option);
    setIsGroupSizeSelected(true);
    const minCount = option.minValue;
    const adultMinCount = Math.floor(minCount / 2);
    const kidsMinCount = Math.ceil(minCount / 2);
    setAdultCount(adultMinCount);
    setKidsCount(kidsMinCount);
    setPopupVisible(true);

    const newsProgressPercentage = 40;
    setProgressPercentage(newsProgressPercentage);

    // Update groupSizeId in finalOrder
    setFinalOrder(prevOrder => ({
      ...prevOrder,
      groupSizeId: option.id,
    }));

    console.log(finalOrder);
  };




  const incrementAdult = () => {
    if (selectedOption && adultCount + 1 <= selectedOption.maxValue) {
      setAdultCount(adultCount + 1);
      const newsProgressPercentage = 40;
      setProgressPercentage(newsProgressPercentage);
      if (kidsCount + adultCount >= selectedOption.maxValue) {
        setKidsCount(selectedOption.maxValue - (adultCount + 1));
        setShowAlertPopup(true);


      }
    }
  };

  const decrementAdult = () => {
    if (totalCount > selectedOption?.minValue && adultCount > 0) {
      setAdultCount(adultCount - 1);
      const newsProgressPercentage = 40;
      setProgressPercentage(newsProgressPercentage);
      if (kidsCount + adultCount - 1 < selectedOption.minValue) {
        setKidsCount(selectedOption.minValue - (adultCount - 1));
        setShowAlertPopup(false);
      }
    }
  };

  const incrementKids = () => {
    if (selectedOption && kidsCount + 1 <= selectedOption.maxValue) {
      setKidsCount(kidsCount + 1);
      const newsProgressPercentage = 40;
      setProgressPercentage(newsProgressPercentage);
      if (adultCount + kidsCount >= selectedOption.maxValue) {
        setAdultCount(selectedOption.maxValue - (kidsCount + 1));
        setShowAlertPopup(true);

      }
    }
  };

  const decrementKids = () => {
    if (totalCount > selectedOption?.minValue && kidsCount > 0) {
      setKidsCount(kidsCount - 1);
      const newsProgressPercentage = 40;
      setProgressPercentage(newsProgressPercentage);
      if (adultCount + kidsCount - 1 < selectedOption.minValue) {
        setAdultCount(selectedOption.minValue - (kidsCount - 1));
        setShowAlertPopup(false);
      }
    }
  };

  const totalCount = adultCount + kidsCount;
  setTotalSizeCount(totalCount);
  setGuestCount(totalCount);

  useEffect(() => {
    if (showAlertPopup) {
      // Show alert popup when count reaches maximum value
      alert("Move to next size");
      setShowAlertPopup(false); // Reset the flag
    }
  }, [showAlertPopup]);

  console.log(finalOrder.date, timeSlotName)


  if (allSizes.length === 0) {
    return <div>...loading</div>;
  }

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
    <div className="group">
      <Header />
      <div className="container">
        <Link to="/dateandtime" style={{ color: "orangered", textDecoration: "none" }}>
          <div >
            <button type="button" className="btn2">
              &#60; Back
            </button>
          </div>
        </Link>

        <h3 className="looks">Looks cool! Go ahead</h3>
        <p className="times"><span>{formatDate(finalOrder.date)}, {timeSlotName}, {finalOrder.address}</span>
        </p>
        <br />
        <div className="row  align-items-center">
          <div className="col-auto">
            <img src={Emoji1} style={{ width: 50, height: 30 }} alt="Emoji1" />
          </div>
          <div className="col">
            <h5 className="mb-0">Select Your Group Size</h5>
          </div>
        </div>

        <br />
        <div className="row cardss">
          {allSizes.map((data) => (
            <div
              key={data.id}
              className={`card py-4 ${selectedOption && selectedOption.id === data.id ? 'clicked' : ''}`}
              onClick={() => {
                handleOptionChange(data);
              }}
            >
              {console.log("selectedOption", selectedOption)}
              {console.log("data", data)}
              <img src={GroupSizeImage} style={{ width: 40, height: 40 }} alt="GroupSize" />
              <br />
              <div className={data.groupType}>
                <span className="gsize" style={{ fontSize: '25px' }}>{data.minValue}+<br /></span>
                <span className="gsize" style={{ fontSize: '20px' }}>{data.groupType}</span>
              </div>
            </div>
          ))}
        </div>
        <br />
        <br />
        {popupVisible && selectedOption && (
          <div className="popup">
            <div className="popup-content">
              <div className="second">
                <div className="row align-items-center">
                  <div className="col-auto">
                    <img src={Vector} style={{ width: 50, height: 30 }} alt="Vector" />
                  </div>
                  <div className="col">
                    <div className="d-flex align-items-center">
                      <h5 className="mb-0 mr-3 ">Select No of Adults & Kids</h5>

                      <h6 className="mb-0 h6img" style={{ marginTop: -0, marginLeft: 25 }}>
                        <img src={Vector} style={{ width: 20, height: 20 }} alt="Vector" />
                        &nbsp;Total - {totalSizeCount}
                      </h6> </div>
                  </div>

                </div>

                <p className="times">You can select minimum {selectedOption.minValue} people including adults and kids</p>
                <br />
                <table>
                  <tbody>
                    <tr>
                      <td className="tdtime"><p className="times">Adults</p></td>
                      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                      <td className="tdtime"><p className="times">Kids</p></td>
                    </tr>
                    <tr>
                      <td className="tdtime">
                        <button className="counts-button" onClick={decrementAdult}>-</button>
                        <span className="counts mx-2 my-2">{adultCount}</span>
                        <button className="counts-button" onClick={incrementAdult}>+</button>
                      </td>
                      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                      <td className="tdtime">
                        <button className="counts-button" onClick={decrementKids}>-</button>
                        <span className="counts mx-2 my-2">{kidsCount}</span>
                        <button className="counts-button" onClick={incrementKids}>+</button>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        )}
        <img src={BtmImg} alt="Bottom" className="btm-image" />
      </div>

      <Footer2040 isGroupSizeSelected={isGroupSizeSelected} progressPercentage={progressPercentage} />   </div>
    // footer

  );
};

export default GroupSize;
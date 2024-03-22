import React, { useContext } from 'react'
import { ApiContext } from "./ApiContextProvider";
import {Link, useNavigate} from "react-router-dom";
import designimg from "../Assets/designimg.png";
import logLogo from "../Assets/loglogo.png";
import errpage from "../Assets/loginPage .png";
import { Button } from '@mui/material';

export const SessionExpire = () => {
    const { setUserRole, setUserName, setUserToken, setUserMail, setUserFullName, setUserPhone,
        trackingStatus,
    setAllergiesMentioned,
    setOrderedFoodList,
    setSelectedFoodTypes,
    setAddOnsList,
    setDataList,
    setFinalOrder,
    setVegItemsSelected,
    setNonVegItemsSelected,
    setWhichCategory,
    userFullName,
    userPhone,
    userMail,
    userName,
    setSelectedItemsByCategory,
    setCheckedIndexes,
    setDataList2,
    setSelectedNonVegFoodItemIds,
    setSelectedVegFoodItemIds,
    selectedOption,
    setSelectedOption,
    setKidsCount,
    setPopupVisible,
    setAllSizes,
    setShowAlertPopup,
    setProgressPercentage,
    setIsGroupSizeSelected,
    setAdultCount,
    setFormData,
    setIsCardClicked,
    setShowRadio,
    setSelectedCheckboxes,
    setAllergiesSelected,
    setSelectedCardIndex,
    setAdditionalAllergyContent,
    setContinueButtonActive,
    } = useContext(ApiContext);

    var navigate = useNavigate();

    const handleBackButtonClick = () => {
        setUserRole("");
    setUserName("");
    setUserToken("");
    setUserFullName("");
    setUserMail("");
    setUserPhone("");

    sessionStorage.removeItem("role");
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("userName");
    sessionStorage.removeItem("mail");
    sessionStorage.removeItem("name");
    sessionStorage.removeItem("phoneNumber");
    sessionStorage.removeItem("trackstatusid");

    setOrderedFoodList([]);
    setAllergiesMentioned([]);
    setSelectedFoodTypes([]);
    setAddOnsList([]);
    setDataList([]);
    setDataList2([]);
    setVegItemsSelected({});
    setNonVegItemsSelected({});
    setWhichCategory(1);
    setSelectedVegFoodItemIds({});
    setSelectedNonVegFoodItemIds({});
    setSelectedItemsByCategory({});
    setCheckedIndexes([]);

    //Groupsize page
    setSelectedOption(null);
    setPopupVisible(false);
    setAllSizes([]);
    setShowAlertPopup(false);
    setProgressPercentage(20);
    setIsGroupSizeSelected(false);
    setAdultCount(
      selectedOption?.minValue ? Math.floor(selectedOption.minValue / 2) : 0
    );
    setKidsCount(
      selectedOption?.minValue ? Math.ceil(selectedOption.minValue / 2) : 0
    );

    //Food Type page
    setFormData({});
    setIsCardClicked(false);

    //Plate Size page
    setSelectedCheckboxes([]);
    setShowRadio(false);
    setSelectedCardIndex(null);
    setAllergiesSelected(null);
    setAdditionalAllergyContent("");
    setContinueButtonActive(false);

    setFinalOrder({
      id: 0,
      oid: "",
      deliveryOptionId: 0,
      plateSizeId: 0,
      additionalNote: "",
      address: "",
      date: "",
      groupSizeId: 0,
      timeSlotId: 0,
      userName: userName,
      additionalAllergy: "",
      contactName: userFullName,
      contactNumber: userPhone,
      contactEmail: userMail,
      drinkId: null,
    });

    navigate("/");
      };
  return (
    <div>

        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start', justifyContent: 'center', height: '70vh', paddingLeft: '20px' }}>
          <img className="loglogo" src={logLogo} alt="" /> <p style={{

            padding: "20px",
            fontSize: "38px",
            color: "black",
            textAlign: "center",
          }}>
            Your Session Has Expired. Please Login Again.
          </p>
          <div className="img" style={{ position: 'fixed', bottom: 0, left: 0 }}>
            <img src={designimg} alt="" />
          </div>
          <div className="imgside" style={{ position: 'fixed', bottom: 0, right: 0 }}>
            <img src={errpage} alt="" />
          </div>
            <Button onClick={handleBackButtonClick} sx={{
              backgroundColor: "#ff6d00", color: 'rgb(255, 255, 255)', alignItems: 'flex-end', justifyContent: "center", marginTop: '20px', marginLeft: '220px', width: '180px', height: '50px', fontWeight: 'bold', fontSize: '18px', '&:hover': {
                backgroundColor: "orangered", // Change this to your desired hover color

              }
            }} >
              Go Back
            </Button>


        </div>
      </div>
  )
}

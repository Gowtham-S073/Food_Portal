import { React, useContext, useEffect, useState } from "react";
import styles from "./Submitted.module.css";
import Submit from "../../Assets/Submit.png";
import Logo from "../../Assets/Logo.png";
import { unstable_gridHeaderFilteringEditFieldSelector } from "@mui/x-data-grid";
import { Link } from "react-router-dom";
import { ApiContext } from "../ApiContextProvider";
import { Button } from "@mui/material";
import designimg from "../../Assets/designimg.png";
import logLogo from "../../Assets/loglogo.png";
import errpage from "../../Assets/loginPage .png";

function Submitted() {
  const [isMobileView, setIsMobileView] = useState(
    window.matchMedia("(max-width: 700px)").matches
  );
  const isSmallScreen = window.innerWidth <= 375;
  const {
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

  useEffect(() => {
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
  }, []);

  if (!trackingStatus) {
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
    <>
      <header className={styles.header}>
        <img src={Logo} className={styles.header_logo} alt="Logo" />
      </header>
      <div className={styles.submitted_container}>
        <img className={styles.submit_img} src={Submit} alt="Submitted" />
        <div className={styles.submitted_details}>
          <h6
            style={{
              paddingBottom: "10px",
              fontWeight: "500",
              fontSize: isSmallScreen ? "20px" : "25px",
              whiteSpace: "nowrap",
              overflow: "hidden",
              textOverflow: "ellipsis",
              maxWidth: "100%",
            }}
          >
            <b>Your request was submitted</b>
          </h6>
          <div className={styles.order_detail} style={{ padding: "10px" }}>
            Use the below Id to view/track your request. It has also been sent
            to your mail. Thank You
          </div>
          <p className={styles.order_id}>{trackingStatus}</p>
          <div>
            <Link to="/trackstatus">
              <button type="button" className={styles.btn_go_home}>
                Track Your Order &#62;
              </button>
            </Link>
          </div>
        </div>
      </div>
    </>
  );
}
export default Submitted;

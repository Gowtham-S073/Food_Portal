
import React, { useContext, useRef, useState } from "react";
import "./Homepage.css";
import logo from "../../Assets/Logo copy.png";
import image1 from "../../Assets/image1.png";
import tracklogo from "../../Assets/tracklogo.png";
import image2 from "../../Assets/image2.png";
import image3 from "../../Assets/image3.png";
import star from "../../Assets/Icon.png";
import location from "../../Assets/location copy.png";
import cart from "../../Assets/cart copy.png";
import share from "../../Assets/share.png";
import review from "../../Assets/Icon.png";
import { Link, useNavigate } from "react-router-dom";
import { Button } from "react-bootstrap";
import { ApiContext } from "../ApiContextProvider";
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import PopUp from './PopUpReview'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import ShareModal from "./Share";

const Homepage = () => {

  var navigate = useNavigate();

  const { setUserRole, setUserName, setUserToken, finalOrder, setUserMail, setUserFullName, setUserPhone,
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
  const handleLogout = () => {

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
    sessionStorage.removeItem('trackstatusid');

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
  



    toast.success('Logged out successfully!', {
      position: 'top-right',
      autoClose: 2000
    });


    navigate("/");
  };
  console.log("Final Order:", finalOrder)


  const [open, setOpen] = useState(false);

  const handleOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const [isShareModalOpen, setShareModalOpen] = useState(false); // State for share modal visibility

  const handleOpenShareModal = () => {
    setShareModalOpen(true);
  };

  const handleCloseShareModal = () => {
    setShareModalOpen(false);
  };

  const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
  };





  return (
    <div>
      <link
        href="https://fonts.googleapis.com/css2?family=Poppins:wght@200&display=swap" rel="stylesheet">
      </link>
      <div className="headerhome">

        <div className="header_logo">
          <div className="navbarbrand">
            <img src={logo} alt="Logo" className="logohome" />
          </div>
        </div>
        <div style={{ display: 'flex' }} className="logoutbuttonmedia">
          <div className="header_menu">
            <img src={tracklogo} alt="Logo" className="track_logo" />
            <div className="track_text" id="track_text">
              <Link to="/trackstatus" className="trackyourorder">
                <span>Track your order</span>
              </Link>
            </div>
          </div>


          <Button
            style={{ background: "#ff6d00", borderColor: "#ff6d00", marginLeft: '10px', color: 'white', }}
            onClick={handleLogout}
          >
            Logout
          </Button>
        </div>
      </div>



      <div className="containershome containers-fluid">
        <div className="main">
          <div className="image_home ">
            <div className="image_1">
              <img className="image_1" src={image1} alt="restaurant image1" />
            </div>
            <div className="images23">
              <div className="image_2">
                <img className="image_2" src={image2} alt="restaurant image2" />
              </div>
              <div className="image_3">
                <img className="image_3" src={image3} alt="restaurant image3" />
              </div>
            </div>
          </div>
          <div className="image_text ">
            <div className="text_homepage" id="image3_homepage">
              <div className="containerhome " >
                <div className="title">
                  <div className="title_text">
                    <span className="black_text">The Spice Saga Restaurant</span>
                    <img className="star_image" src={star} alt="starimage" />
                    <span className="rating_text"> 4.8</span>
                    <span className="grey_text"> (2.5k Ratings)</span>
                  </div>
                </div>
                <div className="cusine_text">Indian, Arabian, Chinese</div>
                <div className="address_text">
                  <img src={location} alt="location" />
                  <span className="addresshome">
                    3041 South Las Vegas Boulevard Las Vegas, NV 89109
                  </span>
                </div>


                <div className="reviewandshare_button">
                  <div className="review_button" onClick={handleOpen}>
                    <img className="review_image" src={review} alt="reviewimage" />
                    <div className="review_text">Add a review</div>
                  </div>

                  <Modal
                    open={open}
                    onClose={handleClose}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                  >
                    <Box sx={style}>
                      <PopUp />
                    </Box>
                  </Modal>

                  <div className="share_button" onClick={handleOpenShareModal}>
                    <img className="share_image" src={share} alt="shareimage" />
                    <div className="share_text">Share</div>
                  </div>

                  <Modal
                    open={isShareModalOpen}
                    onClose={handleCloseShareModal}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                  >
                    <Box sx={style}>
                      <ShareModal />
                    </Box>
                  </Modal>



                </div>

                
                <div className="para_text">
                  One of the finest restaurants for dinning. The Spice Saga
                  restaurant promises a memorable fine dining experience,
                  featuring North Indian cuisine, kebabs, dal and Indian breads.
                  For those who like to be pampered in style, love exquisite
                  dining and enjoy more than just a sip of alcohol ~ The Spice
                  Saga restaurant is the place to be. The restaurant also offers
                  vegetarian and vegan friendly options.
                </div>
                <div className="order_button">

                  {" "}
                  <div type="button" className="order_food" id="order_button">
                    <span className="order_text">
                      <Link to="/dateandtime" className="orderyourfood">
                        {" "}
                        Order your Food
                      </Link>
                    </span>
                    <img className="order_image" src={cart} alt="orderimage" />
                  </div>

                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <footer className="footerhome">
        <span className="footer_text"> © 2022 Kanini Software Solutions.ltd</span>
      </footer>

      <ToastContainer />

    </div>
  );
};

export default Homepage;

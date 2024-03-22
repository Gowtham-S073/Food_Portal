import React, { useContext, useEffect, useState, useRef } from 'react'; 
import axios from 'axios';
import { Variable } from "../Variables";
import 'bootstrap/dist/css/bootstrap.css';
import { Container } from 'react-bootstrap';
import Group1 from '../../Assets/Group1.png';
import bg1 from '../../Assets/bg 1.png';
import pencil from '../../Assets/pencil.png';
import allergyimg from '../../Assets/allergyimg.png'
import Header from '../Header/header';
import Modal from '@mui/material/Modal';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import CardHeader from '@mui/material/CardHeader';
import Button from '@mui/material/Button';
import ClearRoundedIcon from '@mui/icons-material/ClearRounded';
import { ApiContext } from '../ApiContextProvider';
import { Link, useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';

import 'react-toastify/dist/ReactToastify.css';
import './PlateSize.css';
import './IPad.css';
import './IPhone.css';

// Categories are not yet loaded, show a loading indicator or handle accordingly

import designimg from "../../Assets/designimg.png";
import logLogo from "../../Assets/loglogo.png";
import errpage from "../../Assets/loginPage .png";
import { SessionExpire } from '../SessionExpire';


const CheckboxPage = ({ alergy, handleCheckboxSubmit, handleClose }) => {
  // const [selectedCheckboxes, setSelectedCheckboxes] = useState([]);
  // const [additionalAllergyContent, setAdditionalAllergyContent] = useState(''); // State to track textarea content
  const {  setFinalOrder, allergiesMentioned, setAllergiesMentioned, selectedCheckboxes, setSelectedCheckboxes,
    additionalAllergyContent, setAdditionalAllergyContent} = useContext(ApiContext);

    var navigate = useNavigate();

  // Function to handle checkbox selection
  const handleCheckboxChange = (event) => {
    const { value, checked } = event.target;
    if (checked) {
      setSelectedCheckboxes([...selectedCheckboxes, value]);
    } else {
      setSelectedCheckboxes(selectedCheckboxes.filter((item) => item !== value));
    }
  };

  // Function to handle textarea content change
  const handleAdditionalAllergyChange = (event) => {
    setAdditionalAllergyContent(event.target.value);
  };

  const handleOkClick = (event) => {
    event.preventDefault();

    const isAnyCheckboxChecked = selectedCheckboxes.length > 0;
    const isOtherTextareaFilled = additionalAllergyContent.trim() !== '';

    if (!isAnyCheckboxChecked && !isOtherTextareaFilled) {
      toast.error("Please select one Allergy");
    } else {
      setTimeout(() => {

        toast.success("Allergies Selected");

        handleCheckboxSubmit(selectedCheckboxes);

        // Update the additionalAllergy property of finalOrder
        setFinalOrder((prevOrder) => ({
          ...prevOrder,
          additionalAllergy: additionalAllergyContent,
        }));
        const mentionedAllergies = selectedCheckboxes.map((allergyName) => ({
          id: 0,
          ordersId: 0,
          allergyId: alergy.find((allergy) => allergy.allergyName === allergyName)?.id || 0,
        }));

        // Replace the existing allergiesMentioned array with the new array
        setAllergiesMentioned(mentionedAllergies);

        // Log the updated allergiesMentioned array
        console.log('allergiesMentioned', allergiesMentioned);
      }, 0);
    }
  };





  return (
    <>

      <ToastContainer
        closeOnClick={false}
        pauseOnHover={false}
        autoClose={2500}
        pauseOnFocusLoss={false}

        draggable={false}

      />
      <Container className="allergy-container">
        <form onSubmit={handleOkClick}>

          <div id='modal-top'>
            <div>
              <h3 className="choose_allergy">Choose Your Allergies</h3>
            </div>
            <div>
              <ClearRoundedIcon id='clear-icon' onClick={handleClose} />
            </div>
          </div>
          <div className="checkbox-grid">
      {alergy.map((list, index) => (
        <div key={index} className="checkbox-item">
          <label>
            <input
              type="checkbox"
              className="checkbox-tick"
              onChange={handleCheckboxChange}
              value={list.allergyName}
              checked={selectedCheckboxes.includes(list.allergyName)}
            />
            <span className="checkbox-text">{list.allergyName}</span>
          </label>
        </div>
      ))}
    </div>
          {console.log("selectedcheckboxes",selectedCheckboxes)}
          {console.log("alergy", alergy)}
          <div className="allergy-others">
            <div className="others-head">
              <label className='others-text'>Others</label>
            </div>
            <textarea
              value={additionalAllergyContent}
              onChange={handleAdditionalAllergyChange}
            ></textarea>      </div>

          <div className="modal-btn">
            <button className="cancel-button" onClick={handleClose}>Cancel</button>
            <button onClick={handleOkClick} className="save-button">Save</button>
          </div>
        </form>

      </Container></>


  );
};



async function fetchPlateSizes(navigate) {
  try {
    const response = await axios.get(Variable.plateSizeUrl, {
      headers: {
        "accept": "text/plain",
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      }
            })
console.log("401",response.status);

if (response.status === 200) {
    return response.data; // Return the plate size objects
}
else if (response.status === 401) {
  // Handle 401 unauthorized error
  console.log("401",response.status);
  return response.status;
  // You can perform a redirect or show a message to the user here
} else {
  const errorData = await response.json();
  throw new Error(errorData.message);
}
  } catch (error) {
    console.error('Error fetching plate sizes:', error.message);
    navigate('/sessionexpired');
    return null; // Return null in case of an error
  }
}



function formatPlateDetails(plateDetailsObject) {
  const formattedDetails = [];

  for (const category in plateDetailsObject) {
    if (category === 'sodaorWater' && plateDetailsObject[category]) {
      formattedDetails.push('1 water bottle or soda');
    } else {
      const count = plateDetailsObject[category];
      if (count > 0) {
        const item = `${count} ${category}`;
        formattedDetails.push(item);
      }
    }
  }
  return formattedDetails.join(' + ');
}


const PlateSize = () => {
  const [noOptionSelected, setNoOptionSelected] = useState(false); // Initialize with false
  const plateRadioRef = useRef(null);
  const [plateSizes, setPlateSizes] = useState([]);
  const [open, setOpen] = useState(false);
  const [alergy, setAlergy] = useState([]);
  // const [selectedCheckboxes, setSelectedCheckboxes] = useState([]);
  // const [showRadio, setShowRadio] = useState(false);
  // const [selectedCardIndex, setSelectedCardIndex] = useState(null); // Initialize with null

  // const [allergiesSelected, setAllergiesSelected] = useState(false);

  // const [continueButtonActive, setContinueButtonActive] = useState(false); // New state
  const { maxFoodItems, setMaxFoodItems, selectedFoodTypes, setSelectedFoodTypes, finalOrder, setFinalOrder, plateSizeForUse, setPlateSizeForUse, plateCostForUse, setPlateCostForUse, typeFood, setTypeFood,
    timeSlotName, totalSizeCount,
    selectedCardIndex, setSelectedCardIndex,
    allergiesSelected, setAllergiesSelected,
    selectedCheckboxes, setSelectedCheckboxes,
    showRadio, setShowRadio,
    continueButtonActive, setContinueButtonActive
   } = useContext
      (ApiContext);
      var navigate = useNavigate();





  const [progressPercentage, setProgressPercentage] = useState(60);

  const formatDate = (dateString) => {
    const options = { month: 'long', year: 'numeric' };
    const formattedDate = new Date(dateString).toLocaleDateString('en-US', options);

    const day = new Date(dateString).getDate();
    const suffix = day === 1 ? 'st' : day === 2 ? 'nd' : day === 3 ? 'rd' : 'th';

    return `${day}${suffix} ${formattedDate}`;
  };



  useEffect(() => {
    // Fetch plate sizes from the API and update the state
    fetchPlateSizes(navigate)
      .then((plateSizes) => {
        if (plateSizes && Array.isArray(plateSizes)) {
          setPlateSizes(plateSizes);
        } else if(plateSizes === 401) {
          navigate('/sessionexpired');
        }
      })
      .catch((error) => {
        console.error('Error fetching plate sizes:', error.message);
      });
  }, []);

  useEffect(() => {
    axios.get(Variable.AllergyUrl, {
      headers: {
        "accept": "text/plain",
        "Authorization": "Bearer " + sessionStorage.getItem('token')
      }
            })

      .then((response) => {
        setAlergy(response.data);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
  }, []);

  const handleClick = () => {
    setShowRadio(!showRadio);
  };

  const handleOpen = () => {
    setShowRadio(true);
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleCheckboxSubmit = (selectedCheckboxes) => {
    setSelectedCheckboxes(selectedCheckboxes);
    handleClose();
    setProgressPercentage(80);

    setContinueButtonActive(true);
    setNoOptionSelected(false);
  };


  const handlePlateSizeClick = (plateSize, index) => {
    const plateDetailsObject = {};
    for (const key in plateSize) {
      if (key !== 'id' && key !== 'plateType' && key !== 'vegPlateCost' && key !== 'nonvegPlateCost' && key !== 'foodTypeCounts' && key !== 'orders' && key !== 'bothCost') {
        if (key === 'sodaorWater' && !plateSize[key]) {
          continue;
        }
        plateDetailsObject[key] = plateSize[key];
      }
    }

    const { sodaorWater, ...plateDetailsWithoutSodaWater } = plateDetailsObject;
    setMaxFoodItems(plateDetailsWithoutSodaWater);


    // Update plateSizeId in selectedFoodTypes
    const updatedFoodTypes = selectedFoodTypes.map(foodType => ({
      ...foodType,
      plateSizeId: plateSize.id, // Update with the selected plate size's id
    }));
    setSelectedFoodTypes(updatedFoodTypes);

    setFinalOrder((prevOrder) => ({
      ...prevOrder,
      plateSizeId: plateSize.id,
    }));

    setPlateSizeForUse(plateSize.plateType);

    if (typeFood === 'Veg & NonVeg') {
      setPlateCostForUse(plateSize.bothCost);
    }
    else if (typeFood === 'Veg') {
      setPlateCostForUse(plateSize.vegPlateCost);
    }
    else if (typeFood === 'NonVeg') {
      setPlateCostForUse(plateSize.nonvegPlateCost);
    }


    console.log('order', plateDetailsObject);


    console.log(plateDetailsObject);
    console.log("Clicked card index:", index);
    setSelectedCardIndex(index);
    handleClick();
    setShowRadio(true);
    if (
      window.matchMedia('(max-width: 500px) and (min-width: 375px)').matches
    ) {
      plateRadioRef.current.scrollIntoView({ behavior: 'smooth' });
    }


  };

  useEffect(() => {
    console.log('detials', maxFoodItems); // Log the updated maxFoodItems state after each click
    console.log('ids', selectedFoodTypes);
    console.log('final order', finalOrder);
    console.log('type', plateSizeForUse);

  }, [maxFoodItems]);

  const handleYesClick = () => {
    handleOpen();
    setAllergiesSelected(true);
    setContinueButtonActive(false);    // Other logic...
  };

  const handleNoClick = () => {
    setContinueButtonActive(true);
    setAllergiesSelected(false);    // Other logic...
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
    <>
      <Header />

      <div className="container-fluid">

        <div className="there">
          <h2>Almost there!</h2>
        </div>
        <div className='plate-pagination'>
          {formatDate(finalOrder.date)}, {timeSlotName}, {finalOrder.address}<span className="spanorange">|</span> {totalSizeCount} Guests | {typeFood}
        </div>
        <div className="orange">
          <img src={Group1} alt="headerImage" className="grp" />
          <h3 className="platesize">What will be the Plate Size?</h3>
          <br />
        </div>

        <div className="plate-card-container">
          {plateSizes.map((plateSize, index) => {
            const plateDetailsObject = {};
            for (const key in plateSize) {
              if (key !== 'id' && key !== 'plateType' && key !== 'vegPlateCost' && key !== 'nonvegPlateCost' && key !== 'bothCost') {
                plateDetailsObject[key] = plateSize[key];
              }
            }
            const formattedPlateDetails = formatPlateDetails(plateDetailsObject);

            return (
              <Card key={index} onClick={() => handlePlateSizeClick(plateSize, index)} style={{
                cursor: 'pointer',
                border: `1px solid ${
                  selectedCardIndex === index ? '#ff6d00' :  'rgba(209, 209, 209)'
                }`,              }} className='plate-card'>
                <CardHeader className='plate-card-header' title={plateSize.plateType} />
                <CardContent className='plate-card-content'>
                  <Typography>{formattedPlateDetails}</Typography>
                </CardContent>
                <CardContent>
                  <div className='plate-card-footer'>
                    *Veg only: ${plateSize.vegPlateCost}/ plate<br /> *NonVeg Only: ${plateSize.nonvegPlateCost}/ plate<br /> *Both: ${plateSize.bothCost}/ plate
                  </div>
                </CardContent>
              </Card>

            );
          })}


        </div>


        <div className="plate-radio" ref={plateRadioRef}>
          {showRadio && (
            <div className="options">
        {/* ... */}
        <div className="allergy-radio">
          <h3 className="radio-head">Are you allergic to any food?</h3>
          <input
            type="radio"
            name="option"
            value="option1"
            id="yes-radio"
            checked={allergiesSelected === true}
            onClick={handleYesClick}
          />
          <label htmlFor="yes-radio" className="yes-option">
            Yes
          </label>
          <input
            type="radio"
            name="option"
            value="option2"
            id="no-radio"
            checked={allergiesSelected === false}
            onClick={handleNoClick}
          />
          <label htmlFor="no-radio" className="no-option">
            No
          </label>
        </div>
      </div>
          )}
    </div>

      </div>
      <div className="bg2">
        <img src={bg1} alt="background-img" />
      </div>
      <Modal open={open} onClose={handleClose} BackdropProps={{}}>
        <div>
          <CheckboxPage
            alergy={alergy}
            handleCheckboxSubmit={handleCheckboxSubmit}
            handleClose={handleClose}
          />
        </div>
      </Modal>

      {allergiesSelected && selectedCheckboxes.length > 0 && (
        <div className='allergy-show'>
          <div className='allergy-scroll-container'>

            {selectedCheckboxes.map((checkbox, index) => (
              <div key={index} className='allergy-text'>
                {index > 0 && ', '}
                {checkbox}
              </div>
            ))}
          </div>
          <img
            src={pencil}
            alt="edit"
            className="allergy-edit"
            onClick={handleOpen}
          />

        </div>
      )}
      <div className="plate-foot">
        <div className='plate-progress' >
          <div className="progress-container">
            <div className="plate-progress-bar" style={{ width: `${progressPercentage}%` }} ></div>
          </div>
          <h6 className="progress-percentage">{progressPercentage}%</h6>
        </div>
        <Link to="/foodtype" >

          <Button className="back-button" classes={{ root: 'back-button' }} >
            <div className="back-button-text" style={{ textDecoration: 'none', color: 'orange', textTransform: 'capitalize' }}>
              &#60; Back
            </div>

          </Button>
        </Link>


        <Link to='/food_selection' >

          <Button
            className="continue-button"
            style={{
              backgroundColor: continueButtonActive || noOptionSelected ? '#ff6d00' : 'rgba(158, 158, 158, 1)',

              color: continueButtonActive || noOptionSelected ? 'white' : '#333',
            }}
            classes={{ root: 'continue-button' }}
            disabled={!continueButtonActive}
          >

            <div className="continue-button-text no-text-decoration" style={{ textTransform: 'capitalize' }}>
              Continue  &#62;
            </div>
          </Button>
        </Link>
        <div className='plate-steps'>
          <h6>Step <span className='steps-style'>4</span> of 5</h6>
        </div>
      </div>
      <Link style={{ textDecoration: 'none' }}
        to="/foodtype" >

        <Button className="iphone-back-button" classes={{ root: 'iphone-back-button' }}>
          <div className='iphone-back-text' style={{ textTransform: 'capitalize' }}>
            &#60; Back
          </div>

        </Button>
      </Link>
    </>

  );
};

export default PlateSize;
import React, { createContext, useState, useEffect } from 'react';

// Create the context
const ApiContext = createContext();

// Create the context provider component
const ApiContextProvider = ({ children }) => {
  // State to store user role and token from sessionStorage
  const [userRole, setUserRole] = useState(null);
  const [userToken, setUserToken] = useState(null);
  const [userName, setUserName] = useState(null);
  const [userMail, setUserMail] = useState(null);
  const [userFullName, setUserFullName] = useState(null);
  const [userPhone, setUserPhone] = useState(null);
  // const [userId, setUserId] = useState(null);
  const [dataList, setDataList] = useState([]); //additional products posting
  const [dataList2, setDataList2] = useState([]);

  const [trackid,setTrackid] = useState('');
  const [maxFoodItems, setMaxFoodItems] = useState({});
  const [orderedFoodList, setOrderedFoodList] = useState([]); //food_selection component posting
  const [allergiesMentioned, setAllergiesMentioned] = useState([]); //food_selection component posting
  const [selectedFoodTypes, setSelectedFoodTypes] = useState([]); //foodtype component posting
  const [addOnsList, setAddOnsList] = useState([]);  //Addon component posting
  const [totalSizeCount, setTotalSizeCount] = useState(null);   //groupsize component
  const [plateSizeId, setPlateSizeId] = useState(null); //platesize component
//   const [foodTypeCount, setFoodTypeCount] = useState([]); //foodtype component
  const [plateSizeForUse, setPlateSizeForUse] = useState('');
  const [plateCostForUse, setPlateCostForUse] = useState(null);
  const [onlyVeg, setOnlyVeg] = useState(null);
  const [onlyNonVeg, setOnlyNonVeg] = useState(null);
  const [guestCount, setGuestCount] = useState(null);
  const [timeSlotName,setTimeSlotName] = useState('');
  const [typeFood, setTypeFood] = useState('');
  const [vegItemsSelected, setVegItemsSelected] = useState({});

  const [nonVegItemsSelected, setNonVegItemsSelected] = useState({});

  const [selectedVegFoodItemIds, setSelectedVegFoodItemIds] = useState({});

  const [selectedNonVegFoodItemIds, setSelectedNonVegFoodItemIds] = useState({});
  
  const [whichCategory, setWhichCategory] = useState(1);

  const [trackingStatus, setTrackingStatus] = useState();

  const [selectedItemsByCategory, setSelectedItemsByCategory] = useState({});
  const [checkedIndexes, setCheckedIndexes] = useState([]);
  const [datum, setDatum] = useState([]);

  //Group size props
  const [selectedOption, setSelectedOption] = useState(null);
  const [popupVisible, setPopupVisible] = useState(false);
  const [allSizes, setAllSizes] = useState([]);
  const [showAlertPopup, setShowAlertPopup] = useState(false);
  const [progressPercentage, setProgressPercentage] = useState(20);
  const [isGroupSizeSelected, setIsGroupSizeSelected] = useState(false);
  const [adultCount, setAdultCount] = useState(selectedOption?.minValue ? Math.floor(selectedOption.minValue / 2) : 0);
  const [kidsCount, setKidsCount] = useState(selectedOption?.minValue ? Math.ceil(selectedOption.minValue / 2) : 0);

  //Datetime
  // const [selected, setSelected] = useState('1');
  // const [isDatePickerOpen, setIsDatePickerOpen] = useState(false);
  // const [anchorEl, setAnchorEl] = useState(null);
  // const [selectedTimeBtn, setSelectedTimeBtn] = useState(null);
  // const [selectedDate, setSelectedDate] = useState(null);
  // const [isTimeEnabled, setIsTimeEnabled] = useState(false);
  // const [isDateFilled, setIsDateFilled] = useState(false);
  // const [isTimeFilled, setIsTimeFilled] = useState(false);
  // const [isDeliveryOptionFilled, setIsDeliveryOptionFilled] = useState(false);
  // const [isAddressFilled, setIsAddressFilled] = useState(false);
  // const [address, setAddress] = useState('');
  // const isButtonDisabled = !isDateFilled || !isTimeFilled || (selected !== '1' && (!isDeliveryOptionFilled || !isAddressFilled));
  // const [isDateSelected, setIsDateSelected] = useState(false);

  //Food type props
  const [formData, setFormData] = useState({
    selectedOption: '',
    vegCount: Math.floor(totalSizeCount / 2),
    nonvegCount: Math.floor(totalSizeCount / 2),
  });
  const [isCardClicked, setIsCardClicked] = useState(false);

  //Plate Size props
  const [selectedCheckboxes, setSelectedCheckboxes] = useState([]);
  const [showRadio, setShowRadio] = useState(false);
  const [selectedCardIndex, setSelectedCardIndex] = useState(null); // Initialize with null
  const [allergiesSelected, setAllergiesSelected] = useState(null);
  const [additionalAllergyContent, setAdditionalAllergyContent] = useState(''); // State to track textarea content
  const [continueButtonActive, setContinueButtonActive] = useState(false); // New state


  const [finalOrder, setFinalOrder] = useState({
    id: 0,
    oid: '',
    deliveryOptionId: 0,
    plateSizeId: 0,
    additionalNote: '',
    address: '',
    date: '',
    groupSizeId: 0,
    timeSlotId: 0,
    userName: '',
    additionalAllergy: '',
    contactName: '',
    contactNumber: '',
    contactEmail: '',
    // drinkId: null,
  });

  

  
  
  useEffect(() => {
    const storedRole = sessionStorage.getItem('role');
    const storedToken = sessionStorage.getItem('token');
    const storedUsername = sessionStorage.getItem('userName');
    const storedMail = sessionStorage.getItem('mail');
    const storedName = sessionStorage.getItem('name');
    const storedPhone = sessionStorage.getItem('phoneNumber');
  
    setUserRole(storedRole);
    setUserToken(storedToken);
    setUserName(storedUsername);
    setUserMail(storedMail);
    setUserFullName(storedName);
    setUserPhone(storedPhone);

  }, []);
  
  useEffect(() => {
    
  
    setFinalOrder((prevOrder) => ({
      ...prevOrder,
      userName: userName,
      contactName: userFullName,
      contactNumber: userPhone,
      contactEmail: userMail,
    }));
  
  }, [userName, userFullName, userMail, userPhone]);

  return (
    <ApiContext.Provider value={{ userRole, userToken, userName, setUserName, setUserRole, setUserToken,
      userMail, setUserMail, userFullName, setUserFullName, userPhone, setUserPhone,
        trackid, setTrackid, orderedFoodList, setOrderedFoodList, dataList, setDataList, totalSizeCount, setTotalSizeCount,
        selectedFoodTypes, setSelectedFoodTypes, plateSizeId, setPlateSizeId, finalOrder, setFinalOrder,maxFoodItems, setMaxFoodItems,
        allergiesMentioned, setAllergiesMentioned,
        plateSizeForUse, setPlateSizeForUse, onlyVeg, setOnlyVeg, onlyNonVeg, setOnlyNonVeg,
        addOnsList, setAddOnsList,
        guestCount, setGuestCount,
        timeSlotName,setTimeSlotName,
        typeFood, setTypeFood,
        vegItemsSelected, setVegItemsSelected,nonVegItemsSelected, setNonVegItemsSelected,
        plateCostForUse, setPlateCostForUse,
        dataList2, setDataList2,
        trackingStatus, setTrackingStatus,
        selectedItemsByCategory, setSelectedItemsByCategory,
        checkedIndexes, setCheckedIndexes,
        datum, setDatum,
        selectedVegFoodItemIds, setSelectedVegFoodItemIds,
        selectedNonVegFoodItemIds, setSelectedNonVegFoodItemIds,
        whichCategory, setWhichCategory,
        selectedOption, setSelectedOption,
        kidsCount, setKidsCount,
        popupVisible, setPopupVisible,
        allSizes, setAllSizes,
        showAlertPopup, setShowAlertPopup,
        progressPercentage, setProgressPercentage,
        isGroupSizeSelected, setIsGroupSizeSelected,
        adultCount, setAdultCount,
        formData, setFormData,
        isCardClicked, setIsCardClicked,
        showRadio, setShowRadio,
        selectedCheckboxes, setSelectedCheckboxes,
        allergiesSelected, setAllergiesSelected,
        selectedCardIndex, setSelectedCardIndex,
        additionalAllergyContent, setAdditionalAllergyContent,
        continueButtonActive, setContinueButtonActive
        
        // selected, setSelected,
        // isDatePickerOpen, setIsDatePickerOpen,
        // anchorEl, setAnchorEl,
        // selectedTimeBtn, setSelectedTimeBtn,
        // selectedDate, setSelectedDate,
        // isTimeEnabled, setIsTimeEnabled,
        // isDateFilled, setIsDateFilled,
        // isTimeFilled, setIsTimeFilled,
        // isDeliveryOptionFilled, setIsDeliveryOptionFilled,
        // isAddressFilled, setIsAddressFilled,
        // address, setAddress,
        // isButtonDisabled,
        // isDateSelected, setIsDateSelected


        



        }}>
      {children}
    </ApiContext.Provider>
  );
};

export { ApiContext, ApiContextProvider };
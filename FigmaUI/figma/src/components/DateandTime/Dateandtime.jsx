import * as React from 'react';
import './Dateandtime.css';
import { useState, useEffect } from 'react';
import { orange } from '@mui/material/colors';
import { toast } from 'react-toastify';
import { Button, Typography } from '@mui/material';
import dayjs from 'dayjs';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Popover from '@mui/material/Popover';
import { DateCalendar } from '@mui/x-date-pickers/DateCalendar';
import { Link, useNavigate } from 'react-router-dom';
import { styled } from '@mui/material/styles';
import Tooltip, {tooltipClasses } from '@mui/material/Tooltip';
import { ApiContext } from '../ApiContextProvider';
import bg from '../../Assets/bg 1.png';
import Dateimg from '../../Assets/date.png'
import time from '../../Assets/time.png';
import datepicker from '../../Assets/datepicker.png';
import delivery from '../../Assets/delivery.png';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormControl from '@mui/material/FormControl';
import location from '../../Assets/location.png';
import Header from '../Header/header';
import { SessionExpire } from '../SessionExpire';


const Dateandtime = () => {
  const [selected, setSelected] = React.useState('1');
  const [isDatePickerOpen, setIsDatePickerOpen] = React.useState(false);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [selectedTimeBtn, setSelectedTimeBtn] = React.useState(null);
  const [selectedDate, setSelectedDate] = useState(null);
  const [isTimeEnabled, setIsTimeEnabled] = useState(false);
  const [isDateFilled, setIsDateFilled] = useState(false);
  const [isTimeFilled, setIsTimeFilled] = useState(false);
  const [isDeliveryOptionFilled, setIsDeliveryOptionFilled] = useState(false);
  const [isAddressFilled, setIsAddressFilled] = useState(false);
  const [address, setAddress] = useState('');
  const isButtonDisabled = !isDateFilled || !isTimeFilled || (selected !== '1' && (!isDeliveryOptionFilled || !isAddressFilled));
  const [unavailableDates, setUnavailableDates] = useState([]);
  const [delOption, setDelOption] = useState([]);
  const [timeSlots, setTimeSlots] = useState([]);
  const [availableSlots, setAvailableSlots] = useState([]);
  const [isDateSelected, setIsDateSelected] = useState(false);
  const {finalOrder, setFinalOrder, setTimeSlotName} = React.useContext(ApiContext);
  const open = Boolean(anchorEl);
  const id = open ? 'time-popover' : undefined;
  const extra = 'option';
  const today = dayjs();
  const tomorrow = dayjs().add(1, 'day');
  const dateOnly = today.format('YYYY-MM-DD');
  var navigate = useNavigate();


  useEffect(() => {
    fetchData();
    fetchDelivery();
    fetchTime();
  }, []);

  const fetchData = async () => {
    try {
      const response = await fetch(`https://localhost:7239/api/Orders/GetUnAvailableDate/UnAvailableDate${dateOnly}`, {
        method: "GET",
        headers: {
          accept: "text/plain",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem('token')
        },
      });
  
      if (response.status === 200) {
        const jsonData = await response.json();
        const extractedDates = jsonData.map((item) => item.unavailableDate.split('T')[0]);
        setUnavailableDates(extractedDates);
      } else if (response.status === 401) {
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
  

  const fetchDelivery = async () => {
    try {
      const response = await fetch(`https://localhost:7239/api/DeliveryOptions/View_All_DeliveryOptions`,{
        "method":"GET",
        headers:{
            "accept": "text/plain",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        },
      });
      if (response.status === 200) {
        const jsonData = await response.json();
      setDelOption(jsonData);
      if (jsonData.length > 0) {
        setSelected(jsonData[0].deliveryOption1); // Select the first option by default
      }
    }  else if (response.status === 401) {
      // Handle 401 unauthorized error
      console.error("Unauthorized: Please log in again.");
      navigate('/sessionexpired');
      // You can perform a redirect or show a message to the user here
    } else {
      const errorData = await response.json();
      throw new Error(errorData.message);
    }
    }
    catch (error) {
      console.error("Login error:", error);
      <SessionExpire />
    }
  };
  
  const fetchTime = async () => {
    try {
      const response = await fetch(`https://localhost:7239/api/TimeSlots/View_All_TimeSlots`,{
        "method":"GET",
        headers:{
            "accept": "text/plain",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        },
      });
      if (response.status === 200) {
        const res = await response.json();
        setTimeSlots(res);
      }  else if (response.status === 401) {
        // Handle 401 unauthorized error
        console.error("Unauthorized: Please log in again.");
        navigate('/sessionexpired');
        // You can perform a redirect or show a message to the user here
      } else {
        const errorData = await response.json();
        throw new Error(errorData.message);
      }
      
    }
    catch (error) {
      console.error("Login error:", error);
      <SessionExpire />
    }
  };

  const slots = async (selectedDate) => {
    try {
      const response = await fetch(`https://localhost:7239/api/Orders/GetAvailableTimeSlot/AvailableTimeSlot${selectedDate}`,{
        "method":"GET",
        headers:{
            "accept": "text/plain",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
        },
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const jsonData = await response.json();
      setAvailableSlots(jsonData);
    } 
    catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const isTimeSlotAvailable = (picking) => {
    return availableSlots.some((slot) => slot.availableTimeSlot === picking);
  };

  const handleChange = (event) => {
    const option = event.target.value;
    setSelected(option);
    setIsDeliveryOptionFilled(true);
    setIsAddressFilled(option === '1' || !!address);
    // Update the deliveryOptionId property of finalOrder based on the selected option
    setFinalOrder((prevOrder) => ({
      ...prevOrder,
      deliveryOptionId: option,
    }));
    console.log('Selected Delivery Option:', option);
    console.log('Updated Final Order:', finalOrder);
  };

  const handleDateInputClick = () => {
    setIsDatePickerOpen(true);
  };

  const handleTimeButtonClick = (event) => {
    if (selectedDate) {
      setAnchorEl(event.currentTarget);
    }
  };

  const handlePopoverClose = () => {
    setAnchorEl(null);
  };

  const handleApplyButtonClick = () => {
    const selectedTimeSlotText = getTimeSlotText(selectedTimeBtn);
    const inputElement = document.getElementById('time-input');
    inputElement.placeholder = selectedTimeBtn ? selectedTimeSlotText : 'Select time';
    setAnchorEl(null);
    setIsTimeEnabled(true);
    setIsTimeFilled(true);
    setIsDateFilled(!!selectedDate);
  };

  const handleCancelButtonClick = () => {
    setSelectedTimeBtn(null);
    setAnchorEl(null);
    setSelectedDate(null);
    setIsTimeEnabled(false);
    setIsTimeFilled(false);
  };

  const handleTimeBtnClick = (timeBtnId, timeslotname) => {
    setSelectedTimeBtn(timeBtnId);
    setTimeSlotName(timeslotname)
  };

  const getTimeSlotText = (timeBtnId) => {
    const picking = timeSlots.find((slot) => slot.id === timeBtnId);
    return picking ? picking.addTimeSlot : '';
  };

  const theme = createTheme({
    palette: {
      primary: {
        main: orange['A700'],
      },
    },
  });

  const HtmlTooltip = styled(({ className, ...props }) => (
    <Tooltip {...props} classes={{ popper: className }} />
  ))(({ theme }) => ({
    [`& .${tooltipClasses.tooltip}`]: {
      backgroundColor: 'rgba(255, 146, 1, 1)',
      color: 'white',
      maxWidth: 600,
      innerHeight: 90,
      fontSize: 16,
      border: 'none',
      placement:'top',
      borderRadius: 3,
      marginBottom:30,

    },
    [`& .${tooltipClasses.arrow}`]: {
      color: 'rgba(255, 146, 1, 1)',
      fontSize: 8,
    },
  }));

  const isDateDisable = (date) => {
    const day = date.day();
    const dateString = date.format('YYYY-MM-DD');
    if (unavailableDates.includes(dateString)) {
      return true;
    }
  };

  const calculatePercentage = () => {
    if (isAddressFilled){
      return 20;
    }
    return 0;
  }

  useEffect(() => {
    // Update the date property of finalOrder whenever selectedDate changes
    if (selectedDate) {
      const formattedDate = dayjs(selectedDate).format('YYYY-MM-DD');
      setFinalOrder((prevOrder) => ({
        ...prevOrder,
        date: formattedDate,
      }));
      console.log('dateselection',finalOrder);
    console.log('selected', selectedDate)
    }
  }, [selectedDate]);

  useEffect(() => {
    // Update the timeSlotId property of finalOrder whenever selectedTimeBtn changes
    if (selectedTimeBtn !== null) {
      setFinalOrder((prevOrder) => ({
        ...prevOrder,
        timeSlotId: selectedTimeBtn,
      }));
      console.log('timeselection',finalOrder);
    }
  }, [selectedTimeBtn]);

  useEffect(() => {
    // Update the address property of finalOrder whenever the address state changes
    setFinalOrder((prevOrder) => ({
      ...prevOrder,
      address: address,
    }));
    console.log('address',finalOrder);
  }, [address]);
  

  return (
    <div>
      <div className="datecontainer">
        <Header></Header>
        <img src={bg} id="bg" alt="background"></img>
        <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'></link>

        <div>
          <p id="centertext">Let's Started!</p>
        </div>

        <div className="col1">
          <img src={Dateimg} alt="date" id="date"></img>
          <p> When will the party be?</p>
        </div>

        <div className="col2" onClick={handleDateInputClick}>
          <img src={datepicker} alt="datepicker" id="dateimg" />
          <input type="text" className="datepicker" id="date-input" placeholder="Select date" />
        </div>
        <span className="col2hr"><hr /></span>

        <div className="col3" onClick={handleTimeButtonClick} disabled={!isTimeEnabled} >
          <img src={time} alt="time" id="timeimg" />
          <input type="text" className="time" id="time-input" placeholder="Select time" style={{ backgroundColor: '#fff', }} />
        </div>
        <span className="col3hr" sx={{ color: '#e0e0e0', }}><hr /></span>

        <Popover id={id} open={open} anchorEl={anchorEl} onClose={handlePopoverClose}
          anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'left',
          }}
          style={{
            marginTop: '40px',
            boxShadow: 'none',
          }}
          classes={{
            paper: 'popover-paper',
          }}>

          <div class="timeslots">
            <p class="slothead" >Pick a slot</p><br />
            <div class="btn-container">
              {timeSlots.map((res) => (
                !isTimeSlotAvailable(res.addTimeSlot) ? 
                (<HtmlTooltip title="Not Available" placement="top" disableInteractive disableFocusListener disableTouchListener arrow>
                  <div>
                    <button
                      class={`time-btn ${selectedTimeBtn === res.id ? 'selected' : ''}`}
                      onClick={() => handleTimeBtnClick(res.id, res.addTimeSlot)}
                      disabled={!isTimeSlotAvailable(res.addTimeSlot)}>
                      {res.addTimeSlot}
                    </button>
                  </div>
                </HtmlTooltip>) :
                 (<div>
                  <button
                    class={`time-btn ${selectedTimeBtn === res.id ? 'selected' : ''}`}
                    onClick={() => handleTimeBtnClick(res.id, res.addTimeSlot)}
                    disabled={!isTimeSlotAvailable(res.addTimeSlot)} >
                    {res.addTimeSlot}
                  </button>
                </div>)
            ))}
            </div>
            <div class="btns">
              <div><button type="cancel" class="cancel" onClick={handleCancelButtonClick}>Cancel</button></div>
              <div ><button type="submit" class="apply" onClick={handleApplyButtonClick}>Apply</button></div>
            </div>
          </div>
        </Popover>

        <div className='col4'>
          <img src={delivery} alt="delivery" id="delivery" />
          <p>Select a delivery option</p>
        </div>

        <FormControl>
          <RadioGroup row  aria-labelledby="demo-row-radio-buttons-group-label" 
            name="row-radio-buttons-group" 
            value={selected}
            onChange={handleChange}>

            {delOption.map((jsonData) => (
              <FormControlLabel
                key={jsonData.id}
                value={jsonData.id}
                className={`${extra}${jsonData.id}`}
                control={
                  <Radio 
                    sx={{
                      '&.Mui-checked': {
                        color: orange['A700'],
                      },
                      color: '#9e9e9e',
                    }} />
                }
                label={
                  <Typography component="span" sx={{ fontFamily: 'Poppins, sans-serif', color: 'black', }}>
                    {jsonData.deliveryOption1}
                  </Typography>
                } />
            ))}
          </RadioGroup>
        </FormControl>

        {selected === '2' || selected === '3' ? (
          <div>
            <p id="locationdiv">
              <img src={location} id="locimg" alt="location" />
              <input type="text" style={{ fontSize: '16px', }} name="location" id="location" placeholder="Address" onChange={(e) => {
                setAddress(e.target.value);
                setIsAddressFilled(!!e.target.value);
              }} />
            </p>
            <span className="addhr"><hr /></span>
          </div>
        ) : null}

        {isDatePickerOpen && (
          <ThemeProvider theme={theme}>
            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <DateCalendar
                views={['day']}
                minDate={tomorrow}
                shouldDisableDate={isDateDisable}
                class="calendar"
                onChange={(date) => {
                   setSelectedDate(date);
                   setIsDateSelected(true);
                }} />

              <div class="datebox">
                <Button class="btndatecancel" onClick={() => setIsDatePickerOpen(false)}>
                  Cancel
                </Button>
                <Button
                  disabled={!isDateSelected}
                  class={!isDateSelected ? "btndateapplydisable": "btndateapply"}
                  onClick={() => {
                    const inputElement = document.getElementById('date-input');
                    inputElement.placeholder = selectedDate ? selectedDate.format('MMMM D, YYYY') : 'Select date';
                    setIsDatePickerOpen(false);
                    slots(selectedDate.format('YYYY-MM-DD'));
                  }}>
                  Apply
                </Button>
              </div>
            </LocalizationProvider>
          </ThemeProvider>
        )}
      </div>

      <div className="footertime">
          {isDateFilled && isTimeFilled && ((selected === 'Pickup') || (isDeliveryOptionFilled && isAddressFilled)) && (
            <Link to="/home" style={{ color: 'white', textDecoration: "none" }}><button type="button" className="prbtn">&#60; Back</button></Link>
          )}

          <div className="Ifooter">
              Step<span style={{color:'orange',}}> 1 </span> of 5
          </div>
          <div className="progress-bartime">
            <div className="progresstime" style={{ width: `${calculatePercentage()}%` }}></div>
          </div>
          <h6 id="percentagetime" className="mt-1" style={{ left: `${calculatePercentage() === 0 ? '24%' : '34%'}` }}>
            {`${calculatePercentage()}%`}
          </h6>

          <Link to="/groupsize" style={{ color: 'white', textDecoration: "none" }}><button type="button" disabled={isButtonDisabled} class={isButtonDisabled ? "buttonDisabledStyle": "prbtn1"}>Continue &#62;</button></Link>
      </div>
    </div>
  );
};
export default Dateandtime;

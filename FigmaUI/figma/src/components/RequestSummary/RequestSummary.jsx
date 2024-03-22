import React, { useContext, useEffect, useState } from "react";
import Summarylogo from "../../Assets/summarylogo.png";
import Header from '../food_Selection/Components/header'
import "../RequestSummary/RequestSummary.css";
import emailjs from 'emailjs-com';
import { Link, useNavigate } from "react-router-dom";
import { ApiContext } from "../ApiContextProvider";
import axios from "axios";
import OrderDetail from '../OrderDetail/OrderDetail'
import designimg from "../../Assets/designimg.png";
import logLogo from "../../Assets/loglogo.png";
import errpage from "../../Assets/loginPage .png";
import Button from '@mui/material/Button';


function RequestSummary() {
  const {
    userMail,
    userFullName,
    userPhone,
    finalOrder, //post
    setFinalOrder,
    plateSizeForUse,
    vegItemsSelected,
    nonVegItemsSelected,
    addOnsList, //post
    guestCount,
    timeSlotName,
    typeFood,
    plateCostForUse,
    dataList, //post
    dataList2,
    orderedFoodList, //post
    allergiesMentioned, //post
    selectedFoodTypes, //post
    setTrackingStatus, //Return from the Post
  } = useContext(ApiContext);

  const [addOnsCost, setAddOnsCost] = useState([]); // Store calculated costs for add-ons
  const [dataListCost, setDataListCost] = useState([]); // Store calculated costs for dataList
  const [totalCost, setTotalCost] = useState(0); // Total cost variable

  var navigate = useNavigate();

  const discount = totalCost / 10;
  const tax = (totalCost - discount) / 10;
  const finalCost = totalCost - discount + tax;

  const formattedDiscount = discount.toFixed(2);
  const formattedTax = tax.toFixed(2);
  const formattedFinalCost = finalCost.toFixed(2);

  const handleSendRequest = async () => {
    try {
      const response = await axios.post(
        "https://localhost:7239/api/Orders/Add_Order",
        finalOrder
        ,{
          headers: {
            "accept": "text/plain",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
          }
            });
      console.log("Response from Add_Order API:", response.data);
      console.log("final", finalOrder)

      //Email
      // Update the orderId in state variables
      const updatedAddOnsList = addOnsList.map((addOn) => ({
        ...addOn,
        orderId: response.data.id,
      }));

      const updatedDataList = dataList.map((data) => ({
        ...data,
        orderId: response.data.id,
      }));

      const updatedOrderedFoodList = orderedFoodList.map((orderedFood) => ({
        ...orderedFood,
        orderId: response.data.id,
      }));

      const updatedAllergiesMentioned = allergiesMentioned.map((allergy) => ({
        ...allergy,
        ordersId: response.data.id,
      }));

      const updatedSelectedFoodTypes = selectedFoodTypes.map((foodType) => ({
        ...foodType,
        orderId: response.data.id,
      }));
      //   const updatedTrackingStatus = trackingStatus.map((track) => ({
      //     ...track,
      //     orderId: response.data.id,
      //   }));
      const updatedTrackingStatus = {
        orderId: response.data.id,
        status: "pending",
        id: 0,
        tid: "string",
      };

      // Filter allergies with valid allergyId
      const validAllergies = updatedAllergiesMentioned.filter(
        (allergy) => allergy.allergyId
      );


      // Send updated state variables to their respective APIs
      if (addOnsList.length > 0) {
        const addOnsResponse = await axios.post(
          "https://localhost:7239/api/AddOnsDetails/Add_AddOnsDetail",
          updatedAddOnsList
          ,{
            headers: {
              "accept": "text/plain",
              "Authorization": "Bearer " + sessionStorage.getItem('token')
            }
              });
        console.log("Response from Add_AddOnsDetail API:", addOnsResponse.data);
      }

      if (dataList.length > 0) {
        const dataListResponse = await axios.post(
          "https://localhost:7239/api/AdditionalProductsDetails/Add_AdditionalProductsDetail",
          updatedDataList
          ,{
            headers: {
              "accept": "text/plain",
              "Authorization": "Bearer " + sessionStorage.getItem('token')
            }
              });
        console.log(
          "Response from Add_AdditionalProductsDetail API:",
          dataListResponse.data
        );
      }



      const orderedFoodResponse = await axios.post(
        "https://localhost:7239/api/StdFoodOrderDetails/Add_StdFoodOrderDetail",
        updatedOrderedFoodList
        ,{
          headers: {
            "accept": "text/plain",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
          }
            });
      console.log(
        "Response from Add_StdFoodOrderDetail API:",
        orderedFoodResponse.data
      );

      let allergiesResponse;
      if (validAllergies.length > 0) {
        allergiesResponse = await axios.post(
          "https://localhost:7239/api/AllergyDetails/Add_AllergyDetail",
          validAllergies
          ,{
            headers: {
              "accept": "text/plain",
              "Authorization": "Bearer " + sessionStorage.getItem('token')
            }
              });
        console.log(
          "Response from Add_AllergyDetail API:",
          allergiesResponse.data
        );
      } else {
        console.log("No valid allergies to add.");
      }
console.log("updatedSelectedFoodTypes",updatedSelectedFoodTypes);
      const foodTypeCountsResponse = await axios.post(
        "https://localhost:7239/api/FoodTypeCounts/Add_FoodTypeCount",
        updatedSelectedFoodTypes
        ,{
          headers: {
            "accept": "text/plain",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
          }
            });
      console.log(
        "Response from Add_FoodTypeCount API:",
        foodTypeCountsResponse.data
      );

      //   const TrackStatusResponse = await axios.post("https://localhost:7239/api/TrackStatus/Add_TrackStatus", updatedTrackingStatus);
      //   console.log("Response from Add_trackstatus API:", TrackStatusResponse.data);

      const TrackStatusResponse = await axios.post(
        "https://localhost:7239/api/TrackStatus/Add_TrackStatus",
        updatedTrackingStatus
        ,{
          headers: {
            "accept": "text/plain",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
          }
            });
      console.log(
        "Response from Add_trackstatus API:",
        TrackStatusResponse.data
      );

      const TrackId = () => {
        const trackId = TrackStatusResponse.data.tid;
        setTrackingStatus(TrackStatusResponse.data.tid)

        const serviceID = 'KaniniTourism';
        const templateID = 'template_ezgwz3b';
        const userID = 'eKtqdRwigRV6DRuta';

        const templateParams = {
          to_email: [userMail],
          message: `Your TrackID is: ${trackId}`,
        };


        emailjs.send(serviceID, templateID, templateParams, userID)
          .then((response) => {
            if (response.status === 200) {
              // alert(' Email sent successfully.');
              console.log('Email sent successfully:', response);
            } else {
              alert('Failed to send the email. Please try again later.');
              console.error('Failed to send the email:', response);
            }
          })
          .catch((error) => {
            alert('Failed to send the email. Please try again later.');
            console.error('Error sending the email:', error);
          });
      };
      TrackId();
      navigate("/Requestsuccess");


    } catch (error) {
      console.error("Error sending request:", error);

    }
  };


  useEffect(() => {
    const totalAddOnsCost = addOnsCost.reduce((sum, cost) => sum + cost, 0);
    const totalDataListCost = dataListCost.reduce((sum, cost) => sum + cost, 0);
    const newTotalCost =
      totalAddOnsCost + totalDataListCost + guestCount * plateCostForUse;
    setTotalCost(newTotalCost);
  }, [addOnsCost, dataListCost]);

  useEffect(() => {
    // Calculate costs for add-ons
    const addOnsCosts = addOnsList.map((item) => item.quantity * item.cost);
    setAddOnsCost(addOnsCosts);

    // Calculate costs for dataList
    const dataListCosts = dataList.map((item) => item.quantity * item.cost);
    setDataListCost(dataListCosts);
  }, [addOnsList, dataList]);

  const formatDate = (dateString) => {
    const options = { month: "long", year: "numeric" };
    const formattedDate = new Date(dateString).toLocaleDateString(
      "en-US",
      options
    );

    const day = new Date(dateString).getDate();
    const suffix =
      day === 1 ? "st" : day === 2 ? "nd" : day === 3 ? "rd" : "th";

    return `${day}${suffix} ${formattedDate}`;
  };

  if (finalOrder.date === "" || timeSlotName === "") {
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
          <Link to='/order_details'>
            <Button sx={{
              backgroundColor: "#ff6d00", color: 'rgb(255, 255, 255)', alignItems: 'flex-end', justifyContent: "center", marginTop: '20px', marginLeft: '220px', width: '180px', height: '50px', fontWeight: 'bold', fontSize: '18px', '&:hover': {
                backgroundColor: "orangered", // Change this to your desired hover color

              }
            }} >
              Go Back
            </Button>


          </Link>
        </div>
      </div>

    );
  }

  return (
    <>
      {/* NAVBAR */}
      <Header />

      <Link to="/order_details" style={{ textDecoration: 'none' }}>
        <button type="button" className="mobilephonebutton" style={{ color: "orangered", textDecoration: "none" }}>&#60; Back</button>
      </Link>


      <div className="bodyofsummary">
        {/* LOGO AND HEADING */}
        <div className="logoandheadingsummary">
          <img src={Summarylogo} alt="summary" />{" "}
          <h3>
            <b className="Summarytext">Summary</b>
          </h3>
        </div>


        {/* ORDER DETAILS INFORMATION */}

        <div className="orderdetailsinsummary">
          <div className="information1" >
            
            <div className="Informationinsummary">
              Delivery Date: <b>{formatDate(finalOrder.date)}</b>
            </div>
          </div>

          <div className="information2" >
            <div className="Informationinsummary">
              Time: <b>{timeSlotName}</b>
            </div>
            <div className="Informationinsummary">
              Total Head Count: <b>{guestCount}</b>
            </div>
          </div>
        </div>


        {/* CONTACT DETAILS TEXTBOXES */}
        <div className="contactdetailsinsummary">
          <span className="ContactTextinSummary">Contact Details</span>

          <div className="inputdetail">
            <div className="rowsininputdetails">
              <div className="input-container-summary">
                <label htmlFor="full-name">
                  Full Name<span className="required">*</span>:
                </label>
                <input
                  className="inputboxesinsummary"
                  type="text"
                  id="full-name"
                  required
                  defaultValue={userFullName}
                  onChange={(e) =>
                    setFinalOrder({
                      ...finalOrder,
                      contactName: e.target.value,
                    })
                  }
                />
              </div>
              <div className="input-container-summary">
                <label htmlFor="email">
                  Email ID<span className="required">*</span>:
                </label>
                <input
                  className="inputboxesinsummary"
                  type="email"
                  id="email"
                  required
                  defaultValue={userMail}
                  onChange={(e) =>
                    setFinalOrder({
                      ...finalOrder,
                      contactEmail: e.target.value,
                    })
                  }
                />
              </div>
              <div className="input-container-summary" >
                <label htmlFor="contact-number">
                  Contact Number<span className="required">*</span>:
                </label>
                <input
                  className="contactboxforipad"
                  type="tel"
                  id="contact-number"
                  required
                  defaultValue={userPhone}
                  onChange={(e) =>
                    setFinalOrder({
                      ...finalOrder,
                      contactNumber: e.target.value,
                    })
                  }
                />
              </div>
            </div>
            {finalOrder.deliveryOptionId === "2" ||
              finalOrder.deliveryOptionId === "3" ? (
              <div className="delivery-address-summary">
                <label htmlFor="delivery-address">
                  Delivery Address<span className="required">*</span>:
                </label>
                <textarea
                  rows="4"
                  id="delivery-address"
                  required
                  value={finalOrder.address}
                  onChange={(e) =>
                    setFinalOrder({ ...finalOrder, address: e.target.value })
                  }
                ></textarea>
              </div>
            ) : null}
            {console.log(finalOrder)}
          </div>
        </div>

        {/* Tables for order and billing */}
        <div className="tablesinsummary">
          <div className="orderdetailstableinsummary">
            <span className="ordertextinsummary">Order Details</span>
            <table class="order-details-table">
              <thead className="tableheaderinsummary">
                <tr className="tableheadertextinsummary" >
                  <th>Item</th>
                  <th>Qty</th>
                  <th>Cost/Plate</th>
                  <th>Amount</th>
                </tr>
              </thead>
              <tr>
                <td className="text-right-summary">
                  <b>
                    {plateSizeForUse}: {typeFood}
                  </b>
                </td>
                <td className="text-right-summary">
                  <b>{guestCount}</b>
                </td>
                <td className="text-right-summary">
                  <b>${plateCostForUse}</b>
                </td>
                <td className="text-right-summary">
                  <b>${guestCount * plateCostForUse}</b>
                </td>
              </tr>

              <tbody className="right-fooditems">
                <td>
                  {Object.keys(vegItemsSelected).map((category) => (
                    <React.Fragment key={category}>
                      {vegItemsSelected[category].map((item, index) => (
                        <React.Fragment key={index}>
                          <span style={{ color: "var(--dark)" }}>
                            {item.productsName}
                          </span>
                          {index !== vegItemsSelected[category].length - 1 && (
                            <span>, </span>
                          )}
                        </React.Fragment>
                      ))}

                      {category !==
                        Object.keys(vegItemsSelected)[
                        Object.keys(vegItemsSelected).length - 1
                        ] && <span>, </span>}
                    </React.Fragment>
                  ))}

                  {Object.keys(nonVegItemsSelected).length > 0 && (
                    <>
                      {Object.keys(vegItemsSelected).length > 0 && (
                        <span>, </span>
                      )}
                      {Object.keys(nonVegItemsSelected).map((category) => (
                        <React.Fragment key={category}>
                          {nonVegItemsSelected[category].map((item, index) => (
                            <React.Fragment key={index}>
                              <span style={{ color: "var(--dark)" }}>
                                {item.productsName}
                              </span>
                              {index !==
                                nonVegItemsSelected[category].length - 1 && (
                                  <span>, </span>
                                )}
                            </React.Fragment>
                          ))}

                          {category !==
                            Object.keys(nonVegItemsSelected)[
                            Object.keys(nonVegItemsSelected).length - 1
                            ] && <span>, </span>}
                        </React.Fragment>
                      ))}
                    </>
                  )}
                </td>
              </tbody>
              <tr>
                <td className="text-right-summary">
                  {/* <b>Add ons</b> */}
                </td>
                <td className="text-right-summary"></td>
                <td className="text-right-summary"></td>
                <td className="text-right-summary"></td>
              </tr>
              <tr>
                <td class="bold">
                  {addOnsList.map((item, index) => (
                    <React.Fragment key={index}>
                      {item.addonsName}
                      {index !== addOnsList.length - 1 && <br />}
                    </React.Fragment>
                  ))}
                </td>
                <td className="text-right-summary" class="bold">
                  {addOnsList.map((item, index) => (
                    <React.Fragment key={index}>
                      <b>{item.quantity}</b>
                      {index !== addOnsList.length - 1 && <br />}
                    </React.Fragment>
                  ))}
                </td>
                <td className="text-right-summary" class="bold">
                  {addOnsList.map((item, index) => (
                    <React.Fragment key={index}>
                      <b>${item.cost}</b>
                      {index !== addOnsList.length - 1 && <br />}
                    </React.Fragment>
                  ))}
                </td>
                <td className="text-right-summary" class="bold">
                  {addOnsList.map((item, index) => (
                    <React.Fragment key={index}>
                      <b>${item.quantity * item.cost}</b>
                      {index !== addOnsList.length - 1 && <br />}
                    </React.Fragment>
                  ))}
                </td>
              </tr>

              <tr>
                <td className="text-right-summary">
                  {/* <b>Additional Products</b> */}
                </td>
                <td className="text-right-summary"></td>
                <td className="text-right-summary"></td>
                <td className="text-right-summary"></td>
              </tr>

              <tr>
                <td class="bold">
                  {dataList2.map((item, index) => (
                    <React.Fragment key={index}>
                      {item.additionalProductsName}
                      {console.log(item.additionalProductsName)}
                      {index !== dataList.length - 1 && <br />}{" "}

                    </React.Fragment>
                  ))}
                </td >
                <td className="text-right-summary" class="bold">
                  {dataList2.map((item, index) => (
                    <React.Fragment key={index}>
                      <b>{item.quantity}</b>
                      {index !== dataList.length - 1 && <br />}{" "}

                    </React.Fragment>
                  ))}
                </td>
                <td className="text-right-summary" class="bold">
                  {dataList2.map((item, index) => (
                    <React.Fragment key={index}>
                      <b>${item.cost}</b>
                      {index !== dataList.length - 1 && <br />}{" "}

                    </React.Fragment>
                  ))}
                </td>

                <td className="text-right-summary" class="bold">
                  {dataList2.map((item, index) => (
                    <React.Fragment key={index}>
                      <b>${item.quantity * item.cost}</b>
                      {index !== dataList.length - 1 && <br />}{" "}

                    </React.Fragment>
                  ))}
                </td>
              </tr>
              <tr>
                <td><hr></hr></td>
                <td><hr></hr></td>
                <td><hr></hr></td>
                <td><hr></hr></td>
              </tr>

              <tr>
                <td className="font-weight-bold" class="text-right-summary">
                  <b>Total</b>
                </td>
                <td className="text-right-summary"></td>
                <td className="text-right-summary"></td>
                <td className="text-right-summary">
                  <span
                    className="font-weight-bold"
                    style={{ color: "#00B373" }}
                  >
                    <b>${totalCost}</b>
                  </span>
                </td>
              </tr>

              <tr>
                <td><hr></hr></td>
                <td><hr></hr></td>
                <td><hr></hr></td>
                <td><hr></hr></td>
              </tr>
            </table>
          </div>

          <div className="billingdetailstableinsummary">
            <span className="billingtextinsummary">Billing Details</span>
            <table class="billing-details-table">
              <thead className="tableheaderinsummary">
                <tr className="text-right-summary">
                  <th className="tableheadertextinsummary">Payable Summary</th>
                  <th></th>
                </tr>
              </thead>
              <tbody className="tablebodyinsummary">
                <tr className="textinbillingtablesummary">
                  <td className="text-right-billing">Subtotal</td>
                  <td className="text-right-billing"> ${totalCost}</td>
                </tr>
                <tr>
                  <td className="text-right-billing"> Delivery</td>
                  <td className="text-right-billing"><span style={{ color: 'rgb(0, 179, 115)' }}><b> Free</b></span></td>
                </tr>
                <tr>
                  <td className="text-right-billing"> Discount</td>
                  <td className="text-right-billing"> -${formattedDiscount} (10%)</td>
                </tr>
                <tr>
                  <td className="text-right-billing"> Tax</td>
                  <td className="text-right-billing"> +${formattedTax} (10%)</td>
                </tr>
                <tr>
                  <td >
                    <hr />
                  </td>
                  <td>
                    <hr />
                  </td>
                </tr>
                <tr>
                  <td className="text-right-billing-total"> <b>Total Payable</b></td>
                  <td className="text-right-billing-total"> <span style={{ color: 'rgb(0, 179, 115)' }}><b>${formattedFinalCost}</b></span></td>
                </tr>

                <tr>
                  <td >
                    <hr />
                  </td>
                  <td>
                    <hr />
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>


        <br></br>

        <br></br>
        <br></br>
        <br></br>
        <br></br>

        <br></br>
        <br></br>
        <br></br>

        <br></br><br></br><br></br>

        {/* FOOTER */}
        <div className="ft" >
          <Link to='/order_details'><button type="button" className="btnsummary" style={{ color: "orangered", textDecoration: "none" }}>&#60; Back</button>
          </Link>
          <button type="button" onClick={handleSendRequest} id="btn1summary" style={{ color: 'white', textDecoration: "none" }}>Send Request &#62;</button>

          <button type="button" onClick={handleSendRequest} id="btn1summary" style={{ color: 'white', textDecoration: "none" }}>Send Request &#62;</button>

        </div>
      </div>
    </>
  );
}

export default RequestSummary;

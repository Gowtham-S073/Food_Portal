import React, { useContext, useEffect, useState } from "react";
import "./OrderDetail.css";
import Cart from "../../Assets/Cart.png";
import Additional from "../../Assets/Additional.png";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import Logo from "../../Assets/Logo.png";
import Header from '../Header/header';
import { ApiContext } from "../ApiContextProvider";
import { Link } from "react-router-dom";
import { Button } from "@mui/material";
import designimg from "../../Assets/designimg.png";
import logLogo from "../../Assets/loglogo.png";
import errpage from "../../Assets/loginPage .png";

function OrderDetail() {
  const {
    finalOrder,
    setFinalOrder,
    plateSizeForUse,
    vegItemsSelected,
    nonVegItemsSelected,
    addOnsList,
    guestCount,
    timeSlotName,
    typeFood,
    plateCostForUse,
    dataList,
    dataList2,
    orderedFoodList
  } = useContext(ApiContext);

  const [addOnsCost, setAddOnsCost] = useState([]); // Store calculated costs for add-ons
  const [dataListCost, setDataListCost] = useState([]); // Store calculated costs for dataList
  const [totalCost, setTotalCost] = useState(0); // Total cost variable
  const isSmallScreen = window.innerWidth < 375;

  // Calculate the total cost whenever addOnsCost or dataListCost changes
  useEffect(() => {
    const totalAddOnsCost = addOnsCost.reduce((sum, cost) => sum + cost, 0);
    const totalDataListCost = dataListCost.reduce((sum, cost) => sum + cost, 0);
    const newTotalCost = totalAddOnsCost + totalDataListCost + (guestCount * plateCostForUse);
    setTotalCost(newTotalCost);
  }, [addOnsCost, dataListCost]);

  const [isMobileView, setIsMobileView] = useState(window.matchMedia("(max-width: 700px)").matches);

  useEffect(() => {
    const handleResize = () => {
      setIsMobileView(window.matchMedia("(max-width: 700px)").matches);
    };

    window.addEventListener("resize", handleResize);

    // Cleanup the listener when the component is unmounted
    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  useEffect(() => {
    // Calculate costs for add-ons
    const addOnsCosts = addOnsList.map(item => item.quantity * item.cost);
    setAddOnsCost(addOnsCosts);

    // Calculate costs for dataList
    const dataListCosts = dataList.map(item => item.quantity * item.cost);
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

  const [alignment, setAlignment] = useState("Soda");

  const handleValueChange = (event, newAlignment) => {
    if (newAlignment !== null) {
      setAlignment(newAlignment);

      // Get the current plateSizeId from finalOrder
      const currentPlateSizeId = finalOrder.plateSizeId;

      // Update drinkId of finalOrder based on selected toggle and plateSizeId
      let newDrinkId = null;

      if (currentPlateSizeId >= 2 && currentPlateSizeId <= 4) {
        newDrinkId = newAlignment === "Soda" ? 1 : 2;
      }

      setFinalOrder((prevFinalOrder) => ({
        ...prevFinalOrder,
        drinkId: newDrinkId,
      }));
    }
  };
  useEffect(() => {
    if (finalOrder.plateSizeId >= 2 && finalOrder.plateSizeId <= 4) {
      setFinalOrder((prevFinalOrder) => ({
        ...prevFinalOrder,
        drinkId: alignment === "Soda" ? 1 : 2,
      }));
    }
  }, [finalOrder.plateSizeId, alignment]);
  console.log("drink update", finalOrder.drinkId);

  const handleNoteChange = (event) => {
    const newNote = event.target.value;

    // Update the additionalNote field of finalOrder
    setFinalOrder((prevFinalOrder) => ({
      ...prevFinalOrder,
      additionalNote: newNote,
    }));
    console.log("note", finalOrder);
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
      <div>
        <nav>
          <img src={Logo} className="head" alt="headerImage" />
        </nav>
      </div>

      {isMobileView && (
       <Link to="/food_selection"> <Button
          sx={{
            marginTop: "10px",
            backgroundColor: "white",
            textTransform: "none",
            color: "#FF6D00",
            "@media (max-width: 700px)": {
              mx: "30px",
              fontSize: "18px",
            },
            "@media (max-width: 600px)": {
              mx: "0px",
              fontSize: "16px",
            },
            "@media (max-width: 450px)": {
              mx: "0px",
              fontSize: "16px",
            },
          }}
          variant="text"
          color="primary"

        >
          &#60; Back
        </Button></Link>
      )}

      <div className="Detail1">
        <h3>
          <img src={Cart} alt="Cart" /> <b>Order Details</b>
        </h3>
      </div>
      <div className="Detail2">
        <div className="id1" style={{ display: "flex" }}>
          <div className="Info1">
            Delivery Date: <b>{formatDate(finalOrder.date)}</b>
          </div>
          <div className="Info1">
            Time: <b>{timeSlotName}</b>
          </div>
        </div>

        <div className="id2" style={{ display: "flex" }}>
          <div className="Info1">
            Total Head Count: <b>{guestCount}</b>
          </div>
        </div>
      </div>

      {/* Rest of the table content... */}
      <div className="custom-container">
        <div className="row mt-5">
          <div className="col">
            <div className="table-responsive">
              <table className="table table-borderless">
                <tr style={{ backgroundColor: "#EBEBEB", padding: "50px" }}>
                  <td
                    scope="col"
                    style={{
                      width: isSmallScreen ? '50px' : '250px', // Adjust width based on screen width
                      // Add other styles here
                    }}
                  >
                    Item
                  </td>
                  <td
                    scope="col"
                    className="text-right"
                    style={{ minWidth: "50px" }}
                  >
                    Qty
                  </td>
                  <td
                    scope="col"
                    className="text-right"
                    style={{ minWidth: "50px" }}
                  >
                    Cost/Plate
                  </td>
                  <td scope="col" className="text-right">
                    Amount
                  </td>
                </tr>
                <tbody style={{ padding: "30px" }}>
                  <tr>
                    <td>
                      <b>
                        {plateSizeForUse}: {typeFood}
                      </b>
                    </td>
                    <td className="text-right">
                      <b>{guestCount}</b>
                    </td>
                    <td className="text-right">
                      <b>${plateCostForUse}</b>
                    </td>
                    <td className="text-right">
                      <b>${guestCount * plateCostForUse}</b>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      {Object.keys(vegItemsSelected).map((category) => (
                        <React.Fragment key={category}>
                          {vegItemsSelected[category].map((item, index) => (
                            <React.Fragment key={index}>
                              <span style={{ color: "var(--dark)" }}>
                                {item.productsName}
                              </span>
                              {index !==
                                vegItemsSelected[category].length - 1 && (
                                  <span>, </span>
                                )}
                            </React.Fragment>
                          ))}
                          {/* Only add a comma if there are more categories */}
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
                              {nonVegItemsSelected[category].map(
                                (item, index) => (
                                  <React.Fragment key={index}>
                                    <span style={{ color: "var(--dark)" }}>
                                      {item.productsName}
                                    </span>
                                    {index !==
                                      nonVegItemsSelected[category].length -
                                      1 && <span>, </span>}
                                  </React.Fragment>
                                )
                              )}
                              {/* Only add a comma if there are more categories */}
                              {category !==
                                Object.keys(nonVegItemsSelected)[
                                Object.keys(nonVegItemsSelected).length - 1
                                ] && <span>, </span>}
                            </React.Fragment>
                          ))}
                        </>
                      )}
                    </td>

                    <td className="text-right"></td>
                    <td className="text-right"></td>
                    <td className="text-right"></td>
                  </tr>
                  {addOnsList.length > 0 && (
                    <tr>
                      <td>
                        <b>Add ons</b>
                      </td>
                      <td className="text-right"></td>
                      <td className="text-right"></td>
                      <td className="text-right"></td>
                    </tr>
                  )}
                  <tr>
                    <td>
                      {addOnsList.map((item, index) => (
                        <React.Fragment key={index}>
                          {item.addonsName}
                          {index !== addOnsList.length - 1 && <br />}
                        </React.Fragment>
                      ))}
                    </td>
                    <td className="text-right">
                      {addOnsList.map((item, index) => (
                        <React.Fragment key={index}>
                          <b>{item.quantity}</b>
                          {index !== addOnsList.length - 1 && <br />}
                        </React.Fragment>
                      ))}
                    </td>
                    <td className="text-right">
                      {addOnsList.map((item, index) => (
                        <React.Fragment key={index}>
                          <b>${item.cost}</b>
                          {index !== addOnsList.length - 1 && <br />}
                        </React.Fragment>
                      ))}
                    </td>
                    <td className="text-right">
                      {addOnsList.map((item, index) => (
                        <React.Fragment key={index}>
                          <b>${item.quantity * item.cost}</b>
                          {index !== addOnsList.length - 1 && <br />}
                        </React.Fragment>
                      ))}
                    </td>
                  </tr>

                  <tr>
                    <td className="text-right"></td>
                    <td className="text-right"></td>
                    <td className="text-right"></td>
                  </tr>
                  {dataList2.length > 0 && (
                    <tr>
                      <td>
                        <b>Additional Products</b>
                      </td>
                      <td className="text-right"></td>
                      <td className="text-right"></td>
                      <td className="text-right"></td>
                    </tr>
                  )}
                  <tr>
                    <td>
                      {dataList2.map((item, index) => (
                        <React.Fragment key={index}>
                          {item.additionalProductsName}
                          {console.log(item.additionalProductsName)}
                          {index !== dataList.length - 1 && <br />}{" "}
                          {/* Add line break if not the last item */}
                        </React.Fragment>
                      ))}
                    </td>
                    <td className="text-right">
                      {dataList2.map((item, index) => (
                        <React.Fragment key={index}>
                          <b>{item.quantity}</b>
                          {index !== dataList.length - 1 && <br />}{" "}
                          {/* Add line break if not the last item */}
                        </React.Fragment>
                      ))}
                    </td>
                    <td className="text-right">
                      {dataList2.map((item, index) => (
                        <React.Fragment key={index}>
                          <b>${item.cost}</b>
                          {index !== dataList.length - 1 && <br />}{" "}
                          {/* Add line break if not the last item */}
                        </React.Fragment>
                      ))}
                    </td>

                    <td className="text-right">
                      {dataList2.map((item, index) => (
                        <React.Fragment key={index}>
                          <b>${item.quantity * item.cost}</b>
                          {index !== dataList.length - 1 && <br />}{" "}
                          {/* Add line break if not the last item */}
                        </React.Fragment>
                      ))}
                    </td>
                  </tr>

                  {((finalOrder.plateSizeId == 2) || (finalOrder.plateSizeId == 3) || (finalOrder.plateSizeId == 4)) && (<tr>
                    <td>
                      
                      <ToggleButtonGroup
                        color="primary"
                        value={alignment}
                        exclusive
                        aria-label="Platform"
                        onChange={handleValueChange}
                        style={{ width: '20px', height: '40px', borderRadius: '8px' }} // Apply initial size
                      >
                        <ToggleButton
                          value="Soda"
                          style={
                            alignment === "Soda"
                              ? { backgroundColor: "orange", color: "white" }
                              : {}
                          }
                        >
                          Soda
                        </ToggleButton>
                        <ToggleButton
                          value="Water"
                          style={
                            alignment === "Water"
                              ? { backgroundColor: "orange", color: "white" }
                              : {}
                          }
                        >
                          Water
                        </ToggleButton>
                      </ToggleButtonGroup>
                    </td>
                    <td className="text-right">
                      <b>{guestCount}</b>
                    </td>
                    <td className="text-right"> - </td>
                    <td className="text-right"> - </td>
                  </tr>)}

                  {((finalOrder.plateSizeId == 2) || (finalOrder.plateSizeId == 3) || (finalOrder.plateSizeId == 4)) && (<tr>
                    <div className="Charge-detail">
                      <p>
                        {" "}
                        Either{" "}
                        <span style={{ color: "orange" }}> water bottle </span>
                        or <span style={{ color: "orange" }}>soda</span> has no
                        charge for this plate size.
                      </p>
                    </div>
                  </tr>)}
{console.log("final order", finalOrder)}
{console.log("orderedfoodlist", orderedFoodList)}
                  {((finalOrder.plateSizeId == 2) || (finalOrder.plateSizeId == 3) || (finalOrder.plateSizeId == 4)) && (<tr>
                    <td className="text-right"></td>
                    <td className="text-right"></td>
                    <td className="text-right"></td>
                  </tr>)}

                  <tr>
                    <td
                      colSpan="4"
                      style={{ borderTop: "1px solid grey" }}
                    ></td>
                  </tr>
                  <tr>
                    <td className="font-weight-bold">
                      <b>Total</b>
                    </td>
                    <td className="text-right"></td>
                    <td className="text-right"></td>
                    <td className="text-right">
                      <span
                        className="font-weight-bold"
                        style={{ color: "#00B373" }}
                      >
                        <b>${totalCost}</b>
                      </span>
                    </td>
                  </tr>
                  <tr>
                    <td
                      colSpan="4"
                      style={{ borderTop: "1px solid grey" }}
                    ></td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div >

      <div className="Detail3">
        <ul>
          <li>
            <img src={Additional} alt="Additional" />
            <Link to="/additional_Cat" style={{ color: 'orange', textDecoration: 'none' }}>Add Additional menu Items</Link>{" "}
          </li>
          <li>
            <img src={Additional} alt="Additional" />
            <Link to="/addon" style={{ color: 'orange', textDecoration: 'none' }}> Optional Add-ons</Link>
          </li>
        </ul>
      </div>

      <div className="Detail4" style={{ paddingBottom: '90px' }}>
        <p>Additional Note</p>
        <input
          type="text"
          className="form-control bigger-input"
          value={finalOrder.additionalNote}
          onChange={handleNoteChange}
        />
      </div>

      <div className="OrderDetail_footer">
        {isSmallScreen ? (
          // Media query to hide the button on screens with max-width 375px
          <style>
            {`
      @media screen and (max-width: 375px) {
        .OrderDetail_btn {
          display: none;
        }
      }
    `}
          </style>
        ) : (
          <button
            type="button"
            className="OrderDetail_btn"
          >
            <Link
              to="/food_selection"
              style={{
                textDecoration: "none",
                color: "orange", // Normal color
                display: "block", // Make the link a block element to fill the button
                width: "100%", // Full width of the button
                height: "100%", // Full height of the button
                padding: "10px", // Adjust padding as needed
                textAlign: "center", // Center the link content
              }}
              onMouseEnter={(e) => {
                e.target.style.color = "white"; // Change color on hover
              }}
              onMouseLeave={(e) => {
                e.target.style.color = "orange"; // Restore color when not hovering
              }}
            >
              &#60; Back
            </Link>
          </button>
        )}
        <Link to="/requestsummary">  <button type="button" className="OrderDetail_btn1">
          {" "}
          Continue &#62;
        </button>  </Link>
      </div>
    </>
  );
}

export default OrderDetail;
import React, { useState, useEffect, useContext } from 'react';
import axios from 'axios';
import { Row, Col, Card, Alert } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import styles from '../addonPage/addon1.module.css';
import 'react-toastify/dist/ReactToastify.css';
import { ApiContext } from '../ApiContextProvider';
import Header from '../Header/header';
import { Link, useNavigate } from 'react-router-dom';
import Category from '../../Assets/Category.png';
import Available from '../../Assets/Available.png';
import Unavailable from '../../Assets/Unavailable.png';
import { Button } from "@mui/material";
import designimg from "../../Assets/designimg.png";
import logLogo from "../../Assets/loglogo.png";
import errpage from "../../Assets/loginPage .png";

const Addon = () => {
  const {
    addOnsList,
    setAddOnsList,
    checkedIndexes,
    setCheckedIndexes,
    datum,
    setDatum,
    finalOrder, timeSlotName
  } = useContext(ApiContext);

  var navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          "https://localhost:7239/api/AddOnsMasters/View_All_AddOnsMasters" ,{
            headers: {
              "accept": "text/plain",
              "Authorization": "Bearer " + sessionStorage.getItem('token')
            }
              });
  if (response.status === 200) {
        const updatedData = response.data.map((item) => ({ ...item, cnt: 0 }));
        setDatum(updatedData);
  }
  else if (response.status === 401) {
    // Handle 401 unauthorized error
    console.error("Unauthorized: Please log in again.");
    navigate('/sessionexpired');
    // You can perform a redirect or show a message to the user here
  } else {
    const errorData = await response.json();
    throw new Error(errorData.message);
  }
      } catch (error) {
        console.error("Error fetching data:", error);
        navigate('/sessionexpired');

      }
    };

    fetchData();
  }, []);

  const handleCheckboxChange = (index) => {
    setAddOnsList((prevList) => {
      const selectedAddon = datum[index];
      console.log("index", index);

      if (addOnsList.some((addon) => addon.addOnsId === index + 1)) {
        const updatedList = prevList.filter(
          (addOn) => addOn.addOnsId !== selectedAddon.id
        );
        console.log("Updated addOnsList:", updatedList);
        return updatedList;
      } else {
        const updatedList = [
          ...prevList,
          {
            id: 0,
            addOnsId: selectedAddon.id,
            addonsName: selectedAddon.addOnsName,
            orderId: 0,
            quantity: 1,
            cost: selectedAddon.unitPrice,
          },
        ];
        console.log("Updated addOnsList:", updatedList);
        return updatedList;
      }
    });

    setCheckedIndexes((indexes) =>
      indexes.includes(index + 1) ? indexes.filter((i) => i !== index + 1) : [...indexes, index + 1]
    );
  };

  console.log("datum", datum);
  console.log("addons", addOnsList);

  const handleIncrement = (index) => {
    const addon = addOnsList.find((addon) => addon.addOnsId === index + 1);

    if (addon) {
      const existingAddonIndex = addOnsList.findIndex(
        (add) => add.addOnsId === addon.addOnsId
      );

      if (existingAddonIndex !== -1) {
        setAddOnsList((prevList) => {
          const updatedList = [...prevList];
          updatedList[existingAddonIndex] = {
            ...updatedList[existingAddonIndex],
            quantity: updatedList[existingAddonIndex].quantity + 1,
          };
          return updatedList;
        });
      }
    }
  };


  const handleDecrement = (index) => {
    const addon = addOnsList.find((addon) => addon.addOnsId === index + 1);

    if (addon && addon.quantity > 0) {
      const existingAddonIndex = addOnsList.findIndex(
        (add) => add.addOnsId === addon.addOnsId
      );

      if (existingAddonIndex !== -1) {
        setAddOnsList((prevList) => {
          const updatedList = [...prevList];
          if (updatedList[existingAddonIndex].quantity > 1) {
            updatedList[existingAddonIndex] = {
              ...updatedList[existingAddonIndex],
              quantity: updatedList[existingAddonIndex].quantity - 1,
            };
          } else {
            updatedList.splice(existingAddonIndex, 1);
          }
          return updatedList;
        });
      }
    }
  };

  const handleCloseAlert = (index) => {
    setAddOnsList((prevList) =>
      prevList.filter((addOn) => addOn.addOnsId !== index)
    );

    setCheckedIndexes((indexes) => indexes.filter((i) => i !== index));
  };

  const handleBack = () => {
    console.log("Back button clicked");
  };

  const handleContinue = () => {
    console.log("Continue button clicked");
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
      <button className={styles.back_but1} onClick={handleBack}>
        &#60; Back
      </button>

      <Col className={styles.alertsContainer}>
        <div className={styles.alert1}>
          {checkedIndexes.map((addonId) => {
            const addon = addOnsList.find(addon => addon.addOnsId === addonId);
            console.log("add", addon);
            console.log("id", addonId);

            if (addon) {
              return (
                <Alert
                  key={addonId}
                  variant="warning"
                  onClose={() => handleCloseAlert(addonId)}
                  dismissible
                  className={styles.custom_alert}
                >
                  <p className={styles.alert_text}>
                    <br />
                    <p>{addon.addonsName}</p> ({addon.quantity})
                  </p>
                </Alert>
              );
            }
            return null;
          })}

        </div>
      </Col>
      {console.log("checked", checkedIndexes)}
      <div className={styles.desktop}>
        <div className={styles}>
          <h2 className={styles.heading}>
            {" "}
            <img src={Category} alt="" /> {"\u00A0"}
            Add Ons
          </h2>
        </div>
        <Row className={styles.wholebdy}>
          {datum.map((item, index) => (
            <Col key={index} sm={6} md={3}>
              <label htmlFor={item.id}>
                <Card className={styles.final_card}>
                  <Card.Img
                    className={styles.card_img_top}
                    variant="top"
                    src={require(`../../Assets/${item.addOnsName}.png`)}
                  />
                  <Card.Body>
                    <div>
                      <label className={styles.custom_checkbox}>
                        <input
                          type="checkbox"
                          checked={
                            !!addOnsList.some(
                              (addon) => addon.addOnsId === item.id
                            )
                          }
                          id={item.id}
                          onChange={() => handleCheckboxChange(index)}
                        />
                        <span className={styles.checkmark}></span>
                      </label>
                    </div>

                    <div className={styles.btnon}>
                      <button
                        className={styles.btn1}
                        onClick={() => handleDecrement(index)}
                        disabled={
                          !addOnsList.some((addon) => addon.addOnsId === item.id)
                        }
                      >
                        -
                      </button>
                      &nbsp;
                      <span className={styles.cnt1}>
                        {" "}
                        {addOnsList.find((addon) => addon.addOnsId === item.id)
                          ?.quantity || 0}
                      </span>
                      <button
                        className={styles.btn1}
                        onClick={() => handleIncrement(index)}
                        disabled={
                          !addOnsList.some((addon) => addon.addOnsId === item.id)
                        }
                      >
                        +
                      </button>
                    </div>
                    <Card.Text>

                      <div>
                        <p className={styles.unit_price}>${item.unitPrice}</p>
                        <p className={styles.item_name}>{item.addOnsName}</p>
                        <img src={item.addOnsImage} alt="" />

                      </div>
                    </Card.Text>
                  </Card.Body>
                  <p className='' style={{ display: 'flex', position: 'relative', bottom: '51px', width: 'min-content' }}>

                    {item.isAvailable ? (
                      <img style={{ padding: '10px' }} src={Unavailable} ></img>
                    ) : (
                      <img style={{ padding: '5px' }} src={Available} ></img>)}
                    {item.isAvailable === false ? (<span>Unavailable</span>
                    ) : (
                      <span>Available</span>
                    )}</p>

                </Card>
              </label>
            </Col>
          ))}
        </Row>

        <div className={styles.buttonss}>
          <div
            className={styles.footer}
            style={{
              width: "100%",
              height: "80px",
              position: "absolute",
              overflowX: "hidden",
              overflowY: "hidden",
              backgroundColor: "white",
              boxShadow: "11px 3px 11px 3px rgba(12, 11, 11, 0.07",
            }}
          >
            <Row>
              <Col className={styles.backBtn}>
              <Link
                  to="/order_details"><button className={styles.back_but} onClick={handleBack}>
                  &#60; Back
                </button>
                </Link>
              </Col>
              <Col className={styles.alertsContainer}>
                <div className={styles.alert}>
                  {checkedIndexes.map((addonId) => {
                    // Find the add-on object in addOnsList with the matching addOnsId
                    const addon = addOnsList.find(addon => addon.addOnsId === addonId);
                    console.log("add", addon);
                    console.log("id", addonId);

                    if (addon) {
                      return (
                        <Alert
                          key={addonId}
                          variant="warning"
                          onClose={() => handleCloseAlert(addonId)}
                          dismissible
                          className={styles.custom_alert}
                        >
                          <p className={styles.alert_text}>
                            <br />
                            <p>{addon.addonsName}</p> ({addon.quantity})
                          </p>
                        </Alert>
                      );
                    }
                    return null;
                  })}
                </div>
              </Col>
              <Col>
                <div>
                  <button type="button" className={styles.btn2}>
                    <Link
                      to="/order_details"
                      style={{ color: "white", textDecoration: "none" }}
                    >
                      Add Item &#62;
                    </Link>
                  </button>
                </div>
              </Col>
            </Row>
          </div>
        </div>
      </div>
    </>
  );
};

export default Addon;

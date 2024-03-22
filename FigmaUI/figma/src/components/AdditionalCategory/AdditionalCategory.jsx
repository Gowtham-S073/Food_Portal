import styles from './AdditionalCategory.module.css';
import React, { useEffect, useState, useContext } from 'react';
import axios from 'axios';
import CategoryImg from '../../Assets/Category.png';
import { Row, Col, Card, Alert } from 'react-bootstrap';
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import { AppContext } from '../../App';
import Nonveg2 from "../../Assets/Nonveg2.png";
import Veg2 from "../../Assets/Veg2.png";
import Available from "../../Assets/Available.png";
import Unavailable from "../../Assets/Unavailable.png"
import Header from '../Header/header';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import { ApiContext } from '../ApiContextProvider';
import { Link, useNavigate } from 'react-router-dom';
import { Button } from "@mui/material";
import designimg from "../../Assets/designimg.png";
import logLogo from "../../Assets/loglogo.png";
import errpage from "../../Assets/loginPage .png";


function AdditionalCategory() {
  const [Category, setCategory] = useState([]);
  const [product, setProduct] = useState([]);
  const [alignment, setAlignment] = useState('All');
  const { dataList, setDataList, dataList2, setDataList2, finalOrder, timeSlotName, selectedItemsByCategory, setSelectedItemsByCategory } = useContext(ApiContext);
  const [searchQuery, setSearchQuery] = useState('');
  const [checkedIndexes, setCheckedIndexes] = useState([]);
  const [currentcategory, setCurrentcategory] = useState(1);
  const [isveg, setIsveg] = useState();
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [selectedCategoryItemCount, setSelectedCategoryItemCount] = useState(5);
  const [categoryItemCounts, setCategoryItemCounts] = useState({}); // State variable for category item counts
  const [selectedItems, setSelectedItems] = useState([]);
  const isSmallScreen = window.innerWidth < 375;
  const [isMobileView, setIsMobileView] = useState(window.matchMedia("(max-width: 600px)").matches);
  var navigate = useNavigate();

  useEffect(() => {
    // Fetch category item counts and product data based on the selected category
    const fetchData2 = async () => {
      console.log(currentcategory);

      try {
        // Fetch product data based on the selected category
        const response2 = await axios.get(
          'https://localhost:7239/api/AdditionalProducts/View_by_category_AdditionalProducts?cat=' + currentcategory, {
            headers: {
              "accept": "text/plain",
              "Authorization": "Bearer " + sessionStorage.getItem('token')
            }
           });
if (response2.status === 200) {

        setProduct(response2.data);
        console.log("resp", response2.data);
        // Calculate and update category item counts
        const newCategoryItemCounts = {};
        Category.forEach(categoryItem => {
          const itemCount = response2.data.filter(item => item.additionalCategoryId === categoryItem.id).length;
          newCategoryItemCounts[categoryItem.id] = itemCount;
        });
        setCategoryItemCounts(newCategoryItemCounts);
      }  else if (response2.status === 401) {
        // Handle 401 unauthorized error
        console.error("Unauthorized: Please log in again.");
        navigate('/sessionexpired');
        // You can perform a redirect or show a message to the user here
      } else {
        const errorData = await response2.json();
        throw new Error(errorData.message);
      }
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
    fetchData2();
  }, [currentcategory, Category, selectedItemsByCategory ? selectedItemsByCategory[currentcategory] : null]);

  console.log("Final Order:", finalOrder)


  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          'https://localhost:7239/api/AdditionalCategoryMasters/View_All_AdditionalCategoryMasters', {
            headers: {
              "accept": "text/plain",
              "Authorization": "Bearer " + sessionStorage.getItem('token')
            }
            });
if (response.status === 200) {
        setCategory(response.data);
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
        console.error('Error fetching data:', error);
        navigate('/sessionexpired');
      }
    };
    fetchData();
  }, []);


  const handleCategoryClick = (categoryItem, categoryItemCount) => {
    setCurrentcategory(categoryItem);
    setSelectedCategory(categoryItem);
    const itemCount = product.filter(item => item.additionalCategoryId === categoryItem.id).length;
    setSelectedCategoryItemCount(categoryItemCount);
    setSelectedItems(selectedItemsByCategory[categoryItem] || []);
  };

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


  const handleCheck = (name, category) => (event) => {
    const isChecked = event.target.checked;
    const additionalProductsName = name.id;
    const additionalProductsCost = name.unitPrice;
    const additionalName = name.additionalProductsName;


    setSelectedItemsByCategory((prevSelectedItemsByCategory) => {
      const prevSelectedItems = prevSelectedItemsByCategory[currentcategory] || [];
      const updatedSelectedItems = isChecked
        ? [...prevSelectedItems, additionalProductsName]
        : prevSelectedItems.filter((item) => item !== additionalProductsName);
      return {
        ...prevSelectedItemsByCategory,
        [currentcategory]: updatedSelectedItems,
      };
    });

    if (isChecked) {
      const updatedData = {
        additionalProductsId: additionalProductsName,
        orderId: '0',
        quantity: 1,
        cost: additionalProductsCost,
      };
      const newItem = {
        additionalProductsId: additionalProductsName,
        orderId: '0',
        quantity: 0,
        cost: name.unitPrice,
      };
      // setSelectedItems((prevSelectedItems) => [...prevSelectedItems, newItem]);
      const updatedData2 = {
        additionalProductsId: additionalProductsName,
        additionalProductsName: additionalName,
        orderId: '0',
        quantity: 1,
        cost: additionalProductsCost,
      };

      setDataList((prevDataList) => [...prevDataList, updatedData]);
      setDataList2((prevDataList) => [...prevDataList, updatedData2]);
      console.log(dataList);

      if (!checkedIndexes.includes(additionalProductsName)) {
        setCheckedIndexes((prevIndexes) => [...prevIndexes, additionalProductsName]);
      }
    } else {
      console.log('Checkbox is unchecked for item:', name);
      const filteredDataList = dataList.filter(
        (item) => item.additionalProductsId !== additionalProductsName
      );
      const filteredDataList2 = dataList2.filter(
        (item) => item.additionalProductsId !== additionalProductsName
      );
      setDataList(filteredDataList);
      setDataList2(filteredDataList2);

      if (checkedIndexes.includes(additionalProductsName)) {
        setCheckedIndexes((prevIndexes) =>
          prevIndexes.filter((index) => index !== additionalProductsName)
        );
      }
      setSelectedItems((prevSelectedItems) =>
        prevSelectedItems.filter((item) => item.additionalProductsId !== additionalProductsName)
      );
    }
  };

  const handleChange = (event, newAlignment) => {
    setAlignment(newAlignment);
  };

  const handleSearch = (event) => {
    const query = event.target.value;
    setSearchQuery(query);
  };

  const incrementVeg = (item) => {
    const updatedDataList = dataList.map((dataItem) => {
      if (dataItem.additionalProductsId === item.id) {
        return {
          ...dataItem,
          quantity: dataItem.quantity + 1,
        };
      }
      return dataItem;
    });
    const updatedDataList2 = dataList2.map((dataItem) => {
      if (dataItem.additionalProductsId === item.id) {
        return {
          ...dataItem,
          quantity: dataItem.quantity + 1,
        };
      }
      return dataItem;
    });
    setDataList(updatedDataList);
    setDataList2(updatedDataList2);
  };

  const decrementVeg = (item) => {
    const updatedDataList = dataList.map((dataItem) => {
      if (dataItem.additionalProductsId === item.id && dataItem.quantity > 1) {
        return {
          ...dataItem,
          quantity: dataItem.quantity - 1,
        };
      }
      return dataItem;
    });
    const updatedDataList2 = dataList2.map((dataItem) => {
      if (dataItem.additionalProductsId === item.id && dataItem.quantity > 1) {
        return {
          ...dataItem,
          quantity: dataItem.quantity - 1,
        };
      }
      return dataItem;
    });
    setDataList(updatedDataList);
    setDataList2(updatedDataList2);

  };

  const handleCloseAlert = (index) => {
    // Remove the index from checkedIndexes
    setCheckedIndexes((prevIndexes) => prevIndexes.filter((idx) => idx !== index));

    // Remove the index from selectedItemsByCategory
    setSelectedItemsByCategory((prevSelectedItemsByCategory) => {
      const prevSelectedItems = prevSelectedItemsByCategory[currentcategory] || [];
      const updatedSelectedItems = prevSelectedItems.filter((item) => item !== index);

      return {
        ...prevSelectedItemsByCategory,
        [currentcategory]: updatedSelectedItems,
      };
    });

    const updatedIndexes = checkedIndexes.filter((idx) => idx !== index);
    setCheckedIndexes(updatedIndexes);

    // Update the dataList by removing the corresponding item
    const filteredDataList = dataList.filter((item) => item.additionalProductsId !== index);
    setDataList(filteredDataList);

    // Update the dataList2 by removing the corresponding item
    const filteredDataList2 = dataList2.filter((item) => item.additionalProductsId !== index);
    setDataList2(filteredDataList2);

    console.log("Filtered dataList:", filteredDataList);
    console.log("Filtered dataList2:", filteredDataList2);
  };


  const handleAllClick = async () => {
    console.log("All button clicked");
    try {
      // Fetch all items without filtering by isveg parameter
      const response2 = await axios.get('https://localhost:7239/api/AdditionalProducts/View_by_category_AdditionalProducts?cat=' + currentcategory, {
        headers: {
          "accept": "text/plain",
          "Authorization": "Bearer " + sessionStorage.getItem('token')
        }
            });
      setProduct(response2.data);

      // Update the isveg state to null (or any other value that indicates neither Veg nor Non-veg)
      setIsveg(null);

      console.log(product);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const handleVegClick = async () => {
    setIsveg(true);
    const veg = true;
    try {
      const response2 = await axios.get('https://localhost:7239/api/AdditionalProducts/View_by_foodtype_AdditionalProducts?isveg=' +
        veg +
        '&cat=' +
        currentcategory
        , {
          headers: {
            "accept": "text/plain",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
          }
            });
      setProduct(response2.data);
      console.log(product);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    console.log("Veg button clicked");
  };

  const handleNonVegClick = async () => {
    setIsveg(false);
    const veg = false;

    try {
      const response2 = await axios.get(
        'https://localhost:7239/api/AdditionalProducts/View_by_foodtype_AdditionalProducts?isveg=' +
        veg +
        '&cat=' +
        currentcategory
        , {
          headers: {
            "accept": "text/plain",
            "Authorization": "Bearer " + sessionStorage.getItem('token')
          }
            });
      setProduct(response2.data);

      if (response2.data.length === 0) {
        alert('No product found.');
      }
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    console.log("Non-veg button clicked");
  };



  if (Category.length === 0) {
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
    <>
      <div>
        <Header></Header>
        {isMobileView && (
          <Link to="/order_details"> <Button
            sx={{
              marginTop: "10px",
              backgroundColor: "white",
              textTransform: "none",
              color: "#FF6D00",
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

        <div className={styles.Category1}>
          <h2>
            <img src={CategoryImg} alt="Category" /><b>Category</b>
          </h2>
        </div>

        <div className={styles.Top_Header}>
          <div
            className={styles.Search_Bar}
            style={{
              '@media screen and (max-width: 834px)': {
                display: 'none',
              },
            }}>
            <input
              type="search"
              className={styles.nosubmit}
              value={searchQuery}
              onChange={handleSearch}
              placeholder="Search item..."
            />
          </div>

          <div className={styles.CategoryandCount}>
            <div style={{ fontWeight: '600', fontSize: '1.25rem' }}>{Category[currentcategory - 1].additionalCategory}&nbsp;</div> <div style={{ fontSize: '1.25rem' }}>({categoryItemCounts[currentcategory]} items)</div>
          </div>

          <div className={styles.toggle}>
            <ToggleButtonGroup
              color="primary"
              value={alignment}
              exclusive
              onChange={handleChange}
              aria-label="Platform"
            >
              <ToggleButton
                value="All"
                style={alignment === 'All' ? { backgroundColor: 'orange', color: 'white' } : {}}
                onClick={handleAllClick}
              >
                All
              </ToggleButton>
              <ToggleButton
                value="Veg"
                style={alignment === 'Veg' ? { backgroundColor: 'orange', color: 'white' } : {}}
                onClick={handleVegClick}
              >
                Veg
              </ToggleButton>
              <ToggleButton
                value="Non-veg"
                style={alignment === 'Non-veg' ? { backgroundColor: 'orange', color: 'white' } : {}}
                onClick={handleNonVegClick}
              >
                Non-veg
              </ToggleButton>
            </ToggleButtonGroup>
          </div>
        </div>

        <div className={styles.top_header2}>
          {Category === null ? (
            <p>Loading...</p>
          ) : (
            <div className={styles.Category2} >
              {Category.map((categoryItem, categoryIndex) => {
                const categoryItemCount = categoryItemCounts[categoryItem.id] || 0;

                return (
                  <div
                    key={categoryIndex}
                    className={`${styles.item} ${selectedCategory === categoryItem.id ? styles.selectedCategory : ''
                      }`}
                  >
                    <h6
                      className={styles.addh5}
                      onClick={() => handleCategoryClick(categoryItem.id, categoryItemCounts[5])}
                    >
                      {categoryItem.additionalCategory}
                    </h6>

                    {categoryIndex !== Category.length - 1 && (
                      <React.Fragment>
                        <hr className="separator" />
                        <style jsx>{`
                        @media (max-width: 1200px) {
                          .separator {
                            display: none;
                          }
                        }
                      `}</style>
                      </React.Fragment>
                    )}
                  </div>
                );
              })}
            </div>
          )}

          {isMobileView && (
            <div>
              {checkedIndexes.length > 0 && (
                <div className={styles.alertMobile}>
                  {checkedIndexes.map((index) => {
                    const item = product.find((item) => item.id === index);
                    if (!item) {
                      return null; 
                    }
                      const quantity = dataList.find((dataItem) => dataItem.additionalProductsId === index)?.quantity || 1;
                      return (
                      <Alert
                        key={index}
                        variant="warning"
                        onClose={() => handleCloseAlert(index)}
                        dismissible
                        className={styles.custom_alert}
                        style={{ marginLeft: '15px' }}
                      >
                        <p className={styles.alert_text}>
                          <strong>{item.additionalProductsName}</strong> ({quantity})
                        </p>
                      </Alert>
                    );
                  })}
                </div>
              )}
            </div>
          )}

          <Row className={styles.wholebdy}>
            {product
              .filter((item) => item.additionalProductsName.toLowerCase().includes(searchQuery.toLowerCase()))
              .map((item, index) => {
                if (!item) {
                  return null; // Skip this iteration if item is undefined
                }
                const quantity =
                  dataList.find((dataItem) => dataItem.additionalProductsId === item.id)?.quantity || 0;
                return (
                  <Col xs={12} sm={6} md={6} lg={4} key={index}>
                    <label style={{cursor:'pointer'}} htmlFor={item.id}>
                    <Card
                      className={styles.card}
                      style={{
                        backdropFilter: item.isAvailable ? 'none' : 'blur(180px)',
                        opacity: item.isAvailable ? 1 : 0.5,
                      }}
                      >
                      <Card.Body style={{ padding: '0px' }}>
                        <Card.Img
                          className={styles.card_img_top}
                          variant="top"
                          src={require(`../../Assets/FoodCategory/Biriyanis/${item.additionalProductsName}.png`)}
                        />
                        {console.log("selectedItemsByCategory", selectedItemsByCategory)}
                        {console.log("selectedItemsByCategory[currentcategory]", selectedItemsByCategory[currentcategory])}
                        {console.log("currentcategory", currentcategory)}
                        {console.log("includes(item.id)", selectedItemsByCategory[currentcategory]?.includes(item.id))}
                        {console.log("datalist", dataList)}


                        <div className={styles.checkandbutton}>
                          <label className={styles.custom_checkbox} htmlFor={item.id}>
                            <input
                              type="checkbox"
                              name="prod"
                              value={item.id}
                              id={item.id}
                              onChange={handleCheck(item)}
                              disabled={(!item.isAvailable)}
                              checked={(!!selectedItemsByCategory[currentcategory]?.includes(item.id))}
                            />
                            <span className={styles.checkmark}></span>
                          </label>

                          <div>
                            {item.isVeg ? (
                              <img src={Veg2} alt='veggg' height="20px" width="20px" />
                            ) : (
                              <img src={Nonveg2} alt='nonveggg' height="20px" width="20px" />
                            )}
                          </div>

                          <p style={{ paddingTop: '18px', paddingLeft: '10px' }}>{item.additionalProductsName}</p>
                          <p className={styles.unit_price}>$ {item.unitPrice}</p>
                        </div>

                        <div style={{ display: 'flex', marginLeft: '20px' }}>
                          <p style={{ paddingRight: '100px' }}>
                            {item.isAvailable ? (
                              <img style={{ padding: '10px' }} src={Available} ></img>
                            ) : (
                              <img style={{ padding: '10px' }} src={Unavailable} ></img>)}
                            {item.isAvailable === false ? (<span>Unavailable</span>
                            ) : (
                              <span>Available</span>
                            )}</p>

                          <div className={styles.btnbox} >
                            <div className={styles.btnon} style={{ border: '1px solid #D3D3D3', borderRadius: '4px' }}>
                              <button className={styles.Cardbtn} onClick={() => decrementVeg(item)}>
                                -
                              </button>
                              <span className={styles.cnt1} style={{ width: '40px', color: '#FF6D00' }}>{quantity}</span>
                              <button className={styles.Cardbtn} onClick={() => incrementVeg(item)}>
                                +
                              </button>
                            </div>
                          </div>
                        </div>
                      </Card.Body>
                    </Card>
                    </label>
                  </Col>
                );
              })}
          </Row>
          {product
            .filter((item) => item.additionalProductsName.toLowerCase().includes(searchQuery.toLowerCase()))
            .length === 0 && <p className={styles.product_not_found}> ☹️ Product Not Found !</p>}
        </div>
      </div>

      <div>
        <div >
          <div className={styles.footer}>
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
                  to="/order_details"
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


            <div style={{ justifyContent: 'space-evenly' }}>
              {checkedIndexes.length > 0 && (
                <div className={styles.alert}>
                  {checkedIndexes.map((index) => {
                    const item = product.find((item) => item.id === index);
                    if (!item) {
                      return null; // Skip this iteration if item is undefined
                    }
                    const quantity = dataList.find((dataItem) => dataItem.additionalProductsId === index)?.quantity || 1;
                    return (
                      <Alert
                        key={index}
                        variant="warning"
                        onClose={() => handleCloseAlert(index)}
                        dismissible
                        className={styles.custom_alert}
                        style={{ marginLeft: '20px' }}
                      >
                        <p className={styles.alert_text}>
                          <strong>{item.additionalProductsName}</strong> ({quantity})
                        </p>
                      </Alert>
                    );
                  })}
                </div>
              )}
            </div>
            <button type="button" className={styles.btn1}>
              <Link
                to="/order_details"
                style={{
                  textDecoration: "none",
                  color: "white", // Normal color
                  display: "block", // Make the link a block element to fill the button
                  padding: "10px", // Adjust padding as needed
                  textAlign: "center", // Center the link content
                }}
              >
                Add Item &#62;
              </Link>
            </button>
          </div>
        </div>
      </div>
    </>
  );
}

export default AdditionalCategory;


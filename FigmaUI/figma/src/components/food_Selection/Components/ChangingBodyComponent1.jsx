import React, { useState, useEffect, useContext } from "react";
import Future from "../Assets/future_coffee_icon.png";
import Present from "../Assets/present_coffee_icon.png";
import Past from "../Assets/past_coffee_icon.png";
import Veg from "../Assets/veg_item_icon.png";
import { Box, Typography, List } from "@mui/material";
import NonVeg from "../Assets/non_veg_item_icon.png";
import spicy_icon from "../Assets/spicy_icon.png";
import Glutten_free_icon from "../Assets/Glutten_free_icon.png";
import veg_icon from "../Assets/veg_icon.png";
import styles from "./CSS/ChangingBody1.module.css";
import { useMediaQuery, createTheme } from "@mui/material";
import { ApiContext } from "../../ApiContextProvider";
import { useNavigate } from "react-router-dom";

const ChangingBodyComponent1 = ({
  whichCategory,
  setVegItemsSelected,
  setNonVegItemsSelected,
}) => {
  const theme = createTheme({
    breakpoints: {
      values: {
        custom: 646,
      },
    },
  });
  const isMobile = useMediaQuery(theme.breakpoints.down("custom"));
  const [isLargeScreen, setIsLargeScreen] = useState(true);
  const {
    setOrderedFoodList,
    maxFoodItems,
    onlyVeg,
    onlyNonVeg,
    setSelectedNonVegFoodItemIds,
    setSelectedVegFoodItemIds,
    selectedVegFoodItemIds,
    selectedNonVegFoodItemIds,
    vegItemsSelected,
    nonVegItemsSelected,
  } = useContext(ApiContext);
  const [categories, setCategories] = useState([]);
  const [foodItems, setFoodItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [vegFoodItems, setVegFoodItems] = useState([]);
  const [nonvegFoodItems, setNonvegFoodItems] = useState([]); // Filter and update vegFoodItems and nonvegFoodItems based on onlyVeg and onlyNonVeg

  var navigate = useNavigate();
  useEffect(() => {
    updateFoodItems();
  }, [foodItems]);

  useEffect(() => {
    const handleResize = () => {
      const isLargeScreen = window.matchMedia("(min-width: 1036px)").matches;
      setIsLargeScreen(isLargeScreen);
    };

    window.addEventListener("resize", handleResize);
    handleResize();

    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  const updateFoodItems = () => {
    const updatedVegFoodItems = foodItems.filter((item) => {
      if (item.isVeg === 1 || item.isVeg === 2) {
        return true;
      }
      return false;
    });
    const updatedNonvegFoodItems = foodItems.filter((item) => {
      if (item.isVeg === 0 || item.isVeg === 2) {
        return true;
      }
      return false;
    });

    if (onlyVeg && onlyNonVeg) {
      setVegFoodItems(updatedVegFoodItems);
      setNonvegFoodItems(updatedNonvegFoodItems);
    } else if (onlyVeg) {
      setSelectedNonVegFoodItemIds({});
      setNonVegItemsSelected({});
      setVegFoodItems(updatedVegFoodItems);
    } else if (onlyNonVeg) {
      setSelectedVegFoodItemIds({});
      setVegItemsSelected({});
      setNonvegFoodItems(updatedNonvegFoodItems);
    }
  };

  const handleVegFoodItemChange = (event, category) => {
    const selectedId = event.target.value;
    const isSelected = selectedVegFoodItemIds[category]?.includes(selectedId);

    let updatedSelection;

    if (isSelected) {
      updatedSelection = selectedVegFoodItemIds[category]?.filter(
        (id) => id !== selectedId
      );
    } else {
      if (
        selectedVegFoodItemIds[category]?.length <
        maxFoodItems[category.toLowerCase()]
      ) {
        updatedSelection = [...selectedVegFoodItemIds[category], selectedId];
      } else {
        updatedSelection = [
          ...selectedVegFoodItemIds[category].slice(1),
          selectedId,
        ];
      }
    }

    setSelectedVegFoodItemIds((prevSelectedIds) => ({
      ...prevSelectedIds,
      [category]: updatedSelection || [],
    }));

    setVegItemsSelected((prevSelectedItems) => ({
      ...prevSelectedItems,
      [category]: updatedSelection.map((id) => {
        const selectedItem = vegFoodItems.find(
          (item) => item.id.toString() === id
        );
        return selectedItem || null;
      }),
    }));
  };

  useEffect(() => {
    // Combine the selected items from both veg and non-veg
    const combinedItems = [
      ...Object.values(vegItemsSelected).flat(),
      ...Object.values(nonVegItemsSelected).flat(),
    ];

    // Create the orderedFoodList based on the combined items
    const newOrderedFoodList = combinedItems.map((item, index) => ({
      orderId: 0,
      productsId: item.id,
    }));

    // Update the orderedFoodList state
    setOrderedFoodList(newOrderedFoodList);
  }, [vegItemsSelected, nonVegItemsSelected]);

  const handleNonVegFoodItemChange = (event, category) => {
    const selectedId = event.target.value;
    const isSelected =
      selectedNonVegFoodItemIds[category]?.includes(selectedId);

    let updatedSelection;

    if (isSelected) {
      updatedSelection = selectedNonVegFoodItemIds[category]?.filter(
        (id) => id !== selectedId
      );
    } else {
      if (
        selectedNonVegFoodItemIds[category]?.length <
        maxFoodItems[category.toLowerCase()]
      ) {
        updatedSelection = [...selectedNonVegFoodItemIds[category], selectedId];
      } else {
        updatedSelection = [
          ...selectedNonVegFoodItemIds[category].slice(1),
          selectedId,
        ];
      }
    }

    setSelectedNonVegFoodItemIds((prevSelectedIds) => ({
      ...prevSelectedIds,
      [category]: updatedSelection || [],
    }));

    setNonVegItemsSelected((prevSelectedItems) => ({
      ...prevSelectedItems,
      [category]: updatedSelection.map((id) => {
        const selectedItem = nonvegFoodItems.find(
          (item) => item.id.toString() === id
        );
        return selectedItem || null;
      }),
    }));
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const url =
          "https://localhost:7239/api/StdProducts/View_by_category_StdProducts?cat=" +
          whichCategory;
        const response = await fetch(url, {
          method: "GET",
          headers: {
            accept: "text/plain",
            "Content-Type": "application/json",
            Authorization: "Bearer " + sessionStorage.getItem("token"),
          },
        });
        if (response.status === 200) {
          const data = await response.json();
          setFoodItems(data);
          setLoading(false);
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
        console.error("Error fetching data:", error);
      }
    };
    fetchData();
  }, [whichCategory]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await fetch(
          "https://localhost:7239/api/StdFoodCategoryMasters/View_All_StdFoodCategoryMasters",
          {
            method: "GET",
            headers: {
              accept: "text/plain",
              "Content-Type": "application/json",
              Authorization: "Bearer " + sessionStorage.getItem("token"),
            },
          }
        );
        if (response.status === 200) {
          const data = await response.json();
        setCategories(data);
        if (!Array.isArray(data)) {
          throw new Error("Invalid data format");
        }

        const selectedVegFoodItemIdsInitial = {};
        const selectedNonVegFoodItemIdsInitial = {};

        data.forEach((category) => {
          const categoryName = category.category;
          selectedVegFoodItemIdsInitial[categoryName] =
            selectedVegFoodItemIds[categoryName] || [];
          selectedNonVegFoodItemIdsInitial[categoryName] =
            selectedNonVegFoodItemIds[categoryName] || [];
        });

        setSelectedVegFoodItemIds(selectedVegFoodItemIdsInitial);
        setSelectedNonVegFoodItemIds(selectedNonVegFoodItemIdsInitial);
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
        console.error("Error fetching data:", error);
      }
    };

    fetchCategories();
  }, []);

  if (categories.length === 0) {
    return <div>Loading...</div>;
  }

  return (
    <div className={styles.main_change1_body}>
      <Box
        display="flex"
        flexDirection="row"
        alignItems="center"
        py={{ xs: 3, sm: 5 }}
        px={0}
      >
        {categories.map((category, index) =>
          !isMobile ||
          (isMobile && index <= 2 && whichCategory < 4) ||
          (isMobile && index >= 2 && whichCategory > 3) ? (
            <React.Fragment key={category.category}>
              {((index > 0 && index !== 2) ||
                (isMobile && index === 2 && whichCategory <= 3) ||
                (!isMobile && index === 2)) && (
                <span className={styles.separator_line}>----&nbsp;&nbsp;</span>
              )}
              <Box display="flex" flexDirection="row" alignItems="center">
                {isLargeScreen ? (
                  <>
                    {whichCategory - 1 === index && (
                      <img
                        src={Present}
                        alt="icon"
                        height="40px"
                        width="40px"
                      />
                    )}
                    {whichCategory - 1 > index && (
                      <img src={Future} alt="icon" height="40px" width="40px" />
                    )}
                    {whichCategory - 1 < index && (
                      <img src={Past} alt="icon" height="40px" width="40px" />
                    )}
                  </>
                ) : null}
                <Box className={styles.head_desc} px={1}>
                  <Typography
                    variant="body1"
                    className={
                      whichCategory - 1 === index
                        ? styles.selected_text
                        : whichCategory - 1 > index
                        ? styles.previously_selected_text
                        : styles.non_selected_text
                    }
                  >
                    {category.category}
                  </Typography>
                  <Typography
                    fontSize="12px"
                    variant="body2"
                    className={
                      whichCategory - 1 === index
                        ? styles.selected_text_sub
                        : styles.non_selected_text_sub
                    }
                  >
                    Select{" "}
                    {maxFoodItems[categories[index].category.toLowerCase()]}{" "}
                    item
                  </Typography>
                </Box>
              </Box>
            </React.Fragment>
          ) : null
        )}
      </Box>

      <Box
        sx={{
          display: "flex",
          flexDirection: vegFoodItems.length > 0 ? "row" : "column",
          justifyContent: "space-between",
          "@media (max-width: 600px)": {
            flexDirection: "column",
          },
        }}
      >
        <div>
          {vegFoodItems.length > 0 && (
            <div>
              <div className={styles.item_heading}>
                <Box
                  sx={{
                    display: "flex",
                    flexDirection: "row",
                  }}
                >
                  <Box display="flex" alignItems="center">
                    <img
                      src={Veg}
                      className={styles.veg_and_nonveg_headings}
                      alt="icon"
                      height="14px"
                      width="14px"
                      style={{ verticalAlign: "middle" }}
                    />
                    <span>
                      {categories.length > 0 && (
                        <Typography
                          variant="body1"
                          className={styles.cat_heading}
                        >
                          {categories[whichCategory - 1].category}
                        </Typography>
                      )}
                    </span>
                  </Box>
                </Box>
              </div>

              <Box style={{ maxWidth: "fit-content" }}>
                <div
                  className={styles.starter_content}
                  style={{
                    fontSize: "14px",
                    maxWidth: "280px",
                    paddingBottom: "30px",
                    paddingTop: "10px",
                  }}
                >
                  {categories.length > 0 && (
                    <span className={styles.choose_sentence}>
                      You can choose{" "}
                      {
                        maxFoodItems[
                          categories[whichCategory - 1].category.toLowerCase()
                        ]
                      }{" "}
                      {categories[whichCategory - 1].category} depend upon the
                      plate size you selected
                    </span>
                  )}
                </div>

                {loading ? (
                  <Typography>Loading...</Typography>
                ) : (
                  <div style={{ display: "flex", flexDirection: "column" }}>
                    <List>
                      {vegFoodItems &&
                        vegFoodItems.length > 0 &&
                        vegFoodItems.map((item) => {
                          
                          const isSelected = selectedVegFoodItemIds[
                            categories[whichCategory - 1].category
                          ]?.includes(item.id.toString());

                          return (
                            <div
                              key={item.id}
                              className={`${styles.list_item} ${
                                isSelected
                                  ? styles.selected
                                  : styles.not_selected
                              }`}
                              style={{ marginBottom: "30px" }}
                            >
                              <input
                                className="ChangingCheckbox"
                                type="checkbox"
                                id={item.id}
                                name="vegFoodItem"
                                value={item.id}
                                checked={isSelected}
                                onChange={(event) =>
                                  handleVegFoodItemChange(
                                    event,
                                    categories[whichCategory - 1].category
                                  )
                                }
                                style={{
                                  cursor: "pointer",
                                }}
                              />
                              <label
                                className={styles.item_label}
                                htmlFor={item.id}
                                style={{
                                  marginRight: "10px",
                                  cursor: "pointer",
                                }}
                              >
                                {"\u00A0"}
                                {"\u00A0"}
                                {"\u00A0"}
                                {item.productsName}
                              </label>

                              {/* Display images based on conditions */}
                              {item.isSpicy && (
                                <img
                                  src={spicy_icon}
                                  alt="Spicy"
                                  style={{ marginRight: "10px" }}
                                />
                              )}
                              {item.isGluten && (
                                <img
                                  src={Glutten_free_icon}
                                  alt="Gluten"
                                  style={{ marginRight: "10px" }}
                                />
                              )}
                              {item.isVegan && (
                                <img src={veg_icon} alt="Vegan" />
                              )}
                            </div>
                          );
                        })}
                    </List>
                  </div>
                )}
              </Box>
            </div>
          )}
        </div>

        {nonvegFoodItems.length > 0 && (
          <Box
            sx={{
              "@media (max-width: 1550px)": {
                paddingRight: "350px",
              },
              "@media (max-width: 980px)": {
                paddingRight: "200px",
              },
              "@media (max-width: 820px)": {
                paddingRight: "50px",
              },
              "@media (max-width: 500px)": {
                paddingRight: "0px",
              },
            }}
          >
            <div className={styles.item_heading2}>
              <Box
                sx={{
                  display: "flex",
                  flexDirection: "row",
                }}
              >
                <Box display="flex" alignItems="center">
                  <img
                    src={NonVeg}
                    className={styles.veg_and_nonveg_headings}
                    alt="icon"
                    height="14px"
                    width="14px"
                    style={{ verticalAlign: "middle" }}
                  />
                  <span>
                    {categories.length > 0 && (
                      <Typography
                        variant="body1"
                        className={styles.cat_heading}
                      >
                        {categories[whichCategory - 1].category}
                      </Typography>
                    )}
                  </span>
                </Box>
              </Box>
            </div>

            <div
              className={styles.starter_content2}
              style={{
                fontSize: "14px",
                maxWidth: "280px",
                paddingBottom: "30px",
                paddingTop: "10px",
              }}
            >
              {categories.length > 0 && (
                <span className={styles.choose_sentence}>
                  You can choose{" "}
                  {
                    maxFoodItems[
                      categories[whichCategory - 1].category.toLowerCase()
                    ]
                  }{" "}
                  {categories[whichCategory - 1].category} depend upon the plate
                  size you selected
                </span>
              )}
            </div>

            {loading ? (
              <Typography>Loading...</Typography>
            ) : (
              <List>
                {nonvegFoodItems &&
                  nonvegFoodItems.length > 0 &&
                  nonvegFoodItems.map((item) => {
                    // const category = item.category;
                    const isSelected = selectedNonVegFoodItemIds[
                      categories[whichCategory - 1].category
                    ]?.includes(item.id.toString());

                    return (
                      <div
                        key={item.id}
                        className={`${styles.list_item} ${
                          isSelected ? styles.selected : styles.not_selected
                        }`}
                        style={{ marginBottom: "30px" }}
                      >
                        <input
                          type="checkbox"
                          id={`nonveg-${item.id}`} // Add a prefix or suffix to the id for non-veg items
                          name="nonVegFoodItem"
                          value={item.id}
                          checked={isSelected}
                          onChange={(event) =>
                            handleNonVegFoodItemChange(
                              event,
                              categories[whichCategory - 1].category
                            )
                          }
                          style={{ marginRight: "10px", cursor: "pointer" }}
                        />
                        <label
                          className={styles.item_label}
                          htmlFor={`nonveg-${item.id}`} // Match the modified id value
                          style={{ marginRight: "10px", cursor: "pointer" }}
                        >
                          {item.productsName}
                        </label>

                        {/* Display images based on conditions */}
                        {item.isSpicy && (
                          <img
                            src={spicy_icon}
                            alt="Spicy"
                            style={{ marginRight: "10px" }}
                          />
                        )}
                        {item.isGluten && (
                          <img
                            src={Glutten_free_icon}
                            alt="Gluten"
                            style={{ marginRight: "10px" }}
                          />
                        )}
                        {item.isVegan && <img src={veg_icon} alt="Vegan" />}
                      </div>
                    );
                  })}
              </List>
            )}
          </Box>
        )}
      </Box>
    </div>
  );
};

export default ChangingBodyComponent1;

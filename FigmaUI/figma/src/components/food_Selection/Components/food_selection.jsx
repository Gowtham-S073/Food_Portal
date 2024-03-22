import { React, useState, useEffect, useContext } from "react";
import Footer from "./footer";
import Header from "./header";
import StaticBodyComponent from "./StaticBodyComponent";
import { Button, Box } from "@mui/material";
import ChangingBodyComponent1 from "./ChangingBodyComponent1";
import ChangingBodyComponent2 from "./ChangingBodyComponent2";
import styles from "./CSS/food_selection.module.css";
import { ApiContext } from "../../ApiContextProvider";
import { useNavigate } from "react-router-dom";
import designimg from "../../../Assets/designimg.png";
import logLogo from "../../../Assets/loglogo.png";
import errpage from "../../../Assets/loginPage .png";
import { Link } from "react-router-dom";

const Food_Selection = () => {
  const navigate = useNavigate();

  const {
    maxFoodItems,
    onlyVeg,
    onlyNonVeg,
    nonVegItemsSelected,
    setNonVegItemsSelected,
    vegItemsSelected,
    setVegItemsSelected,
    whichCategory,
    finalOrder,
    timeSlotName,
    setWhichCategory,
  } = useContext(ApiContext);

  const [percentage, setPercentage] = useState(80);

  const [isContinueDisabled, setIsContinueDisabled] = useState(false);

  const [isFinalStep, setIsFinalStep] = useState(false);

  useEffect(() => {
    function capitalizeFirstLetter(str) {
      if (str != null) return str.charAt(0).toUpperCase() + str.slice(1);
    }
    const currentCategoryKey = Object.keys(maxFoodItems)[whichCategory - 1];
    const isVegSelected =
      vegItemsSelected[capitalizeFirstLetter(currentCategoryKey)] || [];
    const isNonVegSelected =
      nonVegItemsSelected[capitalizeFirstLetter(currentCategoryKey)] || [];

    let shouldDisableContinue = false;

    if (onlyVeg && !onlyNonVeg) {
      shouldDisableContinue =
        isVegSelected.length !== maxFoodItems[currentCategoryKey];

    } else if (onlyNonVeg && !onlyVeg) {
      shouldDisableContinue =
        isNonVegSelected.length !== maxFoodItems[currentCategoryKey];
    } else {
      shouldDisableContinue =
        isVegSelected.length !== maxFoodItems[currentCategoryKey] ||
        isNonVegSelected.length !== maxFoodItems[currentCategoryKey];
    }

    setIsContinueDisabled(shouldDisableContinue);

    // Check if it's the final step (dessert section)
    if (whichCategory === 5 && !shouldDisableContinue) {
      setIsFinalStep(true);
      setPercentage(100);
    } else {
      setIsFinalStep(false);
      setPercentage(80); // Reset the percentage to 80 for other sections
    }
  }, [vegItemsSelected, nonVegItemsSelected, whichCategory]); // Add ", maxFoodItems, onlyVeg, onlyNonVeg" if required

  const handleContinueClick = () => {
    setWhichCategory((prevCategory) => {
      if (prevCategory === 5) {
        navigate("/order_details");
        return prevCategory; // Navigate to '/additional_Cat' when prevCategory === 6
      } else {
        return prevCategory + 1; // Increment by 1 for other cases
      }
    });
  };

  const handleBackClick = () => {
    setWhichCategory((prevCategory) => {
      if (prevCategory === 1) {
        navigate("/platesize"); // Navigate to '/platesize' when prevCategory === 1
        return prevCategory; // Reset to 5 if the currentCategory is 1
      } else {
        return prevCategory - 1; // Decrement by 1 for other cases
      }
    });
  };

  const [isMobileView, setIsMobileView] = useState(
    window.matchMedia("(max-width: 700px)").matches
  );

  useEffect(() => {
    const handleResize = () => {
      setIsMobileView(window.matchMedia("(max-width: 700px)").matches);
    };

    window.addEventListener("resize", handleResize);

    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  if (finalOrder.date === "" || timeSlotName === "") {
    // Categories are not yet loaded, show a loading indicator or handle accordingly
    return (
      <div>
        <div
          style={{
            display: "flex",
            flexDirection: "column",
            alignItems: "flex-start",
            justifyContent: "center",
            height: "70vh",
            paddingLeft: "20px",
          }}
        >
          <img className="loglogo" src={logLogo} alt="" />{" "}
          <p
            style={{
              padding: "20px",
              fontSize: "38px",
              color: "black",
              textAlign: "center",
            }}
          >
            Go Back to Select previous options
          </p>
          <div
            className="img"
            style={{ position: "fixed", bottom: 0, left: 0 }}
          >
            <img src={designimg} alt="" />
          </div>
          <div
            className="imgside"
            style={{ position: "fixed", bottom: 0, right: 0 }}
          >
            <img src={errpage} alt="" />
          </div>
          <Link to="/dateandtime">
            <Button
              sx={{
                backgroundColor: "#ff6d00",
                color: "rgb(255, 255, 255)",
                alignItems: "flex-end",
                justifyContent: "center",
                marginTop: "20px",
                marginLeft: "220px",
                width: "180px",
                height: "50px",
                fontWeight: "bold",
                fontSize: "18px",
                "&:hover": {
                  backgroundColor: "orangered", // Change this to your desired hover color
                },
              }}
            >
              Go Back
            </Button>
          </Link>
        </div>
      </div>
    );
  }

  return (
    <div>
      <Header />
      {isMobileView && (
        <Button
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
          onClick={handleBackClick}
        >
          &#60; Back
        </Button>
      )}

      <Box
        sx={{
          display: "flex",
          flexDirection: "row",
          justifyContent: "space-between",
          padding: "0px",
          paddingLeft: "150px",

          "@media (max-width: 1550px)": {
            paddingLeft: "0px",
            flexDirection: "column",
          },
          "@media (max-width: 1125px)": {
            paddingLeft: "0px",
            flexDirection: "column",
          },

          "@media (max-width: 600px)": {
            padding: "0px",
            paddingTop: "0px",
            flexDirection: "column",
          },
        }}
      >
        <Box
          sx={{
            backgroundColor: "white",
            color: "black",
            marginTop: "10px",
            marginBottom: "10px",

            display: "flex",
            flexDirection: "column",

            "@media (max-width: 700px)": {
              marginTop: "0px",
              paddingTop: "0px",
            },
            "@media (max-width: 600px)": {
              paddingTop: "0px",
            },
          }}
        >
          <StaticBodyComponent />

          <ChangingBodyComponent1
      
            whichCategory={whichCategory}
            vegItemsSelected={vegItemsSelected}
            setVegItemsSelected={setVegItemsSelected}
            nonVegItemsSelected={nonVegItemsSelected}
            setNonVegItemsSelected={setNonVegItemsSelected}
          />
        </Box>

        <ChangingBodyComponent2
          vegItemsSelected={Object.values(vegItemsSelected).flat()}
          nonVegItemsSelected={Object.values(nonVegItemsSelected).flat()}
          className={styles.ChangingBodyComponent2}
        />
      </Box>

      <Footer
        whichCategory={whichCategory}
        isContinueDisabled={isContinueDisabled}
        onContinueClick={handleContinueClick}
        onBackClick={handleBackClick}
        percentage={percentage}
        setPercentage={setPercentage}
      />
    </div>
  );
};

export default Food_Selection;

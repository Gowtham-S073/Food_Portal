import React, { useState, useEffect } from "react";
import styles from "./CSS/footer.module.css";

const Footer = ({
  isContinueDisabled,
  percentage,
  setPercentage,
  whichCategory,
  onContinueClick,
  onBackClick,
}) => {
  const [isMobileView, setIsMobileView] = useState(
    window.matchMedia("(max-width: 700px)").matches
  );

  useEffect(() => {
    // Add a listener to check for changes in screen size
    const mediaQuery = window.matchMedia("(max-width: 700px)");
    const handleMediaQueryChange = (event) => {
      setIsMobileView(event.matches);
    };

    mediaQuery.addListener(handleMediaQueryChange);

    // Clean up the listener when the component is unmounted
    return () => {
      mediaQuery.removeListener(handleMediaQueryChange);
    };
  }, []);

  useEffect(() => {
    // console.log(isContinueDisabled);
  }, [isContinueDisabled]);

  const handleContinueClick = () => {
    onContinueClick();
  };

  const handleBackClick = () => {
    onBackClick();
  };

  return (
    <div className={styles.footer}>
      {!isMobileView && (
        <>
          <button
            type="button"
            className={styles.btn}
            onClick={handleBackClick}
          >
            &#60; Back
          </button>
          <div className={styles.progress_wrapper}>
            <div className={styles.progress_bar}>
              <div
                className={styles.progress}
                style={{ width: `${percentage}%` }}
              ></div>
              <div
                className={styles.percentage_text}
                style={{ left: `${percentage - 7}%` }}
              >
                {`${percentage}%`}
              </div>
            </div>
          </div>
        </>
      )}

      <div className={styles.continue_step_wrapper}>
        {isMobileView ? (
          <div className={styles.mobile_step_text}>
            Step {"\u00A0"}
            <span className={styles.whichCategorystep}>5</span>
            {"\u00A0"} of 5<span className={styles.step_text}></span>
          </div>
        ) : (
          <div className={styles.step_text}></div>
        )}

        <button
          type="button"
          className={`${styles.btn1} ${
            isContinueDisabled ? styles.buttondisabledstyle : ""
          }`}
          onClick={handleContinueClick}
          disabled={isContinueDisabled}
        >
          Continue &#62;
        </button>
      </div>
    </div>
  );
};

export default Footer;

import React from "react";
import Logo from '../Assets/Logo.png';
import styles from './CSS/headerfoodselection.module.css'
const Header = () => {
  return (
    <div>
      <nav className={styles.nav}>
      <img src={Logo} className={styles.head} alt="headerImage"/>
      </nav>
    </div>
  );
};

export default Header;

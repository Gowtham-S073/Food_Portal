import React, { useContext } from 'react';
// import Button from '@mui/material/Button';
import choose_item_icon from '../Assets/choose_item_icon.png';
import { Box } from '@mui/material';
import spicy_icon from '../Assets/spicy_icon.png'
import Glutten_free_icon from '../Assets/Glutten_free_icon.png'
import veg_icon from '../Assets/veg_icon.png'
import styles from './CSS/StaticBodyComponent.module.css'
import { ApiContext } from '../../ApiContextProvider';

const StaticBodyComponent = () => {

  const {finalOrder, guestCount, plateSizeForUse, timeSlotName, typeFood} = useContext(ApiContext); 

 const formatDate = (dateString) => {
  const options = { month: 'long', year: 'numeric' };
  const formattedDate = new Date(dateString).toLocaleDateString('en-US', options);

  const day = new Date(dateString).getDate();
  const suffix = day === 1 ? 'st' : day === 2 ? 'nd' : day === 3 ? 'rd' : 'th';

  return `${day}${suffix} ${formattedDate}`;
};

  return (
    <div className={styles.static_body}>
      {/* Static content */}
      <h2 className={styles.title}>Filling Time :)</h2>

        <div className={styles.below_title}>
            <span>{formatDate(finalOrder.date)}, {timeSlotName} {finalOrder.address && (<span>,</span>)} {finalOrder.address}{'\u00A0'}{'\u00A0'}|{'\u00A0'}{'\u00A0'}</span>
            <span>{guestCount} Guests{'\u00A0'}{'\u00A0'}|{'\u00A0'}{'\u00A0'}</span>
            <span>{typeFood}{'\u00A0'}{'\u00A0'}|{'\u00A0'}{'\u00A0'}</span>
            <span>{plateSizeForUse} plate</span>
        </div>
        <Box className = {styles.choose_container} sx={{                 
                        display: 'flex',
                        flexDirection: 'row',
                        /* Styles for screens smaller than 600px */
                        '@media (max-width: 660px)': { 
                          flexDirection: 'column', 
                          
                        },
                    }}>
            
            <div className={styles.chooseandimage}>
              <img src={choose_item_icon} height="17px" width="14px" alt="icon"/>
              <span className={styles.choose_title}> Choose items on your plate</span>
            </div>
            

            <Box className = {styles.food_characteristics_container}>
            <img src={spicy_icon} height="12px" width="12px" alt="icon"/>
            <span className={styles.food_characteristics}> - Spicy</span>

            <img src={Glutten_free_icon} height="12px" width="12px" alt="icon"/>
            <span className={styles.food_characteristics}> - Gluten free</span>

            <img src= {veg_icon} height="12px" width="12px" alt="icon"/>
            <span className={styles.food_characteristics}> - Vegan</span>
            </Box>
        </Box>
                        
    </div>
  );
};

export default StaticBodyComponent;
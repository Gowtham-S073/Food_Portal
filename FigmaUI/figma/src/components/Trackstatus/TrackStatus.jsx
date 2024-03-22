import React, { useState, useContext } from 'react';
import styles from './TrackStatus.module.css';
import { useNavigate } from 'react-router-dom';
import { ApiContext } from './../ApiContextProvider';
import Header from '../food_Selection/Components/header';
import { toast } from 'react-toastify';


function TrackStatus() {
  const { setTrackid, userName } = useContext(ApiContext);

  const [checktrackid, setChecktrackid] = useState({
    idInt: 0,
    idString: '',
  });

  console.log(userName);
  const [id, setId] = useState('');
  var navigate = useNavigate();

  const handleInputChange = (event) => {
    setId(event.target.value);
    const newValue = event.target.value;

    setChecktrackid((prevState) => ({
      ...prevState,
      idString: event.target.value,
    }));
  };

  var handleTrackOrder = async () => {
    try {
      console.log(checktrackid.idString);
      const apiUrl = `https://localhost:7239/api/TrackStatus/check_trackid/checktrackid?username=${encodeURIComponent(
        userName
      )}`;
      const response = await fetch(apiUrl, {
        method: 'POST',
        headers: {
          accept: 'text/plain',
          'Content-Type': 'application/json',
          "Authorization": "Bearer " + sessionStorage.getItem('token')
        },
        body: JSON.stringify({ ...checktrackid }),
      });
  
      if (response.status === 200) {
        const myData = await response.json();
  
        console.log(myData);
        checktrackid.idString = id;
        console.log(checktrackid.idString);
        sessionStorage.setItem('trackstatusid', id);
        setTrackid(id);
        navigate('/summary');
      } else {
        const errorData = await response.json();
        throw new Error(errorData.message);
      }
    } catch (err) {
      toast.error(err.message, {
        position: "top-left",
      });
    }
  };
  

  return (
    <section>
      <Header />
      <div className={styles.outerdiv}>
        <div className={styles.innerdiv}>
          <div className={styles.body1}>
            <div className={styles.item1}>
              <p className={styles.item1_content}>Track your order</p>
            </div>
            <div className={styles.item2}>
              <p>
                To track your order, please enter Track ID below. This was given to you in the confirmation email you should have received
              </p>
            </div>
          </div>
          <div className={styles.body2}>
            <div className={styles.item3}>
              <p>Track ID</p>
              <input type="text" className={styles.track_id} placeholder="Enter Track ID" onChange={handleInputChange} /><br /><br />
            </div>
            <div className={styles.item4}>
              <input type="submit" className={styles.btn} value="Track" onClick={handleTrackOrder} />
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}

export default TrackStatus;


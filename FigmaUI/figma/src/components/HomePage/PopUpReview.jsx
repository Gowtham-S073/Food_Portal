import React, { useState } from 'react';
import Rating from '@mui/material/Rating';
import Box from '@mui/material/Box';
import StarIcon from '@mui/icons-material/Star';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const labels = {
    0.5: 'Needs a Dash of Magic',
    1: 'Barely Touched the Taste Buds',
    1.5: 'Flavors in Hibernation',
    2: 'A Decent Culinary Attempt',
    2.5: 'Treading Flavor Waters',
    3: 'Found Some Tasty Treasures',
    3.5: 'Delivering Delicious Delights',
    4: 'Tickling the Taste Buds',
    4.5: 'A Symphony of Satisfying Flavors',
    5: 'Culinary Masterpiece, Taste Explosion!',
};

function getLabelText(value) {
    return `${value} Star${value !== 1 ? 's' : ''}, ${labels[value]}`;
}

export default function HoverRating() {
    const [value, setValue] = useState(2);
    const [hover, setHover] = useState(-1);

    const styles = {
        container: {
            width: '100%',
            maxWidth: 400,
            margin: '0 auto',
            padding: 20,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
        },
        starIcon: {
            opacity: 0.7,
        },
        labelBox: {
            marginTop: 10,
            textAlign: 'center',
            fontWeight: 'bold',
        },
    };
    const handleRatingChange = (event, newValue) => {
        setValue(newValue);
        toast.success('Review submitted!', {
            position: 'top-right',
            autoClose: 2000
        });
    };

    

    return (
        <Box style={styles.container}>
            <Rating
                name="hover-feedback"
                value={value}
                precision={0.5}
                getLabelText={getLabelText}
                onChange={handleRatingChange}
                onChangeActive={(event, newHover) => {
                    setHover(newHover);
                }}
                emptyIcon={<StarIcon style={styles.starIcon} fontSize="inherit" />}
            />
            {value !== null && (
                <Box style={styles.labelBox}>
                    {labels[hover !== -1 ? hover : value]}
                </Box>
            )}
           
        </Box>
    );
}

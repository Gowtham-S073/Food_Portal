import FoodType from "./components/Food Type/FoodType";
import Addon from "./components/addonPage/addon";
import Login from "./loginpage/Login";
import OrderDetail from "./components/OrderDetail/OrderDetail";
import Submitted from "./components/Submitted/Submitted";
import { createContext, useContext } from "react";
import './App.css'
import AdditionalCategory from "./components/AdditionalCategory/AdditionalCategory";
import Food_Selection from "./components/food_Selection/Components/food_selection";
import { Routes, Route } from 'react-router-dom';
import GroupSize from "./components/GroupSize/GroupSize";
import Dateandtime from "./components/DateandTime/Dateandtime";
import TrackStatus from './components/Trackstatus/TrackStatus';
import Summary from './components/Summary/Summary';
import { ApiContext } from "./components/ApiContextProvider";
import Homepage from "./components/HomePage/Homepage";
import PlateSize from "./components/PlateSize/PlateSize";
import RequestSummary from "./components/RequestSummary/RequestSummary";
import NotFound from "./components/NotFound";


export const AppContext = createContext(null);

function App() {
  const { userRole } = useContext(ApiContext);
  return (
    <div >
      <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'></link>

      {/* <AppContext.Provider value={{trackid, setTrackid, orderedFoodList, setOrderedFoodList, dataList, setDataList }}> */}

      <Routes>

        <Route path="/food_selection" element={(userRole === 'user') ?<Food_Selection />: <NotFound />} />
        <Route path='/trackstatus' element={<TrackStatus/>}/>
        <Route path="/home" element={(userRole === 'user') ?<Homepage />: <NotFound />} />
        {console.log("userRole",userRole)}
        <Route path="/order_details" element={(userRole === 'user') ?<OrderDetail/>: <NotFound />} />
        <Route path="/groupsize" element={(userRole === 'user') ?<GroupSize />: <NotFound />} />
        <Route path="/foodtype" element={(userRole === 'user') ?<FoodType />: <NotFound />} />
        <Route path='/summary' element={(userRole === 'user') ?<Summary/>: <NotFound />}/>
        <Route path='/additional_Cat' element={(userRole === 'user') ?<AdditionalCategory/>: <NotFound />}/>
        <Route path='/dateandtime' element={(userRole === 'user') ?<Dateandtime/>: <NotFound />}/>
        <Route path='/platesize' element={(userRole === 'user') ?<PlateSize/>: <NotFound />}/>
        <Route path='/addon' element={(userRole === 'user') ?<Addon/>: <NotFound />}/>
        <Route path='/' element={(userRole !== 'user') ?<Login/>: <NotFound />}/>
        <Route path='/requestsummary' element={(userRole === 'user') ?<RequestSummary/>: <NotFound />}/>
        <Route path='/Requestsuccess' element={(userRole === 'user') ?<Submitted/>: <NotFound />}/>
        <Route path='/*' element={<NotFound />}/>

      </Routes> 
   
      {/* </AppContext.Provider> */}
    </div>
  );
}

export default App;

import React from 'react';
import Header from './components/Header';
import Lights from './pages/Lights';
import Home from './pages/Home';
import AirConditioner from './pages/AirConditioner';
import { BrowserRouter as Router, Routes, Route, BrowserRouter } from 'react-router-dom';
import { NavLink } from 'react-router-dom';
import './App.css';

function App() {
  return (

    <>
      <BrowserRouter>
        <Header className = 'navb'>
          <Routes>
            <Route path='/lights' element={<Lights />} />
            <Route path='/airconditioner' element={<AirConditioner />} />
            <Route path='/home' element={<Home />} />
          </Routes>
        </Header>
      </BrowserRouter>     
    </>

  );

}

export default App;
 // return (
  //   <>
  //     <BrowserRouter>
  //       <Navbar>
  //         <Routes>
  //           <Route path='/lights' element={<Lights />} />
  //           <Route path='/airconditioner' element={<AirConditioner />} />
  //         </Routes>
  //       </Navbar>
  //     </BrowserRouter>
  //     <h1>Smart Home</h1>
  //   </>
  // );

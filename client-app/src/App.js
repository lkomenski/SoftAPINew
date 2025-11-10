import logo from './logo.svg';
import './App.css';
import Weather from './components/Weather';
import Login from './components/Login';
import Home from './components/Home';
import About from './components/About';

import { BrowserRouter, Routes, Route, Link } from 'react-router';

function App() {
  return (
    <BrowserRouter>
      <nav>
        <Link to="/"> Home</Link> |
        <Link to="/about"> About</Link> |
        <Link to="/weather"> Weather</Link> |
        <Link to="/login"> Login</Link>
      </nav>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/weather" element={<Weather />} />
        <Route path="/login" element={<Login />} />
        <Route path="/about" element={<About />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;

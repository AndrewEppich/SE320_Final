import { Container, Row, Col, Card, Button, ListGroup, Nav } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import Header from "./Components/Header";
import Home from "./Pages/Home";
import Scan from "./Pages/Scan";
import Summaries from "./Pages/Summaries";
import History from "./Pages/History";
function App() {
  return (
    <Router>
    <Header />
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/Scan" element={<Scan />} />
     <Route path="/Summaries" element={<Summaries />} />
      <Route path="/History" element={<History />} />
    </Routes>

    </Router>
  );
}

export default App;
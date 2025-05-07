import { Container, Row, Col, Card, Button, ListGroup, Nav } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import Header from "./Components/Header";
import Home from "./Pages/Home";
function App() {
  return (
    <>
    <Header />
    <Home />
    </>
  );
}

export default App;
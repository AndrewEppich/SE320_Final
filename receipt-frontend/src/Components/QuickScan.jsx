import { Container, Row, Col, Card, Button, ListGroup, Nav } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

function QuickScan() {
  return (
    <>
     <Row className="justify-content-center mb-3 mt-5">
            <Col xs={12} md={8} lg={6}>
                <h1>Welcome Back!</h1>
            </Col>
        </Row>
        <Row className="justify-content-center mb-4">
            <Col xs="auto">
                  <Button variant="primary" style={{
                      backgroundColor: '#6f42c1',
                      borderColor: '#6f42c1'
                  }}>Scan Now</Button>
            </Col>
        </Row>
      </> 
  );
}

export default QuickScan;
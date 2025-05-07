import { Container, Row, Col, Card, Button, ListGroup, Nav } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

function HomeMonthOverview() {  
    return (
        <Card>
            <Card.Body>
                <Card.Title className="mb-5 fs-4">This Month</Card.Title>
                <Row className="g-4">
                    <Col md={4}>
                        <Card>
                            <Card.Body>
                                <Card.Title>TOTAL SPENDING</Card.Title>
                                <Card.Text className="fs-3">$---</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col md={4}>
                        <Card>
                            <Card.Body>
                                <Card.Title>TOP VENDORS</Card.Title>
                                <Card.Text className="fs-3">---</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col md={4}>
                        <Card>
                            <Card.Body>
                                <Card.Title>SPENDING BY CATEGORY</Card.Title>
                                <Card.Text className="fs-3">---</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </Card.Body>
        </Card>
    );
}

export default HomeMonthOverview;
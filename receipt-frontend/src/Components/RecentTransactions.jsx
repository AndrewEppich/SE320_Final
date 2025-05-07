import { Container, Row, Col, Card, Button, ListGroup, Nav } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

function RecentTransactions() {
  return (
      <Card className="mb-4">
          <Card.Body>
              <Card.Title>RECENT TRANSACTIONS</Card.Title>
              <Nav
                  variant="pills"
                  className="mt-3"
                  style={{
                      '--bs-nav-pills-link-active-bg': '#6f42c1',
                      '--bs-nav-link-color': '#6f42c1',
                      '--bs-nav-link-hover-color': '#5a32a8'
                  }}
              >
                  <Nav.Item>
                      <Nav.Link
                          active
                          style={{
                              color: 'white',
                              fontWeight: 'bold'
                          }}
                      >
                          1
                      </Nav.Link>
                  </Nav.Item>
                  <Nav.Item>
                      <Nav.Link
                          style={{
                              color: '#6f42c1' 
                          }}
                      >
                          2
                      </Nav.Link>
                  </Nav.Item>
                  <Nav.Item>
                      <Nav.Link style={{ color: '#6f42c1' }}>3</Nav.Link>
                  </Nav.Item>
                  <Nav.Item>
                      <Nav.Link style={{ color: '#6f42c1' }}>4</Nav.Link>
                  </Nav.Item>
              </Nav>
              <ListGroup variant="flush">
                  <ListGroup.Item className="d-flex justify-content-between align-items-center">
                      <div>
                          <h5>---</h5>
                          <small className="text-muted">---</small>
                      </div>
                      <span>$---</span>
                  </ListGroup.Item>
              </ListGroup>
          </Card.Body>
      </Card>
  );
}

export default RecentTransactions;
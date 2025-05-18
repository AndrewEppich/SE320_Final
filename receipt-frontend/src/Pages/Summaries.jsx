import { useEffect, useState } from 'react';
import { Container, Card, Row, Col, Alert, Spinner, Button, ButtonGroup } from 'react-bootstrap';
import { api } from '../services/api';

function Summaries() {
  const [weeklySummary, setWeeklySummary] = useState(null);
  const [monthlySummary, setMonthlySummary] = useState(null);
  const [vendorSummary, setVendorSummary] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [selectedVendor, setSelectedVendor] = useState('');
  const [vendors, setVendors] = useState([]);

  useEffect(() => {
    fetchSummaries();
    fetchVendors();
  }, []);

  const fetchSummaries = async () => {
    try {
      setLoading(true);
      setError(null);

      // Get current date info
      const now = new Date();
      const currentYear = now.getFullYear();
      const currentMonth = now.getMonth() + 1;
      const currentWeek = Math.ceil(now.getDate() / 7);

      // Fetch weekly summary
      const weeklyData = await api.getWeeklySummary(currentYear, currentMonth, currentWeek);
      setWeeklySummary(weeklyData);

      // Fetch monthly summary
      const monthlyData = await api.getMonthlySummary(currentYear, currentMonth);
      setMonthlySummary(monthlyData);

    } catch (err) {
      console.error('Error fetching summaries:', err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const fetchVendors = async () => {
    try {
      const vendorList = await api.getAllVendors();
      setVendors(vendorList);
    } catch (err) {
      console.error('Error fetching vendors:', err);
    }
  };

  const handleVendorSelect = async (vendor) => {
    try {
      setSelectedVendor(vendor);
      const summary = await api.getVendorSummary(vendor);
      setVendorSummary(summary);
    } catch (err) {
      console.error('Error fetching vendor summary:', err);
    }
  };

  if (loading) {
    return (
      <Container className="py-4 text-center">
        <Spinner animation="border" role="status">
          <span className="visually-hidden">Loading...</span>
        </Spinner>
      </Container>
    );
  }

  if (error) {
    return (
      <Container className="py-4">
        <Alert variant="danger">
          <Alert.Heading>Error Loading Summaries</Alert.Heading>
          <p>{error}</p>
        </Alert>
      </Container>
    );
  }

  return (
    <Container className="py-4">
      <h1>Receipt Summaries</h1>

      <Row className="mb-4">
        <Col md={6}>
          <Card>
            <Card.Header>Weekly Summary</Card.Header>
            <Card.Body>
              {weeklySummary ? (
                <>
                  <h5>Total Spent: ${weeklySummary.totalSpent?.toFixed(2)}</h5>
                  <p>Receipts: {weeklySummary.dataJson?.totalReceipts || 0}</p>
                </>
              ) : (
                <p>No weekly data available</p>
              )}
            </Card.Body>
          </Card>
        </Col>

        <Col md={6}>
          <Card>
            <Card.Header>Monthly Summary</Card.Header>
            <Card.Body>
              {monthlySummary ? (
                <>
                  <h5>Total Spent: ${monthlySummary.totalSpent?.toFixed(2)}</h5>
                  <p>Receipts: {monthlySummary.dataJson?.totalReceipts || 0}</p>
                </>
              ) : (
                <p>No monthly data available</p>
              )}
            </Card.Body>
          </Card>
        </Col>
      </Row>

      <Card className="mb-4">
        <Card.Header>Vendor Summary</Card.Header>
        <Card.Body>
          <div className="mb-3">
            <ButtonGroup>
              {vendors.map(vendor => (
                <Button
                  key={vendor}
                  variant={selectedVendor === vendor ? 'primary' : 'outline-primary'}
                  onClick={() => handleVendorSelect(vendor)}
                >
                  {vendor}
                </Button>
              ))}
            </ButtonGroup>
          </div>

          {vendorSummary ? (
            <>
              <h5>Total Spent: ${vendorSummary.totalSpent?.toFixed(2)}</h5>
              <p>Receipts: {vendorSummary.dataJson?.totalReceipts || 0}</p>
            </>
          ) : (
            <p>Select a vendor to view summary</p>
          )}
        </Card.Body>
      </Card>
    </Container>
  );
}

export default Summaries;
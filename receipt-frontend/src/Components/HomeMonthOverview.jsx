import { useEffect, useState } from 'react';
import { Container, Row, Col, Card, Button, ListGroup, Nav, Spinner, Alert } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { api } from '../services/api';

function HomeMonthOverview() {
    const [monthlyData, setMonthlyData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchMonthlyData();
    }, []);

    const fetchMonthlyData = async () => {
        try {
            setLoading(true);
            const now = new Date();
            const data = await api.getMonthlySummary(now.getFullYear(), now.getMonth() + 1);
            console.log('Monthly Data:', data);
            console.log('Vendor Totals:', data?.vendorTotals);
            setMonthlyData(data);
        } catch (err) {
            console.error('Error fetching monthly data:', err);
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    if (loading) {
        return (
            <Card>
                <Card.Body className="text-center">
                    <Spinner animation="border" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </Spinner>
                </Card.Body>
            </Card>
        );
    }

    if (error) {
        return (
            <Card>
                <Card.Body>
                    <Alert variant="danger">
                        <Alert.Heading>Error Loading Monthly Data</Alert.Heading>
                        <p>{error}</p>
                    </Alert>
                </Card.Body>
            </Card>
        );
    }

    const topVendor = monthlyData?.vendorTotals?.[0]
        ? `${monthlyData.vendorTotals[0].vendor} ($${(monthlyData.vendorTotals[0].total || 0).toFixed(2)})`
        : 'No data';


    const categorySpending = monthlyData?.receipts?.reduce((acc, receipt) => {
        if (!receipt) return acc;
        const category = receipt.metadataJson?.category || 'Uncategorized';
        acc[category] = (acc[category] || 0) + (receipt.amount || 0);
        return acc;
    }, {}) || {};

    const topCategory = Object.entries(categorySpending)
        .sort(([, a], [, b]) => b - a)[0]?.[0] || 'No data';

    return (
        <Card>
            <Card.Body>
                <Card.Title className="mb-5 fs-4">This Month</Card.Title>
                <Row className="g-4">
                    <Col md={4}>
                        <Card>
                            <Card.Body>
                                <Card.Title>TOTAL SPENDING</Card.Title>
                                <Card.Text className="fs-3">
                                    ${(monthlyData?.totalSpent || 0).toFixed(2)}
                                </Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col md={4}>
                        <Card>
                            <Card.Body>
                                <Card.Title>TOP VENDOR</Card.Title>
                                <Card.Text className="fs-3">{topVendor}</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col md={4}>
                        <Card>
                            <Card.Body>
                                <Card.Title>TOP CATEGORY</Card.Title>
                                <Card.Text className="fs-3">{topCategory}</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </Card.Body>
        </Card>
    );
}

export default HomeMonthOverview;
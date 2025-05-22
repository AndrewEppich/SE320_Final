import { useEffect, useState } from 'react';
import { Container, Row, Col, Card, Button, ListGroup, Nav, Spinner, Alert } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { api } from '../services/api';

function RecentTransactions() {
    const [receipts, setReceipts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchRecentReceipts();
    }, []);

    const fetchRecentReceipts = async () => {
        try {
            setLoading(true);
            // Get all receipts sorted by date in descending order
            const data = await api.getReceipts('date', 'desc');
            // Take only the first 5 receipts
            setReceipts(data.slice(0, 5));
        } catch (err) {
            console.error('Error fetching recent receipts:', err);
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
                        <Alert.Heading>Error Loading Recent Transactions</Alert.Heading>
                        <p>{error}</p>
                    </Alert>
                </Card.Body>
            </Card>
        );
    }

    return (
        <Card>
            <Card.Body>
                <Card.Title className="mb-4">Recent Transactions</Card.Title>
                {receipts.length === 0 ? (
                    <p className="text-muted">No recent transactions found.</p>
                ) : (
                    <ListGroup variant="flush">
                        {receipts.map(receipt => (
                            <ListGroup.Item key={receipt.receiptID} className="d-flex justify-content-between align-items-center">
                                <div>
                                    <h5>{receipt.vendor || 'Unknown Vendor'}</h5>
                                    <small className="text-muted">
                                        {new Date(receipt.purchaseDate).toLocaleDateString()}
                                    </small>
                                </div>
                                <span>${(receipt.amount || 0).toFixed(2)}</span>
                            </ListGroup.Item>
                        ))}
                    </ListGroup>
                )}
            </Card.Body>
        </Card>
    );
}

export default RecentTransactions;
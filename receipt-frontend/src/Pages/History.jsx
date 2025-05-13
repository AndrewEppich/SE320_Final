import { useEffect, useState } from 'react';
import { Container, Card, ListGroup, Alert, Spinner } from 'react-bootstrap';
import { api } from '../services/api';

function History() {
    const [receipts, setReceipts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchReceipts = async () => {
            try {
                setLoading(true);
                setError(null);
                console.log('Fetching receipts...');
                const data = await api.getReceipts();
                console.log('Received data:', data);
                setReceipts(data);
            } catch (err) {
                console.error('Error in History component:', err);
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchReceipts();
    }, []);

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
                    <Alert.Heading>Error Loading Receipts</Alert.Heading>
                    <p>{error}</p>
                    <hr />
                    <p className="mb-0">
                        Please make sure:
                        <ul>
                            <li>The backend server is running</li>
                            <li>You can access the Swagger UI at https://localhost:7096/swagger</li>
                            <li>There are no CORS issues in the browser console</li>
                        </ul>
                    </p>
                </Alert>
            </Container>
        );
    }

    return (
        <Container className="py-4">
            <h1>Receipt History</h1>
            {receipts.length === 0 ? (
                <Alert variant="info">No receipts found.</Alert>
            ) : (
                <Card>
                    <ListGroup variant="flush">
                        {receipts.map(receipt => (
                            <ListGroup.Item key={receipt.receiptID} className="d-flex justify-content-between align-items-center">
                                <div>
                                    <h5>{receipt.vendor}</h5>
                                    <small className="text-muted">
                                        {new Date(receipt.purchaseDate).toLocaleDateString()}
                                    </small>
                                </div>
                                <span>${receipt.amount}</span>
                            </ListGroup.Item>
                        ))}
                    </ListGroup>
                </Card>
            )}
        </Container>
    );
}

export default History;
import { useEffect, useState } from 'react';
import { Container, Card, ListGroup, Alert, Spinner, Button, ButtonGroup } from 'react-bootstrap';
import { api } from '../services/api';

function History() {
    const [receipts, setReceipts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [sortConfig, setSortConfig] = useState({ field: null, order: 'asc' });

    useEffect(() => {
        fetchReceipts();
    }, [sortConfig]);

    const fetchReceipts = async () => {
        try {
            setLoading(true);
            setError(null);
            console.log('Fetching receipts...');
            const data = await api.getReceipts(sortConfig.field, sortConfig.order);
            console.log('Received data:', data);
            setReceipts(data);
        } catch (err) {
            console.error('Error in History component:', err);
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    const handleSort = (field) => {
        setSortConfig(prevConfig => ({
            field,
            order: prevConfig.field === field && prevConfig.order === 'asc' ? 'desc' : 'asc'
        }));
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
                    <Alert.Heading>Error Loading Receipts</Alert.Heading>
                    <p>{error}</p>
                    <hr />
                    <p className="mb-0">
                        Please make sure:
                        backend is running
                    </p>
                </Alert>
            </Container>
        );
    }

    return (
        <Container className="py-4">
            <h1>Receipt History</h1>
            <div className="mb-3">
                <ButtonGroup>
                    <Button 
                        variant={sortConfig.field === 'date' ? 'primary' : 'outline-primary'}
                        onClick={() => handleSort('date')}
                    >
                        Sort by Date {sortConfig.field === 'date' && (sortConfig.order === 'asc' ? '↑' : '↓')}
                    </Button>
                    <Button 
                        variant={sortConfig.field === 'amount' ? 'primary' : 'outline-primary'}
                        onClick={() => handleSort('amount')}
                    >
                        Sort by Amount {sortConfig.field === 'amount' && (sortConfig.order === 'asc' ? '↑' : '↓')}
                    </Button>
                    <Button
                        variant={sortConfig.field === 'vendor' ? 'primary' : 'outline-primary'}
                        onClick={() => handleSort('vendor')}
                    >
                        Sort by Vendor {sortConfig.field === 'vendor' && (sortConfig.order === 'asc' ? '↑' : '↓')}
                    </Button>
                </ButtonGroup>
            </div>
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
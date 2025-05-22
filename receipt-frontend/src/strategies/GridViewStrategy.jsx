import { Row, Col, Card } from 'react-bootstrap';
import { ViewStrategy } from './ViewStrategy.jsx';

export class GridViewStrategy extends ViewStrategy {
    render(receipts) {
        return (
            <Row xs={1} md={2} lg={3} className="g-4">
                {receipts.map(receipt => (
                    <Col key={receipt.receiptID}>
                        <Card>
                            <Card.Body>
                                <Card.Title>{receipt.vendor}</Card.Title>
                                <Card.Subtitle className="mb-2 text-muted">
                                    {new Date(receipt.purchaseDate).toLocaleDateString()}
                                </Card.Subtitle>
                                <Card.Text className="h4">
                                    ${receipt.amount}
                                </Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        );
    }
}
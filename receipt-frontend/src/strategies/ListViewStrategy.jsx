import { ListGroup } from 'react-bootstrap';
import { ViewStrategy } from './ViewStrategy.jsx';

export class ListViewStrategy extends ViewStrategy {
    render(receipts) {
        return (
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
        );
    }
}
import { useState, useRef } from 'react';
import { Container, Row, Col, Form, Button, Alert } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

function Scan() {
  const [selectedFile, setSelectedFile] = useState(null);
  const [preview, setPreview] = useState(null);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);
  const [loading, setLoading] = useState(false);
  const [currentReceipt, setCurrentReceipt] = useState(null);
  const [manualValues, setManualValues] = useState({
    vendor: '',
    amount: '',
    purchaseDate: ''
  });
  const fileInputRef = useRef(null);

  const handleFileSelect = (event) => {
    const file = event.target.files[0];
    if (file) {
      if (!file.type.startsWith('image/')) {
        setError('Please select an image file');
        return;
      }
      setSelectedFile(file);
      setError(null);
      setSuccess(null);
      
      //preview
      const reader = new FileReader();
      reader.onloadend = () => {
        setPreview(reader.result);
      };
      reader.readAsDataURL(file);
    }
  };

  const clearForm = () => {
    setSelectedFile(null);
    setPreview(null);
    setError(null);
    setCurrentReceipt(null);
    setManualValues({
      vendor: '',
      amount: '',
      purchaseDate: ''
    });
    if (fileInputRef.current) {
      fileInputRef.current.value = '';
    }
  };

  const handleManualValueChange = (e) => {
    const { name, value } = e.target;
    setManualValues(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleManualUpdate = async () => {
    if (!currentReceipt) {
      setError('No receipt to update');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const response = await fetch(`http://localhost:5000/api/Receipts/${currentReceipt.receiptID}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          ...currentReceipt,
          vendor: manualValues.vendor,
          amount: parseFloat(manualValues.amount),
          purchaseDate: new Date(manualValues.purchaseDate).toISOString()
        }),
      });

      if (!response.ok) {
        throw new Error('Failed to update receipt');
      }

      setSuccess('Receipt updated successfully!');
      clearForm();
    } catch (err) {
      setError(err.message || 'Failed to update receipt');
    } finally {
      setLoading(false);
    }
  };

  const handleUpload = async () => {
    if (!selectedFile) {
      setError('Please select a file first');
      return;
    }

    setLoading(true);
    setError(null);
    setSuccess(null);

    try {
      const formData = new FormData();
      formData.append('image', selectedFile);

      const response = await fetch('http://localhost:5000/api/Receipts/scan', {
        method: 'POST',
        body: formData,
      });

      if (!response.ok) {
        const errorData = await response.text();
        throw new Error(errorData || 'Failed to upload receipt');
      }

      const data = await response.json();
      setCurrentReceipt(data);
      setManualValues({
        vendor: data.vendor || '',
        amount: data.amount?.toString() || '',
        purchaseDate: data.purchaseDate ? new Date(data.purchaseDate).toISOString().split('T')[0] : ''
      });
      setSuccess('Receipt uploaded successfully! You can now manually adjust the values if needed.');
      
    } catch (err) {
      setError(err.message || 'Failed to upload receipt');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container className="py-5">
      <Row className="justify-content-center">
        <Col xs={12} md={8} lg={6}>
          <h1 className="text-center mb-4">Scan Receipt</h1>
          
          <Form>
            <Form.Group className="mb-3">
              <Form.Label>Select Receipt Image</Form.Label>
              <Form.Control
                ref={fileInputRef}
                type="file"
                accept="image/*"
                onChange={handleFileSelect}
                disabled={loading}
              />
            </Form.Group>

            {preview && (
              <div className="mb-3">
                <img
                  src={preview}
                  alt="Receipt preview"
                  style={{ maxWidth: '100%', maxHeight: '300px' }}
                  className="img-thumbnail"
                />
              </div>
            )}

            {currentReceipt && (
              <div className="mb-3">
                <h4>Manual Override</h4>
                <Form.Group className="mb-3">
                  <Form.Label>Vendor</Form.Label>
                  <Form.Control
                    type="text"
                    name="vendor"
                    value={manualValues.vendor}
                    onChange={handleManualValueChange}
                    placeholder="Enter vendor name"
                  />
                </Form.Group>

                <Form.Group className="mb-3">
                  <Form.Label>Amount</Form.Label>
                  <Form.Control
                    type="number"
                    step="0.01"
                    name="amount"
                    value={manualValues.amount}
                    onChange={handleManualValueChange}
                    placeholder="Enter amount"
                  />
                </Form.Group>

                <Form.Group className="mb-3">
                  <Form.Label>Purchase Date</Form.Label>
                  <Form.Control
                    type="date"
                    name="purchaseDate"
                    value={manualValues.purchaseDate}
                    onChange={handleManualValueChange}
                  />
                </Form.Group>

                <Button
                  variant="primary"
                  onClick={handleManualUpdate}
                  disabled={loading}
                  className="w-100 mb-3"
                  style={{
                    backgroundColor: '#6f42c1',
                    borderColor: '#6f42c1'
                  }}
                >
                  {loading ? 'Updating...' : 'Update Receipt'}
                </Button>
              </div>
            )}

            {error && (
              <Alert variant="danger" className="mb-3">
                {error}
              </Alert>
            )}

            {success && (
              <Alert variant="success" className="mb-3">
                {success}
              </Alert>
            )}

            <Button
              variant="primary"
              onClick={handleUpload}
              disabled={!selectedFile || loading}
              className="w-100"
              style={{
                backgroundColor: '#6f42c1',
                borderColor: '#6f42c1'
              }}
            >
              {loading ? 'Uploading...' : 'Upload Receipt'}
            </Button>
          </Form>
        </Col>
      </Row>
    </Container>
  );
}

export default Scan;
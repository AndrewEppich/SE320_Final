import { useState, useRef } from 'react';
import { Container, Row, Col, Form, Button, Alert } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

function Scan() {
  const [selectedFile, setSelectedFile] = useState(null);
  const [preview, setPreview] = useState(null);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);
  const [loading, setLoading] = useState(false);
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
    if (fileInputRef.current) {
      fileInputRef.current.value = '';
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
      setSuccess('Receipt uploaded successfully!');
      clearForm();
      
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
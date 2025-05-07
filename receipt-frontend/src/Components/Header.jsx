import React from 'react';
import { Navbar, Nav, Container } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

function Header() {
    return (
        <Navbar bg="primary" variant="dark" expand="lg" style={{ "--bs-primary-rgb": "111, 66, 193" }} >
            <Container>
                <Navbar.Brand href="/">Receipt Scanner</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="justify-content-center flex-grow-1">
                        <Nav.Link href="/" active>Home</Nav.Link>
                        <Nav.Link href="/Scan">Scan/Upload</Nav.Link>
                        <Nav.Link href="/Summaries">Summaries</Nav.Link>
                        <Nav.Link href="/History">History</Nav.Link>
                    </Nav>
                    <Nav className='justify-content-end'>
                        <Nav.Link href="/Login" className="text-light">Login</Nav.Link>
                        <Nav.Link href="/Register" className="text-light">Register</Nav.Link>
                    </Nav>

                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default Header;
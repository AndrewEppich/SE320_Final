
import React from 'react';
import { Navbar, Nav, Container } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

function Header() {
    return (
        <Navbar bg="primary" variant="dark" expand="lg" style={{ "--bs-primary-rgb": "111, 66, 193" }} >
            <Container>
                <Navbar.Brand as={NavLink} to="/">Receipt Scanner</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="justify-content-center flex-grow-1">
                        <Nav.Link
                            as={NavLink}
                            to="/"
                            end
                            className={({ isActive }) => isActive ? "active fw-bold" : ""}
                        >
                            Home
                        </Nav.Link>
                        <Nav.Link
                            as={NavLink}
                            to="/Scan"
                            className={({ isActive }) => isActive ? "active fw-bold" : ""}
                        >
                            Scan/Upload
                        </Nav.Link>
                        <Nav.Link
                            as={NavLink}
                            to="/Summaries"
                            className={({ isActive }) => isActive ? "active fw-bold" : ""}
                        >
                            Summaries
                        </Nav.Link>
                        <Nav.Link
                            as={NavLink}
                            to="/History"
                            className={({ isActive }) => isActive ? "active fw-bold" : ""}
                        >
                            History
                        </Nav.Link>
                    </Nav>
                    {/* <Nav className='justify-content-end'>
                        <Nav.Link
                            as={NavLink}
                            to="/Login"
                            className={({ isActive }) => isActive ? "active fw-bold" : "text-light"}
                        >
                            Login
                        </Nav.Link>
                        <Nav.Link
                            as={NavLink}
                            to="/Register"
                            className={({ isActive }) => isActive ? "active fw-bold" : "text-light"}
                        >
                            Register
                        </Nav.Link>
                    </Nav> */}
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default Header;
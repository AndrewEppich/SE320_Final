import { Container, Row, Col, Card, Button, ListGroup, Nav } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import HomeMonthOverview from "../Components/HomeMonthOverview";
import RecentTransactions from "../Components/RecentTransactions";
import QuickScan from "../Components/QuickScan";


function Home() {
    return (
        <Container fluid className="px-4 py-3 text-center" style={{ backgroundColor: '#f7f5f1', minHeight: '100vh' }}>

            <div className="mb-5"><QuickScan /></div>
            <div className="mb-5"><HomeMonthOverview /></div>
            <RecentTransactions/>

        </Container>
    );
}
export default Home;
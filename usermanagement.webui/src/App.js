import { Container } from 'react-bootstrap';
import { Outlet } from 'react-router-dom';
import { Header } from './components/Header';
import { AuthProvider } from './contexts/AuthContext';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';



function App() {
    return (
        <AuthProvider>
            <Header />
            <ToastContainer />
            <Container className="my-2">
                <Outlet />
            </Container>
        </AuthProvider>
    );
}

export default App;

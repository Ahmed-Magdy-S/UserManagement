import { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { Form, Button, Row, Col } from 'react-bootstrap';
import { FormContainer } from '../components/FormContainer';
import { useAuthContext } from '../contexts/AuthContext';
import { FormInput } from '../components/FormInput';
import { useFormInput } from '../hooks/useFormInput';
import { toast } from 'react-toastify';


const LoginScreen = () => {
    const userName = useFormInput("");
    const password = useFormInput("");
    const navigate = useNavigate();
    const { login } = useAuthContext();
    const { state } = useLocation()
    const [loading, setLoading] = useState(false);

    const loginHandler = async (e) => {

        e.preventDefault();

        const loginDto = {
            "userName": userName.value,
            "password": password.value
        }
        try {
            setLoading(true)
            await login(loginDto)
            navigate(state?.path || "/");
            toast.success("User logged in successfully");
        }
        catch (err) {
            toast.error(err.message);
        }
        finally {
            setLoading(false)
        }

    };



    return (
        <FormContainer>
            <h1>Sign In</h1>

            <Form onSubmit={loginHandler}>
                <FormInput
                    type="text"
                    placeholder="Enter Userame"
                    label="Userame"
                    state={userName}
                />
                <FormInput
                    type="password"
                    placeholder="Enter Password"
                    label="Password"
                    state={password}
                />

               

                <Button disabled={loading} type='submit' variant='primary' className='mt-3'>
                    Sign In
                </Button>
            </Form>

            {loading && <p>Loading...</p>}

            <Row className='py-3'>
                <Col>
                    New Customer? <Link to={`/register`}>Register</Link>
                </Col>
            </Row>
        </FormContainer>
    );
};

export { LoginScreen };
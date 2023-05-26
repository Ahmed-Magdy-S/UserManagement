import { useState } from "react";
import { Link } from 'react-router-dom';
import { Form, Button, Row, Col } from 'react-bootstrap';
import { FormContainer } from '../components/FormContainer';
import { FormInput } from '../components/FormInput';
import { useFormInput } from '../hooks/useFormInput';
import { useAuthContext } from '../contexts/AuthContext';
import { toast } from 'react-toastify';


const RegisterScreen = () => {
    const [loading, setLoading] = useState(false);
    const { register} = useAuthContext();
    const firstname = useFormInput("");
    const fathername = useFormInput("");
    const familyname = useFormInput("");
    const address = useFormInput("");
    const birthdate = useFormInput("");
    const userName = useFormInput("");
    const password = useFormInput("");

    const submitHandler = async (e) => {
        e.preventDefault();
        const registerDto = {
            "userName": userName.value,
            "firstName": firstname.value,
            "fatherName": fathername.value,
            "familyName": familyname.value,
            "address": address.value,
            "birthdate": birthdate.value,
            "password": password.value
        }
        try {
            setLoading(true)
            await register(registerDto)
            toast.success("User registerd successfully");
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
            <h1>Register</h1>
            <Form onSubmit={submitHandler}>
                <FormInput
                    type="text"
                    placeholder="Enter First Name"
                    label="First Name"
                    state={firstname}
                />
                <FormInput
                    type="text"
                    placeholder="Enter Father Name"
                    label="Father Name"
                    state={fathername}
                />
                <FormInput
                    type="text"
                    placeholder="Enter Family Name"
                    label="Family Name"
                    state={familyname}
                />
                <FormInput
                    type="text"
                    placeholder="Enter Userame"
                    label="Userame"
                    state={userName}
                />
                <FormInput
                    type="text"
                    placeholder="Enter Address"
                    label="Address"
                    state={address}
                />
                <FormInput
                    type="password"
                    placeholder="Enter Password"
                    label="Password"
                    state={password}
                />
                <FormInput
                    type="date"
                    placeholder="Enter Birthdate"
                    label="Birthdate"
                    state={birthdate}
                />

                <Button disabled={loading} type='submit' variant='primary' className='mt-3'>
                    Register
                </Button>
            </Form>

            {loading && <p>Loading...</p>}

            <Row className='py-3'>
                <Col>
                    Already have an account? <Link to={`/login`}>Login</Link>
                </Col>
            </Row>
        </FormContainer>
    );
};

export { RegisterScreen };
import { useState, useEffect } from "react";
import { Form, Button, Col , Row} from 'react-bootstrap';
import { FormContainer } from '../components/FormContainer';
import { toast } from 'react-toastify';
import { getProfile, updateProfile, resetPassword } from "../api/accountApi";
import { useNavigate } from 'react-router-dom';
import { useAuthContext } from "../contexts/AuthContext";

function ProfileScreen() {
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    const { token } = useAuthContext();
    const [firstname, setFirstname] = useState("");
    const [fathername, setFathrname] = useState("");
    const [familyname, setFamilyname] = useState("");
    const [address, setAddress] = useState("");
    const [birthdate, setBirthdate] = useState("");
    const [userName, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [newPassword, setNewPassword] = useState("");

    useEffect(() => {
        try {
            setLoading(true)

            const pass = prompt("enter your password:")
            const profile = getProfile({ password: pass }, token)
            profile.then(data => {
                setUsername(data?.userName)
                setFirstname(data?.firstName);
                setFathrname(data?.fatherName);
                setFamilyname(data?.familyName);
                setAddress(data?.address);
                setBirthdate(new Date().toISOString(data?.birthdate).slice(0, 10));
                setPassword(pass);
            })
        }
        catch (e) {
            navigate("/")
            toast.error("error " + e.message)
        }

        finally {
            setLoading(false)
        }



    }, []);

    const resetPasswordHandler = async (e) => {
        e.preventDefault();

        const resetPasswordDto = {
            "oldPassword": password,
            "newPassword": newPassword,

        }
        try {
            setLoading(true)
            await resetPassword(resetPasswordDto, token)
            toast.success("password updated successfully");
            setPassword(newPassword);
        }
        catch (err) {
            toast.error(err.message);
        }
        finally {
            setLoading(false)
        }
    };



    const submitHandler = async (e) => {
        e.preventDefault();

        const updateDto = {
            "userName": userName,
            "firstName": firstname,
            "fatherName": fathername,
            "familyName": familyname,
            "address": address,
            "birthdate": birthdate,
            "password": password
        }
        try {
            setLoading(true)
            await updateProfile(updateDto, token)
            toast.success("profile updated successfully");
        }
        catch (err) {
            toast.error(err.message);
        }
        finally {
            setLoading(false)
        }
    };

    if (loading) return <p>Loading...</p>

    return (
        <FormContainer>
            <h1>Profile</h1>
            <Form onSubmit={submitHandler}>
                <Form.Group className='my-2' controlId='username'>
                    <Form.Label>User Name</Form.Label>
                    <Form.Control
                        type='text'
                        placeholder='Enter username'
                        value={userName}
                        onChange={(e) => setUsername(e.target.value)}
                    ></Form.Control>
                </Form.Group>

                <Form.Group className='my-2' controlId='firstname'>
                    <Form.Label>First Name</Form.Label>
                    <Form.Control
                        type='text'
                        placeholder='Enter firstname'
                        value={firstname}
                        onChange={(e) => setFirstname(e.target.value)}
                    ></Form.Control>
                </Form.Group>

                <Form.Group className='my-2' controlId='fathername'>
                    <Form.Label>Father Name</Form.Label>
                    <Form.Control
                        type='text'
                        placeholder='Enter fathername'
                        value={fathername}
                        onChange={(e) => setFathrname(e.target.value)}
                    ></Form.Control>
                </Form.Group>

                <Form.Group className='my-2' controlId='familyname'>
                    <Form.Label>Family Name</Form.Label>
                    <Form.Control
                        type='text'
                        placeholder='Enter familyname'
                        value={familyname}
                        onChange={(e) => setFamilyname(e.target.value)}
                    ></Form.Control>
                </Form.Group>

                <Form.Group className='my-2' controlId='address'>
                    <Form.Label>Address</Form.Label>
                    <Form.Control
                        type='text'
                        placeholder='Enter address'
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                    ></Form.Control>
                </Form.Group>

                <Form.Group className='my-2' controlId='birthdate'>
                    <Form.Label>First Name</Form.Label>
                    <Form.Control
                        type='date'
                        placeholder='Enter birthdate'
                        value={birthdate}
                        onChange={(e) => setBirthdate(e.target.value)}
                    ></Form.Control>
                </Form.Group>

                <Form.Group className='my-2' controlId='password'>
                    <Form.Label>Password</Form.Label>
                    <Form.Control
                        type='password'
                        placeholder='Enter password'
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    ></Form.Control>
                </Form.Group>

                <Form.Group className='my-2' controlId='newPassword'>
                    <Form.Label>New Password</Form.Label>
                    <Row>
                        <Col>
                            <Form.Control
                                type='password'
                                placeholder='Enter new password'
                                value={newPassword}
                                onChange={(e) => setNewPassword(e.target.value)}
                            ></Form.Control>
                        </Col>
                        <Col>
                            <Button disabled={loading} onClick={resetPasswordHandler} variant='primary' className='mt-3'>
                                Change Password
                            </Button>
                        </Col>
                    </Row>
            
                </Form.Group>

                <Button disabled={loading} type='submit' variant='primary' className='mt-3'>
                    Update Profile
                </Button>
            </Form>

            {loading && <p>Loading...</p>}
        </FormContainer>
    );
}

export { ProfileScreen };
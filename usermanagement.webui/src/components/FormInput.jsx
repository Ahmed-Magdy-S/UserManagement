import React from 'react';
import { Form } from 'react-bootstrap';

function FormInput({ label, type, placeholder, state }) {



    return (
        <Form.Group className='my-3' controlId={label} >
            <Form.Label>{label}</Form.Label>
            <Form.Control
                type={type}
                placeholder={placeholder}
                {...state}
            ></Form.Control>
        </Form.Group>
    );
}

export { FormInput };
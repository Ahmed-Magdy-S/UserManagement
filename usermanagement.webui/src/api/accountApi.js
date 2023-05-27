
const BASE_URL = "https://localhost:7163/api/account";


const updateProfile = async (updateProfileDto,token) => {

    try {
        const response = await fetch(`${BASE_URL}/profile`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
            body: JSON.stringify(updateProfileDto)
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.log(`${errorData.title} ${errorData.detail}`)
            throw new Error(`${errorData.title} ${errorData.detail}`);
        }

        const data = await response.json();
        console.log('User registered successfully:', data);

    } catch (error) {
        console.error('Error registering user:', error.message);
        throw error;
    }
}

const getProfile = async (requestProfileDto,token) => {

    try {
        const response = await fetch(`${BASE_URL}/profile`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
            body: JSON.stringify(requestProfileDto)
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.log(`${errorData.title} ${errorData.detail}`)
            throw new Error(`${errorData.title} ${errorData.detail}`);
        }

        const data = await response.json();

        return data;

    } catch (error) {
        console.error('Error getting profile:', error.message);
        throw error;
    }
}

const resetPassword = async (resetPasswordDto ,token) => {
    try {
        const response = await fetch(`${BASE_URL}/ResetPassword`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
            body: JSON.stringify(resetPasswordDto)
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.log(`${errorData.title} ${errorData.detail}`)
            throw new Error(`${errorData.title} ${errorData.detail}`);
        }

    } catch (error) {
        console.error('Error getting profile:', error.message);
        throw error;
    }
}

export { updateProfile, resetPassword, getProfile }
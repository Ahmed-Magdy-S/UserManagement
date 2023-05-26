import { useState, createContext, useContext } from "react";

const AuthContext = createContext();

const BASE_URL = "https://localhost:7163/api/account";

function AuthProvider({ children }) {
    const [authenticated, setAuthenticated] = useState(false);

    const register = async (registerDto) => {

        try
        {
            const response = await fetch(`${BASE_URL}/register`, {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(registerDto)
            });

            if (!response.ok) {
                const errorData = await response.json();
                console.log(`${errorData.title} ${errorData.detail}`)
                throw new Error(`${errorData.title} ${errorData.detail}`);
            }

            const data = await response.json();
            console.log('User registered successfully:', data);
            setAuthenticated(true);
        } catch (error) {
            console.error('Error registering user:', error.message);
            throw error;
        }

    }

    const login = async (loginDto) => {

        try {
            const response = await fetch(`${BASE_URL}/login`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(loginDto)
            });

            if (!response.ok) {
                const errorData = await response.json();
                console.log(`${errorData.title} ${errorData.detail}`)
                throw new Error(`${errorData.title} ${errorData.detail}`);
            }

            const data = await response.json();
            setAuthenticated(true);

            console.log('User logged in successfully:', data);
        } catch (error) {
            console.error('Error login:', error.message);
            throw error;
        }
    }

    const logout = () => {
      
    }

    

    const contextValue = { authenticated, register, login, logout };

    return <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>;

}

function useAuthContext() {
    return useContext(AuthContext);
}


export { AuthProvider, useAuthContext }
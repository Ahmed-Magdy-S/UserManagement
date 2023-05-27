import { useState, createContext, useContext, useEffect } from "react";
import { tokenHandler } from "../utils/tokenHandler";

const AuthContext = createContext();

const BASE_URL = "https://localhost:7163/api/account";

function AuthProvider({ children }) {

    const [token, setToken] = useState(tokenHandler.getToken("jwt"));
    const [authenticated, setAuthenticated] = useState(false);
    const [username, setUsername] = useState(null);

    useEffect(() => {
        setToken(tokenHandler.getToken("jwt"))
        console.log(token)
        if (!token || token.trim().length === 0) {
            setAuthenticated(false);
            setUsername(null)
        }
        else {
            setAuthenticated(true);
            setUsername(localStorage.getItem("user"))
        }

    }, [token])

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
            tokenHandler.setToken("jwt", data.token, data.expiration)
            setToken(data.token);
            setUsername(data.username)
            localStorage.setItem("user", data.username);


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
            tokenHandler.setToken("jwt", data.token, data.expiration)
            setToken(data.token);
            setUsername(data.username)
            localStorage.setItem("user", data.username);
            console.log('User logged in successfully:', data);
        } catch (error) {
            console.error('Error login:', error.message);
            throw error;
        }
    }

    const logout = () => {
        tokenHandler.deleteToken("jwt");
        setToken("");
        localStorage.removeItem("user");
        setUsername(null)

    }


    

    const contextValue = { authenticated, register, login, logout, token, username };

    return <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>;

}

function useAuthContext() {
    return useContext(AuthContext);
}


export { AuthProvider, useAuthContext }
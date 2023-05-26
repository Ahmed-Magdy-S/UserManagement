import { useLocation, Navigate } from "react-router-dom";
import { useAuthContext } from "../contexts/AuthContext";


function RequireAuth({ children }) {
    const { authenticated } = useAuthContext();
    const location = useLocation();

    return authenticated === true ? children : <Navigate to="/login" replace state={{ path: location.pathname }} />;
}

export { RequireAuth }
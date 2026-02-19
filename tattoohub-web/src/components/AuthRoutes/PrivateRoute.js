import { Navigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

function PrivateRoute( {children} ) {
    const {isAuthenticated, user, loading} = useAuth();

    if(loading) {
        return null;
    }

    if(!isAuthenticated) {
        return <Navigate to="/auth/login"/>;
    }

    return children;
}

export default PrivateRoute;
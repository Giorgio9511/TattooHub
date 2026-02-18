import { authApi } from "../../api/authApi";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import '../../styles/Login.css';

function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        try{
            const response = await authApi.login( {
                email,
                password
            });

            const {roles, token, email: userEmail, userId} = response.data;
            const role = roles[0];
            console.log(response.data);

            localStorage.setItem("token", token);
            localStorage.setItem("userEmail", userEmail);
            localStorage.setItem("userId", userId);
            localStorage.setItem("role", role);

            if(role === "Admin"){
                navigate("/admin/dashboard");
            }else{
                navigate("/artist/dashboard");
            }
        } catch (error) {
            console.error("Error en login", error);
            alert("Credenciales inválidas");
        }   
    };

    return (
        <div className="contenedor">
            <div className="card-form">
                <form onSubmit={handleSubmit}>
                    <h2 className="titulo-card">Login</h2>

                    <div className="form-group">
                        <input
                            className="input-form"
                            type="email"
                            placeholder="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>
                    
                    <div className="form-group">
                        <input
                        className="input-form"
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>
                    

                    <button className="btn-success" type="submit">Ingresar</button>
                </form>
            </div>        
        </div>
        
    );
}

export default Login;
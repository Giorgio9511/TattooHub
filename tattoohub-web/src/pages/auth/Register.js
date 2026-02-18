import { authApi } from "../../api/authApi";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import '../../styles/Register.css';

function Register() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [nombreCompleto, setNombreCompleto] = useState("");
    const [nombreEstudio, setNombreEstudio] = useState("");
    const [direccionEstudio, setDireccionEstudio] = useState("");

    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        try{
            const response = await authApi.register( {
            email,
            password,
            nombreCompleto,
            nombreEstudio,
            direccionEstudio
        });

        const {roles, token, email: userEmail, userId} = response.data;
        const role = roles[0];
        console.log(response.data);

        localStorage.setItem("token", token);
        localStorage.setItem("userEmail", userEmail);
        localStorage.setItem("userId", userId);
        localStorage.setItem("role", role);

        navigate("/artist/dashboard");
        } catch(error) {
            console.log("Error en registro", error);
            alert("Registro invalido");
        }
        
    };

    return (
        <div className="contenedor">
            <div className="card-form">
                <form onSubmit={handleSubmit}>
                    <h2 className="titulo-card">Registro</h2>
                    <div className="form-group">
                        <label className="label-form">Nombre:</label>
                        <input
                            className="input-form"
                            placeholder="Nombre Completo..."
                            value={nombreCompleto}
                            onChange={(e) => setNombreCompleto(e.target.value)}>
                        </input>
                    </div>
                    <div className="form-group">
                        <label className="label-form">Email:</label>
                        <input
                            className="input-form"
                            placeholder="Email..."
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}>
                        </input>
                    </div>
                    <div className="form-group">
                        <label className="label-form">Nombre de tu estudio:</label>
                        <input
                            className="input-form"
                            placeholder="Nombre del estudio..."
                            value={nombreEstudio}
                            onChange={(e) => setNombreEstudio(e.target.value)}>
                        </input>
                    </div>
                    <div className="form-group">
                        <label className="label-form">Direccion de tu estudio:</label>
                        <input
                            className="input-form"
                            placeholder="Direccion del estudio..."
                            value={direccionEstudio}
                            onChange={(e) => setDireccionEstudio(e.target.value)}>
                        </input>
                    </div>
                    <div className="form-group">
                        <label className="label-form">Contraseña:</label>
                        <input
                            className="input-form"
                            type="password"
                            placeholder="Ingrese su contraseña..."
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}>
                        </input>
                    </div>
                    <button type="submit" className="btn-success">Registrarme</button>
                </form>
            </div>
        </div>
    );
}

export default Register;
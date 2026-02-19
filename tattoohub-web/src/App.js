import './App.css';
import { BrowserRouter, Routes, Route, Link, useNavigate } from "react-router-dom";
import { useAuth } from './context/AuthContext';

import ArtistList from './pages/admin/ArtistsList';
import CreateArtist from './pages/admin/CreateArtist';
import EditArtist from './pages/admin/EditArtist';
import Login from './pages/auth/Login';
import Home from './pages/Home';
import Register from './pages/auth/Register';
import ArtistDashboard from './pages/artist/Dasboard';
import PrivateRoute from './components/AuthRoutes/PrivateRoute';

function App() {
  const {isAuthenticated, logout, user} = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/auth/login");
  }

  return (
        <div className="App">
          
          <div className='barra-navegacion'>
            <Link to='/home'>TATTOOHUB</Link>
            <div className='link-group'>
              {isAuthenticated ? (
                <>
                  <span className='saludo'>
                    Hola {user?.email}
                  </span>
                  <button className='logout-btn' onClick={handleLogout}>Logout</button>
                </>
              ) : (
                <>
                  <Link to='/auth/login'>Login</Link>
                  <Link to='/auth/register'>Registrarme</Link>          
                </>
              )}
              </div>
          </div>

          <header>

          </header>

          <main>
            <Routes>
              {/* Inicio */}
              <Route path='/home' element={<Home/>}/>
              
              {/* Administrador */}
              <Route path="/admin/artists" element={
                <PrivateRoute>
                    <ArtistList />
                </PrivateRoute>} 
              />
              <Route path="/admin/artists/create" element={<CreateArtist />} />
              <Route path="/admin/artists/edit/:id" element={<EditArtist />} />
              
              {/* Artista */}
              <Route path='/artist/dashboard' element={<ArtistDashboard/>}/>

              {/* Autenticación */}
              <Route path='/auth/login' element={<Login/>}/>
              <Route path='/auth/register' element={<Register/>}/>
            </Routes>
          </main>

        </div>
  );
}

export default App;

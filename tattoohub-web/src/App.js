import './App.css';
import { BrowserRouter, Routes, Route, Link } from "react-router-dom";

import ArtistList from './pages/admin/ArtistsList';
import CreateArtist from './pages/admin/CreateArtist';
import EditArtist from './pages/admin/EditArtist';
import Login from './pages/auth/Login';
import Home from './pages/Home';
import Register from './pages/auth/Register';
import ArtistDashboard from './pages/artist/Dasboard';

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        
        <div className='barra-navegacion'>
          <Link to='/home'>TATTOOHUB</Link>
          <div className='link-group'>
            <Link to='/auth/login'>Login</Link>
            <Link to='/auth/register'>Registrarme</Link>
          </div>
        </div>

        <header>

        </header>

        <main>
          <Routes>
            {/* Inicio */}
            <Route path='/home' element={<Home/>}/>
            
            {/* Administrador */}
            <Route path="/admin/artists" element={<ArtistList />} />
            <Route path="/admin/artists/create" element={<CreateArtist />} />
            <Route path="/admin/artists/edit/:id" element={<EditArtist />} />
            
            {/* Artista */}
            <Route path='/artist/dashboard' element={<ArtistDashboard/>}/>

            {/* Autenticación */}
            <Route path='/auth/login' element={<Login/>}/>
            <Route path='/auth/register' element={<Register/>}/>
            <Route path='/auth/register' element={<Register/>}/>
          </Routes>
        </main>

      </div>
    </BrowserRouter>
  );
}

export default App;

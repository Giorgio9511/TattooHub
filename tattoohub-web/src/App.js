import logo from './logo.svg';
import './App.css';
import ArtistList from './components/Artists/ArtistList';

function App() {
  return (
    <div className="App">
      <div className='barra-navegacion'>
        <a href='/'>TATTOOHUB</a>
      </div>
      <header>
        <h1>TattooHub</h1>
        <p>Plataforma para tatuadores</p>
      </header>
      <main>
        <ArtistList/>
      </main>
    </div>
  );
}

export default App;

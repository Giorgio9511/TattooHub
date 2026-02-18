import { useEffect, useState } from "react";
import { artistApi } from "../../api/artistApi";
import { Link } from "react-router-dom";

export default function ArtistList() {
    const [artists, setArtists] = useState([]);

    useEffect(() => {
        fetchArtists();
    }, []);

    const fetchArtists = async () => {
        try{
            const response = await artistApi.getAll();
            setArtists(response.data);
        }catch(error){
            console.error("Error cargando artistas", error);
        }
    };

    const handleDelete = async (id) => {
        try {
            await artistApi.deactivate(id);
            fetchArtists(); // recargar lista
        } catch (error) {
            console.error("Error desactivando artista", error);
        }
    };

    return (
        <div>
            <h2>Lista de artistas</h2>
            <Link to="/admin/artists/create">
                <button>Crear artista</button>
            </Link>
            <ul>
                {artists.map((artist) => (
                    <li key={artist.id}>
                        {artist.nombreCompleto} - {artist.nombreEstudio}
                        <Link to={`/admin/artist/edit/${artist.id}`}>
                            <button>Editar</button>
                        </Link>

                        <button onClick={() => handleDelete(artist.id)}>
                            Desactivar
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    )
}
import React, {useEffect, useState} from 'react';
import { artistApi } from '../../api/artistApi';

function ArtistList() {
    const [artists, setArtists] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        loadArtists();
    }, []);

    const loadArtists = async () => {
        try{
            setLoading(true);
            const response = await artistApi.getAll();
            setArtists(response.data);
        }catch(err){
            setError('Error al cargar artistas');
            console.error(err);
        }finally{
            setLoading(false);
        }
    };

    return (
        <div className='artist-list'>
            <h2>Artistas activos</h2>
            <div className='artists-grid'>
                {artists.map(artist => (
                    <div key={artist.id} className='artist-card'>
                        <h3>{artist.nombreCompleto}</h3>
                        <p>{artist.nombreEstudio}</p>
                        <p className='email'>{artist.email}</p>
                        {artist.bio && <p className='bio'>{artist.bio}</p>}
                    </div>
                ))}
            </div>
        </div>
    )
}

export default ArtistList;
import axios from "axios";

const API_BASE_URL = 'http://localhost:5081/api';

const api = axios.create({
    baseURL: API_BASE_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

export const artistApi = {
    getAll: () => api.get('/artist'),
    getById: (id) => api.get(`/artists/${id}`),
    create: (data) => api.post('/artists', data),
    update: (id, data) => api.put(`/artists/${id}`, data),
    deactivate: (id) => api.delete(`/artists/${id}`), 
};
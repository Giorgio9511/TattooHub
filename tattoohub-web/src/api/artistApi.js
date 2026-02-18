import api from "./axiosConfig"; 

export const artistApi = {
    getAll: () => api.get('/admin/artist'),
    getById: (id) => api.get(`/admin/artist/${id}`),
    create: (data) => api.post('/admin/artist', data),
    update: (id, data) => api.put(`/admin/artist/${id}`, data),
    deactivate: (id) => api.delete(`/admin/artist/${id}`), 
};
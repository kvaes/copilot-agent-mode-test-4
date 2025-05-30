import axios from 'axios'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:7071/api'

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
})

// Request interceptor for error handling
api.interceptors.request.use(
  (config) => {
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

// Response interceptor for error handling
api.interceptors.response.use(
  (response) => {
    return response
  },
  (error) => {
    console.error('API Error:', error)
    return Promise.reject(error)
  }
)

export const eventsApi = {
  async getEvents(filters = {}) {
    try {
      const params = new URLSearchParams()
      if (filters.date) {
        params.append('date', filters.date)
      }
      if (filters.location) {
        params.append('location', filters.location)
      }
      
      const response = await api.get(`/events?${params.toString()}`)
      return response.data
    } catch (error) {
      throw new Error(error.response?.data || 'Failed to fetch events')
    }
  },

  async getEvent(id) {
    try {
      const response = await api.get(`/events/${id}`)
      return response.data
    } catch (error) {
      throw new Error(error.response?.data || 'Failed to fetch event')
    }
  },

  async createEvent(eventData) {
    try {
      const response = await api.post('/events', eventData)
      return response.data
    } catch (error) {
      throw new Error(error.response?.data || 'Failed to create event')
    }
  },

  async updateEvent(id, eventData) {
    try {
      const response = await api.put(`/events/${id}`, eventData)
      return response.data
    } catch (error) {
      throw new Error(error.response?.data || 'Failed to update event')
    }
  },

  async deleteEvent(id) {
    try {
      await api.delete(`/events/${id}`)
      return true
    } catch (error) {
      throw new Error(error.response?.data || 'Failed to delete event')
    }
  },

  async registerForEvent(eventId, registrationData) {
    try {
      const response = await api.post(`/events/${eventId}/register`, registrationData)
      return response.data
    } catch (error) {
      throw new Error(error.response?.data || 'Failed to register for event')
    }
  }
}

export default api
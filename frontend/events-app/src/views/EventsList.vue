<template>
  <div class="container">
    <h2>Events</h2>
    
    <!-- Filters -->
    <div class="filters card">
      <h3>Filter Events</h3>
      <div class="filters">
        <div class="form-group">
          <label for="date">Date:</label>
          <input
            type="date"
            id="date"
            v-model="filters.date"
            @change="loadEvents"
          />
        </div>
        <div class="form-group">
          <label for="location">Location:</label>
          <input
            type="text"
            id="location"
            v-model="filters.location"
            placeholder="Search by location"
            @input="debounceSearch"
          />
        </div>
        <div class="form-group">
          <button @click="clearFilters" class="btn btn-secondary">Clear Filters</button>
        </div>
      </div>
    </div>

    <!-- Loading state -->
    <div v-if="loading" class="loading">
      Loading events...
    </div>

    <!-- Error state -->
    <div v-if="error" class="card">
      <div class="error">{{ error }}</div>
      <button @click="loadEvents" class="btn">Retry</button>
    </div>

    <!-- Events grid -->
    <div v-if="!loading && !error" class="event-grid">
      <div
        v-for="event in events"
        :key="event.id"
        class="card event-item"
        @click="goToEvent(event.id)"
      >
        <h3>{{ event.name }}</h3>
        <div class="event-date">
          {{ formatDate(event.date) }} at {{ formatTime(event.startTime) }}
        </div>
        <div class="event-location">📍 {{ event.location }}</div>
        <div class="registrations">
          {{ event.registrations?.length || 0 }} registered
        </div>
      </div>
    </div>

    <!-- No events state -->
    <div v-if="!loading && !error && events.length === 0" class="card">
      <p>No events found matching your criteria.</p>
    </div>
  </div>
</template>

<script>
import { eventsApi } from '../services/api.js'

export default {
  name: 'EventsList',
  data() {
    return {
      events: [],
      loading: false,
      error: null,
      filters: {
        date: '',
        location: ''
      },
      debounceTimer: null
    }
  },
  mounted() {
    this.loadEvents()
  },
  methods: {
    async loadEvents() {
      this.loading = true
      this.error = null
      
      try {
        this.events = await eventsApi.getEvents(this.filters)
      } catch (error) {
        this.error = error.message
      } finally {
        this.loading = false
      }
    },
    
    debounceSearch() {
      clearTimeout(this.debounceTimer)
      this.debounceTimer = setTimeout(() => {
        this.loadEvents()
      }, 500)
    },
    
    clearFilters() {
      this.filters.date = ''
      this.filters.location = ''
      this.loadEvents()
    },
    
    goToEvent(id) {
      this.$router.push(`/events/${id}`)
    },
    
    formatDate(dateString) {
      return new Date(dateString).toLocaleDateString()
    },
    
    formatTime(timeString) {
      // timeString format: "HH:mm:ss"
      return timeString.substring(0, 5)
    }
  }
}
</script>
<template>
  <div class="container">
    <!-- Loading state -->
    <div v-if="loading" class="loading">
      Loading event details...
    </div>

    <!-- Error state -->
    <div v-if="error" class="card">
      <div class="error">{{ error }}</div>
      <button @click="loadEvent" class="btn">Retry</button>
      <router-link to="/" class="btn btn-secondary">Back to Events</router-link>
    </div>

    <!-- Event details -->
    <div v-if="!loading && !error && event" class="card">
      <div style="display: flex; justify-content: space-between; align-items: start; margin-bottom: 20px;">
        <div>
          <h2>{{ event.name }}</h2>
          <div class="event-date">
            📅 {{ formatDate(event.date) }} at {{ formatTime(event.startTime) }}
          </div>
          <div class="event-location">📍 {{ event.location }}</div>
        </div>
        <router-link to="/" class="btn btn-secondary">← Back to Events</router-link>
      </div>

      <div class="registrations-count">
        <strong>{{ event.registrations?.length || 0 }} people registered</strong>
      </div>
    </div>

    <!-- Registration form -->
    <div v-if="!loading && !error && event" class="card">
      <h3>Register for this Event</h3>
      
      <div v-if="registrationSuccess" class="success">
        Successfully registered for the event!
      </div>
      
      <div v-if="registrationError" class="error">
        {{ registrationError }}
      </div>

      <form v-if="!registrationSuccess" @submit.prevent="submitRegistration">
        <div class="form-group">
          <label for="name">Name *</label>
          <input
            type="text"
            id="name"
            v-model="registration.name"
            required
            maxlength="100"
          />
        </div>

        <div class="form-group">
          <label for="email">Email *</label>
          <input
            type="email"
            id="email"
            v-model="registration.email"
            required
            maxlength="200"
          />
        </div>

        <div class="form-group">
          <label for="pronouns">Pronouns</label>
          <input
            type="text"
            id="pronouns"
            v-model="registration.pronouns"
            placeholder="e.g., they/them, she/her, he/him"
            maxlength="50"
          />
        </div>

        <div class="form-group">
          <label>
            <input
              type="checkbox"
              v-model="registration.optInForCommunication"
            />
            I would like to receive further communications about events
          </label>
        </div>

        <button type="submit" class="btn" :disabled="registrationLoading">
          {{ registrationLoading ? 'Registering...' : 'Register for Event' }}
        </button>
      </form>
    </div>

    <!-- Event not found -->
    <div v-if="!loading && !error && !event" class="card">
      <p>Event not found.</p>
      <router-link to="/" class="btn">Back to Events</router-link>
    </div>
  </div>
</template>

<script>
import { eventsApi } from '../services/api.js'

export default {
  name: 'EventDetails',
  props: {
    id: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      event: null,
      loading: false,
      error: null,
      registration: {
        name: '',
        email: '',
        pronouns: '',
        optInForCommunication: false
      },
      registrationLoading: false,
      registrationError: null,
      registrationSuccess: false
    }
  },
  mounted() {
    this.loadEvent()
  },
  methods: {
    async loadEvent() {
      this.loading = true
      this.error = null
      
      try {
        this.event = await eventsApi.getEvent(this.id)
      } catch (error) {
        this.error = error.message
      } finally {
        this.loading = false
      }
    },
    
    async submitRegistration() {
      this.registrationLoading = true
      this.registrationError = null
      
      try {
        await eventsApi.registerForEvent(this.id, this.registration)
        this.registrationSuccess = true
        // Reload event to get updated registration count
        await this.loadEvent()
      } catch (error) {
        this.registrationError = error.message
      } finally {
        this.registrationLoading = false
      }
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

<style scoped>
.registrations-count {
  margin: 20px 0;
  padding: 10px;
  background-color: #f8f9fa;
  border-radius: 4px;
}

.form-group label {
  display: flex;
  align-items: center;
  gap: 8px;
}

.form-group input[type="checkbox"] {
  width: auto;
}
</style>
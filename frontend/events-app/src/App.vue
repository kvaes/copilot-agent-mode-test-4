<template>
  <div id="app">
    <header class="header">
      <div class="container">
        <h1>Events App</h1>
        <nav>
          <router-link to="/" class="btn">Home</router-link>
        </nav>
      </div>
    </header>
    <main>
      <router-view />
    </main>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
.header {
  background-color: #007bff;
  color: white;
  padding: 20px 0;
  margin-bottom: 20px;
}

.header .container {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.header h1 {
  margin: 0;
}

.header nav a {
  color: white;
  text-decoration: none;
  margin-left: 10px;
}
</style>
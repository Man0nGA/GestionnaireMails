<template>
  <div class="user-profile">
    <div class="profile-header">
      <img :src="user.photo" alt="Photo de profil" class="profile-photo"/>
      <h1>{{ user.prenom }} {{ user.nom }}</h1>
    </div>
    <div class="profile-details">
      <p><strong>Email:</strong> {{ user.email }}</p>
      <p><strong>Téléphone:</strong> {{ user.telephone }}</p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'UserProfile',
  props: {
    user: {
      type: Object,
      required: true,
      default: () => ({
        nom: 'Gautier',
        prenom: 'Manon',
        email: 'test@example.com',
        telephone: '0123456789',
        photo: 'https://via.placeholder.com/150'
      })
    }
  }
}
</script>

<style scoped>
.user-profile {
  font-family: Arial, sans-serif;
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ddd;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.profile-header {
  text-align: center;
  margin-bottom: 20px;
}

.profile-photo {
  border-radius: 50%;
  width: 150px;
  height: 150px;
  object-fit: cover;
}

.profile-details {
  line-height: 1.6;
}

.profile-details p {
  margin: 10px 0;
}
</style>

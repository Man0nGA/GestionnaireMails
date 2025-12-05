<!-- UserProfile.vue -->
<template>
  <div v-if="user">
    <p>Bienvenue, {{ user.username }}</p>
  </div>
</template>

<script setup>
import { computed } from 'vue';
import { useStore } from 'vuex';

const store = useStore();
const user = computed(() => store.getters.user);
console.log(user);
</script>
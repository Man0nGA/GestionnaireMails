<script setup>
import { computed } from 'vue';
import { useStore } from 'vuex';

const store = useStore();
const emails = computed(() => store.getters.emails);
</script>

<template>
  <div v-if="emails && emails.length">
    <h2>📬 Your Emails</h2>
    <ul>
      <li v-for="email in emails" :key="email.id">
        <strong>{{ email.subject }}</strong><br />
        From: {{ email.from?.emailAddress?.name }}
      </li>
    </ul>
  </div>
</template>
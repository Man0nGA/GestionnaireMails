<script setup>
import { useStore } from 'vuex';
import BaseButton from './BaseButton.vue';
import { signIn, fetchEmails, fetchUserProfile } from '../lib/microsoftGraph.js';

const store = useStore();

const signInHandler = async () => {
  try {
    await signIn(); // Just triggers the popup and sets active account

    const userProfile = await fetchUserProfile(); // ✅ Fetch rich user profile
    store.commit('setUser', userProfile); // Now user has displayName, mail, etc.

    const mails = await fetchEmails();
    store.commit('setEmails', mails);
  } catch (error) {
    console.error("Erreur lors du sign-in ou fetch mails :", error);
  }
};
</script>

<template>
  <BaseButton @click="signInHandler">Connexion Microsoft</BaseButton>
</template>

<template>
  <BaseButton type="button" @click="$emit('back')" style="margin-bottom:1rem">Retour</BaseButton>
  <form class="mail-composer" @submit.prevent="sendMail">
    <input v-model="to" type="email" placeholder="A" required />
    <input v-model="subject" type="text" placeholder="Objet" required />
    <textarea v-model="body" placeholder="Message" required></textarea>
    <div class="center">
        <BaseButton type="submit">Envoyer</BaseButton>
    </div>
  </form>
</template>

<script setup>
import { ref } from 'vue'
import BaseButton from '../components/BaseButton.vue'
const to = ref('')
const subject = ref('')
const body = ref('')
const emit = defineEmits(['back', 'send-mail'])

function sendMail() {
  emit('send-mail', {
    id: Date.now(),
    sender: to.value,
    subject: subject.value,
    body: body.value
  })
  to.value = ''
  subject.value = ''
  body.value = ''
}
</script>

<style scoped>
.mail-composer {
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.05);
  padding: 1rem;
  margin: 1rem;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}
input, textarea {
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 0.5rem;
}
button {
  background: #2a3cff;
  color: #fff;
  border: none;
  border-radius: 4px;
  padding: 0.5rem 1rem;
  cursor: pointer;
  max-width: 100px;
}
button:hover {
  background: #1a2acc;
}
.center {
  display: flex;
  justify-content: center;
}
</style>

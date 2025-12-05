<script setup>
import { computed } from 'vue'
import { useStore } from 'vuex'

const store = useStore()
const mail = computed(() => store.getters.selectedMail)
</script>

<template>
  <div v-if="mail" class="mail-viewer">
    <h3>{{ mail.subject }}</h3>
    <p><strong>From:</strong> {{ mail.sender.emailAddress.name }}</p>
    <div class="mail-body" v-html="mail.body.content"></div>
  </div>
  <div v-else class="mail-viewer-empty">
    <em>Select a mail to view its content.</em>
  </div>
</template>

<style scoped>
.mail-viewer {
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.05);
  padding: 1rem;
  margin: 1rem;
  min-width: 300px;
}
.mail-viewer-empty {
  color: #888;
  padding: 2rem;
  text-align: center;
}
</style>

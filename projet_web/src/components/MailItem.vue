<template>
  <li class="mail-item" @click="onClick">
    <strong>{{ mail.sender }}</strong>: {{ mail.subject }}
  </li>
</template>

<script setup>
const props = defineProps({
  mail: {
    type: Object,
    required: true
  }
})

const emit = defineEmits(['select'])

function onClick() {
  emit('select', props.mail)
}
</script>

<style scoped>
.mail-item {
  cursor: pointer;
  padding: 0.5rem 0;
  border-bottom: 1px solid #000;
  transition: background 0.2s;
}
.mail-item:hover {
  background: #e6eaff;
}
</style>

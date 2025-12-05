<template>
  <button
    class="base-btn"
    :style="buttonStyle"
    :disabled="disabled"
    v-bind="$attrs"
  >
    <slot />
  </button>
</template>

<script setup>
import { computed } from 'vue'

const props = defineProps({
  disabled: Boolean,
  color: {
    type: String,
    default: 'primary',
    validator: (value) => ['primary', 'warn', 'danger'].includes(value)
  }
})

const colorPalette = {
  primary: { bg: '#42b983', hover: '#4ace93', focus: '#47d696' },
  warn:    { bg: '#ff5722', hover: '#ff7043', focus: '#ff8a65' },
  danger:  { bg: '#e53935', hover: '#ef5350', focus: '#e57373' }
}

const styleVars = computed(() => colorPalette[props.color])

const buttonStyle = computed(() => ({
  backgroundColor: props.disabled ? '#cfd8dc' : styleVars.value.bg,
  color: props.disabled ? '#777' : 'white',
  border: 'none',
  borderRadius: '5px',
  padding: '0.6em 1.2em',
  margin: '1em',
  fontWeight: 'bold',
  cursor: props.disabled ? 'not-allowed' : 'pointer',
  transition: 'all 0.3s ease'
}))
</script>

<style scoped>
.base-btn:hover:not(:disabled) {
  transform: scale(1.03);
}

.base-btn:focus:not(:disabled) {
  outline: 2px solid #2c7a59;
}
</style>

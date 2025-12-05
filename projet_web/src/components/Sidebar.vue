<template>
  <nav class="sidebar">
    <ul>
      <li v-for="item in items" :key="item" :class="{active: item === selected}" @click="$emit('update:selected', item)">
        {{ item }}
      </li>
    </ul>
  </nav>
</template>

<script setup>
const props = defineProps({
  selected: String
})
const items = ['Inbox', 'Sent', 'Drafts', 'Trash']
</script>

<style scoped>
.main-layout {
  display: flex;
  height: 100%;
  background: #f4f6fa;
}

.sidebar {
  flex-shrink: 0;
}

.content {
  flex: 1;
  display: flex;
  gap: 2rem;
  padding: 2rem;
  height: 100%;
}

.mail-section {
  min-width: 420px;
  display: flex;
  flex-direction: column;
}

ul {
  flex-grow: 1;
  overflow-y: auto;
  margin-top: 1rem;
  padding: 0;
  list-style: none;
}
</style>

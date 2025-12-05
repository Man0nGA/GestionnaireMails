<script setup>
import BaseHeader from './BaseHeader.vue'
import BaseFooter from './BaseFooter.vue'
</script>

<template>
  <div class="layout">
    <base-header />
    <main class="layout-content">
      <slot />
    </main>
    <base-footer />
  </div>
</template>


<style scoped>
.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  width: 100%;
}

.layout-content {
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow: auto;
}
</style>


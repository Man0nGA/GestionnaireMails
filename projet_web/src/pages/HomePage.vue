<script setup>
import {ref, reactive, computed} from 'vue'
import BaseButton from '../components/BaseButton.vue'
import MailViewer from '../components/MailViewer.vue'
import MailItem from '../components/MailItem.vue'

import {useStore} from "vuex";
import {onMounted} from "vue"
import {signIn} from "@/lib/microsoftGraph.js";
const store = useStore();
const mails = computed(() => store.getters.emails);

const clickCount = ref(0)

function handleButtonClick() {
  clickCount.value++
  return new Promise((resolve) => {
    setTimeout(resolve, clickCount.value * 1000)
  })
}

const state = reactive({
  selectedCategory: 'Inbox',
  selectedMail: null
})

function handleSelectMail(mail) {
  store.dispatch('selectMail', mail)
}

function handleCategoryChange(category) {
  state.selectedCategory = category
  state.selectedMail = null
}

const tab = ref('recu')

onMounted(async () => {
  try {
    // 1. Vérifier si l'utilisateur est déjà dans le store (connecté)
    if (!store.getters.user) {
      // 2. Sinon, lancer la connexion
      console.log("🔐 Utilisateur non connecté, connexion en cours...");
      await signIn();
      console.log("✅ Utilisateur connecté");
    }

    // 3. Après connexion, récupérer les emails
    console.log('📩 fetchEmails lancé...');
    await store.dispatch('fetchEmails');
    console.log('✅ emails après chargement :', store.getters.emails);
    console.log('🔐 accessToken =', store.getters.user?.accessToken);

  } catch (error) {
    console.error("Erreur lors de la connexion ou du fetch :", error);
  }
});
</script>

<template>
  <div class="main-layout">
    <Sidebar :selected="state.selectedCategory" @update:selected="handleCategoryChange" />
    <div class="content">
      <div class="mail-section">
        <BaseButton @click="$emit('create-message')">Créer un message</BaseButton>

        <div class="tabs">
          <button :class="{active: tab === 'recu'}" @click="tab = 'recu'">Reçus</button>
          <button :class="{active: tab === 'envoye'}" @click="tab = 'envoye'">Envoyés</button>
        </div>

        <ul v-if="tab === 'recu'">
          <li v-for="mail in mails.Inbox" :key="mail.id" @click="handleSelectMail(mail)" style="cursor: pointer">
            De: {{ mail.sender.emailAddress.name}} — Sujet: {{ mail.subject }}
            <template v-if="mail.ccRecipients && mail.ccRecipients.length">
              — CC : {{ mail.ccRecipients}}
            </template>
            <template v-if="mail.bccRecipients && mail.bccRecipients.length">
              — CCI : {{ mail.bccRecipients}}
            </template>
          </li>
        </ul>
        <ul v-else>
          <li v-for="mail in mails.SentItems" :key="mail.id" @click="handleSelectMail(mail)" style="cursor: pointer">
            Destinataire : {{mail.toRecipients[0].emailAddress.name}}
          </li>
        </ul>
      </div>
      <MailViewer/>
    </div>
  </div>
</template>


<style scoped>
.main-layout {
  display: flex;
  min-height: 100vh;
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
}
.mail-section {
  min-width: 420px;
}
.tabs {
  display: flex;
  gap: 1rem;
  margin-bottom: 1rem;
}
.tabs button {
  background: #e6eaff;
  border: none;
  border-radius: 4px 4px 0 0;
  padding: 0.5rem 1.5rem;
  cursor: pointer;
  font-weight: bold;
  color: #222;
  transition: background 0.2s;
}
.tabs button.active {
  background: #e6eaff;
  color: #222;
}
</style>

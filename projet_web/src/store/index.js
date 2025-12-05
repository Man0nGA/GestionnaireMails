import { createStore } from 'vuex';

export default createStore({
    state() {
        return {
            user: null,
            emails: {
                Inbox: [],
                SentItems: []
            },
            loadingEmails: false,
            emailError: null,
            selectedMail: null,  // <== ajouté ici
        }
    },

    mutations: {
        setUser(state, user) {
            state.user = user;
        },
        setEmails(state, emails) {
            state.emails = emails;
        },
        setSelectedMail(state, mail) {   // <== mutation pour sélectionner un mail
            state.selectedMail = mail;
        }
    },

    actions: {
        async fetchEmails({ commit, state }) {
            if (!state.user || !state.user.accessToken) {
                console.warn('Pas de token d’accès');
                return;
            }
            const token = state.user.accessToken;

            async function fetchFolder(folder) {
                const response = await fetch(`https://graph.microsoft.com/v1.0/me/mailFolders/${folder}/messages?$top=20`, {
                    headers: { Authorization: `Bearer ${token}` }
                });
                if (!response.ok) {
                    throw new Error(`Erreur lors de la récupération des mails du dossier ${folder}`);
                }
                const data = await response.json();
                return data.value;
            }

            try {
                const [inboxMails, sentMails] = await Promise.all([
                    fetchFolder('Inbox'),
                    fetchFolder('SentItems')
                ]);

                commit('setEmails', {
                    Inbox: inboxMails,
                    SentItems: sentMails
                });
            } catch (e) {
                console.error(e);
            }
        },

        selectMail({ commit }, mail) {    // <== action pour setter la sélection
            commit('setSelectedMail', mail);
        }
    },

    getters: {
        user: (state) => state.user,
        emails: (state) => state.emails,
        loadingEmails: (state) => state.loadingEmails,
        emailError: (state) => state.emailError,
        selectedMail: (state) => state.selectedMail   // <== getter sélection mail
    }
});
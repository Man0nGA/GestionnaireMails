import * as msal from '@azure/msal-browser'
import store from '../store'  // <-- importe ton store Vuex

const msalInstance = new msal.PublicClientApplication({
    auth: {
        clientId: import.meta.env.VITE_APP_OAUTH_CLIENT_ID,
        redirectUri: window.location.origin
    },
    cache: {
        cacheLocation: "sessionStorage"
    }
});

let msalInitialized = false;

export async function initializeMsal() {
    if (!msalInitialized) {
        if (msalInstance.initialize) {
            await msalInstance.initialize();
        }
        msalInitialized = true;
    }
}

const loginRequest = {
    scopes: ["User.Read", "Mail.ReadWrite"]
};

export async function signIn() {
    await initializeMsal();

    try {
        const authResult = await msalInstance.loginPopup(loginRequest);
        msalInstance.setActiveAccount(authResult.account);

        const tokenResponse = await msalInstance.acquireTokenSilent({
            scopes: loginRequest.scopes,
            account: authResult.account
        });

        store.commit('setUser', {
            ...authResult.account,
            accessToken: tokenResponse.accessToken
        });

        return authResult.account;
    } catch (error) {
        console.error("Erreur login:", error);
        throw error;
    }
}

// IMPORTANT : Modification ici pour gérer le compte actif
export async function acquireToken() {
    await initializeMsal();

    let account = msalInstance.getActiveAccount();

    // Si pas de compte actif, on lance le login popup pour connecter l'utilisateur
    if (!account) {
        try {
            const authResult = await msalInstance.loginPopup(loginRequest);
            msalInstance.setActiveAccount(authResult.account);
            account = authResult.account;

            store.commit('setUser', {
                ...authResult.account,
                accessToken: authResult.accessToken
            });
        } catch (loginError) {
            console.error("Erreur login dans acquireToken:", loginError);
            throw loginError;
        }
    }

    try {
        const tokenResponse = await msalInstance.acquireTokenSilent({
            scopes: loginRequest.scopes,
            account: account
        });

        return tokenResponse.accessToken;
    } catch (error) {
        console.warn("acquireTokenSilent a échoué, essai loginPopup", error);

        const tokenResponse = await msalInstance.acquireTokenPopup({
            scopes: loginRequest.scopes
        });

        return tokenResponse.accessToken;
    }
}

// Ajout d'une vérification avant fetch des mails
export async function fetchEmails() {
    const account = msalInstance.getActiveAccount();
    if (!account) {
        throw new Error("Utilisateur non connecté. Veuillez vous connecter avant de récupérer les mails.");
    }

    const accessToken = await acquireToken();

    const response = await fetch("https://graph.microsoft.com/v1.0/me/messages", {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    if (!response.ok) {
        throw new Error("Erreur récupération mails : " + response.statusText);
    }

    const data = await response.json();
    return data.value;
}

export async function fetchUserProfile() {
    const account = msalInstance.getActiveAccount();
    if (!account) {
        throw new Error("Utilisateur non connecté. Veuillez vous connecter avant de récupérer le profil.");
    }

    const accessToken = await acquireToken();

    const response = await fetch("https://graph.microsoft.com/v1.0/me", {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    if (!response.ok) {
        throw new Error("Erreur récupération profil utilisateur : " + response.statusText);
    }

    return await response.json();
}

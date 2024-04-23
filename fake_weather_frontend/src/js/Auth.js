import {defineStore} from "pinia";
import router from "@/router";

// If token remaining lifetime is less than this value we need to refresh token
const remainingTokenLifetimeToRefresh = 3600

const apiBaseUrl = process.env.VUE_APP_API_URL

// Non-persistent storage (for Remember Me mode disabled)
const useAuthNonPersistentStore = defineStore("authNonPersistent", {
    state: () =>
        ({
            credentials: null,
            token: null
        }),

    getters:
        {
            getCredentials: (state) => state.credentials,
            getToken: (state) => state.token
        },

    actions:
        {
            // Save credentials
            storeCredentials(credentials)
            {
                this.credentials = credentials
            },

            // Clear credentials
            clearCredentials()
            {
                this.credentials = null
            },

            // Store token
            storeToken(token)
            {
                this.token = token
            },

            // Clear token
            clearToken()
            {
                this.token = null
            }
        },

    persist: {
        storage: sessionStorage
    },
});

// Persistent storage (for Remember Me mode enabled)
const useAuthPersistentStore = defineStore("authPersistent", {
    state: () =>
        ({
            credentials: null,
        }),

    getters:
        {
            getCredentials: (state) => state.credentials
        },

    actions:
        {
            // Save credentials
            storeCredentials(credentials)
            {
                this.credentials = credentials
            },

            // Clear credentials
            clearCredentials()
            {
                this.credentials = null
            }
        },

    persist: true
});

// Save token to storage
async function AuthStoreToken(token, expiration)
{
    useAuthNonPersistentStore().storeToken({ token: token, expiration: expiration })
}

// Save credentials
async function AuthStoreCredentials(login, password, isSaveToPersistentStorage)
{
    const store = isSaveToPersistentStorage ? useAuthPersistentStore() : useAuthNonPersistentStore()
    store.storeCredentials({ login: login, password: password })
}

async function AuthLogCreatureIn(login, password, isRememberMe)
{
    const token = await GetTokenByCredentials(login, password)

    if (token === null)
    {
        alert("Не удалось войти")

        return
    }

    await AuthStoreToken(token.token, token.expiration)
    await AuthStoreCredentials(login, password, isRememberMe)

    await router.push("/")
}

async function GetTokenByCredentials(login, password)
{
    const response = await fetch(apiBaseUrl + "/api/Users/Login", {
        method: "POST",
        body: JSON.stringify({
            "loginData": {
                "login": login,
                "password": password
            }
        }),
        headers: {"Content-Type": "application/json"}
    })

    if (response.status !== 200)
    {
        return null
    }

    const logInResult = (await response.json()).loginResult

    if (!logInResult.isSuccessful)
    {
        return null
    }

    return { token: logInResult.token, expiration: logInResult.expiration };
}

// Get authentication token
async function AuthGetToken()
{
    return useAuthNonPersistentStore().getToken
}

async function AuthRefreshToken()
{
    const credentials = await AuthGetCredentials()
    if (credentials === null)
    {
        return // We aren't logged in
    }

    const token = await AuthGetToken()

    /* Checking if token exists and not going to expire */
    if (token === null || (Date.parse(token.expiration) - Date.now() < remainingTokenLifetimeToRefresh))
    {
        /* If there is no token, we must get it */
        const tokenFromBackend = await GetTokenByCredentials(credentials.login, credentials.password)

        if (tokenFromBackend === null)
        {
            return
        }

        await AuthStoreToken(tokenFromBackend.token, tokenFromBackend.expiration)
    }
}

// Get credentials
async function AuthGetCredentials()
{
    const persistentCredentials = useAuthPersistentStore().getCredentials

    if (persistentCredentials !== null)
    {
        return persistentCredentials
    }

    return useAuthNonPersistentStore().getCredentials
}

// Get creature's login
async function AuthGetCreatureLogin()
{
    return (await AuthGetCredentials()).login
}

// Checks if creature logged in
async function AuthIsCreatureLoggedIn()
{
    await AuthRefreshToken()

    const token = await AuthGetToken()

    return token !== null // If we have token, then believe that creature is logged in
}

// Clear stored credentials
async function AuthClearCredentials()
{
    useAuthPersistentStore().clearCredentials()
    useAuthNonPersistentStore().clearCredentials()
}

async function AuthLogCreatureOut()
{
    await AuthClearCredentials()

    useAuthNonPersistentStore().clearToken()

    await router.push("/")
    router.go(0)

}

export
{
    AuthLogCreatureIn,
    AuthGetToken,
    AuthRefreshToken,
    AuthIsCreatureLoggedIn,
    AuthGetCreatureLogin,
    AuthLogCreatureOut
}
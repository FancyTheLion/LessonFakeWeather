import {defineStore} from "pinia";
import router from "@/router";

// If token remaining lifetime is less than this value we need to refresh token
const remainingTokenLifetimeToRefresh = 300

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

// Save token to storage
async function AuthStoreToken(token, expiration)
{
    useAuthNonPersistentStore().storeToken({ token: token, expiration: expiration })
}

// Save credentials
async function AuthStoreCredentials(login, password)
{
    useAuthNonPersistentStore().storeCredentials({ login: login, password: password })
}

async function AuthLogCreatureIn(login, password) {
    const token = await GetTokenByCredentials(login, password)

    if (token === null)
    {
        alert("Не удалось войти")

        return
    }

    await AuthStoreToken(token.token, token.expiration)
    await AuthStoreCredentials(login, password)

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

    /* Checking for token validity */
    if (token === null || (Date.parse(token.expiration) - Date.now() < remainingTokenLifetimeToRefresh))
    {
        /* If there is no token, we must get it */
        const tokenFromBackend = await GetTokenByCredentials(credentials.login, credentials.password)

        if (tokenFromBackend === null)
        {
            return
        }

        await AuthStoreToken(tokenFromBackend.token, tokenFromBackend.expiration)
        return
    }
}

// Get credentials
async function AuthGetCredentials()
{
    return useAuthNonPersistentStore().getCredentials
}

export
{
    AuthLogCreatureIn,
    AuthGetToken,
    AuthRefreshToken
}
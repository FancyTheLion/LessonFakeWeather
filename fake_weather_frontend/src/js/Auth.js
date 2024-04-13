import {defineStore} from "pinia";
import router from "@/router";

const apiBaseUrl = process.env.VUE_APP_API_URL

// Non-persistent storage (for Remember Me mode disabled)
const useAuthNonPersistentStore = defineStore("authNonPersistent", {
    state: () =>
        ({
            token: null
        }),

    getters:
        {
            getToken: (state) => state.token
        },

    actions:
        {
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

// Store token to storage
async function AuthStoreToken(token, expiration)
{
    useAuthNonPersistentStore().storeToken({ token: token, expiration: expiration })
}

async function AuthLogCreatureIn(login, password)
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

    if (response.status !== 200) {
        alert("Неожиданная ошибка при попытке войти на сайт!")
        return
    }

    const logInResult = (await response.json()).loginResult

    if (!logInResult.isSuccessful)
    {
        alert("Некорректный логин или пароль!");
        return
    }

    await AuthStoreToken(logInResult.token, logInResult.expiration)

    await router.push("/")
}

export
{
    AuthLogCreatureIn
}
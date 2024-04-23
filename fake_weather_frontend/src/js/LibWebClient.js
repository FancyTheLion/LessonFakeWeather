import {AuthGetToken, AuthRefreshToken} from "@/js/Auth";

const apiBaseUrl = process.env.VUE_APP_API_URL

// Make GET request
async function WebClientSendGetRequest
(
    relativeUrl
)
{
    await AuthRefreshToken()

    const authToken = await AuthGetToken()

    let headers = {}
    if (authToken !== null)
    {
        headers = { 'Authorization': 'Bearer ' + authToken.token }
    }

    const response = await fetch(apiBaseUrl + relativeUrl, {
        method: 'GET',
        headers: headers
    })

    return response;
}

// Make POST request
async function WebClientSendPostRequest
(
    relativeUrl,
    request
)
{
    await AuthRefreshToken()

    const authToken = await AuthGetToken()

    let headers = { 'Content-Type': 'application/json' }
    if (authToken !== null)
    {
        headers.Authorization = 'Bearer ' + authToken.token
    }

    const response = await fetch(apiBaseUrl + relativeUrl, {
        method: 'POST',
        body: JSON.stringify(request),
        headers: headers
    })

    return response
}

// Post a form (mostly probably with a file)
async function WebClientPostForm
(
    relativeUrl,
    request
)
{
    await AuthRefreshToken()

    const authToken = await AuthGetToken()

    let headers = { }
    if (authToken !== null)
    {
        headers.Authorization = 'Bearer ' + authToken.token
    }

    const response = await fetch(apiBaseUrl + relativeUrl, {
        method: 'POST',
        body: request,
        headers: headers
    })

    return response
}

export
{
    WebClientSendGetRequest,
    WebClientSendPostRequest,
    WebClientPostForm
}


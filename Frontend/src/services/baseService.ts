import ClientError from "../errors/ClientError";
import authorizationHandler from "../utilities/authorizationHandler";
import {configData} from "../../config";

const baseFetch = async (url: string, method?: string, body?: string) => {
    const expirationTime = authorizationHandler.getToken().expirationTime;
    if (expirationTime && new Date(expirationTime).getTime() - new Date().getTime() < configData.twoMinutes) {
        const token = await executeRequest(configData.authorizationUrl +configData.tokenUrl + `?userId=${authorizationHandler.getUserId()}`);
        authorizationHandler.saveAuthorizationInfo(token);
    }

    return executeRequest(url, method, body);
}

const executeRequest = async (url: string, method?: string, body?: string) => {
    const response = await fetch(configData.baseUrl + url, {
        method: method ? method : 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': JSON.stringify(authorizationHandler.getToken()),
            'GoogleAuthorization': authorizationHandler.getGoogleToken()
        },
        body: body
    });

    if (response.status.toString()[0] === '4') {
        throw new ClientError();
    }

    if (response.status.toString()[0] !== '2') {
        throw new Error();
    }

    const text = await response.text();

    if(text)
        return JSON.parse(text);
}

export default {
    baseFetch
}
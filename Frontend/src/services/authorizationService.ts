import baseService from "./baseService";
import {configData} from "../../config";

const authorizationUrl = configData.authorizationUrl;

const signIn = async (email: string, password: string) => {
    return baseService.baseFetch(authorizationUrl + configData.signInUrl + `?email=${email}&password=${password}`);
}

const signInWithGoogle = async (email: string) => {
    return baseService.baseFetch(authorizationUrl+configData.signInWithGoogleUrl+ `?email=${email}`);
}

const createUser = async (user: { name: string, phone: string, email: string, password: string }) => {
    return baseService.baseFetch(authorizationUrl+configData.signUpUrl, 'POST', JSON.stringify(user));
}

export default {
    signIn,
    signInWithGoogle,
    createUser
}
import baseService from "./baseService";
import authorizationHandler from "../utilities/authorizationHandler";
import {configData} from "../../config";

const usersUrl=configData.usersUrl;

const getUser = async () => {
    return baseService.baseFetch(usersUrl + authorizationHandler.getUserId());
}

const getUserReservations = async () => {
    return baseService.baseFetch(usersUrl + authorizationHandler.getUserId() +configData.userReservationsUrl );
}

const updateUser = async (userInfo: { name?: string, phone?: string, oldPassword?: string, password?: string }) => {
    return baseService.baseFetch(usersUrl + authorizationHandler.getUserId(), 'PATCH', JSON.stringify(userInfo));
}

const deleteUser = async () => {
    return baseService.baseFetch(usersUrl + authorizationHandler.getUserId(), 'DELETE');
}

export default {
    getUser,
    getUserReservations,
    updateUser,
    deleteUser
}
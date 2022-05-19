import toastHandler from "./toastHandler";
import {configData} from "../../config";

const validateName = (name: string): boolean => {
    if (!name) {
        toastHandler.info('Name cannot be empty');
        return false;
    }
    return true;
}

const validatePhone = (phone: string): boolean => {
    if (!configData.phonePattern.test(phone)) {
        toastHandler.info('Invalid phone number');
        return false;
    }
    return true;
}

const validateEmail = (email: string): boolean => {
    if (!configData.emailPattern.test(email)) {
        toastHandler.info('Invalid email');
        return false;
    }
    return true;
}

const validatePassword = (password: string): boolean => {
    if (password.length < 8) {
        toastHandler.info('Password must be at least 8 characters long');
        return false;
    }
    return true;
}

const validateDates = (startDate: string, endDate: string): boolean => {
    if (startDate && endDate && endDate < startDate) {
        toastHandler.info("End date must be after start date");
        return false;
    }
    return true;
}

const validatePriceRange = (from: number, to: number): boolean => {
    if (to && from && to < from) {
        toastHandler.info('Max price must be larger than min price');
        return false;
    }
    return true;
}

const validateRoomRange = (from: number, to: number): boolean => {
    if (to && from && to < from) {
        toastHandler.info('Max number of rooms must be larger than min number of rooms');
        return false;
    }
    return true;
}

export default {
    validateName,
    validatePhone,
    validateEmail,
    validatePassword,
    validateDates,
    validatePriceRange,
    validateRoomRange
}
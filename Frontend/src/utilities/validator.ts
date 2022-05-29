import toastHandler from "./toastHandler";
import {configData} from "../../config";

const validateName = (name: string): boolean => {
    if (!name) {
        toastHandler.info('Имя не может быть пустым');
        return false;
    }
    return true;
}

const validatePhone = (phone: string): boolean => {
    if (!configData.phonePattern.test(phone)) {
        toastHandler.info('Некорректный телефон');
        return false;
    }
    return true;
}

const validateEmail = (email: string): boolean => {
    if (!configData.emailPattern.test(email)) {
        toastHandler.info('Некорректный email');
        return false;
    }
    return true;
}

const validatePassword = (password: string): boolean => {
    if (password.length < 8) {
        toastHandler.info('Минимальная допустимая длина пароля - 8 символов');
        return false;
    }
    return true;
}

const validateDates = (startDate: string, endDate: string): boolean => {
    if (startDate && endDate && endDate < startDate) {
        toastHandler.info("Дата начала не может быть позднее даты окончания");
        return false;
    }
    return true;
}

const validatePriceRange = (from: number, to: number): boolean => {
    if (to && from && to < from) {
        toastHandler.info('Минимальная цена за день не может быть больше максимальной цены за день');
        return false;
    }
    return true;
}

const validateRoomRange = (from: number, to: number): boolean => {
    if (to && from && to < from) {
        toastHandler.info('Минимальное количество комнат не может быть больше максимального количества комант');
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
import baseService from "./baseService";
import {configData} from "../../config";

const createReservation = async (reservation: { userId: number, villaId: number, startDate: string, endDate: string }) => {
    return baseService.baseFetch(configData.reservationsUrl, 'POST', JSON.stringify(reservation));
}

const deleteReservation = async (reservationId: number) => {
    return baseService.baseFetch(configData.reservationsUrl + reservationId, 'DELETE');
}

export default {
    createReservation,
    deleteReservation
}
import React from "react";
import toastHandler from "./toastHandler";
import DatesSetting from "../component/shared/input/DatesSetting";

const setDates = (reservedDates?: {startDate:string, endDate: string}[]) => {
    toastHandler.info(
        <DatesSetting onCancel={()=>toastHandler.dismiss()} reservedDates={reservedDates}/>, Infinity);
}

export default {
    setDates
}
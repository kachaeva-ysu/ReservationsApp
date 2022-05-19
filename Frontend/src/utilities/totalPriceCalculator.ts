import {configData} from "../../config";

const getTotalPrice = (startDateString: string, endDateString: string, priceForDay: number) => {
    const startDate = new Date(startDateString).getTime();
    const endDate = new Date(endDateString).getTime();
    const numberOfDays = ((endDate - startDate) / configData.millisecondsInDay + 1);
    return numberOfDays * priceForDay + '$';
}

export default {
    getTotalPrice
}
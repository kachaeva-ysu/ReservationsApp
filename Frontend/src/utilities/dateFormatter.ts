const getFormattedDate = (format: string, date: Date): string => {
    if (format.toLowerCase() === 'yyyy-mm-dd') {
        let dd = date.getDate().toString();
        if (dd.length === 1)
            dd = '0' + dd;
        let mm = (date.getMonth() + 1).toString();
        if (mm.length === 1)
            mm = '0' + mm;
        return date.getFullYear() + '-' + mm + '-' + dd;
    }
    throw new Error('Unsupported date format');
}


const getDateWithoutTime = (date: string): string => {
    return date.slice(0, 10);
}

export default {
    getFormattedDate,
    getDateWithoutTime
}
import React from "react";
import DatePicker from 'react-datepicker';
import s from "./DateInput.css";
import dateFormatter from "../../../utilities/dateFormatter";

type DateInputProps = {
    id: string,
    value: string,
    onDateChange: (newDate: string) => void,
    isError: boolean,
    filterDate?: (date: Date) => boolean
}

const DateInput = ({id, value, onDateChange, isError, filterDate}: DateInputProps) => {
    return (
        <DatePicker className={isError ? s.dateInputWithError : s.dateInput}
                    id={id} selected={value ? new Date(value) : null}
                    onChange={(e: Date) => onDateChange(dateFormatter.getFormattedDate('yyyy-mm-dd', e))}
                    dateFormat='yyyy-MM-dd' minDate={new Date()}
                    filterDate={filterDate} autoComplete='off'
        />
    )
}

export default DateInput;
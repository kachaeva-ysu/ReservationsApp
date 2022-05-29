import React, {useContext, useState} from "react";
import DatesSelection from "./DatesSelection";
import Button from "../clickable/Button";
import validator from "../../../utilities/validator";
import Title from "../output/Title";
import dateFormatter from "../../../utilities/dateFormatter";
import {AppContext} from "../../../contexts/AppContext";

type DateSettingProps = {
    onCancel: () => void,
    reservedDates?: {startDate: string, endDate: string}[]
}

const DatesSetting = ({onCancel, reservedDates}: DateSettingProps) => {
    const [errors, setErrors] = useState({isStartDateError: false, isEndDateError: false});
    const {value: {startDate, endDate}, setValue} = useContext(AppContext);
    const [dates, setDates] = useState(
        {
            startDate: startDate,
            endDate: endDate
        });

    const handleSetStartDate = (newDate: string) => {
        setDates((prevState) => ({
            ...prevState,
            ...{startDate: newDate}
        }));
    }

    const handleSetEndDate = (newDate: string) => {
        setDates((prevState) => ({
            ...prevState,
            ...{endDate: newDate}
        }));
    }

    const handleSetErrors = (newProps: { isStartDateError?: boolean, isEndDateError?: boolean }) => {
        setErrors((prevState) => ({
            ...prevState,
            ...newProps
        }));
    }

    const handleCancel = () => {
        onCancel();
    }

    const handleConfirm = () => {
        if (validateDates()) {
            setValue(dates);
            onCancel();
        }
    }

    const validateDates = () => {
        if (!validator.validateDates(dates.startDate, dates.endDate)) {
            handleSetErrors({isStartDateError: true});
            handleSetErrors({isEndDateError: true});
            return false;
        } else {
            handleSetErrors({isStartDateError: false});
            handleSetErrors({isEndDateError: false});
        }

        return true;
    }

    const filterDate = (unformattedDate: Date) => {
        const date = dateFormatter.getFormattedDate('yyyy-mm-dd', unformattedDate);
        return !reservedDates ? true : reservedDates.every(dates =>
            date < dateFormatter.getDateWithoutTime(dates.startDate) ||
            date > dateFormatter.getDateWithoutTime(dates.endDate));
    }

    return (
        <div>
            <Title value='Выберите даты'/>
            <DatesSelection startDate={dates.startDate} endDate={dates.endDate}
                            setStartDate={handleSetStartDate} setEndDate={handleSetEndDate}
                            isStartDateError={errors.isStartDateError}
                            isEndDateError={errors.isEndDateError} filterDate={filterDate}
            />
            <Button value='OK' onClick={handleConfirm} isDark={true}/>
            <Button value='Отмена' onClick={handleCancel} isDark={true}/>
        </div>
    );
}

export default DatesSetting;
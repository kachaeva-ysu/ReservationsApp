import React from "react";
import Label from "../output/Label";
import DateInput from "./DateInput";

type DateSelectionProps = {
    startDate: string,
    endDate: string,
    setStartDate: (newDate: string) => void,
    setEndDate: (newDate: string) => void,
    isStartDateError: boolean,
    isEndDateError: boolean,
    filterDate?: (date: Date) => boolean
}

const DatesSelection = ({
                            startDate,
                            endDate,
                            setStartDate,
                            setEndDate,
                            isStartDateError,
                            isEndDateError,
                            filterDate
                        }: DateSelectionProps) => {
    return (
        <>
            <div>
                <Label htmlFor='startDate' value='Start date:'/>
                <DateInput id='startDate' value={startDate} onDateChange={setStartDate}
                           isError={isStartDateError} filterDate={filterDate}
                />
            </div>
            <div>
                <Label htmlFor='endDate' value='End date:'/>
                <DateInput id='endDate' value={endDate} onDateChange={setEndDate}
                           isError={isEndDateError} filterDate={filterDate}
                />
            </div>
        </>
    );
}

export default DatesSelection;
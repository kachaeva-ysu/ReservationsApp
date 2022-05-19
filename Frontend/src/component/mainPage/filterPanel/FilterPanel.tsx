import React, {useContext, useState} from "react";
import DatesSelection from "../../shared/input/DatesSelection";
import PriceRangeSelection from "./PriceRangeSelection";
import NumberOfRoomsRangeSelection from "./NumberOfRoomsRangeSelection";
import PoolSelection from "./PoolSelection";
import Button from "../../shared/clickable/Button";
import validator from "../../../utilities/validator";
import {AppContext} from "../../../contexts/AppContext";
import s from "../../shared/container/FlexDisplayed.css";

const FilterPanel = () => {
    const {value: {startDate, endDate, priceFrom, priceTo, roomFrom, roomTo, pool}, setValue} = useContext(AppContext);
    const [errors, setErrors] = useState(
        {isStartDateError: false, isEndDateError: false, isPriceRangeError: false, isRoomRangeError: false});

    const handleSetErrors = (newErrors: {
        isStartDateError?: boolean,
        isEndDateError?: boolean, isPriceRangeError?: boolean, isRoomRangeError?: boolean
    }) => {
        setErrors((prevState) => ({
            ...prevState,
            ...newErrors
        }));
    }

    const handleSetStartDate = (newDate: string) => {
        setValue({startDate: newDate});
    }
    const handleSetEndDate = (newDate: string) => {
        setValue({endDate: newDate});
    }
    const handleSetPriceFrom = (newPrice: number) => {
        setValue({priceFrom: newPrice});
    }
    const handleSetPriceTo = (newPrice: number) => {
        setValue({priceTo: newPrice});
    }
    const handleSetRoomFrom = (newRoom: number) => {
        setValue({roomFrom: newRoom});
    }
    const handleSetRoomTo = (newRoom: number) => {
        setValue({roomTo: newRoom});
    }
    const handleSetPool = (newPool: boolean) => {
        setValue({pool: newPool});
    }
    const handleClick = () => {
        const isInputValid = validateInput();
        if (isInputValid) {
            setValue({isFilterPanelActive: false});
        }
    }
    const validateInput = () => {
        if (!validateDates()) {
            return false;
        }
        return validateRanges();
    }
    const validateDates = () => {
        if (!validator.validateDates(startDate, endDate)) {
            handleSetErrors({isStartDateError: true});
            handleSetErrors({isEndDateError: true});
            return false;
        } else {
            handleSetErrors({isStartDateError: false});
            handleSetErrors({isEndDateError: false});
        }

        return true;
    }
    const validateRanges = () => {
        if (!validator.validatePriceRange(priceFrom, priceTo)) {
            handleSetErrors({isPriceRangeError: true});
            return false;
        } else {
            handleSetErrors({isPriceRangeError: false});
        }

        if (!validator.validateRoomRange(roomFrom, roomTo)) {
            handleSetErrors({isRoomRangeError: true});
            return false;
        } else {
            handleSetErrors({isRoomRangeError: false});
        }

        return true;
    }

    return (
        <div className={s.flexDisplayed}>
            <DatesSelection startDate={startDate} endDate={endDate}
                            setStartDate={handleSetStartDate} setEndDate={handleSetEndDate}
                            isStartDateError={errors.isStartDateError}
                            isEndDateError={errors.isEndDateError}
            />
            <PriceRangeSelection priceFrom={priceFrom} priceTo={priceTo}
                                 setPriceFrom={handleSetPriceFrom} setPriceTo={handleSetPriceTo}
                                 isError={errors.isPriceRangeError}/>
            <NumberOfRoomsRangeSelection roomFrom={roomFrom} roomTo={roomTo}
                                         setRoomFrom={handleSetRoomFrom} setRoomTo={handleSetRoomTo}
                                         isError={errors.isRoomRangeError}/>
            <PoolSelection pool={pool} setValue={handleSetPool}/>
            <Button value='Search' onClick={handleClick}/>
        </div>
    );
}

export default FilterPanel;
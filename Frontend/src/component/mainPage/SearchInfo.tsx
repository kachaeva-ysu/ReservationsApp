import React, {useContext} from "react";
import InfoSection from "../shared/output/InfoSection";
import Button from "../shared/clickable/Button";
import converter from "../../utilities/converter";
import {AppContext} from "../../contexts/AppContext";
import s from "../shared/container/FlexDisplayed.css";

const SearchInfo = () => {
    const {value: {startDate, endDate, pool, priceFrom, priceTo, roomFrom, roomTo}, setValue} = useContext(AppContext);
    const priceText = converter.convertRangeToText(priceFrom, priceTo, true);
    const roomsText = converter.convertRangeToText(roomFrom, roomTo);
    const poolText = converter.convertBoolToString(pool);
    const buttonText = startDate || endDate || priceText || roomsText || pool ? 'Edit filter parameters' : 'Set filter parameters';

    return (
        <div className={s.flexDisplayed}>
            {startDate && <InfoSection id='startDate' caption='Start date:' text={startDate}/>}
            {endDate && <InfoSection id='endDate' caption='End date:' text={endDate}/>}
            {priceText && <InfoSection id='price' caption='Price for day:' text={priceText}/>}
            {roomsText && <InfoSection id='rooms' caption='Number of rooms:' text={roomsText}/>}
            {pool && <InfoSection id='pool' caption='Has a pool:' text={poolText}/>}
            <Button value={buttonText} onClick={() => setValue({isFilterPanelActive: true})}/>
        </div>
    );
}

export default SearchInfo;
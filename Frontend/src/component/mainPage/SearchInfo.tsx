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
    const buttonText = startDate || endDate || priceText || roomsText || pool ? 'Редактировать параметры поиска' : 'Задать параметры поиска';

    return (
        <div className={s.flexDisplayed}>
            {startDate && <InfoSection id='startDate' caption='Дата начала:' text={startDate}/>}
            {endDate && <InfoSection id='endDate' caption='Дата окончания:' text={endDate}/>}
            {priceText && <InfoSection id='price' caption='Цена за день:' text={priceText}/>}
            {roomsText && <InfoSection id='rooms' caption='Количество комнат:' text={roomsText}/>}
            {pool && <InfoSection id='pool' caption='Бассейн:' text={poolText}/>}
            <Button value={buttonText} onClick={() => setValue({isFilterPanelActive: true})}/>
        </div>
    );
}

export default SearchInfo;
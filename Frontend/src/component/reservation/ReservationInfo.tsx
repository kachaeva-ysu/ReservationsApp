import React from "react";
import InfoSection from "../shared/output/InfoSection";
import s from "../shared/container/FlexDisplayed.css";

type ReservationInfo = {
    resourceName: string,
    startDate: string,
    endDate: string,
    totalPrice: string
}

const ReservationInfo = ({resourceName, startDate, endDate, totalPrice}: ReservationInfo) => {
    return (
        <div className={s.flexDisplayed}>
            <InfoSection id='resourceName' caption='Название виллы:' text={resourceName}/>
            <InfoSection id='startDate' caption='Дата начала:' text={startDate}/>
            <InfoSection id='endDate' caption='Дата окончания:' text={endDate}/>
            <InfoSection id='totalPrice' caption='Итоговая стоимость:' text={totalPrice}/>
        </div>
    )
}

export default ReservationInfo;
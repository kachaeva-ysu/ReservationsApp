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
            <InfoSection id='resourceName' caption='Name:' text={resourceName}/>
            <InfoSection id='startDate' caption='Start date:' text={startDate}/>
            <InfoSection id='endDate' caption='End date:' text={endDate}/>
            <InfoSection id='totalPrice' caption='Total price:' text={totalPrice}/>
        </div>
    )
}

export default ReservationInfo;
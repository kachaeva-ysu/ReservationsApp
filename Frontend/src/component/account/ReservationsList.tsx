import React from "react";
import Header from "../shared/output/Header";
import Title from "../shared/output/Title";
import ReservationPane from "./ReservationPane";
import s from "../shared/container/FlexDisplayed.css"

type ReservationsListProps = {
    userReservations: ({
        id: number,
        startDate: string,
        endDate: string,
        villaId: number,
        villaName: string,
        totalPrice: string
    })[]
}

const ReservationsList = ({userReservations}: ReservationsListProps) => {
    return (
        <>
            <Header value='Мои резервации' subheader={true}/>
            {userReservations.length === 0 && <Title value='У Вас еще нет ни одной резервации'/>}
            {userReservations.length !== 0 &&
            <div className={s.flexDisplayed}>
                {userReservations.map(userReservation =>
                    <ReservationPane userReservation={userReservation} key={userReservation.id}/>
                )}
            </div>}
        </>
    );
}

export default ReservationsList;
import React, {useState} from "react";
import InfoSection from "../shared/output/InfoSection";
import Button from "../shared/clickable/Button";
import reservationService from "../../services/reservationService";
import toastHandler from "../../utilities/toastHandler";
import confirmationHandler from "../../utilities/confirmationHandler";
import s from "../shared/container/Pane.css";
import dateFormatter from "../../utilities/dateFormatter";

type ReservationPaneProps = {
    userReservation: { id: number, startDate: string, endDate: string, villaId: number, villaName: string, totalPrice: string }
}

const ReservationPane = ({userReservation}: ReservationPaneProps) => {
    const [isCanceled, setIsCanceled] = useState(false);
    const detailsPath = `/details/${userReservation.villaId}`;

    const onCancelClick = () => {
        confirmationHandler.confirm('Вы уверены, что хотите отменить резервацию?', cancelReservation);
    }

    const cancelReservation = async () => {
        try {
            await reservationService.deleteReservation(userReservation.id);
            setIsCanceled(true);
            toastHandler.success('Резервация успешно отменена');
         } catch {
             toastHandler.error('Не удалось отменить резервацию. Попробуйте позже');
         }
    }

    return (
        <>
            {!isCanceled &&
            <div className={s.pane}>
                <InfoSection id='startDate' caption='Дата начала:'
                             text={dateFormatter.getDateWithoutTime(userReservation.startDate)}/>
                <InfoSection id='endDate' caption='Дата окончания:'
                             text={dateFormatter.getDateWithoutTime(userReservation.endDate)}/>
                <InfoSection id='villaName' caption='Название виллы:' text={userReservation.villaName} path={detailsPath}/>
                <InfoSection id='totalPrice' caption='Итоговая стоимость:' text={userReservation.totalPrice + '$'}/>
                {userReservation.startDate>dateFormatter.getFormattedDate('yyyy-mm-dd', new Date()) &&
                <Button value='Отменить' onClick={onCancelClick} isDark={true}/>}
            </div>}
        </>
    )
}

export default ReservationPane;
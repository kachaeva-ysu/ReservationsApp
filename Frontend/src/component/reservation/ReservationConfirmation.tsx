import React, {useContext, useEffect, useState} from "react";
import PageTemplate from "../shared/container/PageTemplate";
import ReservationInfo from "./ReservationInfo";
import Button from "../shared/clickable/Button";
import totalPriceCalculator from "../../utilities/totalPriceCalculator";
import villaService from "../../services/villaService";
import reservationService from "../../services/reservationService";
import {AppContext} from "../../contexts/AppContext";
import {UserAuthorizationInfoContext} from "../../contexts/UserAuthorizationInfoContext";
import toastHandler from "../../utilities/toastHandler";
import {useHistory} from "react-router-dom";
import ClientError from "../../errors/ClientError";
import datesSettingHandler from "../../utilities/datesSettingHandler";

const ReservationConfirmation = () => {
    const [resource, setResource] = useState({
        id: 0,
        name: '',
        description: '',
        priceForDay: 0,
        numberOfRooms: 0,
        hasPool: false
    });
    const [reservedDates, setReservedDates] = useState([{startDate: '', endDate: ''}]);
    const {value: {startDate, endDate, selectedResourceId}, excludeReservedResource} = useContext(AppContext);
    const {userId} = useContext(UserAuthorizationInfoContext);
    const history = useHistory();

    useEffect(() => {
        if (!selectedResourceId || !startDate || !endDate) {
            history.push('/');
            return;
        }
        const effect = async () => {
            try {
                const villaDetails = await villaService.getVillaDetails(selectedResourceId);
                setResource(villaDetails.villa);
                setReservedDates(villaDetails.reservedDates);
            } catch (error) {
                if (error instanceof ClientError) {
                    toastHandler.info('Villa is already reserved for these dates');
                } else {
                    toastHandler.error('Failed to fetch villa');
                }
                history.goBack();
            }
        }
        effect();
    }, []);

    const onConfirm = async () => {
        if (userId === 0) {
            toastHandler.info('Sign in to confirm reservation');
            history.push('/signIn');
            return;
        }
        try {
            await createReservation(userId, startDate, endDate);
            excludeReservedResource(selectedResourceId);
            toastHandler.success('Reservation completed');
        } catch (error) {
            if (error instanceof ClientError) {
                toastHandler.error('Dates are not available');
            } else {
                toastHandler.error('Failed to create reservation');
            }
        }
        history.push('/');
    }

    const createReservation = async (userId: number, startDate: string, endDate: string) => {
        await reservationService.createReservation({
            userId: userId, villaId: selectedResourceId, startDate: startDate, endDate: endDate
        });
    }

    const onEditDates = () => {
        history.push(`/details/${selectedResourceId}`);
        datesSettingHandler.setDates(reservedDates);
    }

    return (
        <PageTemplate headerValue='Confirm your reservation'>
            <ReservationInfo resourceName={resource.name} startDate={startDate} endDate={endDate}
                             totalPrice={totalPriceCalculator.getTotalPrice(startDate, endDate, resource.priceForDay)}
            />
            <Button onClick={onConfirm} value='Confirm'/>
            <Button onClick={onEditDates} value='Edit dates'/>
        </PageTemplate>
    )
}

export default ReservationConfirmation;
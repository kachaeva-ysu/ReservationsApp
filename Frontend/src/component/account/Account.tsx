import React, {useContext, useEffect, useState} from "react";
import PageTemplate from "../shared/container/PageTemplate";
import UserInfoSection from "./UserInfoSection";
import AccountButtons from "./AccountButtons";
import EditForm from "./EditForm";
import ChangePasswordForm from "./ChangePasswordForm";
import ReservationsList from "./ReservationsList";
import userService from "../../services/userService";
import toastHandler from "../../utilities/toastHandler";
import confirmationHandler from "../../utilities/confirmationHandler";
import {useHistory} from "react-router-dom";
import s from "../shared/container/FlexDisplayed.css";
import {UserAuthorizationInfoContext} from "../../contexts/UserAuthorizationInfoContext";

const Account = () => {
    const [userInfo, setUserInfo] = useState({
        name: '', phone: '', email: '', password: '',
        isBeingEdited: false, isPasswordBeingChanged: false
    });
    const [userReservations, setUserReservations] = useState([]);
    const {clearUserAuthorizationInfo} = useContext(UserAuthorizationInfoContext);
    const history = useHistory();

    useEffect(() => {
        const effect = async () => {
            try {
                const user = await userService.getUser();
                setUserInfo(user);
            } catch {
                toastHandler.error('Не удалось получить пользователя. Попробуйте позже');
                history.goBack();
            }
        }
        effect();
    }, []);

    useEffect(() => {
        const effect = async () => {
            try {
                const userReservations = await userService.getUserReservations();
                setUserReservations(userReservations);
            } catch {
                toastHandler.error('Не удалось получить резервации. Попробуйте позже');
            }
        }
        effect();
    }, []);

    const handleUserInfoChange = (newUserProps: {
        name?: string, phone?: string, email?: string, password?: string,
        isBeingEdited?: boolean, isPasswordBeingChanged?: boolean }) => {
            setUserInfo((prevState) => ({
                ...prevState,
                ...newUserProps
            }));
    }

    const onDeleteClick = () => {
        confirmationHandler.confirm('Вы уверены, что хотите удалить аккаунт?', deleteAccount);
    }

    const deleteAccount = async () => {
        try {
            await userService.deleteUser();
            clearUserAuthorizationInfo();
            toastHandler.success('Аккаунт удален');
            history.goBack();
        } catch {
            toastHandler.error('Не удалось удалить аккаунт. Попробуйте позже');
        }
    }

    const onEditClick = () => {
        handleUserInfoChange({isBeingEdited: true});
    }

    const onChangePasswordClick = () => {
        handleUserInfoChange({isPasswordBeingChanged: true});
    }

    return (
        <PageTemplate headerValue='Мой аккаунт'>
            {!userInfo.isBeingEdited && !userInfo.isPasswordBeingChanged &&
            <div className={s.flexDisplayed}>
                <UserInfoSection userInfo={userInfo}/>
                <AccountButtons onEditClick={onEditClick} onDeleteClick={onDeleteClick}
                                onChangePasswordClick={onChangePasswordClick}
                />
            </div>}
            {userInfo.isBeingEdited &&
            <EditForm userInfo={userInfo} onUserInfoChange={handleUserInfoChange}/>}
            {userInfo.isPasswordBeingChanged &&
            <ChangePasswordForm userPassword={userInfo.password} onUserInfoChange={handleUserInfoChange}/>}
            <ReservationsList userReservations={userReservations}/>
        </PageTemplate>
    )
}

export default Account;
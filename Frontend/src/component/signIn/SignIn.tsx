import React, {useContext, useState} from "react";
import PageTemplate from "../shared/container/PageTemplate";
import SignInForm from "./SignInForm";
import authorizationService from "../../services/authorizationService";
import toastHandler from "../../utilities/toastHandler";
import ClientError from "../../errors/ClientError";
import {useHistory} from "react-router-dom";
import {UserAuthorizationInfoContext} from "../../contexts/UserAuthorizationInfoContext";
import {AppContext} from "../../contexts/AppContext";

const SignIn = () => {
    const [userInfo, setUserInfo] = useState({email: '', password: ''});
    const {saveUserAuthorizationInfo} = useContext(UserAuthorizationInfoContext);
    const {value: {selectedResourceId}} = useContext(AppContext);
    const history = useHistory();

    const handleEmailChange = (newEmail: string) => {
        setUserInfo((prevState) => ({
            ...prevState,
            ...{email: newEmail}
        }));
    }

    const handlePasswordChange = (newPassword: string) => {
        setUserInfo((prevState) => ({
            ...prevState,
            ...{password: newPassword}
        }));
    }

    const onSignInClick = async () => {
        try {
            if (!validateUserInfo()) {
                toastHandler.info('Email и пароль не могут быть пустыми');
                return;
            }
            const user = await authorizationService.signIn(userInfo.email, userInfo.password);
            saveUserAuthorizationInfo(user.token, user.userId);
            toastHandler.success('Вход успешно выполнен');

            if (!selectedResourceId) {
                history.goBack();
            } else {
                history.push('/reservation/confirm');
            }
        } catch (error) {
            if (error instanceof ClientError) {
                toastHandler.info('Некорректный email или пароль');
            } else {
                toastHandler.error('Не удалось получить пользователя. Попробуйте позже');
                history.goBack();
            }
        }
    }

    const validateUserInfo = () => {
        if (!userInfo.email) {
            return false;
        }

        return userInfo.password;
    }

    return (
        <PageTemplate headerValue='Вход'>
            <SignInForm onSignInClick={onSignInClick} userInfo={userInfo}
                        onEmailChange={handleEmailChange} onPasswordChange={handlePasswordChange}
            />
        </PageTemplate>
    )
}

export default SignIn;
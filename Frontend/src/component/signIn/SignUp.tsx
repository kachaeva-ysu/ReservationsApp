import React, {useContext, useState} from "react";
import PageTemplate from "../shared/container/PageTemplate";
import SignUpForm from "./SignUpForm";
import validator from "../../utilities/validator";
import authorizationService from "../../services/authorizationService";
import toastHandler from "../../utilities/toastHandler";
import ClientError from "../../errors/ClientError";
import {useHistory} from "react-router-dom";
import {UserAuthorizationInfoContext} from "../../contexts/UserAuthorizationInfoContext";
import {AppContext} from "../../contexts/AppContext";

const SignUp = () => {
    const [userInfo, setUserInfo] = useState({name: '', phone: '', email: '', password: ''});
    const [errors, setErrors] = useState({
        isNameError: false,
        isPhoneError: false,
        isEmailError: false,
        isPasswordError: false
    });
    const {saveUserAuthorizationInfo} = useContext(UserAuthorizationInfoContext);
    const {selectedResourceId} = useContext(AppContext).value;
    const history = useHistory();

    const handleErrorsChange = (newErrors: {
        isNameError?: boolean, isPhoneError?: boolean, isEmailError?: boolean, isPasswordError?: boolean
    }) => {
        setErrors((prevState) => ({
            ...prevState,
            ...newErrors
        }));
    }

    const handleNameChange = (newName: string) => {
        setUserInfo((prevState) => ({
            ...prevState,
            ...{name: newName}
        }));
    }

    const handlePhoneChange = (newPhone: string) => {
        setUserInfo((prevState) => ({
            ...prevState,
            ...{phone: newPhone}
        }));
    }
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

    const onSignUpClick = async () => {
        const isInputValid = validateInput();
        if (isInputValid) {
            try {
                const user = await authorizationService.createUser(userInfo);
                saveUserAuthorizationInfo(user.token, user.userId);
                toastHandler.success('You successfully signed up');

                if (!selectedResourceId) {
                    history.push('/');
                } else {
                    history.push('/reservation/confirm');
                }
            } catch (error) {
                if (error instanceof ClientError) {
                    handleErrorsChange({isEmailError: true});
                    toastHandler.info('User with this email already exists');
                } else {
                    toastHandler.error('Failed to create a user');
                    history.goBack();
                }
            }
        }
    }

    const validateInput = () => {
        if (!validator.validateName(userInfo.name)) {
            handleErrorsChange({isNameError: true});
            return false;
        } else {
            handleErrorsChange({isNameError: false});
        }

        if (!validator.validatePhone(userInfo.phone)) {
            handleErrorsChange({isPhoneError: true});
            return false;
        } else {
            handleErrorsChange({isPhoneError: false});
        }

        if (!validator.validateEmail(userInfo.email)) {
            handleErrorsChange({isEmailError: true});
            return false;
        } else {
            handleErrorsChange({isEmailError: false});
        }

        if (!validator.validatePassword(userInfo.password)) {
            handleErrorsChange({isPasswordError: true});
            return false;
        } else {
            handleErrorsChange({isPasswordError: false});
        }

        return true;
    }

    return (
        <PageTemplate headerValue='Sign up'>
            <SignUpForm userInfo={userInfo} errors={errors} onNameChange={handleNameChange}
                        onPhoneChange={handlePhoneChange} onEmailChange={handleEmailChange}
                        onPasswordChange={handlePasswordChange} onSignUpClick={onSignUpClick}
            />
        </PageTemplate>
    )
}

export default SignUp;
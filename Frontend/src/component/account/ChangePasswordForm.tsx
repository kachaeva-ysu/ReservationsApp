import React, {useState} from "react";
import InputSection from "../shared/input/InputSection";
import Button from "../shared/clickable/Button";
import userService from "../../services/userService";
import validator from "../../utilities/validator";
import toastHandler from "../../utilities/toastHandler";
import s from "../shared/container/Pane.css";
import ClientError from "../../errors/ClientError";

type ChangePasswordFormProps = {
    userPassword: string,
    onUserInfoChange: (newUserInfo: { password?: string, isPasswordBeingChanged?: boolean }) => void
}

const ChangePasswordForm = ({userPassword, onUserInfoChange}: ChangePasswordFormProps) => {
    const [password, setPassword] = useState({oldPassword: '', newPassword: '',});
    const [errors, setErrors] = useState({isOldPasswordError: false, isNewPasswordError: false});

    const handleErrorsChange = (newErrors: { isOldPasswordError?: boolean, isNewPasswordError?: boolean }) => {
        setErrors((prevState) => ({
            ...prevState,
            ...newErrors
        }));
    }

    const handleNewPasswordChange = (newPassword: string) => {
        setPassword((prevState) => ({
            ...prevState,
            ...{newPassword: newPassword}
        }));
    }

    const handleOldPasswordChange = (newPassword: string) => {
        setPassword((prevState) => ({
            ...prevState,
            ...{oldPassword: newPassword}
        }));
    }

    const onSaveButtonClick = async () => {
        const isInputValid = validateInput();
        if (isInputValid) {
            try {
                await userService.updatePassword({oldPassword: password.oldPassword, password: password.newPassword});
                onUserInfoChange({password: password.newPassword});
                toastHandler.success('Password was successfully changed');
                onUserInfoChange({isPasswordBeingChanged: false});
            } catch(error) {
                console.log(error);
                if (error instanceof ClientError) {
                    handleErrorsChange({isOldPasswordError: true});
                    toastHandler.info('Old password is incorrect');
                }
                else {
                    handleErrorsChange({isOldPasswordError: false});
                    toastHandler.error('Failed to change password');
                }
            }
        }
    }

    const onCancelButtonClick = async () => {
        onUserInfoChange({isPasswordBeingChanged: false});
    }

    const validateInput = () => {
        if (!validator.validatePassword(password.newPassword)) {
            handleErrorsChange({isNewPasswordError: true});
            return false;
        } else {
            handleErrorsChange({isNewPasswordError: false});
        }

        return true;
    }

    return (
        <div className={s.pane}>
            <InputSection id='oldPassword' type='password' labelValue='Old password'
                          value={password.oldPassword} isError={errors.isOldPasswordError}
                          onChange={handleOldPasswordChange}
            />
            <InputSection id='newPassword' type='password' labelValue="New password"
                          value={password.newPassword} isError={errors.isNewPasswordError}
                          onChange={handleNewPasswordChange}
            />
            <Button value='Save' onClick={onSaveButtonClick}/>
            <Button value='Cancel' onClick={onCancelButtonClick}/>
        </div>
    )
}

export default ChangePasswordForm;
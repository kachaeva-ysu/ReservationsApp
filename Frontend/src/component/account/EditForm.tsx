import React, {useState} from "react";
import InputSection from "../shared/input/InputSection";
import Button from "../shared/clickable/Button";
import userService from "../../services/userService";
import validator from "../../utilities/validator";
import toastHandler from "../../utilities/toastHandler";
import s from "../shared/container/Pane.css";

type EditFormProps = {
    userInfo: { name: string, phone: string },
    onUserInfoChange: (newUserInfo: { name?: string, phone?: string, isBeingEdited?: boolean }) => void
}

const EditForm = ({userInfo, onUserInfoChange}: EditFormProps) => {
    const [newUserInfo, setNewUserInfo] = useState({name: userInfo.name, phone: userInfo.phone});
    const [errors, setErrors] = useState({isNameError: false, isPhoneError: false});

    const handleErrorsChange = (newErrors: { isNameError?: boolean, isPhoneError?: boolean }) => {
        setErrors((prevState) => ({
            ...prevState,
            ...newErrors
        }));
    }

    const handleNameChange = (newName: string) => {
        setNewUserInfo((prevState) => ({
            ...prevState,
            ...{name: newName}
        }));
    }

    const handlePhoneChange = (newPhone: string) => {
        setNewUserInfo((prevState) => ({
            ...prevState,
            ...{phone: newPhone}
        }));
    }

    const onSaveButtonClick = async () => {
        const isInputValid = validateInput();
        if (isInputValid) {
            try {
                await userService.updateUser({name: newUserInfo.name, phone: newUserInfo.phone});
                onUserInfoChange({name: newUserInfo.name, phone: newUserInfo.phone});
            } catch {
                toastHandler.error('Не удалось отредактировать аккаунт. Попробуйте позже');
            }
            onUserInfoChange({isBeingEdited: false});
        }
    }

    const onCancelButtonClick = async () => {
        onUserInfoChange({isBeingEdited: false});
    }

    const validateInput = () => {
        if (!validator.validateName(newUserInfo.name)) {
            handleErrorsChange({isNameError: true});
            return false;
        } else {
            handleErrorsChange({isNameError: false});
        }

        if (!validator.validatePhone(newUserInfo.phone)) {
            handleErrorsChange({isPhoneError: true});
            return false;
        } else {
            handleErrorsChange({isPhoneError: false});
        }

        return true;
    }

    return (
        <div className={s.pane}>
            <InputSection id='name' labelValue='Имя' value={newUserInfo.name}
                          isError={errors.isNameError} onChange={handleNameChange}
            />
            <InputSection id='phone' labelValue="Телефон" value={newUserInfo.phone}
                          isError={errors.isPhoneError} onChange={handlePhoneChange}
            />
            <Button value='Сохранить' onClick={onSaveButtonClick}/>
            <Button value='Отменить' onClick={onCancelButtonClick}/>
        </div>
    )
}

export default EditForm;
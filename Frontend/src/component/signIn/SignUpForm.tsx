import React from "react";
import InputSection from "../shared/input/InputSection";
import Button from "../shared/clickable/Button";
import s from "../shared/container/Pane.css";

type SignUpFormProps = {
    userInfo: { name: string, phone: string, email: string, password: string },
    errors: {
        isNameError: boolean,
        isPhoneError: boolean,
        isEmailError: boolean,
        isPasswordError: boolean
    }
    onNameChange: (newName: string) => void,
    onPhoneChange: (newPhone: string) => void,
    onEmailChange: (newEmail: string) => void,
    onPasswordChange: (newPassword: string) => void,
    onSignUpClick: () => void
}
const SignUpForm = ({
                        userInfo, errors, onNameChange, onPhoneChange, onEmailChange, onPasswordChange,
                        onSignUpClick
                    }: SignUpFormProps) => {
    return (
        <div className={s.pane}>
            <InputSection id='name' labelValue='Имя' value={userInfo.name}
                          isError={errors.isNameError} onChange={onNameChange}
            />
            <InputSection id='phone' labelValue='Телефон' value={userInfo.phone}
                          isError={errors.isPhoneError} onChange={onPhoneChange}
            />
            <InputSection id='email' labelValue='Email' value={userInfo.email}
                          isError={errors.isEmailError} onChange={onEmailChange}
            />
            <InputSection id='password' type='password' labelValue='Пароль' value={userInfo.password}
                          isError={errors.isPasswordError} onChange={onPasswordChange}
            />
            <Button onClick={onSignUpClick} value='Зарегистрироваться'/>
        </div>
    )
}

export default SignUpForm;
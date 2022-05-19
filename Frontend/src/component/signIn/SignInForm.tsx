import React from "react";
import InputSection from "../shared/input/InputSection";
import Button from "../shared/clickable/Button";
import s from "../shared/container/Pane.css";
import GoogleSignIn from "./GoogleSignIn.js";
import LinkButton from "../shared/clickable/LinkButton";

type SignInFormProps = {
    userInfo: { email: string, password: string },
    onEmailChange: (newEmail: string) => void,
    onPasswordChange: (newPassword: string) => void,
    onSignInClick: () => void
}

const SignInForm = ({userInfo, onEmailChange, onPasswordChange, onSignInClick}: SignInFormProps) => {
    return (
        <div className={s.pane}>
            <InputSection id='email' labelValue='Email' value={userInfo.email} onChange={onEmailChange}/>
            <InputSection id='password' type='password' labelValue='Password'
                          value={userInfo.password} onChange={onPasswordChange}
            />
            <Button onClick={onSignInClick} value='Sign in'/>
            <LinkButton to='/signUp' value='Sign up'/>
            <GoogleSignIn/>
        </div>
    )
}

export default SignInForm;
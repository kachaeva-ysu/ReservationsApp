import React, {useContext} from "react";
import {GoogleLogin} from 'react-google-login';
import s from "../shared/clickable/GoogleSignIn.css";
import authorizationHandler from "../../utilities/authorizationHandler";
import authorizationService from "../../services/authorizationService";
import toastHandler from "../../utilities/toastHandler";
import {UserAuthorizationInfoContext} from "../../contexts/UserAuthorizationInfoContext";
import {AppContext} from "../../contexts/AppContext";
import {useHistory} from "react-router-dom";
import {configData} from "../../../config.ts";

const GoogleSignIn = () => {
    const {saveUserAuthorizationInfo} = useContext(UserAuthorizationInfoContext);
    const {value: {selectedResourceId}} = useContext(AppContext);
    const history = useHistory();

    const onSignInClick = async (res) => {
        authorizationHandler.setGoogleToken(res.tokenObj.id_token);
        try {
            const user = await authorizationService.signInWithGoogle(res.profileObj.email);
            saveUserAuthorizationInfo(user.token, user.userId);
            toastHandler.success('You successfully signed in');
            if (!selectedResourceId) {
                history.goBack();
            } else {
                history.push('/reservation/confirm');
            }
        } catch (error) {
            toastHandler.error('Failed to get user');
            history.goBack();
        }
    }

    return (
        <div>
            <GoogleLogin className={s.signInButton} clientId={configData.clientId}
                         onSuccess={onSignInClick} buttonText='Sign in with google'
            />
        </div>
    );
}

export default GoogleSignIn;
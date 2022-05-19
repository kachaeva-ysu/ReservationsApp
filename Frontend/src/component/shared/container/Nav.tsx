import React, {useContext} from "react";
import {Link, useHistory} from "react-router-dom";
import s from "./Nav.css";
import {UserAuthorizationInfoContext} from "../../../contexts/UserAuthorizationInfoContext";
import {GoogleLogout} from "react-google-login";
import {configData} from "../../../../config";


const Nav = () => {
    const {userId, clearUserAuthorizationInfo} = useContext(UserAuthorizationInfoContext);
    const history = useHistory();
    const clientId = configData.clientId;

    const signOutClicked = () => {
        clearUserAuthorizationInfo();
        if (history.location.pathname === '/account') {
            history.push('/');
        }
    }

    return (
        <nav className={s.nav}>
            <Link className={s.refLeft} to='/'>Main page</Link>
            {userId === 0 && <Link className={s.refRight} to='/signIn'>Sign in</Link>}
            {userId !== 0 &&
            <GoogleLogout clientId={clientId} render ={()=>
                <button className={s.refRight} onClick={signOutClicked}>Sign out</button>}
            />}
            {userId !== 0 && <Link className={s.refRight} to='/account'>Account</Link>}
        </nav>
    );
}

export default Nav;
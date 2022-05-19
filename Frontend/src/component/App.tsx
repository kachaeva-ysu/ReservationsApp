import React from "react";
import MainPage from "./mainPage/MainPage";
import SignIn from "./signIn/SignIn";
import SignUp from "./signIn/SignUp";
import Details from "./details/Details";
import ReservationConfirmation from "./reservation/ReservationConfirmation";
import Account from "./account/Account";
import {AppContextProvider} from "../contexts/AppContext";
import {UserAuthorizationInfoContextProvider} from "../contexts/UserAuthorizationInfoContext";
import {BrowserRouter, Redirect, Route, Switch} from "react-router-dom";
import {Toaster} from 'react-hot-toast';

const App = () => {
    return (
        <UserAuthorizationInfoContextProvider>
            <AppContextProvider>
                <Toaster position='top-right' data-test-toaster/>
                <BrowserRouter>
                    <Switch>
                        <Route exact path='/' component={MainPage}/>
                        <Route exact path='/signIn' component={SignIn}/>
                        <Route exact path='/signUp' component={SignUp}/>
                        <Route exact path='/details/:resourceId' render={(props) =>
                            <Details resourceId={parseInt(props.match.params.resourceId)}/>}
                        />
                        <Route exact path='/reservation/confirm' component={ReservationConfirmation}/>
                        <Route exact path='/account' component={Account}/>
                        <Route render={() => <Redirect to='/'/>}/>
                    </Switch>
                </BrowserRouter>
            </AppContextProvider>
        </UserAuthorizationInfoContextProvider>
    );
}

export default App;
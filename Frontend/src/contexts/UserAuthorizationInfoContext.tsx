import React, {useState} from "react";
import authorizationHandler from "../utilities/authorizationHandler";

interface IUserAuthorizationInfoContext {
    userId: number,
    saveUserAuthorizationInfo: (newToken: {
        accessToken: string, refreshToken: string, expirationTime: string
    }, newUserId: number) => void,
    clearUserAuthorizationInfo: () => void
}

const defaultValue = {
    userId: 0,
    saveUserAuthorizationInfo: () => {
    },
    clearUserAuthorizationInfo: () => {
    }
}

const UserAuthorizationInfoContext = React.createContext<IUserAuthorizationInfoContext>(defaultValue);

type UserAuthorizationInfoContextProviderProps = {
    children: React.ReactNode
}

const UserAuthorizationInfoContextProvider = ({children}: UserAuthorizationInfoContextProviderProps) => {
    const [userId, setUserId] = useState(authorizationHandler.getUserId());
    const [, setAccessToken] = useState(authorizationHandler.getToken().accessToken);
    const [, setRefreshToken] = useState(authorizationHandler.getToken().refreshToken);
    const [, setExpirationTime] = useState(authorizationHandler.getToken().expirationTime);

    const saveUserAuthorizationInfo = (newToken: { accessToken: string, refreshToken: string, expirationTime: string }, newUserId: number) => {
        authorizationHandler.saveAuthorizationInfo(newToken, newUserId);
        setUserId(newUserId);
        setAccessToken(newToken.accessToken);
        setRefreshToken(newToken.refreshToken);
        setExpirationTime(newToken.expirationTime);
    }

    const clearUserAuthorizationInfo = () => {
        authorizationHandler.clearAuthorizationInfo();
        setUserId(0);
        setAccessToken('');
        setRefreshToken('');
        setExpirationTime('');
    }

    return (
        <UserAuthorizationInfoContext.Provider
            value={{userId, saveUserAuthorizationInfo, clearUserAuthorizationInfo}}>
            {children}
        </UserAuthorizationInfoContext.Provider>
    )
}

export {UserAuthorizationInfoContextProvider, UserAuthorizationInfoContext};


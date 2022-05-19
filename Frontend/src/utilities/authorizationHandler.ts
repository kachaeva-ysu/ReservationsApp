const saveAuthorizationInfo = (newToken: { accessToken: string, refreshToken: string, expirationTime: string }, newUserId?: number) => {
    if(newUserId)
        localStorage.setItem('userId', newUserId.toString());
    localStorage.setItem('accessToken', newToken.accessToken);
    localStorage.setItem('refreshToken', newToken.refreshToken);
    localStorage.setItem('expirationTime', newToken.expirationTime);
}

const clearAuthorizationInfo = () => {
    localStorage.setItem('userId', '');
    localStorage.setItem('accessToken', '');
    localStorage.setItem('refreshToken', '');
    localStorage.setItem('expirationTime', '');
    localStorage.setItem('googleToken', '');
}

const getUserId = () => {
    const userId = localStorage.getItem('userId');
    return userId ? parseInt(userId) : 0;
}

const getToken = () => {
    const accessToken = localStorage.getItem('accessToken');
    const refreshToken = localStorage.getItem('refreshToken');
    const expirationTime = localStorage.getItem('expirationTime');
    return {accessToken, refreshToken, expirationTime};
}

const getGoogleToken = ()=> {
    const token = localStorage.getItem('googleToken');
    if (token) {
        return token;
    }
    return '';
}

const setGoogleToken = (token: string) => {
    localStorage.setItem('googleToken', token);
}

export default {
    saveAuthorizationInfo,
    clearAuthorizationInfo,
    getUserId,
    getToken,
    getGoogleToken,
    setGoogleToken
}

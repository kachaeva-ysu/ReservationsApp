import React from "react";
import InfoSection from "../shared/output/InfoSection";
import s from "../shared/container/FlexDisplayed.css";

type UserInfoSectionProps = {
    userInfo: { name: string, phone: string, email: string }
}

const UserInfoSection = ({userInfo}: UserInfoSectionProps) => {
    return (
        <div className={s.flexDisplayed}>
            <InfoSection id='name' caption='Name:' text={userInfo.name}/>
            <InfoSection id='phone' caption='Phone:' text={userInfo.phone}/>
            <InfoSection id='email' caption='Email:' text={userInfo.email}/>
        </div>
    )
}

export default UserInfoSection;
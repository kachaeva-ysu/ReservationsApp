import React from "react";
import s from "./InfoSection.css";

type InfoSectionProps = {
    id: string,
    caption: string,
    text: string,
    path?:string
}

const InfoSection = ({id, caption, text, path}: InfoSectionProps) => {
    return (
        <div className={s.infoSection}>
            <label className={s.caption} htmlFor={id}>{caption}</label>
            {path? <a href={path} id={id}>{text}</a>:
            <span className={s.text} id={id}>{text}</span>}
        </div>
    )
}

export default InfoSection;
import React from "react";
import s from "./Title.css";

type TitleProps = {
    value: string
}

const Title = ({value, ...rest}: TitleProps) => {
    return (
        <h1 className={s.title} {...rest}>{value}</h1>
    )
}

export default Title;
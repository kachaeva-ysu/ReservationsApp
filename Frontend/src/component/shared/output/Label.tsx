import React from "react";
import s from "./Label.css";

type LabelProps = {
    htmlFor: string,
    value: string,
    isClose?: boolean
}

const Label = ({htmlFor, value, isClose}: LabelProps) => {
    return (
        <label className={isClose ? s.closeLabel : s.label} htmlFor={htmlFor}>{value}</label>
    );
}
export default Label;
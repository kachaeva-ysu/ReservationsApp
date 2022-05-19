import React from "react";
import s from "./Button.css";

type ButtonProps = {
    value: string,
    isDark?: boolean,
    onClick: () => void;
}

const Button = ({value, onClick, isDark}: ButtonProps) => {
    return (
        <button className={isDark ? s.darkButton : s.button} onClick={onClick} data-test-button>{value}</button>
    )
}

export default Button;
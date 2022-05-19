import React from "react";
import {Link} from "react-router-dom";
import s from "./LinkButton.css";

type LinkButtonProps = {
    to: string,
    value: string,
    isDark?: boolean,
    onClick?: () => void
}

const LinkButton = ({to, value, isDark, onClick}: LinkButtonProps) => {
    return (
        <Link to={to}>
            <button className={isDark ? s.darkLinkButton : s.linkButton} onClick={onClick}>
                {value}
            </button>
        </Link>
    );
}

export default LinkButton;
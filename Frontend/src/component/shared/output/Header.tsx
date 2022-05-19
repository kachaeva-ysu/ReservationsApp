import React from 'react';
import s from "./Header.css";

type HeaderProps = {
    value: string,
    subheader?: boolean
}

const Header = ({value, subheader}: HeaderProps) => {
    return (
        <h2 className={subheader ? s.subheader : s.header}>{value}</h2>
    );
}
export default Header;

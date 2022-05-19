import React from "react";
import s from "./Checkbox.css";

type CheckboxProps = {
    id: string,
    value: boolean,
    onChange: (newValue: boolean) => void
}

const Checkbox = ({id, value, onChange}: CheckboxProps) => {
    return (
        <input className={s.checkbox} type="checkbox" id={id} checked={value}
               onChange={(e) => onChange(e.target.checked)}/>
    );
}
export default Checkbox;

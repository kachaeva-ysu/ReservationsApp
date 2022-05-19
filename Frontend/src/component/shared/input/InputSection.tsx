import React from "react";
import Label from "../output/Label";
import s from "./InputSection.css";

type InputSectionProps = {
    id: string,
    labelValue: string,
    type?: string,
    value: string,
    onChange: (newValue: string) => void,
    isError?: boolean
}

const InputSection = ({id, labelValue, type, value, onChange, isError}: InputSectionProps) => {
    return (
        <>
            <Label value={labelValue} htmlFor={id}/>
            <input className={isError ? s.textInputWithError : s.textInput} type={type ? type : 'text'}
                   id={id} value={value} onChange={(e) => onChange(e.target.value)}
            />
        </>
    )
}

export default InputSection;
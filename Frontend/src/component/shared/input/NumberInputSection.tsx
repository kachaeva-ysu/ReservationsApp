import React from "react";
import Label from "../output/Label";
import s from "./NumberInputSection.css";

type NumberInputSectionProps = {
    id: string,
    labelValue: string,
    value: number,
    onChange: (newValue: number) => void,
    isError?: boolean,
    isPrice?: boolean
}

const NumberInputSection = ({id, labelValue, value, onChange, isError, isPrice}: NumberInputSectionProps) => {
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value);
        if (!isNaN(value) && value >= 0) {
            onChange(value);
        } else {
            onChange(0);
        }
    }

    return (
        <>
            <Label value={labelValue} htmlFor={id} isClose={true}/>
            <input className={isError ? s.numberInputWithError : s.numberInput} id={id}
                   value={value === 0 ? '' : value} onChange={handleChange} type='number' min={0}
            />
            {isPrice && <Label value='$' htmlFor={id} isClose={true}/>}
        </>
    )
}

export default NumberInputSection;
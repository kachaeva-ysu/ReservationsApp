import React from "react";
import NumberInputSection from "./NumberInputSection";

type NumberRangeSectionProps = {
    fromId: string,
    toId: string,
    from: number,
    to: number,
    onFromChange: (newValue: number) => void,
    onToChange: (newValue: number) => void,
    isPrice?: boolean,
    isError?: boolean
}

const NumberRangeSelection = ({
                                  fromId,
                                  toId,
                                  from,
                                  to,
                                  onFromChange,
                                  onToChange,
                                  isPrice,
                                  isError
                              }: NumberRangeSectionProps) => {
    return (
        <>
            <NumberInputSection id={fromId} labelValue='от' value={from} onChange={onFromChange}
                                isError={isError} isPrice={isPrice}
            />
            <NumberInputSection id={toId} labelValue='до' value={to} onChange={onToChange}
                                isError={isError} isPrice={isPrice}
            />
        </>
    );
}
export default NumberRangeSelection;
import React from "react";
import Label from "../../shared/output/Label";
import NumberRangeSelection from "../../shared/input/NumberRangeSelection";

type PriceRangeSelectionProps = {
    priceFrom: number,
    priceTo: number,
    setPriceFrom: (newPrice: number) => void,
    setPriceTo: (newPrice: number) => void,
    isError: boolean
}

const PriceRangeSelection = ({priceFrom, priceTo, setPriceFrom, setPriceTo, isError}: PriceRangeSelectionProps) => {

    return (
        <div>
            <Label htmlFor='priceForDayRange' value='Price per day:'/>
            <NumberRangeSelection fromId='priceFrom' toId='priceTo' from={priceFrom} to={priceTo}
                                  onFromChange={setPriceFrom} onToChange={setPriceTo} isPrice={true} isError={isError}
            />
        </div>
    )
}

export default PriceRangeSelection;
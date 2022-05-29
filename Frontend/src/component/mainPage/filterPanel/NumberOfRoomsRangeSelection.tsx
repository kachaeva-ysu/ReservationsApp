import React from "react";
import Label from "../../shared/output/Label";
import NumberRangeSelection from "../../shared/input/NumberRangeSelection";

type NumberOfRoomsRangeSelectionProps = {
    roomFrom: number,
    roomTo: number,
    setRoomFrom: (newValue: number) => void,
    setRoomTo: (newValue: number) => void,
    isError: boolean
}

const NumberOfRoomsRangeSelection = ({
                                         roomFrom,
                                         roomTo,
                                         setRoomFrom,
                                         setRoomTo,
                                         isError
                                     }: NumberOfRoomsRangeSelectionProps) => {
    return (
        <div>
            <Label htmlFor='numberOfRoomsRange' value='Количество комнат:'/>
            <NumberRangeSelection fromId='roomFrom' toId='roomTo' from={roomFrom} to={roomTo}
                                  onFromChange={setRoomFrom} onToChange={setRoomTo} isError={isError}
            />
        </div>
    )
}

export default NumberOfRoomsRangeSelection;
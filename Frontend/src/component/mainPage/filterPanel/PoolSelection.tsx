import React from "react";
import Label from "../../shared/output/Label";
import Checkbox from "../../shared/input/Checkbox";
import s from "../../shared/container/MarginBottom.css";

type PoolSelectionProps = {
    pool: boolean,
    setValue: (newValue: boolean) => void
}

const PoolSelection = ({pool, setValue}: PoolSelectionProps) => {
    return (
        <div className={s.marginBottom} data-test-pool-section>
            <Label htmlFor='pool' value='Has a pool'/>
            <Checkbox id='pool' value={pool} onChange={setValue}/>
        </div>
    )
}

export default PoolSelection;

import React from "react";
import InfoSection from "../shared/output/InfoSection";
import converter from "../../utilities/converter";

type ResourceInfoProps = {
    description: string,
    price: string,
    rooms: string,
    pool: boolean
}

const ResourceInfo = ({description, price, rooms, pool}: ResourceInfoProps) => {
    return (
        <>
            <InfoSection id='description' caption='Description:' text={description}/>
            <InfoSection id='price' caption='Price for day:' text={price}/>
            <InfoSection id='rooms' caption='Number of rooms:' text={rooms}/>
            <InfoSection id='pool' caption='Pool:' text={converter.convertBoolToString(pool)}/>
        </>
    );
}

export default ResourceInfo;

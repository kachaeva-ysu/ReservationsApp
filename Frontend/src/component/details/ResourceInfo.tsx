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
            <InfoSection id='description' caption='Описание:' text={description}/>
            <InfoSection id='price' caption='Цена за день:' text={price}/>
            <InfoSection id='rooms' caption='Количество комнат:' text={rooms}/>
            <InfoSection id='pool' caption='Бассейн:' text={converter.convertBoolToString(pool)}/>
        </>
    );
}

export default ResourceInfo;

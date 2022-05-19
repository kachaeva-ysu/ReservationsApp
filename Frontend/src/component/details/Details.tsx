import React, {useContext, useEffect, useState} from "react";
import PageTemplate from "../shared/container/PageTemplate";
import Image from "../shared/output/Image";
import ResourceInfo from "./ResourceInfo";
import LinkButton from "../shared/clickable/LinkButton";
import villaService from "../../services/villaService";
import imageService from "../../services/imageService";
import toastHandler from "../../utilities/toastHandler";
import {useHistory} from "react-router-dom";
import {AppContext} from "../../contexts/AppContext";
import Button from "../shared/clickable/Button";
import datesSettingHandler from "../../utilities/datesSettingHandler";

type DetailsProps = {
    resourceId: number
}

const Details = ({resourceId}: DetailsProps) => {
    const [resource, setResource] = useState({
        id: 0,
        name: '',
        description: '',
        priceForDay: 0,
        numberOfRooms: 0,
        hasPool: false
    });
    const [reservedDates, setReservedDates] = useState([{startDate: '', endDate: ''}]);
    const [imageSource, setImageSource] = useState('');
    const {value: {startDate, endDate}, setValue} = useContext(AppContext);
    const history = useHistory();

    useEffect(() => {
        const effect = async () => {
            try {
                const villaDetails = await villaService.getVillaDetails(resourceId);
                setResource(villaDetails.villa);
                setReservedDates(villaDetails.reservedDates);
            } catch {
                toastHandler.error('Failed to fetch villa details');
                history.goBack();
            }
        }
        effect();
    }, []);

    useEffect(() => {
        const effect = async () => {
            try {
                const imageSource = await imageService.getImage(resourceId);
                setImageSource(imageSource);
            } catch {
                toastHandler.error('Failed to fetch villa image');
            }
        }
        effect();
    }, []);

    return (
        <PageTemplate headerValue={resource.name}>
            <Image imageSource={imageSource} isBig={true}/>
            <ResourceInfo description={resource.description} price={resource.priceForDay + '$'}
                          rooms={resource.numberOfRooms.toString()} pool={resource.hasPool}
            />
            {startDate && endDate && <LinkButton to='/reservation/confirm' value='Make reservation'
                                                 onClick={() => setValue({selectedResourceId: resourceId})}
            />}
            {(!startDate || !endDate) && <Button value='Make reservation'
                                                 onClick={() => datesSettingHandler.setDates(reservedDates)}
            />}
        </PageTemplate>
    );
}

export default Details;
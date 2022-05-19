import React, {useContext, useEffect, useState} from "react";
import Image from "../shared/output/Image";
import Title from "../shared/output/Title";
import LinkButton from "../shared/clickable/LinkButton";
import s from "../shared/container/Pane.css";
import villaService from "../../services/villaService";
import imageService from "../../services/imageService";
import {AppContext} from "../../contexts/AppContext";
import toastHandler from "../../utilities/toastHandler";
import Button from "../shared/clickable/Button";
import datesSettingHandler from "../../utilities/datesSettingHandler";

type ResourcePaneProps = {
    resourceId: number,
    name: string
}

const ResourcePane = ({resourceId, name}: ResourcePaneProps) => {
    const [imageSource, setImageSource] = useState('');
    const {value: {startDate, endDate}, setValue} = useContext(AppContext);
    const detailsPath = `/details/${resourceId}`;

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
        <div className={s.pane} data-test-resource-pane>
            <Image imageSource={imageSource}/>
            <Title value={name} data-test-title/>
            <LinkButton to={detailsPath} value='Details' isDark={true}/>
            {startDate && endDate && <LinkButton to='/reservation/confirm' isDark={true} value='Make reservation'
                                                 onClick={() => setValue({selectedResourceId: resourceId})}
            />}
            {(!startDate || !endDate) && <Button value='Make reservation' isDark={true}
                                                 onClick={() => datesSettingHandler.setDates()}
            />}
        </div>
    );
}

export default ResourcePane;
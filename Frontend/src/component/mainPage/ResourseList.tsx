import React, {useContext} from "react";
import Title from "../shared/output/Title";
import ResourcePane from "./ResourcePane";
import s from "../shared/container/FlexDisplayed.css"
import {AppContext} from "../../contexts/AppContext";

const ResourceList = () => {
    const {resources} = useContext(AppContext).value;
    return (
        <>
            {resources.length === 0 && <Title value='No matches' data-test-title/>}
            {resources.length !== 0 &&
            <div className={s.flexDisplayed}>
                {resources.map(resource =>
                    <ResourcePane key={resource.id} resourceId={resource.id} name={resource.name}/>)}
            </div>}
        </>
    );
}

export default ResourceList;
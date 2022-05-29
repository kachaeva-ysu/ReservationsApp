import React, {useContext} from "react";
import PageTemplate from "../shared/container/PageTemplate";
import FilterPanel from "./filterPanel/FilterPanel";
import SearchInfo from "./SearchInfo";
import ResourceList from "./ResourseList";
import {AppContext} from "../../contexts/AppContext";

const MainPage = () => {
    const {isFilterPanelActive} = useContext(AppContext).value;

    return (
        <PageTemplate headerValue='Найдите идеальную виллу здесь:'>
            {isFilterPanelActive && <FilterPanel/>}
            {!isFilterPanelActive && <SearchInfo/>}
            {!isFilterPanelActive && <ResourceList/>}
        </PageTemplate>
    );
}

export default MainPage;
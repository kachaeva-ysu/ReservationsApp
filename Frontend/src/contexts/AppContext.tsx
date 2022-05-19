import React, {useEffect, useState} from "react";
import validator from "../utilities/validator";
import villaService from "../services/villaService";
import toastHandler from "../utilities/toastHandler";

type Resource ={
    id: number;
    name: string, description: string, numberOfRooms: number,
    priceForDay: number, hasPool: boolean
}

type AppContextValue ={
    startDate: string, endDate: string, pool: boolean, priceFrom: number, priceTo: number,
    roomFrom: number, roomTo: number, selectedResourceId: number,
    resources: Resource[],
    isFilterPanelActive: boolean
}

type NewAppContextProps = {
    startDate?: string, endDate?: string, pool?: boolean, priceFrom?: number,
    priceTo?: number, roomFrom?: number, roomTo?: number, selectedResourceId?: number,
    resources?: Resource[], isFilterPanelActive?: boolean
}

interface IAppContext {
    value: AppContextValue
    setValue: (newProps: NewAppContextProps) => void
    excludeReservedResource: (resourceId: number)=>void
}

const defaultValue = {
    value: {
        startDate: '',
        endDate: '',
        pool: false,
        priceFrom: 0,
        priceTo: 0,
        roomFrom: 0,
        roomTo: 0,
        selectedResourceId: 0,
        resources: [] as Resource[],
        isFilterPanelActive: false
    },
    setValue: () => {
    },
    excludeReservedResource: () => {
    }
}

type AppContextProviderProps = {
    children: React.ReactNode
}

const AppContext = React.createContext<IAppContext>(defaultValue);

const AppContextProvider = ({children}: AppContextProviderProps) => {
    const [value, setValue] = useState(defaultValue.value);

    const handleSetValue = (newProps: NewAppContextProps) => {
        setValue((prevState) => ({
            ...prevState,
            ...newProps
        }));
    }

    useEffect(() => {
        if (!value.isFilterPanelActive) {
            if (validator.validateDates(value.startDate, value.endDate)) {
                const effect = async () => {
                    try {
                        const villaInfo = getQuery();
                        const villas = await villaService.getVillas(villaInfo);
                        handleSetValue({resources: villas});

                    } catch (error) {
                        toastHandler.error('Failed to fetch villas');
                    }
                }
                effect();
            }
        }
    }, [value.startDate, value.endDate, value.isFilterPanelActive]);

    const getQuery = () => {
        let villaInfo = '?';
        if (value.startDate) {
            villaInfo += 'startDate=' + value.startDate + '&';
        }
        if (value.endDate) {
            villaInfo += 'endDate=' + value.endDate + '&';
        }
        if (value.priceFrom) {
            villaInfo += 'minPriceForDay=' + value.priceFrom + '&';
        }
        if (value.priceTo) {
            villaInfo += 'maxPriceForDay=' + value.priceTo + '&';
        }
        if (value.roomFrom) {
            villaInfo += 'minNumberOfRooms=' + value.roomFrom + '&';
        }
        if (value.roomTo) {
            villaInfo += 'maxNumberOfRooms=' + value.roomTo + '&';
        }
        if (value.pool) {
            villaInfo += 'hasPool=' + value.pool + '&';
        }
        return villaInfo.substring(0,villaInfo.length-1);
    }

    const excludeReservedResource = (reservedResourceId: number) => {
        const updatedResources = value.resources.filter((resource) => resource.id !== reservedResourceId);
        handleSetValue({resources: updatedResources});
    }

    return (
        <AppContext.Provider value={{value, setValue: handleSetValue, excludeReservedResource}}>
            {children}
        </AppContext.Provider>
    )
}

export {AppContextProvider, AppContext};
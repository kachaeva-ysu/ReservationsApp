/**
 * @jest-environment jsdom
 */

import React from "react";
import {jest, expect, test, beforeEach} from '@jest/globals';
import {render, screen} from '@testing-library/react';
import FilterPanel from "../../component/mainPage/filterPanel/FilterPanel";
import {AppContext, AppContextProvider}  from "../../contexts/AppContext";

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

type AppContextProviderValue = {
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

// const mockAppContextProvider = (children: React.ReactNode) => {
//     return <AppContext.Provider value={defaultValue}>{children}</AppContext.Provider>}
const mockAppContext = React.createContext(defaultValue);
const mockAppContextProvider = (children: React.ReactNode) => {
    return <mockAppContext.Provider value={defaultValue}>{children}</mockAppContext.Provider>};

jest.mock("../../contexts/AppContext", () => ({
    __esModule: true,
    AppContext: mockAppContext,
    AppContextProvider: ({children}: {children: React.ReactNode}) => {return mockAppContextProvider(children)}
}));

beforeEach(() => {
    render(<AppContextProvider> <FilterPanel/></AppContextProvider>);
});

test('Price from should be empty after rendering', () => {
    const priceFrom = screen.getByLabelText('priceFrom');
    expect(priceFrom.textContent).toBe('');
});
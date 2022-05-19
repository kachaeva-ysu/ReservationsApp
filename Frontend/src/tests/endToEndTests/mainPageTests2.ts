import {configData} from "../../../config";
import MainPage from './pages/MainPage';
import fetch from "node-fetch";
import * as https from "https";

const testVilla = {name: 'Test villa', description: 'Test description', priceForDay: 100, numberOfRooms: 3, hasPool: true};

const httpsAgent = new https.Agent({
    rejectUnauthorized: false,
});

const addVilla = async (villa: {name:string, description: string, priceForDay: number, numberOfRooms: number, hasPool: boolean}) => {
    const response = await fetch(configData.baseUrl + configData.villasUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(villa),
        agent: httpsAgent
    });
    return response.json();
}

const deleteVilla = async(villaId: number) => {
    await fetch(configData.baseUrl + configData.villasUrl + villaId, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        agent: httpsAgent
    });
}

let testVillaId: number;

fixture`Main page`
    .page (configData.baseClientUrl)
    .beforeEach(async () => {
        const villa = await addVilla(testVilla);
        testVillaId = villa.id;
    })
    .afterEach(async () => {
        await deleteVilla(testVillaId);
    });


test('Test villa appears', async () => {
    await MainPage.assertResourcePaneWithTitleAppears(testVilla.name);
});
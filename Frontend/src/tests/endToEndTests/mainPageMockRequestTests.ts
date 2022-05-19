import {configData} from "../../../config";
import {RequestMock} from 'testcafe';
import MainPage from "./pages/MainPage";

const mock = RequestMock().onRequestTo(configData.baseUrl+configData.villasUrl)
    .respond(undefined, 500,{ 'access-control-allow-origin': '*' });

fixture`Main page`
    .page (configData.baseClientUrl)
    .requestHooks(mock);

test('Failed to fetch villas toaster appears', async () => {
    await MainPage.assertToaster('Failed to fetch villas');
});

test('No villa appears', async () => {
    await MainPage.assertResourcesCount(0);
});

test('No matches title appears', async () => {
    await MainPage.assertNoMatchesTitle();
});
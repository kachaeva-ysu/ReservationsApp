import {configData} from "../../../config";
import MainPage from './pages/MainPage';
import dateFormatter from "../../utilities/dateFormatter";

fixture`Main page`
    .page (configData.baseClientUrl)

test('5 villas appears', async () => {
    await MainPage.assertResourcesCount(5);
});

test('1 villa appears with number of rooms to 3 filter', async () => {
    await MainPage.clickSetFilterParameters();
    await MainPage.setRoomToFilter('3');
    await MainPage.searchResources();
    await MainPage.assertRoomText('to 3');
    await MainPage.assertResourcesCount(1);
});

test('3 villas appears with has pool filter checked', async () => {
    await MainPage.clickSetFilterParameters();
    await MainPage.togglePoolFilter();
    await MainPage.searchResources();
    await MainPage.assertPoolText('Yes');
    await MainPage.assertResourcesCount(3);
});

test('5 villas appears with start date today filter', async () => {
    await MainPage.clickSetFilterParameters();
    await MainPage.setStartDateFilter(dateFormatter.getFormattedDate('yyyy-mm-dd', new Date()));
    await MainPage.searchResources();
    await MainPage.assertStartDateText(dateFormatter.getFormattedDate('yyyy-mm-dd', new Date()));
    await MainPage.assertResourcesCount(5);
});

test('No matches title appears with price for day to 100 filter', async t => {
    await MainPage.clickSetFilterParameters();
    await MainPage.setPriceToFilter('100');
    await MainPage.searchResources();
    await MainPage.assertPriceText('to 100 $')
    await MainPage.assertNoMatchesTitle();
});
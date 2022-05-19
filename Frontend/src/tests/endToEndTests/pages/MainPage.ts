import {Selector, t} from 'testcafe';

class MainPage {
    private setFilterParametersButton: Selector;
    private startDateFilter: Selector;
    private roomToFilter: Selector;
    private priceToFilter: Selector;
    private poolFilter: Selector;
    private searchButton: Selector;
    private resourcePane: Selector;
    private resourcePaneTitle: Selector;
    private startDateText: Selector;
    private roomsText: Selector;
    private priceText: Selector;
    private poolText: Selector;
    private noMatchesTitle: Selector;
    private toaster: Selector;

    constructor () {
        this.setFilterParametersButton = Selector('[data-test-button]').withText('Set filter parameters');
        this.startDateFilter = Selector("#startDate");
        this.roomToFilter = Selector('#roomTo');
        this.priceToFilter = Selector("#priceTo");
        this.poolFilter = Selector("#pool");
        this.searchButton = Selector('[data-test-button]').withText('Search');
        this.resourcePane = Selector('[data-test-resource-pane]');
        this.resourcePaneTitle = Selector('[data-test-resource-pane] [data-test-title]');
        this.startDateText = Selector('#startDate');
        this.roomsText = Selector('#rooms');
        this.priceText = Selector('#price');
        this.poolText = Selector('#pool');
        this.noMatchesTitle = Selector('[data-test-title]');
        this.toaster = Selector('[data-test-toaster]');
    }

    async clickSetFilterParameters() {
        await t.click(this.setFilterParametersButton);
    }

    async setStartDateFilter(value: string) {
        await t.typeText(this.startDateFilter, value);
        await t.pressKey('enter');
    }

    async setRoomToFilter(value: string) {
        await t.typeText(this.roomToFilter, value);
    }

    async setPriceToFilter(value: string) {
        await t.typeText(this.priceToFilter, value)
    }

    async togglePoolFilter() {
        await t.click(this.poolFilter);
    }

    async searchResources() {
        await t.click(this.searchButton);
    }

    async assertResourcesCount(expectedCount: number) {
        await t.expect(this.resourcePane.count).eql(expectedCount);
    }

    async assertStartDateText(expectedText: string) {
        await t.expect(this.startDateText.innerText).eql(expectedText);
    }

    async assertPriceText(expectedText: string) {
        await t.expect(this.priceText.innerText).eql(expectedText);
    }

    async assertRoomText(expectedText: string) {
        await t.expect(this.roomsText.innerText).eql(expectedText);
    }

    async assertPoolText(expectedText: string) {
        await t.expect(this.poolText.innerText).eql(expectedText);
    }

    async assertNoMatchesTitle() {
        await t.expect(this.noMatchesTitle.innerText).eql('No matches');
    }

    async assertToaster(value: string) {
        await t.expect(this.toaster.innerText).eql(value);
    }

    async assertResourcePaneWithTitleAppears(title: string) {
        await t.expect(this.resourcePaneTitle.withText(title).exists).ok();
    }
}

export default new MainPage();
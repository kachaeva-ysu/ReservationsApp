import Header from "../../component/shared/output/Header";
import {expect, test} from '@jest/globals';
import renderer from 'react-test-renderer';
import React from "react";

test('Should render Header component', () => {
    const header = renderer.create(<Header value='Test header'/>).toJSON();
    expect(header).toMatchSnapshot();
});

test.only('Should render Header component 2', () => {
    const header = renderer.create(<Header value='Test header' subheader={true}/>).toJSON();
    expect(header).toMatchSnapshot();
});
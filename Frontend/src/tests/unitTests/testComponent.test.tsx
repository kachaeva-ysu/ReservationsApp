/**
 * @jest-environment jsdom
 */

import React from "react";
import {expect, test, beforeEach} from '@jest/globals';
import {render, fireEvent, screen} from '@testing-library/react';
import TestComponent from "./TestComponent";

let span: HTMLElement;
let button: HTMLElement;

beforeEach(() => {
    render(<TestComponent/>);
    span = screen.getByRole('span');
    button = screen.getByRole('button');
})

test('Count should be 0 after rendering', () => {
    expect(span.textContent).toBe('0');
});

test('Should increase count after button click', () => {
    fireEvent.click(button);
    expect(span.textContent).toBe('1');
});
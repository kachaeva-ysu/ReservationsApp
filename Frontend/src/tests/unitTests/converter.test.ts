import converter from "../../utilities/converter";
import {expect, test} from '@jest/globals';

test('Converts true to yes', () => {
    expect(converter.convertBoolToString(true)).toBe('Yes');
});

test('Converts false to no', () => {
    expect(converter.convertBoolToString(false)).toBe('No');
});

test('Converts price range to text with only from value', () => {
    expect(converter.convertRangeToText(100, 0, true)).toBe('from 100 $ ');
})

test('Converts price range to text with only to value', () => {
    expect(converter.convertRangeToText(0, 500, true)).toBe('to 500 $ ');
})

test('Converts price range to text with from and to value', () => {
    expect(converter.convertRangeToText(100, 500, true)).toBe('from 100 $ to 500 $ ');
})

test('Converts room range to text with only from value', () => {
    expect(converter.convertRangeToText(3, 0)).toBe('from 3 ');
})

test('Converts room range to text with only to value', () => {
    expect(converter.convertRangeToText(0, 5)).toBe('to 5 ');
})

test('Converts room range to text with from and to value', () => {
    expect(converter.convertRangeToText(3, 5)).toBe('from 3 to 5 ');
})
import dateFormatter from "../../utilities/dateFormatter";
import {expect, test} from '@jest/globals';

test('Get unsupported date format throws an exception', () => {
    expect(() => dateFormatter.getFormattedDate('dd-mm', new Date())).toThrow('Unsupported date format');
})
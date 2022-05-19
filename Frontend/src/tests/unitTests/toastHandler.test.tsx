import toastHandler, {ToastContent} from "../../utilities/toastHandler";
import {toast} from "react-hot-toast";
import {jest, expect, test} from '@jest/globals';
import React from "react";

test('Success method calls toast', () => {
    const toastSuccessFunc = jest.spyOn(toast, 'success');
    toastHandler.success('Success!');
    expect(toastSuccessFunc).toHaveBeenCalledWith(<ToastContent value='Success!'/>);
})
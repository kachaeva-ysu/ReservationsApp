/**
 * @jest-environment jsdom
 */

import React from "react";
import {expect, test, beforeEach} from '@jest/globals';
import {render, fireEvent, screen, getAllByAltText, getAllByLabelText} from '@testing-library/react';
import ChangePasswordForm from "../../component/account/ChangePasswordForm";

const oldPassword = "OldPassword";
const onUserInfoChange = (newUserInfo: { password?: string, isPasswordBeingChanged?: boolean }) => {};

test('Old password input should be empty after rendering', () => {
    render(<ChangePasswordForm userPassword={oldPassword} onUserInfoChange={onUserInfoChange}/>);
    const oldPasswordInput = screen.getByLabelText('Old password');
    expect(oldPasswordInput.textContent).toBe('');
})
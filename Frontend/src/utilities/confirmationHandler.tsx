import React from "react";
import toastHandler from "./toastHandler";
import Button from "../component/shared/clickable/Button";
import Label from "../component/shared/output/Label";

const confirm = (message: string, onConfirm: () => void) => {
    toastHandler.info(
        <>
            <Label htmlFor='confirm' value={message} isClose={true}/>
            <Button onClick={() => handleConfirm(onConfirm)} value='Да' isDark={true}/>
            <Button onClick={() => toastHandler.dismiss()} value='Нет' isDark={true}/>
        </>
    )
}

const handleConfirm = (onConfirm: () => void) => {
    onConfirm();
    toastHandler.dismiss();
}

export default {
    confirm
}
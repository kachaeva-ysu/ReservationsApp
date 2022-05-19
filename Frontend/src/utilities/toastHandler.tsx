import React, {ReactElement} from "react";
import {toast} from "react-hot-toast";

type ToastContentProps = {
    value: ReactElement | string
}
export const ToastContent = ({value}: ToastContentProps)=> <div data-test-toaster>{value}</div>;

const error = (value: string) => {
    toast.error(<ToastContent value={value}/>);
}

const success = (value: string) => {
    toast.success(<ToastContent value={value}/>);
}

const info = (value: ReactElement | string, duration?: number) => {
    toast(<ToastContent value={value}/>, {duration: duration});
}

const dismiss = () => {
    toast.dismiss();
}

export default {
    ToastContent,
    error,
    success,
    info,
    dismiss
}




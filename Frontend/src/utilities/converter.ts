import {configData} from "../../config";

const convertBoolToString = (value: boolean): string => {
    if (value) {
        return 'Yes';
    }
    return 'No';
}

const convertRangeToText = (from: number, to: number, isPrice?: boolean): string => {
    let text = '';
    if (from) {
        text += `from ${from} `;
        if (isPrice) {
            text += '$ ';
        }
    }
    if (to) {
        text += `to ${to} `;
        if (isPrice) {
            text += '$ ';
        }
    }
    return text;
}

const convertBytesToImageSource = (bytes: string): string => {
    return configData.imageSource+bytes;
}

export default {
    convertBoolToString,
    convertRangeToText,
    convertBytesToImageSource
}
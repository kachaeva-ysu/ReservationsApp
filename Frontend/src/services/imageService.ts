import {configData} from "../../config";

const getImage = async (villaId: number) => {
    const response = await fetch(configData.baseUrl + `staticFiles/villa${villaId}.jpg`, {
        method: 'GET',
        headers: {
            //'Content-Type': 'application/json',
        }
    });
    const blob = await response.blob();
    const urlCreator = window.URL || window.webkitURL;
    const imageUrl = urlCreator.createObjectURL(blob);
    return imageUrl;
}

export default {
    getImage
}
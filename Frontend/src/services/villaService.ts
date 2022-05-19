import baseService from "./baseService";
import {configData} from "../../config";
import converter from "../utilities/converter";

const villasUrl = configData.villasUrl;

const getVillas = async (villaInfo: string) => {
    return baseService.baseFetch(villasUrl + villaInfo);
}

const getVillaDetails = async(villaId: number) => {
    return baseService.baseFetch(villasUrl+villaId);
}

// const getVillaImage = async (villaId: number) => {
//     const bytes = await baseService.baseFetch(configData.villaImagesUrl + villaId);
//     return converter.convertBytesToImageSource(bytes);
// }

const addVilla = async(villa: {name: string, description: string, priceForDay: number, numberOfRooms: number, hasPool: boolean}) => {
    return baseService.baseFetch(villasUrl, 'POST', JSON.stringify(villa));
}

const deleteVilla = async(villaId: number) => {
    return baseService.baseFetch(villasUrl+villaId, 'DELETE');
}

export default {
    getVillas,
    getVillaDetails,
    //getVillaImage,
    addVilla,
    deleteVilla
}
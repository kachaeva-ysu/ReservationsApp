import React from "react";
import s from "./Image.css";

type ImageProps = {
    imageSource: string,
    isBig?: boolean
}

const Image = ({imageSource, isBig}: ImageProps) => {
    return (
        <img className={isBig ? s.bigImg : s.img} src={imageSource} alt='villa image'/>
    )
}

export default Image;
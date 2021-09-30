import "./ImagesGallery.css";
import React from 'react';
import { nanoid } from 'nanoid';
import { useState, useEffect } from "react";
import imagesIndicatorUrl from "../../images/icons/images-gallery-indicator.svg";
import { cacheImages } from "../../shared-funcs";

var imagesUrls = [];
var indicator;
var activeIndicator;
var indicatorsKeys = new Array(3);
indicatorsKeys.forEach(key => key = nanoid(1));

export default function ImagesGallery(props) {
  //#region State
  const [indexShowedImage, setIndexShowedImage] = useState(-1);
  const indicatorsShowedImage = updateIndicators(indicator,
    activeIndicator, imagesUrls.length, indexShowedImage);
  //#endregion
  //#region Effects
  useEffect(() => {
    cacheImages(...props.imagesUrls, imagesIndicatorUrl);
    imagesUrls = [...props.imagesUrls];
    setIndexShowedImage(0);//Чтобы запустить ререндеринг в первый раз и
    //заполнить showedImage, indicatorsShowedImage
    indicator = (<img className="images-gallery__indicator" src={imagesIndicatorUrl} />)
    activeIndicator = (<img className="images-gallery__indicator images-gallery__indicator_active"
      src={imagesIndicatorUrl} />)
  }, []);
  //#endregion
  return (
    <div className="images-gallery">
      <div className="images-gallery__container-image">
        <div className="images-gallery__shadow-over-image" onClick={
          onClickShowNewImage}></div>
        <img className="images-gallery__image" src={imagesUrls[
          indexShowedImage]}></img>
      </div>
      <div className="images-gallery__indicators">
        {indicatorsShowedImage}
      </div>
    </div>
  );
  //#region Other, using state
  function onClickShowNewImage(e) {
    if (e.pageX < e.target.offsetWidth / 2) {
      if (indexShowedImage > 0) {
        setIndexShowedImage(indexShowedImage - 1);
      } else return;
    } else {
      if (indexShowedImage < imagesUrls.length - 1) {
        setIndexShowedImage(indexShowedImage + 1);
      } else return;
    }
    return;
  }
  //#endregion
}
//#region Other, not using state
function cloneREWithKey(indicator, keY) {
  return React.cloneElement(indicator, { key: keY })
}
function updateIndicators(indicator, activeIndicator, numberImages,
  indexShowedImage) {
  let outputIndicators = [];

  if (numberImages > 0) {
    if (indexShowedImage == 0) {
      outputIndicators.push(cloneREWithKey(activeIndicator, 1));
    } else {
      outputIndicators.push(cloneREWithKey(indicator, 1));
    }
  }
  if (numberImages > 1) {
    if ((indexShowedImage == numberImages - 1) && (numberImages == 2)) {
      outputIndicators.push(cloneREWithKey(activeIndicator, 2));
    } else if ((indexShowedImage > 0) && (indexShowedImage <
      numberImages - 1)) {
      outputIndicators.push(cloneREWithKey(activeIndicator, 2));
    } else {
      outputIndicators.push(cloneREWithKey(indicator, 2));
    }
  }
  if (numberImages > 2) {
    if (indexShowedImage == numberImages - 1) {
      outputIndicators.push(cloneREWithKey(activeIndicator, 3));
    } else {
      outputIndicators.push(cloneREWithKey(indicator, 3));
    }
  }
  return outputIndicators;
}
//#endregion
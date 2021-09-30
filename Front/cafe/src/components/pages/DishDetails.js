import "./DishDetails.css";
import { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { generateImagesUrls, random } from "../../shared-funcs";
import { isEqual } from "lodash";
import CafeAPI from "../../CafeAPI";
import StatusCodeService from "../../StatusCodeService"

export default function DishDetails() {
  const dishId = (new URLSearchParams(useLocation().search)).get("dishId");
  //#region State
  const [dishDetails, setDishDetails] =
    useState({
      dishId: null,
      name: null,
      weight: null,
      cost: null,
      description: null,
      calorie: null,
      categories: [],
    });
  //#endregion
  //#region Effects
  useEffect(() => refreshDishDetails(dishId), [])
  //#endregion
  return (
    <div className="dish-details">
      <p className="dish-details__title dish-details__text">
        {dishDetails?.name}
      </p>
      <img src={imagesDishesUrls[dishDetails?.name]}
        className="dish-details__image" />
      <div className="dish-details__description">
        <p className="dish-details__dish-description dish-details__text">
          {dishDetails?.description}
        </p>
        <p className="dish-details__dish-weight dish-details__text">
          {dishDetails?.weight} грамм
        </p>
        <p className="dish-details__dish-calorie dish-details__text">
          {dishDetails?.calorie} ккал.
        </p>
        <p className="dish-details__dish-cost dish-details__text">
          {dishDetails?.cost} р.
        </p>
        <p className="dish-details__dish-catogories
          dish-details__text">
          Входит в категории:      {dishDetails?.categories?.join(",       ")}
        </p>
      </div>
    </div>
  );
  //#region Refresh
  function refreshDishDetails(dishId) {
    new CafeAPI().menu.getDishDetails(dishId, (error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => setDishDetails(prevDishDetails =>
          isEqual(prevDishDetails, data) ? prevDishDetails : data))
        .if([400], response, null)
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
  }
  //#endregion
}
//#region Other, not using state
const imagesDishesUrls = generateImagesUrls(
  require.context('../../images/menu/dishes-images/big',
    false, /\.(png|jpe?g|svg)$/));
//#endregion
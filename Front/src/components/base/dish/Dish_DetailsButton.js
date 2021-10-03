import "./Dish_DetailsButton.css";
import { NavLink } from "react-router-dom";
import urls from "../../../urls";

import Dish from "./Dish";

export default function Dish_DetailsButton(props) {
  return (
    <div className="dish-details-button">
      <div className="dish-details-button__dish">
        <Dish dishData={props.dish} />
      </div>
      <NavLink className="dish-details-button__details-button"
        to={urls.dishDetails + "?dishId=" + props?.dish?.dishId}>
        <div className="dish-details-button__details-button-icon"></div>
      </NavLink>
    </div>
  );
}
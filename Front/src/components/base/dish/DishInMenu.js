import "./DishInMenu.css";

import Dish_DetailsButton from "./Dish_DetailsButton";

export default function DishInMenu(props) {
  const dishInMenu = (
    <div className="dish-in-menu">
      <img className="dish-in-menu__image"
        src={props.imgSrc} />
      <div className="dish-in-menu__dish-details-button">
        <Dish_DetailsButton dish={props.dish} />
      </div>
    </div>
  );
  return dishInMenu;
}


  
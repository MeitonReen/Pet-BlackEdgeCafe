import "./DishInMenuDishes.css";
import Dish_DetailsButton from "../../Dish_DetailsButton";

export default function DishInMenuDishes(props) {
  return (
    <div className="dish-in-menu-dishes">
      <img className="dish-in-menu-dishes__image"
        src={props.dish.imageUrl} />
      <div className="dish-in-menu-dishes__dish-details-button">
        <Dish_DetailsButton dish={props.dish} />
      </div>
    </div>
  );
}
import "./DishInOrderPage.css";

import Dish_DetailsButton from "./Dish_DetailsButton";
import OperationsOnDishInCart from "../OperationsOnDishInCart";

export default function DishInOrderPage(props) {
  return (
    <div className="dish-in-order-page">
      <img className="dish-in-order-page__dish-image
        dish-in-order-page__dish-image_operation-add"
        onClick = {() => props.operations.add(props.dish.dishId)}
        src={props.imgSrc} />
      <div className="dish-in-order-page__operations">
        <OperationsOnDishInCart operations={props.operations}
          dish={props.dish} quantityOfThisDishInCart={
            props.quantityOfThisDishInCart}
        />
      </div>
      <div className="dish-in-order-page__dish-details-button">
        <Dish_DetailsButton dish={props.dish} />
      </div>
    </div>
  );
}

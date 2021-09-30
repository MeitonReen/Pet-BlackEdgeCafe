import "./DishInCart.css";

import Dish_DetailsButton from "./Dish_DetailsButton";
import OperationsOnDishInCart from "../OperationsOnDishInCart";

export default function DishInCart(props) {
  return (
    <div className="dish-in-cart">
      <div className="dish-in-cart__operations">
        <OperationsOnDishInCart
          operations={props.operations}
          dish={props.dish}
          quantityOfThisDishInCart={props.quantityOfThisDishInCart}
        />
      </div>
      <div className="dish-in-cart__dish-details-button">
        <Dish_DetailsButton dish={props.dish} />
      </div>
    </div>
  );
}
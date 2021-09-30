import "./DishInMyOrdersPage.css";

import Dish_DetailsButton from "./Dish_DetailsButton";

export default function DishInMyOrdersPage(props) {
  return (
    <div className="dish-in-my-order-page">
      <div className="dish-in-my-order-page__quantity">
        x{props?.quantityOfThisDishInOrder}
      </div>
      <div className="dish-in-my-order-page__dish-details-button">
        <Dish_DetailsButton dish={props?.dish} />
      </div>
    </div>
  );
}
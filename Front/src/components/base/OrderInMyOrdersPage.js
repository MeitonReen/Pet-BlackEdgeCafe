import "./OrderInMyOrdersPage.css";
import { wordEndingOnNumber, dotsToCommas } from "../../shared-funcs";

export default function OrderInMyOrdersPage(props) {
  //#region PrepareJSX
  let discountOnPromocodes = ((props?.order?.amount !==
    props?.order?.amountIncludingPromocodes) ? (
    <p className="order-in-my-order-page__discount-on-promocodes
        order-in-my-order-page__text">
      скидка по промокодам:        {dotsToCommas((props?.order?.amount -
      props?.order?.amountIncludingPromocodes)?.toString())} р.
    </p>
  ) : null);
  //#endregion
  return (
    <div className="order-in-my-order-page"
      onClick={() => props?.operationOnClick(props?.order)}>
      <p className="order-in-my-order-page__order-number
        order-in-my-order-page__text">
        Заказ №    {props?.order?.orderId.slice(0, 8)}
      </p>
      <p className="order-in-my-order-page__quantity-dishes
        order-in-my-order-page__text">
        из {props?.order?.quantityDishes} блюд{wordEndingOnNumber(
        props?.order?.quantityDishes)}
      </p>
      <p className="order-in-my-order-page__amount
        order-in-my-order-page__text">
        итоговой стоимостью:        {dotsToCommas(
        props?.order?.amount?.toString())} р.
      </p>
      {discountOnPromocodes}
    </div>
  );
}
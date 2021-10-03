import "./MiniCart.css";
import { Link } from "react-router-dom";
import { dotsToCommas } from "../../shared-funcs";
import urls from "../../urls";

export default function MiniCart(props) {
  let addStrikethroughClassName = null;
  let amountIncludingPromocodes = null;

  if (props?.amountIncludingPromocodes &&
    props?.amount != props?.amountIncludingPromocodes) {

    addStrikethroughClassName = " mini-cart__item_strikethrough";

    amountIncludingPromocodes = (
      <p className="mini-cart__text 
        mini-cart__amount-cart-including-promocodes">
        {dotsToCommas(props.amountIncludingPromocodes)} р.</p>
    );
  }
  return (
    <div className="mini-cart">
      <p className="mini-cart__quantity-dishes-in-cart
        mini-cart__text">
        {props.quantity}
      </p>
      <Link className="mini-cart__link-to-cart"
        to={urls.cart}>
        <div className="mini-cart__icon"></div>
      </Link>
      <p className={"mini-cart__amount-cart mini-cart__text " +
        addStrikethroughClassName}>
        {props.amount} р.
      </p>
      {amountIncludingPromocodes}
    </div>
  );
}
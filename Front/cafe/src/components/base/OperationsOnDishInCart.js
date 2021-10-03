import "./OperationsOnDishInCart.css";

export default function OperationsOnDishInCart(props) {
  //#region PrepareJSX
  const operationAdd = (<div
    className="operations-on-dish-in-cart__operation-add
      operations-on-dish-in-cart__operation"
    onClick={() => props.operations.add(props.dish.dishId)} />);

  const operationDelete = (<div
    className="operations-on-dish-in-cart__operation-delete
      operations-on-dish-in-cart__operation"
    onClick={() => props.operations.delete(props.dish.dishId)} />);

  const operationDeleteAllById = (<div
    className="operations-on-dish-in-cart__operation-delete-all
      operations-on-dish-in-cart__operation"
    onClick={() => props.operations.deleteAllById(
      props.dish.dishId)} />);
  const countDishes = (
    <div className="operations-on-dish-in-cart__count-dishes">
      x{props.quantityOfThisDishInCart}
    </div>);
  //#endregion
  return (
    props.quantityOfThisDishInCart > 0) ?
    (<div className="operations-on-dish-in-cart">
      {operationAdd}
      {operationDelete}
      {operationDeleteAllById}
      {countDishes}
    </div>) : (<div className="operations-on-dish-in-cart">
      {operationAdd}
    </div>);
}
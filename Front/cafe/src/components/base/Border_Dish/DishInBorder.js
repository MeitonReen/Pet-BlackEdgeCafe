import BorderRectangle from "../BorderRectangle/BorderRectangle";
import Dish from "../Dish/Dish"

export default function DishWithBorder(props) {
  return (
    <BorderRectangle>
      <Dish Dish = {props.DishWithBorder}/>
    </BorderRectangle>
  );
}
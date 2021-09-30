import DishWithDetails from "../DishWithDetails/DishWithDetails";
import BorderRectangle from "../BorderRectangle/BorderRectangle";

export default function DishWithDetailsWithBorder(props) {
  return (
    <BorderRectangle>
      <DishWithDetails DishWithDetails = {props.DishWithDetailsWithBorder}/>
    </BorderRectangle>
  );
}
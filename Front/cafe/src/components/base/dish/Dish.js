import "./Dish.css";
import {dotsToCommas} from "../../../shared-funcs";

export default function Dish(props) {
  let addStrikethroughClassName = null;
  let costIncludingPromocodes = <p></p>;

  if (props?.dishData?.costIncludingPromocodes &&
    props?.dishData?.costIncludingPromocodes != props?.dishData?.cost) {

    addStrikethroughClassName = " dish__item_strikethrough";

    costIncludingPromocodes = (
      <p className="dish__item dish__item_cost-including-promocodes">
        {dotsToCommas(props?.dishData?.costIncludingPromocodes)}р.</p>
    );
  }
  return (
    <div className="dish">
      <p className="dish__item dish__item_name">{props.dishData?.name}</p>
      <p className="dish__item dish__item_weight">{props.dishData?.weight}г.</p>
      <p className={"dish__item dish__item_cost" +
        addStrikethroughClassName}>{props?.dishData?.cost}р.</p>
      {costIncludingPromocodes}
    </div>
  );
}
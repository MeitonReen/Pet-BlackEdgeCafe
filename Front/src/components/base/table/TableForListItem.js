import "./TableForListItem.css";
import { wordEndingOnNumber } from "../../../shared-funcs";

export default function TableForListItem(props) {
  return (
    <div className="table-for-list-item">
      <p className="table-for-list-item__table-number
        table-for-list-item__text">
        столик № {props?.table?.tableNumber}
      </p>
      <p className="table-for-list-item__number-of-seats
        table-for-list-item__text">
        за столиком {props?.table?.numberOfSeats} мест
          {wordEndingOnNumber(props?.table?.numberOfSeats)}
      </p>
    </div>
  );
}
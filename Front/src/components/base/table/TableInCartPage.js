import "./TableInCartPage.css";
import TableForListItem from "./TableForListItem";

export default function TableInCartPage(props) {
  return (
    <div className="table-in-cart-page"
      onClick={() => props?.operationOnClick(props?.table?.tableId)}>
      <TableForListItem table={props?.table}/>
    </div>
  );
}
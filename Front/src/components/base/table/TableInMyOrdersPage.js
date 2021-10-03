import "./TableInMyOrdersPage.css";
import TableForListItem from "./TableForListItem";

export default function TableInMyOrdersPage(props) {
  return (
    <div className="table-in-my-orders-page"
      onClick={() => props?.operationOnClick(
        props?.ordersOnTable)
      }>
      <TableForListItem table={props?.ordersOnTable?.table} />
    </div>
  );
}
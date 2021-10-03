import "./TableInCreateAnOrderPage.css";
import TableForListItem from "./TableForListItem";

export default function TableInCreateAnOrderPage(props) {
  return (
    <div className="table-in-create-an-order-page"
      onClick={() => props?.operationOnClick(props?.table?.tableId)}>
      <TableForListItem table={props?.table}/>
    </div>
  );
}
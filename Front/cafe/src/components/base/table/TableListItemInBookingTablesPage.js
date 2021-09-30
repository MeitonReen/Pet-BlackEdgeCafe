import "./TableListItemInBookingTablesPage.css";
import TableForListItem from "./TableForListItem";

export default function TableListItemInBookingTablesPage(props) {
  return (
    <div className="table-list-item-in-booking-tables-page">
      <TableForListItem table={props?.table}/>
    </div>
  );
}
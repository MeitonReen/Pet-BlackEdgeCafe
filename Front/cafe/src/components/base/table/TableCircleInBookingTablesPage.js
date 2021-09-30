import "./TableCircleInBookingTablesPage.css";
import { wordEndingOnNumber } from "../../../shared-funcs";

export default function TableCircleInBookingTablesPage(props) {
  let decorateBooked = props.isBooked ?
    "table-in-booking-tables-page_is-booked" :
    "table-in-booking-tables-page_is-not-booked";

    decorateBooked += props.isBookedByClient ?
    " table-in-booking-tables-page_is-booked-by-client" : "";
    
  const markBookedTable = props.isBooked ? (
    <p className="table-in-booking-table-page__mark-booked-table">
      Занят
    </p>
  ) : null;

  return (
    <div className="table-in-booking-tables-page"
      onClick={() => operationExecute()}>
      <div className={"table-in-booking-tables-page__border-circle " + decorateBooked}>
        <div className="table-in-booking-tables-page__container-text">
          {markBookedTable}
          <p className="table-in-booking-tables-page__table-number
          table-in-booking-tables-page__text">
            № {props.table.tableNumber}
          </p>
          <p className="table-in-booking-tables-page__number-of-seats
          table-in-booking-tables-page__text">
            {props.table.numberOfSeats} мест{wordEndingOnNumber(props.table.numberOfSeats)}
          </p>
        </div>
      </div>
    </div>
  );
  function operationExecute() {
    if (props.isBookedByClient) {
      return props.operations.unbookATable(props.table.tableId);
    }
    if (!props.isBooked) {
      return props.operations.bookATable(props.table.tableId);
    }
    return;
  }
}


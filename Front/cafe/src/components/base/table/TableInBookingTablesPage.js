import "./TableInBookingTablesPage.css";

export default function TableInBookingTablesPage(props) {
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
    <div className={"table-in-booking-tables-page "+
    decorateBooked}
      onClick={() => operationExecute()}>
      <img className=
        "table-in-booking-tables-page__table-image"
        src={props.tableImageSrc}/>

      <p className="table-in-booking-tables-page__table-number
          ">
        № {props.table.tableNumber}
      </p>
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


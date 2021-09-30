import "./BookingTablesPage.css";
import urls from "../../urls";
import { Redirect } from "react-router-dom";
import { useEffect, useState } from "react";
import { isEqual } from "lodash";
import { generateImagesUrls } from "../../shared-funcs";
import StatusCodeService from "../../StatusCodeService"
import CafeAPI from "../../CafeAPI"

import DefaultPageWithMenu from "../base/DefaultPageWithMenu";
import TableListItemInBookingTablesPage from "../base/table/TableListItemInBookingTablesPage";
import List from "../base/List";
import BookingTables from "../base/BookingTables";

export default function BookingTablesPage(props) {
  //#region State
  const [redirectToAuthentification, setRedirectToAuthentification] =
    useState(false);
  const [tables, setTables] = useState([]);
  const [bookedTableIds, setBookedTableIds] = useState([{
    tableId: null,
    isBookedByClient: false,
    bookedTableSeconds: 0
  }]);
  const [bookedTablesData, setBookedTablesData] = useState({});
  //#endregion
  //#region Effects
  useEffect(() => {
    refreshTables();
    refreshBookedTableIds();

    const globalDecrementTimer = setInterval(() => {
      setBookedTablesData(prevBookedTablesData => {

        let newBookedTablesData = {};
        Object.entries(prevBookedTablesData).forEach(([tableNumber,
          prevBookedTableData]) => {
          let newBookedTableData = {...prevBookedTableData};

          if (newBookedTableData.bookedTableSeconds > 0) {
            newBookedTableData.bookedTableSeconds -= 1;
          }
          if (newBookedTableData.isBooked &&
            newBookedTableData.bookedTableSeconds == 0) {
            refreshBookedTableIds();
          }
          newBookedTablesData[tableNumber] = newBookedTableData;
        });
        return newBookedTablesData;
      });
    }, 1000);

    const refreshBookedTableIdsTimer = setInterval(() =>
      refreshBookedTableIds(), 10000);

    return () => {
      clearInterval(globalDecrementTimer);
      clearInterval(refreshBookedTableIdsTimer);
    }
  }, []);

  useEffect(() => {
    const newBookedTablesData = {};
    tables?.forEach(table => {
      if (!table?.tableId || !table?.tableNumber) {
        return null;
      }
      const tableIsBooked = bookedTableIds.find(bookedTableId =>
        bookedTableId.tableId === table.tableId);

      newBookedTablesData[table.tableNumber] = {
        tableId: table.tableId,
        isBooked: tableIsBooked != undefined ? true : false,
        isBookedByClient: tableIsBooked?.isBookedByClient ?? false,
        bookedTableSeconds: tableIsBooked?.bookedTableSeconds ?? 0
      }
    });

    setBookedTablesData(prevBookedTablesData => isEqual(prevBookedTablesData,
      newBookedTablesData) ? prevBookedTablesData : newBookedTablesData);
  }, [tables, bookedTableIds]);
  //#endregion
  //#region Prepare JSX
  const bookedTablesByClientAsJSX = bookedTableIds
    .filter(bookedTableId =>
      bookedTableId.isBookedByClient && bookedTableId.tableId)
    .map(bookedTableIdsByClient => {
      const bookedTableByClient = tables?.find(table =>
        table.tableId === bookedTableIdsByClient.tableId);

      return bookedTableByClient?.tableId ?
        (<TableListItemInBookingTablesPage key={bookedTableByClient.tableId}
          table={bookedTableByClient} />) : null
    });

  const body = (
    <div className="booking-tables-page__body">
      <div className="booking-tables-page__main">
        <BookingTables bookATable={bookATable}
          unbookATable={unbookATable}
          bookedTablesData={bookedTablesData}
        />

      </div>
      <div className="booking-tables-page__aside">
        <div className="booking-tables-page__booked-tables">
          <List title="Забронированные столики:"
            itemsLayout="flex-column-nowrap">

            {bookedTablesByClientAsJSX}
          </List>
        </div>
      </div>
    </div>
  );
  const redirectUrl = "?redirectUrl=" + urls.bookindTables;
  //#endregion
  return (
    redirectToAuthentification ? (
      <Redirect to={urls.login + redirectUrl} />
    ) : (
      <div className="booking-tables-page">
        <DefaultPageWithMenu body={body} isLogin={props.isLogin}
          setIsLogin={props.setIsLogin} />
      </div>
    )
  );
  //#region Actions
  function bookATable(tableId) {
    if (tableId) {
      new CafeAPI().bookedTables.bookATable(tableId, (error, data, response) => {
        new StatusCodeService()
          .if([200], response, () => refreshBookedTableIds())
          .if([400], response, null)
          .if([401], response, () => setRedirectToAuthentification(true))
          .if([500], response, null)
          .if([], error, null)
          .if([], response, null);
      });
    }
  }
  function unbookATable(tableId) {
    if (tableId) {
      new CafeAPI().bookedTables.unbookATable(tableId, (error, data, response) => {
        new StatusCodeService()
          .if([200], response, refreshBookedTableIds())
          .if([400], response, null)
          .if([401], response, () => setRedirectToAuthentification(true))
          .if([500], response, null)
          .if([], error, null)
          .if([], response, null);
      });
    }
    return;
  }
  //#endregion
  //#region Refresh 
  function refreshTables() {
    new CafeAPI().tables.getTables((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => setTables(prevTables => isEqual(
          prevTables, data) ? prevTables : data))
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
    return;
  }
  function refreshBookedTableIds() {
    new CafeAPI().bookedTables
      .getBookedTablesIdsWithMarksIsBookedByClient((error, data,
        response) => {
        new StatusCodeService()
          .if([200], response, () => setBookedTableIds(prevTableIds =>
            isEqual(prevTableIds, data) ? prevTableIds : data))
          .if([401], response, () => setRedirectToAuthentification(true))
          .if([500], response, null)
          .if([], error, null)
          .if([], response, null);
      });
    return;
  }
  //#endregion
}
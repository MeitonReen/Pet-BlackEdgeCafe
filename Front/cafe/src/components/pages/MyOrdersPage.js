import "./MyOrdersPage.css";
import DefaultPageWithMenu from "../base/DefaultPageWithMenu";
import List from "../base/List";
import DishInMyOrdersPage from "../base/dish/DishInMyOrdersPage";
import OrderInMyOrderPage from "../base/OrderInMyOrdersPage";
import TableInMyOrdersPage from "../base/table/TableInMyOrdersPage";

import { useEffect, useState } from "react";
import { Redirect } from "react-router-dom";
import { isEqual } from "lodash";
import urls from "../../urls";
import CafeAPI from "../../CafeAPI";
import StatusCodeService from "../../StatusCodeService"

export default function MyOrdersPage(props) {
  //#region State
  const [redirectToAuthentification, setRedirectToAuthentification] =
    useState(false);

  const [ordersOnTables, setOrdersOnTables] = useState([
    {
      table: {
        tableId: null,
        tableNumber: null,
        NumberOfSeats: null,
      },
      orders: [
        {
          orderId: null,
          quantityDishes: null,
          Amount: null,
          AmountIncludingPromocodes: null,
          cookingStatus: null,
          dishesGroupedByDishId: [
            {
              dishId: null,
              name: null,
              weight: null,
              cost: null,
              quantity: null
            }
          ]
        }
      ]
    }]);
  const [selectedOrdersOnTable, selectOrdersOnTable] = useState(null);
  const [selectedOrder, selectOrder] = useState(null);
  //#endregion 
  //#region Effects
  useEffect(() => {
    refreshOrdersOnTables();
  }, []);
  useEffect(() => {

    const newSelectedOrdersOnTable = ordersOnTables?.find(
      ordersOnTable => ordersOnTable?.table?.tableId !=
        undefined && ordersOnTable?.table?.tableId != null);

    if (!selectedOrdersOnTable) {
      selectOrdersOnTable(newSelectedOrdersOnTable);
    }

  }, [ordersOnTables]);
  useEffect(() => {
    selectOrder(selectedOrdersOnTable?.orders?.find(orderOnTable =>
      orderOnTable?.orderId != undefined && orderOnTable?.orderId != null));
  }, [selectedOrdersOnTable])
  //#endregion
  //#region Prepare JSX
  //#region PrepareListItems
  const tablesAsJSX = ordersOnTables?.map(ordersOnTable => (
    <TableInMyOrdersPage key={ordersOnTable.table.tableId}
      ordersOnTable={ordersOnTable}
      operationOnClick={selectOrdersOnTable}
    />
  ));
  const ordersOnSelectedTableAsJSX = selectedOrdersOnTable?.orders?.map(
    order => (
      <OrderInMyOrderPage key={order.orderId} order={order}
        operationOnClick={selectOrder}
      />
    ));
  const dishesInSelectedOrderAsJSX = selectedOrder?.dishesGroupedByDishId
    ?.map(dish => (
      <DishInMyOrdersPage key={dish.dishId} dish={dish}
        quantityOfThisDishInOrder={dish.quantity}
      />
    ));
  //#endregion
  //#region PrepareLists
  const tablesList = selectedOrdersOnTable ? (
    <List title={"Выберите столик:"}
      selectedItemByKey={selectedOrdersOnTable?.table?.tableId}
      simpleHovered
      sortByKeyDefault
      itemsLayout="flex-column-nowrap">

      {tablesAsJSX}
    </List>
  ) : null;
  const ordersOnSelectedTableList = selectedOrder ? (
    <List title={"Заказы на столик № " +
      selectedOrdersOnTable?.table?.tableNumber + ":"}
      selectedItemByKey={selectedOrder?.orderId}
      simpleHovered
      sortByKeyDefault
      itemsLayout="flex-column-nowrap">

      {ordersOnSelectedTableAsJSX}
    </List>
  ) : null;
  const dishesInSelectedOrder = selectedOrder ? (
    <List title={"Блюда в заказе № " + selectedOrder?.orderId?.slice(0, 8)
      + ":"}
      sortByKeyDefault
      itemsLayout="flex-column-nowrap">

      {dishesInSelectedOrderAsJSX}
    </List>
  ) : null;
  //#endregion
  const body = selectedOrdersOnTable ? (
    <div className="my-orders-page__body">
      <div className="my-orders-page__dishes-in-select-order">
        {dishesInSelectedOrder}
      </div>
      <div className="my-orders-page__orders-on-select-table">
        {ordersOnSelectedTableList}
      </div>
      <div className="my-orders-page__tables">
        {tablesList}
      </div>
    </div>
  ) : (
    <p className="my-orders-page__title-if-empty">
      Заказов пока нет
    </p>);
  const redirectUrl = "?redirectUrl=" + urls.createAnOrder;
  //#endregion
  return (
    redirectToAuthentification ? (
      <Redirect to={urls.login + redirectUrl} />
    ) : (
      <div className="my-orders-page">
        <DefaultPageWithMenu body={body} isLogin={props.isLogin}
          setIsLogin={props.setIsLogin} />
      </div>
    )
  );
  //#region Refresh
  function refreshOrdersOnTables() {
    new CafeAPI().orders.getOrdersOnTables((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => {
          if (!isEqual(ordersOnTables, data)) {
            setOrdersOnTables(data);
          }
        })
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
  }
  //#endregion
}

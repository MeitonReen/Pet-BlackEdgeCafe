import "./CartPage.css";
import urls from "../../urls";
import { useEffect, useState } from "react";
import { Redirect, Switch, Route } from "react-router-dom";
import { includes, isEqual } from "lodash";
import {
  wordEndingOnNumber,
  compareByKeyDefault,
  dotsToCommas,
} from "../../shared-funcs";
import CafeAPI from "../../CafeAPI"
import StatusCodeService from "../../StatusCodeService"

import DefaultPageWithMenu from "../base/DefaultPageWithMenu";
import DishInCart from "../base/dish/DishInCart";
import TableInCartPage from "../base/table/TableInCartPage";
import List from "../base/List";

export default function CartPage(props) {
  //#region State
  const [redirectToAuthentification, setRedirectToAuthentification] =
    useState(false);
  const [redirectToIndex, setRedirectToIndex] = useState(false);
  const [cartState, setCartState] = useState({
    table: {
      tableId: null,
      tableNumber: null,
      numberOfSeats: null,
    },
    amount: null,
    amountIncludingPromocodes: null,
    quantityDishes: null,
    dishesGroupedByDishId: [
      {
        dishId: null,
        name: null,
        weight: null,
        cost: null,
        costIncludingPromocodes: null,
        quantity: null,
      },
    ],
  });
  const [bookedTables, setBookedTables] = useState([]);
  const [selectedTableIdInCart, setSelectedTableIdInCart] = useState(null);
  const [promocode, setPromocode] = useState(promocodeSpecStrs.initial);
  const [promocodeAccepted, setPromocodeAccepted] = useState(-1);
  const [cookingStatus, setCookingStatus] = useState(false);
  //#endregion
  //#region Effects
  useEffect(() => {
    refreshBookedTables();
    refreshCookingStatus();
    refreshSelectedTableIdInCart();
  }, []);
  useEffect(() => {
    refreshCartState();
  }, [selectedTableIdInCart, promocodeAccepted]);
  //#endregion
  //#region Prepare JSX
  const dishesListItemsAsJSX = !Array.isArray(
    cartState.dishesGroupedByDishId
  )
    ? undefined
    : cartState.dishesGroupedByDishId
      .map((dish) => {
        const dishAsJSX = (
          <DishInCart
            key={dish.dishId}
            dish={dish}
            operations={{
              add: addDish,
              delete: deleteDish,
              deleteAllById: deleteDishesByDishId,
            }}
            quantityOfThisDishInCart={dish.quantity}
          />
        );
        return dish.quantity === 0 ? (
          <div
            className="cart-page__dishes-list-item
            cart-page__list-item-through-line"
            key={dishAsJSX.key}
          >
            {dishAsJSX}
          </div>
        ) : (
          <div className="cart-page__dishes-list-item" key={dishAsJSX.key}>
            {dishAsJSX}
          </div>
        );
      })
      .sort(compareByKeyDefault);

  const decoratePromocodeAsClassName =
    promocodeAccepted === 1
      ? "cart-page__promocode_accepted"
      : promocodeAccepted === 0
        ? "cart-page__promocode_denied"
        : "";
  const promocodeFormAsJSX = (
    <form
      className="cart-page__promocode"
      onSubmit={(e) =>
        addPromocode(
          e,
          promocode,
          setPromocode,
          setPromocodeAccepted,
          setRedirectToAuthentification
        )
      }
    >
      <input
        className={
          "cart-page__promocode-input input " + decoratePromocodeAsClassName
        }
        value={promocode}
        onFocus={(e) => clearInput(e, setPromocode, setPromocodeAccepted)}
        onBlur={(e) => inputPromocodeSetDefault(e, setPromocode)}
        onChange={(e) => setPromocode(e.target.value)}
        type="text"
      />
      <button
        className="cart-page__promocode-check-button
              button"
        type="submit"
      >
        Проверить
      </button>
    </form>
  );
  const cartTotalText =
    "Итог:   " +
    cartState.quantityDishes +
    " блюд" +
    wordEndingOnNumber(cartState.quantityDishes) +
    (cartState?.amountIncludingPromocodes &&
      cartState?.amountIncludingPromocodes != cartState?.amount
      ? "  на  сумму:   " +
      dotsToCommas(cartState.amountIncludingPromocodes) +
      " р." +
      " ,\n\nскидка по промокодам:   " +
      dotsToCommas((cartState.amount - cartState
        .amountIncludingPromocodes)) +
      " р."
      : "  на  сумму:  " +
      dotsToCommas(cartState.amount) +
      " р.");

  const bookedTablesAsJSX = bookedTables.map((bookedTable) => (
    <TableInCartPage
      key={bookedTable.tableId}
      table={bookedTable}
      operationOnClick={selectTableIdOnClickToListItem}
    />
  ));

  const mainTitle =
    cartState.table != undefined && cartState.table != null
      ? "Ваш заказ на столик № " + cartState.table.tableNumber + ":"
      : "Ваш заказ:";
  const body = (
    <Switch>
      <Route exact path={urls.cart}>
        <div className="cart-page__body">
          <div className="cart-page__main">
            <div className="cart-page__order">
              <p className="cart-page__order-title">{mainTitle}</p>
              <div className="cart-page__dishes-list">
                {dishesListItemsAsJSX}
              </div>
            </div>

            <div className="cart-page__booked-tables">
              <List
                title={bookedTablesAsJSX.length != 0 ? "На какой столик принести заказ?:" :
                  "Забронируйте столик"}
                simpleHovered
                selectedItemByKey={selectedTableIdInCart}
                itemsLayout="flex-column-nowrap"
              >
                {bookedTablesAsJSX}
              </List>
            </div>
          </div>
          <div className="cart-page__aside">
            {promocodeFormAsJSX}

            <p className="cart-page__total">{cartTotalText}</p>
            <div className="cart-page__cooking-status">
              <input
                type="checkbox"
                name="cookingStatus"
                value={cookingStatus}
                onChange={(e) => putCookingStatus(e.target.value)}
              />
              <label htmlFor="cookingStatus">Готовить сейчас</label>
            </div>
            <button
              className="cart-page__put-order button"
              type="button"
              onClick={() => putOrder()}
            >
              Заказать
            </button>
          </div>
        </div>
      </Route>
      <Route exact path={urls.payment}>
        {/*Переход к оплате, пока отключено*/}
      </Route>
    </Switch>
  );
  const redirectUrl = "?redirectUrl=" + urls.createAnOrder;
  //#endregion
  return redirectToAuthentification ? (
    <Redirect to={urls.login + redirectUrl} />
  ) : redirectToIndex ? (
    <Redirect to={urls.index} />
  ) : (
    <div className="cart-page">
      <DefaultPageWithMenu body={body} isLogin={props.isLogin}
        setIsLogin={props.setIsLogin} />
    </div>
  );
  //#region Actions
  function addDish(dishId) {
    if (dishId) {
      new CafeAPI().cart.addDish(dishId, (error, data, response) => {
        new StatusCodeService()
          .if([200], response, () => refreshCartState())
          .if([401], response, () => setRedirectToAuthentification(true))
          .if([400], response, null)
          .if([404], response, null)
          .if([500], response, null)
          .if([], error, null)
          .if([], response, null);
      });
    }
    return;
  }
  function deleteDish(dishId) {
    if (dishId) {
      new CafeAPI().cart.deleteDish(dishId, (error, data, response) => {
        new StatusCodeService()
          .if([200], response, () => {
            let deleteDish = cartState.dishesGroupedByDishId.find(
              (dish) => dish.dishId === dishId
            );
            if (deleteDish) {
              deleteDish.quantity -= 1;
            }
            refreshCartState();
          })
          .if([400], response, null)
          .if([401], response, () => setRedirectToAuthentification(true))
          .if([404], response, null)
          .if([500], response, null)
          .if([], error, null)
          .if([], response, null);
      });
    }
    return;
  }
  function deleteDishesByDishId(dishId) {
    if (dishId) {
      new CafeAPI().cart.deleteDishesByDishId(dishId, (error, data,
        response) => {
        new StatusCodeService()
          .if([200], response, () => {
            let deleteDishesOnDishId = cartState.dishesGroupedByDishId.find(
              (dish) => dish.dishId === dishId
            );
            if (deleteDishesOnDishId) {
              deleteDishesOnDishId.quantity = 0;
            }
            refreshCartState();
          })
          .if([400], response, null)
          .if([401], response, () => setRedirectToAuthentification(true))
          .if([404], response, null)
          .if([500], response, null)
          .if([], error, null)
          .if([], response, null);
      });
    }
    return;
  }
  function putOrder() {
    new CafeAPI().orders.createOrderFromCart((error, data, response) => {
      new StatusCodeService()
        .if([201], response, () => setRedirectToIndex(true))
        .if([400], response, null)
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([404], response, null)
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
    return;
  }
  function putCookingStatus(checkedValue) {
    new CafeAPI().cart.setCookingStatus((checkedValue ? "Now" : "Later"),
      (error, data, response) => {
        new StatusCodeService()
          .if([201], response, () => setCookingStatus(prevState =>
            (prevState !== checkedValue) ? checkedValue : prevState))
          .if([401], response, () => setRedirectToAuthentification(true))
          .if([404], response, null)
          .if([500], response, null)
          .if([], error, null)
          .if([], response, null);
      });
    return;
  }
  function addPromocode(e) {
    e.preventDefault();
    if (Object.values(promocodeSpecStrs).includes(promocode)) {
      return;
    }
    new CafeAPI().cart.addPromocode(promocode, (error, data,
      response) => {
      new StatusCodeService()
        .if([200], response, () => {
          setPromocodeAccepted(1);
          setPromocode(promocodeSpecStrs.promocodeAdded);
        })
        .if([400], response, () => {
          setPromocodeAccepted(0);
          setPromocode(promocodeSpecStrs.promocodeDenied);
        })
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([404], response, () => {
          setPromocodeAccepted(0);
          setPromocode(promocodeSpecStrs.promocodeDenied);
        })
        .if([500], response, () => {
          setPromocodeAccepted(0);
          setPromocode(promocodeSpecStrs.promocodeError);
        })
        .if([], error, null)
        .if([], response, null);
    });
    return;
  }
  function selectTableIdOnClickToListItem(tableId) {
    if (tableId) {
      new CafeAPI().cart.setTableId(tableId, (error, data, response) => {
        new StatusCodeService()
          .if([200], response, () => {
            setSelectedTableIdInCart(prevState => {
              console.log("prevState=", prevState);
              console.log("newState=", data);
              return isEqual(prevState, data.tableId) ? prevState : data.tableId;
            });
          })
          .if([400], response, () => refreshBookedTables())
          .if([401], response, () => setRedirectToAuthentification(true))
          .if([404], response, null)
          .if([500], response, null)
          .if([], error, null)
          .if([], response, null);
      });
    }
    return;
  }
  //#endregion
  //#region Refresh
  function refreshBookedTables() {
    new CafeAPI().bookedTables.getBookedTables((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => {
          setBookedTables((prevBookedTables) =>
            isEqual(prevBookedTables, data) ? prevBookedTables : data)
        })
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
    return;
  }
  function refreshSelectedTableIdInCart() {
    new CafeAPI().cart.getTableId((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => {
          setSelectedTableIdInCart((prevState) =>
            isEqual(prevState, data.tableId) ? prevState : data.tableId)
        })
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([404], response, null)
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
    return;
  }
  function refreshCookingStatus() {
    new CafeAPI().cart.getCookingStatus((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => {
          setCookingStatus(data.cookingStatus === "Later" ? false :
            setCookingStatus === "Now" ? true : true);
        })
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([404], response, null)
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
  }
  function refreshCartState() {
    new CafeAPI().cart.getCartState((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => {
          if (isEqual(cartState, data)) {
            return cartState;
          }
          const strikethroughElements =
            cartState.dishesGroupedByDishId?.filter((dish) => dish
              .quantity === 0 && !data.dishesGroupedByDishId.some(
                (newDish) => newDish.dishId === dish.dishId));
          data.dishesGroupedByDishId = data.dishesGroupedByDishId
            .concat(strikethroughElements);
          setCartState(data);
        })
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([404], response, () => new CafeAPI().cart.createCart(() =>
          refreshCartState()))
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
  }
  //#endregion
  //#region Other, using state
  function clearInput(e) {
    if (Object.values(promocodeSpecStrs).includes(e.target.value)) {
      e.target.value = "";
      setPromocode("");
      setPromocodeAccepted(-1);
    }
  }
  function inputPromocodeSetDefault(e) {
    if (e.target.value == "") {
      e.target.value = promocodeSpecStrs.initial;
      setPromocode(promocodeSpecStrs.initial);
    }
    return;
  }
  //#endregion
}
//#region Other, not using state
const promocodeSpecStrs = {
  initial: "Промокод",
  promocodeDenied: "Промокод не найден",
  promocodeAdded: "Промокод добавлен",
  promocodeError: "Ошибка"
};
//#endregion

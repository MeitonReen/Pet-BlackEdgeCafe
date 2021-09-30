import "./CreateAnOrderPage.css";
import DefaultPageWithMenu from "../base/DefaultPageWithMenu";
import Menu from "../base/Menu";
import DishInOrderPage from "../base/dish/DishInOrderPage";
import MiniCart from "../base/MiniCart";

import urls from "../../urls";
import { useEffect, useState } from "react";
import { Redirect, Switch, Route } from "react-router-dom";
import { isEqual } from "lodash";
import { generateImagesUrls } from "../../shared-funcs";
import CafeAPI from "../../CafeAPI";
import StatusCodeService from "../../StatusCodeService"

export default function CreateAnOrderPage(props) {
  //#region State
  const [redirectToAuthentification, setRedirectToAuthentification] =
    useState(false);
  const [dishesFromMenu, setDishesFromMenu] = useState([]);
  const [miniCartState, setMiniCartState] = useState({
    amount: null,
    amountIncludingPromocodes: null,
    quantityDishes: null,
    dishIdsGroupedByDishId: [{
      dishId: null,
      quantity: null
    }]
  });
  //#endregion 
  //#region Effects
  useEffect(() => {
    refreshDishesFromMenu();
    refreshMiniCartState();
  }, []);
  //#endregion
  //#region Prepare JSX
  const dishesFromMenuAsJSX = !Array.isArray(dishesFromMenu) ?
    undefined : dishesFromMenu.map((dishFromMenu) => (
      <DishInOrderPage
        key={dishFromMenu.dishId}
        dish={dishFromMenu}
        operations={{
          add: addDish,
          delete: deleteDish,
          deleteAllById: deleteDishesByDishId
        }}
        quantityOfThisDishInCart={miniCartState
          .dishIdsGroupedByDishId?.find(dish => dish.dishId ===
            dishFromMenu.dishId)?.quantity ?? 0}
        imgSrc={imagesModulesUrls[dishFromMenu.name]}
      />)
    );

  const body = (
    <Switch>
      <Route exact path={urls.createAnOrder}>
        <div className="create-an-order-page__body">
          <div className="create-an-order-page__main">
            <Menu>
              {dishesFromMenuAsJSX}
            </Menu>
          </div>
          <div className="create-an-order-page__mini-cart">
            <MiniCart quantity={miniCartState?.quantityDishes}
              amount={miniCartState?.amount}
              amountIncludingPromocodes={miniCartState
                ?.amountIncludingPromocodes}
            />
          </div>
        </div>
      </Route>
    </Switch>
  );
  const redirectUrl = "?redirectUrl=" + urls.createAnOrder;
  //#endregion
  return (
    redirectToAuthentification ? (
      <Redirect to={urls.login + redirectUrl} />
    ) : (
      <div className="create-an-order-page">
        <DefaultPageWithMenu body={body} isLogin={props.isLogin}
          setIsLogin={props.setIsLogin} />
      </div>
    )
  );
  //#region Actions
  function addDish(dishId) {
    if (dishId) {
      new CafeAPI().cart.addDish(dishId, (error, data, response) => {
        new StatusCodeService()
          .if([200], response, () => refreshMiniCartState())
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
  function deleteDish(dishId) {
    if (dishId) {
      new CafeAPI().cart.deleteDish(dishId, (error, data, response) => {
        new StatusCodeService()
          .if([200], response, () => {
            let deleteDish = miniCartState.dishIdsGroupedByDishId.find(
              (dish) => dish.dishId === dishId);
            if (deleteDish) {
              deleteDish.quantity -= 1;
            }
            refreshMiniCartState();
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
            let deleteDishesOnDishId = miniCartState.dishIdsGroupedByDishId
              .find((dish) => dish.dishId === dishId);
            if (deleteDishesOnDishId != undefined &&
              deleteDishesOnDishId != null) {
              deleteDishesOnDishId.quantity = 0;
            }
            refreshMiniCartState();
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
  //#endregion
  //#region Refresh 
  function refreshDishesFromMenu() {
    new CafeAPI().menu.getMenuIncludingPromocodes((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => setDishesFromMenu(
          prevDishesFromMenu => isEqual(prevDishesFromMenu, data) ?
            prevDishesFromMenu : data))
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([404], response, null)
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
    return;
  }
  function refreshMiniCartState() {
    new CafeAPI().cart.getMiniCartState((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => setMiniCartState(prevMiniCartState =>
          isEqual(prevMiniCartState, data) ? prevMiniCartState : data))
        .if([401], response, () => setRedirectToAuthentification(true))
        .if([404], response, () =>
          new CafeAPI().cart.createCart(() => {
            refreshDishesFromMenu();
            refreshMiniCartState();
          }))
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
    return;
  }
  //#endregion
}
//#region Other, not using state
const imagesModulesUrls = generateImagesUrls(
  require.context('../../images/menu/dishes-images/small',
    false, /\.(png|jpe?g|svg)$/));
//#endregion

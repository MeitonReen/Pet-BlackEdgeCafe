import "./MenuPage.css";
import DefaultPageWithMenu from "../base/DefaultPageWithMenu";
import Menu from "../base/Menu";
import DishInMenu from "../base/dish/DishInMenu";
import CafeAPI from "../../CafeAPI"
import StatusCodeService from "../../StatusCodeService"

import { useEffect, useState } from "react";
import urls from "../../urls";
import { generateImagesUrls } from "../../shared-funcs";

export default function MenuPage(props) {
  //#region State
  const [dishesFromMenu, setDishesFromMenu] = useState([]);
  //#endregion
  //#region Effects
  useEffect(() =>
    new CafeAPI().menu.getMenu((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => setDishesFromMenu(data))
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    }), []);
  //#endregion
  //#region Prepare JSX
  const dishesToMenu = !Array.isArray(dishesFromMenu) ?
    null : dishesFromMenu.map(dishFromMenu => (
      <DishInMenu key={dishFromMenu.dishId} dish={dishFromMenu}
        imgSrc={imagesModulesUrls[dishFromMenu.name]} />)
    );

  const body = (
    <Menu>
      {dishesToMenu}
    </Menu>
  );
  //#endregion
  return (
    <div className="menu-page">
      <DefaultPageWithMenu body={body} isLogin={props.isLogin}
        setIsLogin={props.setIsLogin} />
    </div>
  );
}
//#region Other, not using state
const imagesModulesUrls = generateImagesUrls(
  require.context('../../images/menu/dishes-images/small',
    false, /\.(png|jpe?g|svg)$/));
//#endregion
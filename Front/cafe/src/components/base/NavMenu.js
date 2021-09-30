import "./NavMenu.css";
import { NavLink } from "react-router-dom";
import urls from "../../urls";
import CafeAPI from "../../CafeAPI";
import { useState } from "react";
import { Redirect } from "react-router-dom";
import StatusCodeService from "../../StatusCodeService"

export default function NavMenu(props) {
  //#region State
  const [redirectToIndex, setRedirectToIndex] = useState(false);
  //#endregion
  //#region PrepareJSX
  let logoutJSX = null;
  if (props?.isLogin) {
    logoutJSX = <li className="nav-menu__item">
      <a className="a nav-menu__item_a"
        onClick={() => logout()}
      >ВЫЙТИ</a>
    </li>
  }
  //#endregion
  return (
    (redirectToIndex === false) ? (
      <ul className="nav-menu">
        <li className="nav-menu__item">
          <NavLink to={urls.about} className="a nav-menu__item_a">О КАФЕ</NavLink>
        </li>
        <li className="nav-menu__item">
          <NavLink to={urls.menu} className="a nav-menu__item_a">МЕНЮ</NavLink>
        </li>
        <li className="nav-menu__item">
          <NavLink to={urls.createAnOrder} className="a nav-menu__item_a">ЗАКАЗАТЬ</NavLink>
        </li>
        <li className="nav-menu__item">
          <NavLink to={urls.bookindTables} className="a nav-menu__item_a">БРОНИРОВАТЬ</NavLink>
        </li>
        <li className="nav-menu__item">
          <NavLink to={urls.myOrders} className="a nav-menu__item_a">МОИ ЗАКАЗЫ</NavLink>
        </li>
        <li className="nav-menu__item">
          <NavLink to={urls.contacts} className="a nav-menu__item_a">КОНТАКТЫ</NavLink>
        </li>
        {logoutJSX}
      </ul>) :
      <Redirect to={urls.index} />
  );
  //#region Actions
  function logout() {
    new CafeAPI().account.logout((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => {
          props.setIsLogin(false);
          setRedirectToIndex(true);
        })
        .if([401], response, null)
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
  }
  //#endregion
}
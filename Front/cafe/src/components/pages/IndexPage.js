import "./IndexPage.css";
import { NavLink } from "react-router-dom";
import urls from "../../urls";

import DefaultPageWithMenu from "../base/DefaultPageWithMenu"

export default function IndexPage(props) {
  //#region PrepareJSX
  const body = (
    <div className="index-page__body">
      <p className="index-page__title">Добро пожаловать</p>
      <NavLink className="a" to={urls.bookindTables}>
        <p className="index-page__text">забронировать столик?</p>
      </NavLink>
    </div>
  );
  //#endregion
  return (
    <div className="index-page">
      <DefaultPageWithMenu body={body} isLogin={props.isLogin}
        setIsLogin={props.setIsLogin} />
    </div>
  );
}
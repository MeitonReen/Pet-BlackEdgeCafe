import "./DefaultPageWithMenu.css";

import DefaultPage from "./DefaultPage"
import NavMenu from "./NavMenu"

export default function DefaultPageWithMenu(props) {
  //#region PrepareJSX
  const menu = (
    <div className="default-page-with-menu__menu">
      <NavMenu isLogin={props.isLogin} setIsLogin={props.setIsLogin} />
    </div>
  );
  //#endregion
  return (
    <DefaultPage header={menu} body={props.body} footer={props.footer} />
  );
}
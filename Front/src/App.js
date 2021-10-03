import './App.css';
import { Switch, Route } from "react-router-dom";
import urls from "./urls";
import { useState, useEffect } from "react";
import CafeAPI from "./CafeAPI"
import StatusCodeService from "./StatusCodeService"

import CartPage from "./components/pages/CartPage";
import BookingTablesPage from "./components/pages/BookingTablesPage";
import MyOrdersPage from "./components/pages/MyOrdersPage";
import IndexPage from "./components/pages/IndexPage";
import AboutPage from "./components/pages/AboutPage";
import ContactsPage from "./components/pages/ContactsPage";
import MenuPage from "./components/pages/MenuPage";
import CreateAnOrderPage from "./components/pages/CreateAnOrderPage";
import Registration from "./components/pages/Registration"
import Login from "./components/pages/Login";
import DishDetails from "./components/pages/DishDetails";

export default function App(props) {
  //#region State
  const [isLogin, setIsLogin] = useState(false);
  //#endregion
  //#region Effects
  useEffect(() =>
  {
    refreshIsLogin();
  }, [])
  //#endregion
  return (
    <Switch>
      <Route path={urls.index}>
        <IndexPage isLogin={isLogin} setIsLogin={setIsLogin} />
      </Route>
      <Route path={urls.about}>
        <AboutPage isLogin={isLogin} setIsLogin={setIsLogin} />
      </Route>
      <Route path={urls.dishDetails}>
        <DishDetails />
      </Route>
      <Route path={urls.contacts}>
        <ContactsPage isLogin={isLogin} setIsLogin={setIsLogin} />
      </Route>
      <Route path={urls.menu}>
        <MenuPage isLogin={isLogin} setIsLogin={setIsLogin} />
      </Route>
      <Route path={urls.createAnOrder}>
        <CreateAnOrderPage isLogin={isLogin} setIsLogin={setIsLogin} />
      </Route>
      <Route path={urls.cart}>
        <CartPage isLogin={isLogin} setIsLogin={setIsLogin} />
      </Route>
      <Route path={urls.bookindTables}>
        <BookingTablesPage isLogin={isLogin} setIsLogin={setIsLogin} />
      </Route>
      <Route path={urls.myOrders}>
        <MyOrdersPage isLogin={isLogin} setIsLogin={setIsLogin} />
      </Route>
      <Route path={urls.registration}>
        <Registration />
      </Route>
      <Route path={urls.login}>
        <Login setIsLogin={setIsLogin}/>
      </Route>
    </Switch>
  );
  //#region Actions
  function refreshIsLogin() {
    new CafeAPI().account.checkLogin((error, data, response) => {
      new StatusCodeService()
        .if([200], response, () => setIsLogin(true))
        .if([401], response, () => setIsLogin(false))
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null);
    });
  }
  //#endregion
}

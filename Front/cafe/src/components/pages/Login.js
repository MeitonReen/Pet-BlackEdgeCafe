import "./Login.css";
import urls from "../../urls";
import { useState } from "react";
import { Redirect, useLocation } from "react-router-dom";
import CafeAPI from "../../CafeAPI";
import StatusCodeService from "../../StatusCodeService"

export default function Login(props) {
  //#region State
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [authorizationReceived, setAuthorizationReceived] = useState(-1);
  const [redirectToRegistration, setRedirectToRegistration] =
    useState(false);
  const redirectUrl = (new URLSearchParams(useLocation().search))
    .get('redirectUrl');
  //#endregion
  return (
    redirectToRegistration ? (<Redirect to={urls.registration +
      (redirectUrl?.length > 0 ? "?redirectUrl=" + redirectUrl : "")}
    />) :
      ((authorizationReceived === 0) || (authorizationReceived === -1)) ?
        (
          <div className="login">
            <div className="login__form">
              <label className="login__label-login login__text" htmlFor="login__login">
                Логин:
              </label>
              <input value={login} onChange={e => setLogin(e.target.value)}
                className="input login__login login__text" type="text"
                id="login__login" />

              <label className="login__label-password login__text"
                htmlFor="login__password">
                Пароль:
              </label>
              <input className="input login__password login__text"
                value={password} onChange={e => setPassword(e.target.value)} type="password"
                id="login__password" />
              <div className="login-buttons">
                <button onClick={() => signIn()}
                  className="button login__button-enter login__text"
                  type="button">Войти</button>
                <span className="login__text"> или </span>
                <button className="button
                  login__button-redirect-to-registration
                  login__text"
                  onClick={() => setRedirectToRegistration(true)}
                  type="button">зарегистрироваться</button>
                <span className="login__text"> ?</span>
                {authorizationReceived === 0 ? accessDenied : null}
              </div>
            </div>
          </div>
        ) : (<Redirect to={redirectUrl?.length > 0 ? redirectUrl :
          urls.index} />)
  );
  //#region Actions
  function signIn() {
    new CafeAPI().account.login(login, password, (error, data,
      response) => {
      new StatusCodeService()
        .if([200], response, () => {
          setAuthorizationReceived(1);
          props.setIsLogin(true);
        })
        .if([400], response, () => setAuthorizationReceived(0))
        .if([500], response, null)
        .if([], error, null)
        .if([], response, () => setAuthorizationReceived(0));
    });
  }
  //#endregion
}
//#region Other, not using state
const accessDenied = (
  <p className="login__text login__access-denied">Неверный логин или пароль</p>);
//#endregion
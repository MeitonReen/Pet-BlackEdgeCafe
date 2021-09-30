import "./Registration.css";
import urls from "../../urls";
import { useState } from "react";
import { Redirect, useLocation } from "react-router-dom";
import CafeAPI from "../../CafeAPI";
import StatusCodeService from "../../StatusCodeService"

export default function Registration() {
  //#region State
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [error, setError] = useState({ message: "" });
  const [registrationConfirmed, setRegistrationConfirmed] = useState(-1);
  //#endregion
  //#region Prepare JSX
  const redirectUrl = (new URLSearchParams(useLocation().search))
    .get('redirectUrl');
  const errorAsJSX = (
    <div className="registration__error registration__text">
      {error.message}
    </div>);
  //#endregion
  return (
    ((registrationConfirmed === 0) || (registrationConfirmed === -1)) ?
      (
        <div className="registration">
          <div className="registration__form">
            <label className="registration__text"
              htmlFor="registration__login">
              Логин:
            </label>
            <input value={login} onChange={e => setLogin(e.target.value)}
              className="input registration__text" type="text"
              id="registration__login" />

            <label className="registration__text"
              htmlFor="registration__password">
              Пароль:
            </label>
            <input className="input registration__text"
              value={password} onChange={e => setPassword(e.target.value)}
              type="password"
              id="registration__password" />

            <label className="registration__text"
              htmlFor="registration__confirm-password">
              Повторите пароль:
            </label>
            <input className="input registration__text"
              value={confirmPassword} onChange={e => setConfirmPassword(
                e.target.value)} type="password"
              id="registration__confirm-password" />

            <button
              onClick={() => registration()}
              className="button registration__text
                registration__button-registration"
              type="button">Зарегистрироваться</button>
            {!registrationConfirmed ? errorAsJSX : null}
          </div>
        </div>
      ) : (<Redirect to={redirectUrl?.length > 0 ? redirectUrl :
        urls.index} />)
  );
  //#region Actions
  function registration() {
    new CafeAPI().account.registration(login, password, confirmPassword,
      (error, data, response) => {
        new StatusCodeService()
          .if([200], response, () => setRegistrationConfirmed(1))
          .if([400], response, () => setRegistrationConfirmed(0))
          .if([500], response, null)
          .if([], error, () => setError({ message: error?.message }))
          .if([], response, () => setRegistrationConfirmed(0));
      });
  }
  //#endregion
}

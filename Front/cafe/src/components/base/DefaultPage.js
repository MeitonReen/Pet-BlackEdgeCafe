import "./DefaultPage.css";
import "../../shared-css.css"
import { NavLink } from "react-router-dom";
import urls from "../../urls";

export default function DefaultPage(props) {
  const title = "Black Edge";
  return (
    <div className="default-page">
      <header className="default-page__header
        default-page__header_sticky">
        <NavLink to={urls.index}
          className="a default-page__header-title">
          {title}
        </NavLink>
        <div className="default-page__header-main">
          {props.header}
        </div>
      </header>

      <section className="default-page__body">
        {props.body}
      </section>
      <footer className="default-page__footer">
        {props.footer}
      </footer>
    </div>
  );
}
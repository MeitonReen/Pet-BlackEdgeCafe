import "./ContactsPage.css";
import "../../shared-css.css";
import DefaultPageWithMenu from "../base/DefaultPageWithMenu"

export default function ContactsPage(props) {
  //#region PrepareJSX
  const body = (
    <div className="contacts-page__body">
      <p className="contacts-page__work-shedule-title">
        <span className="underline">График работы:</span>
      </p>
      <p className="contacts-page__work-shedule-text">c понедельника по субботу с 09:00 до 05:00, воскресение - выходной.</p>

      <p className="contacts-page__contacts">
        <span className="underline">Контакты:</span>
      </p>
      <table className="contacts-page__contacts-table">
        <colgroup>
          <col />
          <col className="contacts-table__vertical-gap" />
          <col />
        </colgroup>
        <tbody>
          <tr>
            <td rowSpan="2">Забронировать столик</td>
            <td />{/*gap*/}
            <td> +7 123 456-78-99</td>
          </tr>
          <tr>
            <td />{/*gap*/}
            <td>black_edge@gmail.com</td>
          </tr>
          <tr>
            <td className="contacts-table__horizon-gap"></td>
            <td></td>
          </tr>
          <tr>
            <td rowSpan="2">По вопросам сотрудничества</td>
            <td />{/*gap*/}
            <td>+7 987 654-32-11</td>
          </tr>
          <tr>
            <td />{/*gap*/}
            <td>black_edge_org@gmail.com</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
  //#endregion
  return (
    <div className="contacts-page">
      <DefaultPageWithMenu isLogin={props.isLogin}
        setIsLogin={props.setIsLogin} body={body} />
    </div>
  );
}
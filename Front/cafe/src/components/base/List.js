import "./List.css";
import { compareByKeyDefault } from "../../shared-funcs";

export default function List(props) {
  //#region Settings
  /*
  props?.itemsLayout
  props?.simpleHovered
  props?.selectedItemByKey
  props?.sortOnCompareFunc
  props?.sortByKeyDefault
  */
  //#endregion

  //#region PrepareJSX
  let classNameForListItems = "list__items";
  switch (props?.itemsLayout) {
    case "flex-column-wrap":
      classNameForListItems += " list__items_flex-column-wrap";
      break;
    case "flex-column-nowrap":
      classNameForListItems += " list__items_flex-column-nowrap";
      break;
    case "grid-one-column":
      classNameForListItems += " list__items_grid-one-column";
      break;
    default:
      classNameForListItems += " list__items_default";
      break;
  }

  let classNameForEveryListItem = "list__item";
  if (props?.simpleHovered) classNameForEveryListItem +=
    " list__item_simple-hovered";
  
  const listItemsAsJSX = props?.children?.map(
    childItem => {
      let classNameForCurrentListItem = classNameForEveryListItem;
      
      if (props?.selectedItemByKey === childItem?.key) {
        classNameForCurrentListItem +=
          " list__item_selected-by-key";
      }
      return (
        <div className={classNameForCurrentListItem}
          key={childItem?.key}>
          {childItem}
        </div>
      )
    }
  );
  if (props?.sortOnCompareFunc != undefined &&
    props?.sortOnCompareFunc != null) {
    listItemsAsJSX?.sort(props?.sortOnCompareFunc);
  } else {
    if (props?.sortByKeyDefault) {
      listItemsAsJSX?.sort(compareByKeyDefault);
    }
  }
  //#endregion
  return (
    <div className="list">
      <p className="list__title list__text">{props.title}</p>
      <div className={classNameForListItems}>
        {listItemsAsJSX}
      </div>
    </div>
  );
}
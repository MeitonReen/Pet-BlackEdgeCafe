import "./Menu.css";
import "../../shared-css.css"
import { useState, useEffect } from "react";
import { generateImagesUrls } from "../../shared-funcs";
import PlacerDishesToMenu from "../../PlacerDishesToMenu/PlacerDishesToMenu";
import arrowLeft from "../../images/icons/arrow-left.svg";
import arrowRight from "../../images/icons/arrow-right.svg";
import urls from "../../urls";
import { isEqual } from "lodash";
import CafeAPI from "../../CafeAPI"
import StatusCodeService from "../../StatusCodeService"

export default function Menu(props) {
  //#region Little comment
  /*props.children is dishesJSX[] with .key for each == dishId. 
  menuPagesGenerate() convert dishesJSX[] to menuPages array:
  [ 
    {
      backgroundSrc: str,
      title: str,
      dishesCategoriesJSX: [
        {
          name: str <showing category name>,
          dishes: <dishesJSX> [
          ]//key == .key from each props.children
        },
      ]//key == dishCategoryName
    }
  ]//key == pageTitle
  */
  //#endregion
  //#region State
  const [dishIdsByCategories, setDishIdsByCategories] = useState([
    {
      dishId: null,
      categoryIds: [],
      categoryNames: []
    }
  ]);
  const menuPages = menuPagesGenerate(props.children);
  const [selectedMenuPageIndex, setSelectedMenuPageIndex] = useState(
    menuPages?.findIndex(menuPage => menuPage.title) ?? 0)
  //#endregion
  //#region Effects
  useEffect(() => {
    refreshDishIdsByCategories();
  }, []);
  //#endregion
  //#region Prepare JSX
  const backgroundDecor = (
    Object.entries(imagesBackgroundDecorUrls).map(([imageName, imageSrc]) => (
      <img
        key={imageName}
        src={imageSrc}
        className={"menu-dishes__background-decor-item " +
          "menu-dishes__background-decor-" +
          decorImageNameToCssName[imageName]} />
    ))
  );
  //#endregion
  return (
    <div className="menu-dishes">
      <div className="menu-dishes__shadow-over-menu-background
        shadow-over"></div>
      <img className="menu-dishes__menu-background"
        src={menuPages?.[selectedMenuPageIndex]?.backgroundSrc} />

      <div className="menu-dishes__background-decor">
        {backgroundDecor}
      </div>

      <div className="menu-dishes__menu">
        <div className="menu-dishes__nav-container-left
          menu-dishes__nav-container"
          onClick={() => prevPage()}
        >
          <div className="menu-dishes__nav-left menu-dishes__nav">
            <img className="menu-dishes__hidden-arrow-img"
              src={arrowLeft} />
          </div>
        </div>
        <div className="menu-dishes__nav-container-right
          menu-dishes__nav-container"
          onClick={() => nextPage(menuPages)}
        >
          <div className="menu-dishes__nav-right menu-dishes__nav">
            <img className="menu-dishes__hidden-arrow-img"
              src={arrowRight} />
          </div>
        </div>

        <p className="menu-dishes__menu-title">{
          menuPages?.[selectedMenuPageIndex]?.title}</p>

        <div className="menu-dishes__menu-categories">
          {menuPages?.[selectedMenuPageIndex]?.dishesCategoriesJSX}
        </div>
      </div>
    </div>
  );
  //#region Other, using state
  function menuPagesGenerate(dishesFromChildrenProp) {
    let placerDishesToMenu = new PlacerDishesToMenu(imagesMenuUrls,
      dishIdsByCategories);
    let p = placerDishesToMenu
      .placeDishesToCategories(dishesFromChildrenProp)
      .getMenuPages();
    return p;
  }
  function nextPage(menuPages) {
    if (selectedMenuPageIndex != menuPages.length - 1) {
      setSelectedMenuPageIndex(selectedMenuPageIndex + 1);
    }
  }
  function prevPage() {
    if (selectedMenuPageIndex != 0) {
      setSelectedMenuPageIndex(selectedMenuPageIndex - 1);
    }
  }
  function refreshDishIdsByCategories() {
    new CafeAPI().menu.getDishIdsByCategories((error, data, response) =>
      new StatusCodeService()
        .if([200], response, () => setDishIdsByCategories(prevState =>
          isEqual(prevState, data) ? prevState : data))
        .if([500], response, null)
        .if([], error, null)
        .if([], response, null)
    )
  }
  //#endregion
}
//#region Other, not using state
const imagesMenuUrls = generateImagesUrls(
  require.context('../../images/menu',
    false, /\.(png|jpe?g|svg)$/));
const imagesBackgroundDecorUrls = generateImagesUrls(
  require.context('../../images/menu/background-decor',
    false, /\.(png|jpe?g|svg)$/));
const decorImageNameToCssName = {
  "Декор-Ч1": "ch1",
  "Декор-Ч2": "ch2",
  "Декор-Б": "b",
  "Декор-М": "m",
  "Декор-П1": "p1",
  "Декор-П2": "p2",
  "Декор-РД": "rd",
  "Декор-Св": "sv",
  "Декор-С3": "s3",
  "Декор-Т": "t",
};
//#endregion
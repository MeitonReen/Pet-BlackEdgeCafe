import MenuPresenter from "./MenuPresenter"

export default class PlacerDishesToMenu {
  constructor(imagesMenuUrls, dishIdsByCategories) {
    this.imagesMenuUrls = imagesMenuUrls;
    this.dishIdsByCategories = dishIdsByCategories;

    this.dishCategories = {
      soups: [],
      salads: [],
      roast: [],
      snacks: [],
      alcoLongDrinks: [],
      alcoShotDrinks: [],
      nonAlcoDrinks: []
    }
    //#region Service constants
    this.menuPageNames = {
      mainMenu: "Основное меню",
      drinks: "Напитки"
    }
    this.dishCategoriesByJsNames = {
      mainMenu: "Основное меню",
      snacks: "Закуски",
      salads: "Салаты",
      roast: "Жаркое",
      soups: "Супы",
      drinks: "Напитки",
      alcoLongDrinks: "Лонги",
      alcoShotDrinks: "Шоты",
      nonAlcoDrinks: "Безалкогольные"
    }
    this.cssNamesByDishCategories = {
      [this.dishCategoriesByJsNames.mainMenu]: "mainMenu",
      [this.dishCategoriesByJsNames.snacks]: "snacks",
      [this.dishCategoriesByJsNames.salads]: "salads",
      [this.dishCategoriesByJsNames.roast]: "roast",
      [this.dishCategoriesByJsNames.soups]: "soups",
      [this.dishCategoriesByJsNames.drinks]: "drinks",
      [this.dishCategoriesByJsNames.alcoLongDrinks]: "alco-long-drinks",
      [this.dishCategoriesByJsNames.alcoShotDrinks]: "alco-shot-drinks",
      [this.dishCategoriesByJsNames.nonAlcoDrinks]: "non-alco-drinks"
    }

    this.dishCategoryArrays = {
      [this.dishCategoriesByJsNames.snacks]: this.dishCategories.snacks,
      [this.dishCategoriesByJsNames.salads]: this.dishCategories.salads,
      [this.dishCategoriesByJsNames.roast]: this.dishCategories.roast,
      [this.dishCategoriesByJsNames.soups]: this.dishCategories.soups,
      [this.dishCategoriesByJsNames.alcoLongDrinks]: this.dishCategories.alcoLongDrinks,
      [this.dishCategoriesByJsNames.alcoShotDrinks]: this.dishCategories.alcoShotDrinks,
      [this.dishCategoriesByJsNames.nonAlcoDrinks]: this.dishCategories.nonAlcoDrinks
    }
    //#endregion
    this.menuPages = {
      mainMenu: {
        name: this.menuPageNames.mainMenu,
        categories:
          [
            {
              name: this.dishCategoriesByJsNames.soups,
              cssName: this.cssNamesByDishCategories.soups,
              dishes: this.dishCategories.soups
            },
            {
              name: this.dishCategoriesByJsNames.salads,
              cssName: this.cssNamesByDishCategories.salads,
              dishes: this.dishCategories.salads
            },
            {
              name: this.dishCategoriesByJsNames.roast,
              cssName: this.cssNamesByDishCategories.roast,
              dishes: this.dishCategories.roast
            },
            {
              name: this.dishCategoriesByJsNames.snacks,
              cssName: this.cssNamesByDishCategories.snacks,
              dishes: this.dishCategories.snacks
            }
          ]
      },
      drinks: {
        name: this.menuPageNames.drinks,
        categories:
          [
            {
              name: this.dishCategoriesByJsNames.alcoLongDrinks,
              cssName: this.cssNamesByDishCategories.alcoLongDrinks,
              dishes: this.dishCategories.alcoLongDrinks
            },
            {
              name: this.dishCategoriesByJsNames.alcoShotDrinks,
              cssName: this.cssNamesByDishCategories.alcoShotDrinks,
              dishes: this.dishCategories.alcoShotDrinks
            },
            {
              name: this.dishCategoriesByJsNames.nonAlcoDrinks,
              cssName: this.cssNamesByDishCategories.nonAlcoDrinks,
              dishes: this.dishCategories.nonAlcoDrinks
            }
          ]
      }
    }
    this.menuPresenter = new MenuPresenter(this.menuPages,
      this.cssNamesByDishCategories,
      this.imagesMenuUrls);
  }
  placeDishesToCategories(dishesJSX) {
    dishesJSX?.forEach(dishJSX => this.placeDishToCategory(dishJSX));
    return this;
  }
  placeDishToCategory(dishJSX) {
    this.getCategoriesByDishId(dishJSX.key)
      ?.forEach(dishCategory =>
        this.dishCategoryArrays[dishCategory]?.push(dishJSX)
      );
  }
  getCategoriesByDishId(dishId) {
    return this.dishIdsByCategories?.find(rec => rec.dishId == dishId)
      ?.categoryNames;
  }
  getMenuPages() {
    return this.menuPresenter?.create();
  }
}
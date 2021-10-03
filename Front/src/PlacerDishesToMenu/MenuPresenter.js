export default class MenuPresenter {
  constructor(menuPages, cssNamesByDishCategories, imagesMenuUrls) {
    this.menuPages = menuPages;
    this.cssNamesByDishCategories = cssNamesByDishCategories;
    this.imagesMenuUrls = imagesMenuUrls;
  }
  create() {
    return Object.entries(this.menuPages)
      .map(([pageName, page]) => {
        return {
          backgroundSrc: this.imagesMenuUrls[page.name],
          title: page.name,
          dishesCategoriesJSX: page.categories.map(category =>
            this.presentationCategory(category))
        }
      })
  }
  presentationCategory(category) {
    return (
      <div className={"menu-dishes__dishes-category " +
        "menu-dishes__dishes-category_" +
        this.cssNamesByDishCategories[category.name]}
        key={category.name}>

        <p className="menu-dishes__dishes-category-title">
          {category.name}
        </p>
        <div className={"menu-dishes__dishes " +
          "menu-dishes__dishes-" +
            this.cssNamesByDishCategories[category.name]}>

          {this.presentationDishesFromCategory(category.dishes)}

        </div>
      </div>);
  }
  presentationDishesFromCategory(categoryDishes) {
    return categoryDishes.map(dish => this.presentationDish(dish));
  }
  presentationDish(dish) {
    return (
      <div className="menu-dishes__dish" key={dish.key}>
        {dish}
      </div>
    );
  }
}
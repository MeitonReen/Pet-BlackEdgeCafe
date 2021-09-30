import * as CafeApi from "./api/src/index"
import StatusCodeService from "./StatusCodeService"

export default class CafeAPI {
	constructor() {
	}
	get account() {
		return new Account();
	}
	get cart() {
		return new Cart();
	}
	get menu() {
		return new Menu();
	}
	get orders() {
		return new Orders();
	}
	get tables() {
		return new Tables();
	}
	get bookedTables() {
		return new BookedTables();
	}
}
class Account {
	constructor() {
		this.apiInstance = new CafeApi.AccountApi();
		this.apiInstance.apiClient.defaultHeaders = {};
		this.apiInstance.apiClient.enableCookies = true;
	}
	login(login, password, callback) {
		this.apiInstance.login(login, password, callback);
		return;
	}
	checkLogin(callback) {
		this.apiInstance.checkLogin(callback);
		return;
	}
	logout(callback) {
		this.apiInstance.logout(callback);
		return;
	}
	registration(login, password, confirmPassword, callback) {
		this.apiInstance.registration(login, password,
			confirmPassword, callback);
		return;
	}
}
class AntiforgeryToken {
	constructor() {
		this.apiInstance = new CafeApi.AntiforgeryTokenApi();
		this.apiInstance.apiClient.defaultHeaders = {};
		this.apiInstance.apiClient.enableCookies = true;
	}
	getAntiforgeryToken(thenCallback) {
		this.apiInstance.getAntiforgeryToken(
			(error, data, response) => {
				new StatusCodeService()
					.if([204], response, () => {
						let cSRFToken = response.headers["x-csrf-token-response"];
						if (thenCallback) {
							thenCallback(cSRFToken);
						}
					})
					.if([500], response, null);
			});
		return;
	}
}
class Cart {
	constructor() {
		this.apiInstance = new CafeApi.CartApi();
		this.apiInstance.apiClient.defaultHeaders = {};
		this.apiInstance.apiClient.enableCookies = true;

		//this.apiInstanceCSRFToken = new AntiforgeryToken();
		return;
	}
	addDish(dishId, callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.addDish(cSRFToken, dishId, callback));*/
		this.apiInstance.addDish(dishId, callback)
		return;
	}
	addPromocode(promocode, callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.addPromocode(cSRFToken, promocode, callback));*/
		this.apiInstance.addPromocode(promocode, callback)
		return;
	}
	createCart(callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.createCart(cSRFToken, callback));*/
		this.apiInstance.createCart(callback)
		return;
	}
	deleteDish(dishId, callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.deleteDish(dishId, cSRFToken, callback));*/
		this.apiInstance.deleteDish(dishId, callback)
		return;
	}
	deleteDishesByDishId(dishId, callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.deleteDishesByDishId(dishId, cSRFToken, callback));*/
		this.apiInstance.deleteDishesByDishId(dishId, callback)
		return;
	}
	getCartState(callback) {
		this.apiInstance.getCartState(callback);
		return;
	}
	getCookingStatus(callback) {
		this.apiInstance.getCookingStatus(callback);
		return;
	}
	getMiniCartState(callback) {
		this.apiInstance.getMiniCartState(callback);
		return;
	}
	getTableId(callback) {
		this.apiInstance.getTableId(callback);
		return;
	}
	setCookingStatus(newCookingStatus, callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.setCookingStatus(cSRFToken, newCookingStatus,
				callback));*/
		this.apiInstance.setCookingStatus(newCookingStatus,
			callback)
		return;
	}
	setTableId(tableId, callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.setTableId(cSRFToken, tableId, callback));*/
		this.apiInstance.setTableId(tableId, callback)
		return;
	}
}
class Menu {
	constructor() {
		this.apiInstance = new CafeApi.MenuApi();
		this.apiInstance.apiClient.defaultHeaders = {};
		return;
	}
	getDishDetails(dishId, callback) {
		this.apiInstance.getDishDetails(dishId, callback);
		return;
	}
	getDishIdsByCategories(callback) {
		this.apiInstance.getDishIdsByCategories(callback);
		return;
	}
	getMenu(callback) {
		this.apiInstance.getMenu(callback);
		return;
	}
	getMenuIncludingPromocodes(callback) {
		this.apiInstance.apiClient.enableCookies = true;
		this.apiInstance.getMenuIncludingPromocodes(callback);
		return;
	}
}
class Orders {
	constructor() {
		this.apiInstance = new CafeApi.OrdersApi();
		this.apiInstance.apiClient.defaultHeaders = {};
		this.apiInstance.apiClient.enableCookies = true;

		//this.apiInstanceCSRFToken = new AntiforgeryToken();
		return;
	}
	createOrderFromCart(callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.createOrderFromCart(cSRFToken, callback));*/
		this.apiInstance.createOrderFromCart(callback)
		return;
	}
	getOrdersOnTables(callback) {
		this.apiInstance.getOrdersOnTables(callback);
		return;
	}
}
class Tables {
	constructor() {
		this.apiInstance = new CafeApi.TablesApi();
		this.apiInstance.apiClient.defaultHeaders = {};
	}
	getTables(callback) {
		this.apiInstance.getTables(callback);
		return;
	}
}
class BookedTables {
	constructor() {
		this.apiInstance = new CafeApi.BookedTablesApi();
		this.apiInstance.apiClient.enableCookies = true;
		this.apiInstance.apiClient.defaultHeaders = {};

		//this.apiInstanceCSRFToken = new AntiforgeryToken();
		return;
	}
	bookATable(tableId, callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.bookATable(cSRFToken, tableId, callback));*/
		this.apiInstance.bookATable(tableId, callback)
		return;
	}
	unbookATable(tableId, callback) {
		/*this.apiInstanceCSRFToken.getAntiforgeryToken(cSRFToken =>
			this.apiInstance.unbookATable(cSRFToken, tableId, callback));*/
		this.apiInstance.unbookATable(tableId, callback)
		return;
	}
	getBookedTables(callback) {
		this.apiInstance.getBookedTables(callback);
		return;
	}
	getBookedTablesIdsWithMarksIsBookedByClient(callback) {
		this.apiInstance.getBookedTablesIdsWithMarksIsBookedByClient(callback);
		return;
	}
}
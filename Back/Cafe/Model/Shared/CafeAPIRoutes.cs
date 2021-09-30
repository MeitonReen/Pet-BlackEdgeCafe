using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cafe.Model.Shared
{
	public static class CafeAPIRoutes
	{
		public static class V1
		{
			public const string This = "api/v1";
			public static class Account
			{
				public const string Login = "account/login";
				public const string Logout = "account/logout";
				public const string Registration = "account/registration";
			}
			public static class AntiforgeryToken
			{
				public const string This = "antiforgery-token";
			}
			public static class BookedTables
			{
				public const string This = "booked-tables";
				public const string TableIdsWithMarksIsBookedByClient = "booked-tables/tableids-with-marks-is-booked-by-client";
			}
			public static class Cart
			{
				public const string This = "cart";
				public const string CookingStatus = "cart/cooking-status";
				public const string Dish = "cart/dishes/{dishId}/dish";
				public const string Dishes = "cart/dishes";
				public const string MiniCart = "cart/mini-cart";
				public const string Promocodes = "cart/promocodes";
				public const string TableId = "cart/table-id";
			}
			public static class Error
			{
				public const string This = "/error";
			}
			public static class Menu
			{
				public const string This = "menu";
				public const string DishDetails = "menu/dishes/details/{dishId}";
				public const string DishesIncludingAppliedPromocodes = "menu/dishes-including-applied-promocodes";
				public const string DishIdsByCategories = "menu/dishids-by-categories";
			}
			public static class Orders
			{
				public const string CreateOrderFromCart = "orders/create-order-from-cart";
				public const string OrdersOnTables = "orders/orders-on-tables";
			}

			public static class Tables
			{
				public const string This = "tables";
			}
		}
	}
}

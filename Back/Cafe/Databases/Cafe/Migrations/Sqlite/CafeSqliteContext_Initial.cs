using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cafe.Databases.Cafe.Migrations.Sqlite
{
	public partial class CafeSqliteContext_Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "DishCategories",
				columns: table => new
				{
					CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
					Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DishCategories_CategoryId", x => x.CategoryId);
				});

			migrationBuilder.CreateTable(
				name: "Dishes",
				columns: table => new
				{
					DishId = table.Column<Guid>(type: "TEXT", nullable: false),
					Name = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
					Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
					Weight = table.Column<int>(type: "INTEGER", nullable: false),
					Cost = table.Column<int>(type: "INTEGER", nullable: false),
					Calorie = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Dishes_DishId", x => x.DishId);
				});

			migrationBuilder.CreateTable(
				name: "Promocodes",
				columns: table => new
				{
					PromocodeId = table.Column<Guid>(type: "TEXT", nullable: false),
					PromocodeText = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
					CoefficientDiscount = table.Column<float>(type: "REAL", nullable: false),
					IsValid = table.Column<bool>(type: "INTEGER", nullable: false, defaultValueSql: "((1))")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Promocodes_PromocodeId", x => x.PromocodeId);
				});

			migrationBuilder.CreateTable(
				name: "Tables",
				columns: table => new
				{
					TableId = table.Column<Guid>(type: "TEXT", nullable: false),
					TableNumber = table.Column<int>(type: "INTEGER", nullable: false),
					NumberOfSeats = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Tables_TableId", x => x.TableId);
				});

			migrationBuilder.CreateTable(
				name: "DishesByCategories",
				columns: table => new
				{
					DishId = table.Column<Guid>(type: "TEXT", nullable: false),
					CategoryId = table.Column<Guid>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DishesByCaregories_DishIdCategoryId", x => new { x.DishId, x.CategoryId });
					table.ForeignKey(
						name: "FK_DishesByCategories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "DishCategories",
						principalColumn: "CategoryId",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_DishesByCategories_DishId",
						column: x => x.DishId,
						principalTable: "Dishes",
						principalColumn: "DishId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "PromocodesForDishCategories",
				columns: table => new
				{
					PromocodeId = table.Column<Guid>(type: "TEXT", nullable: false),
					CategoryId = table.Column<Guid>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PromocodesForDishCategories_PromocodeIdDishCategoryId", x => new { x.PromocodeId, x.CategoryId });
					table.ForeignKey(
						name: "FK_PromocodesForDishCategories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "DishCategories",
						principalColumn: "CategoryId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_PromocodesForDishCategories_PromocodeId",
						column: x => x.PromocodeId,
						principalTable: "Promocodes",
						principalColumn: "PromocodeId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "BookedTables",
				columns: table => new
				{
					BookedTableId = table.Column<Guid>(type: "TEXT", nullable: false),
					ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
					TableId = table.Column<Guid>(type: "TEXT", nullable: false),
					DateTimeATableIsWillBeFree = table.Column<DateTime>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BookedTables_BookedTableId", x => x.BookedTableId);
					table.ForeignKey(
						name: "FK_BookedTables_TableId",
						column: x => x.TableId,
						principalTable: "Tables",
						principalColumn: "TableId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Carts",
				columns: table => new
				{
					CartId = table.Column<Guid>(type: "TEXT", nullable: false),
					ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
					TableId = table.Column<Guid>(type: "TEXT", nullable: true),
					CookingStatus = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
					Amount = table.Column<int>(type: "INTEGER", nullable: false),
					AmountIncluding_Valid_Applied_Promocodes = table.Column<float>(type: "REAL", nullable: true),
					RowVersion = table.Column<byte[]>(type: "BLOB", nullable: true),
					SqliteVersion = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Carts_CartIdClientId", x => new { x.CartId, x.ClientId });
					table.ForeignKey(
						name: "FK_Carts_TableId",
						column: x => x.TableId,
						principalTable: "Tables",
						principalColumn: "TableId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Orders",
				columns: table => new
				{
					OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
					ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
					TableId = table.Column<Guid>(type: "TEXT", nullable: false),
					DateTimeIsCompleted = table.Column<DateTime>(type: "TEXT", nullable: false),
					CookingStatus = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
					Amount = table.Column<int>(type: "INTEGER", nullable: false),
					AmountIncluding_Applied_Promocodes = table.Column<float>(type: "REAL", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Orders_OrderIdClientId", x => new { x.OrderId, x.ClientId });
					table.ForeignKey(
						name: "FK_Orders_TableId",
						column: x => x.TableId,
						principalTable: "Tables",
						principalColumn: "TableId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "AppliedPromocodesInCarts",
				columns: table => new
				{
					PromocodeId = table.Column<Guid>(type: "TEXT", nullable: false),
					CartId = table.Column<Guid>(type: "TEXT", nullable: false),
					ClientId = table.Column<Guid>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppliedPromocodesInCarts_PromocodeIdCartId", x => new { x.PromocodeId, x.CartId });
					table.ForeignKey(
						name: "FK_AppliedPromocodesInCarts_OrdersIdClientId",
						columns: x => new { x.CartId, x.ClientId },
						principalTable: "Carts",
						principalColumns: new[] { "CartId", "ClientId" },
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AppliedPromocodesInCarts_PromocodeId",
						column: x => x.PromocodeId,
						principalTable: "Promocodes",
						principalColumn: "PromocodeId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "CartsLinkedDishes",
				columns: table => new
				{
					ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
					CartId = table.Column<Guid>(type: "TEXT", nullable: false),
					DishId = table.Column<Guid>(type: "TEXT", nullable: false),
					CostIncluding_Valid_Applied_Promocodes = table.Column<float>(type: "REAL", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CartsLinkedDishes_CartIdClientIdDishId", x => new { x.ClientId, x.CartId, x.DishId });
					table.ForeignKey(
						name: "FK_CartsLinkedDishes_CartIdClientId",
						columns: x => new { x.CartId, x.ClientId },
						principalTable: "Carts",
						principalColumns: new[] { "CartId", "ClientId" },
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_CartsLinkedDishes_DishId",
						column: x => x.DishId,
						principalTable: "Dishes",
						principalColumn: "DishId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "DishesInCarts",
				columns: table => new
				{
					DishesInCartsId = table.Column<Guid>(type: "TEXT", nullable: false),
					CartId = table.Column<Guid>(type: "TEXT", nullable: false),
					ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
					DishId = table.Column<Guid>(type: "TEXT", nullable: false),
					CostIncluding_Valid_Applied_Promocodes = table.Column<float>(type: "REAL", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DishesInCarts_DishesInCartsId", x => x.DishesInCartsId);
					table.ForeignKey(
						name: "FK_DishesInCarts_CartIdClientId",
						columns: x => new { x.CartId, x.ClientId },
						principalTable: "Carts",
						principalColumns: new[] { "CartId", "ClientId" },
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_DishesInCarts_DishId",
						column: x => x.DishId,
						principalTable: "Dishes",
						principalColumn: "DishId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AppliedPromocodesInOrders",
				columns: table => new
				{
					PromocodeId = table.Column<Guid>(type: "TEXT", nullable: false),
					OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
					ClientId = table.Column<Guid>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppliedPromocodesInOrders_PromocodeIdOrderId", x => new { x.PromocodeId, x.OrderId });
					table.ForeignKey(
						name: "FK_AppliedPromocodesInOrders_OrdersIdClientId",
						columns: x => new { x.OrderId, x.ClientId },
						principalTable: "Orders",
						principalColumns: new[] { "OrderId", "ClientId" },
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AppliedPromocodesInOrders_PromocodeId",
						column: x => x.PromocodeId,
						principalTable: "Promocodes",
						principalColumn: "PromocodeId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "DishesInOrders",
				columns: table => new
				{
					DishesInOrderId = table.Column<Guid>(type: "TEXT", nullable: false),
					OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
					ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
					DishId = table.Column<Guid>(type: "TEXT", nullable: false),
					CostIncluding_Applied_Promocodes = table.Column<float>(type: "REAL", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DishesInOrders_DishesInOrderId", x => x.DishesInOrderId);
					table.ForeignKey(
						name: "FK_DishesInOrders_DishId",
						column: x => x.DishId,
						principalTable: "Dishes",
						principalColumn: "DishId",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_DishesInOrders_OrderIdClientId",
						columns: x => new { x.OrderId, x.ClientId },
						principalTable: "Orders",
						principalColumns: new[] { "OrderId", "ClientId" },
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("faf45d13-629c-4fde-7fb1-08d9721b6049"), "Основное меню" });

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("58b484db-a2fe-4391-7fb0-08d9721b6049"), "Безалкогольные" });

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("9174f131-68cf-47d2-7faf-08d9721b6049"), "Шоты" });

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("ebb4cebc-0f51-4587-7fae-08d9721b6049"), "Лонги" });

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("7d02e0a8-05fb-4fdf-7fad-08d9721b6049"), "Закуски" });

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("31c65a6d-8cd6-40fd-7fac-08d9721b6049"), "Рыба" });

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("625db075-f113-4487-7fab-08d9721b6049"), "Жаркое" });

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("9d9f9b5a-7ccb-4f17-7faa-08d9721b6049"), "Супы" });

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("fe86c0c4-5c54-48e9-7fa9-08d9721b6049"), "Салаты" });

			migrationBuilder.InsertData(
				table: "DishCategories",
				columns: new[] { "CategoryId", "Name" },
				values: new object[] { new Guid("ca7ae39d-1628-4e54-7fb2-08d9721b6049"), "Напитки" });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("c828dc04-a100-4f1b-7f98-08d9721b6049"), 500, 255, "Мясная тарелка – это калейдоскоп деликатесов, изящно дополненный овощами и соусом от шеф-повара. Каждая тарелка грамотно создаётся нашим шеф-поваром в рамках вкусовой палитры. Мясная тарелка станет прекрасным дополнением к крепкому алкоголю – коньяку, бренди или виски, а также сухому красному вину", "Мясная тарелка", 240 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("a338d18a-01a2-4427-7fa8-08d9721b6049"), 260, 290, "Ром — 30 мл, водка — 30 мл, трипл сек — 15 мл, ананасовый сок — 30 мл, фреш лайма — 20 мл, сахарный сироп — 15 мл, долька ананаса", "Джангл Джус", 140 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("c2c519e8-9258-453d-7fa7-08d9721b6049"), 190, 220, "Яблочный ликёр – 20 мл, лимонный сок – 15 мл, шотландский виски – 20 мл", "Яблочный сауэр", 55 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("f42ce5c7-ea9a-45a5-7fa6-08d9721b6049"), 280, 270, "Водка – 45 мл, томатный сок – 90 мл, лимонный сок – 15 мл, вустерский соус – 2-3 капли, табаско – щепотка – 2-3 капли, смесь соли c порошком сельдерея – 1 щепотка, чёрный перец – 1 щепотка", "Кровавая мэри", 155 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("a4d5cc1f-c92f-4105-7fa5-08d9721b6049"), 270, 250, "Ликер мятный зеленый – 15 мл, ликер сливочный Бейлис – 15 мл, бренди – 15 мл", "Ирландский флаг", 45 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("6ad4b3ce-2d3b-4741-7fa3-08d9721b6049"), 400, 340, "Кофейный ликёр – 1 часть, айриш крем – 1 часть, светлый ром – 1 часть", "Б-51", 120 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("a6acf138-f250-44d0-7fa2-08d9721b6049"), 400, 340, "Белый ром – 30 мл, Блю Кюросао – 20 мл, кокосовое молоко – 50 мл, водка – 30 мл, огурец, ломтики персика", "Скай мохито", 330 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("45be7b65-2794-403d-7fa1-08d9721b6049"), 440, 370, "Белый ром – 50 мл, клубничный сироп – 15 мл, содовая – 100 мл, лайм – 60 г, клубника – 120 г, мята – 3 г", "Клубничный мохито", 370 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("b34a9e55-2d86-4875-7fa0-08d9721b6049"), 480, 345, "Водка – 120 мл, грейпфрутовый сок – 220 мл, гренадин – 15 мл", "Алёша", 320 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("172d110a-1195-4e30-7f9f-08d9721b6049"), 320, 350, "Лимонный сок – 5 мл, сахарный сироп – 5 мл, ликер мараскино – 15 мл, ликер трипл сек – 15 мл, коньяк – 50 мл, ангостуру биттер – 1 дэш, апельсиновый биттер – 1 дэш, лимонная цедра, сахарная окаемка", "Бренди круста", 300 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("4f244538-79ff-4241-7f9e-08d9721b6049"), 480, 350, "Салат Цезарь — одно из самых популярных и востребованных блюд, на протяжении более чем сотни лет это бессменная классика европейской кухни. За долгие годы этот салат набрал десятки интересных вариаций состава и подачи, в нашем кафе его подают с креветками", "Цезарь с креветками", 300 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("613c4221-7af7-4609-7f9d-08d9721b6049"), 450, 350, "Тартар из лосося это блюдо французской кухни. Основой рецепта является тартар из говядины, но вместо мяса используется лосось. Свежий или малосольный лосось маринуется вместе с луком и специями, и подается порционно на тарелке вместе с авокадо и гренками", "Тартар из лосося", 250 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("21ef72f6-8452-4b71-7f9c-08d9721b6049"), 500, 400, "Многокомпонентный красный суп с наваристым вкусом. Мы готовим борщ на мясе телятины и говядины чтобы избежать большого содержания в блюде холестерина, по мере готовности добавляем свёклу, томат, картофель, лук, морковь. В качестве дополнительных ингредиентов мы добавляем сладкий перец и грибы", "Борщец", 400 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("035bf9b2-046d-400b-7f9b-08d9721b6049"), 500, 500, "Благодаря маринаду на базе 9 специй стейк получается очень пикантный, мягкий и сочный. Подаётся с соусом от шеф-повара и овощами", "Говяжий стейк", 350 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("dab47268-f6bf-4e4f-7f9a-08d9721b6049"), 600, 500, "Благодаря маринаду на базе 9 специй стейк получается очень пикантный, мягкий и сочный. Подаётся с соусом от шеф-повара и овощами", "Свиной стейк", 350 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("608f017c-d7b8-4237-7f99-08d9721b6049"), 500, 880, "Греческий салат – легкое, вкусное и исключительно полезное блюдо. Сочный помидор, хрустящий огурец, нарезанный красный лук, сладкий перец, рассыпчатый сыр «Фета», пухлые маслины и оливковое масло", "Греческий салат", 280 });

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[] { new Guid("08476b46-5546-41a0-7fa4-08d9721b6049"), 200, 120, "Водка — 25 мл, гренадин – 25 мл, красный острый соус \"Табаско\" – 5 мл", "Боярский", 55 });

			migrationBuilder.InsertData(
				table: "Promocodes",
				columns: new[] { "PromocodeId", "CoefficientDiscount", "PromocodeText" },
				values: new object[] { new Guid("7d6397d4-38be-440a-7fce-08d9721b6049"), 0.07f, "HAPPY_WINTER_7%" });

			migrationBuilder.InsertData(
				table: "Promocodes",
				columns: new[] { "PromocodeId", "CoefficientDiscount", "PromocodeText" },
				values: new object[] { new Guid("4bec85a6-4093-43e7-7fcd-08d9721b6049"), 0.1f, "BlackEdge_SpringPromotion_10%" });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("a3ac5a37-91be-4e10-7fb3-08d9721b6049"), 3, 1 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("46139d5f-46ae-4746-7fca-08d9721b6049"), 4, 24 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("0dc791b0-1e4b-4eba-7fc9-08d9721b6049"), 4, 23 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("d1f7a12e-927d-41c9-7fc8-08d9721b6049"), 3, 22 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("c6dac042-3ad0-4474-7fc7-08d9721b6049"), 3, 21 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("d75b47dd-701c-477b-7fc6-08d9721b6049"), 3, 20 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("85af3eb6-b31f-43fd-7fc5-08d9721b6049"), 3, 19 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("466c8310-2e42-423c-7fc4-08d9721b6049"), 4, 18 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("64e5e06b-528c-4b50-7fc3-08d9721b6049"), 4, 17 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("41cc2d80-f76c-472d-7fc2-08d9721b6049"), 4, 16 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("4e7fba45-7597-4544-7fc1-08d9721b6049"), 2, 15 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("b4015d97-0f55-42aa-7fc0-08d9721b6049"), 2, 14 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("d175e60f-4a93-4b3a-7fbf-08d9721b6049"), 2, 13 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("e56920c7-8bd9-4827-7fbe-08d9721b6049"), 5, 12 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("4eaadeb4-19cb-48b2-7fbd-08d9721b6049"), 2, 11 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("fd89d4f5-6da1-47de-7fbc-08d9721b6049"), 2, 10 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("575e94ea-673d-44d0-7fbb-08d9721b6049"), 2, 9 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("f89367f0-26db-4b0c-7fba-08d9721b6049"), 16, 8 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("01b3386b-c43b-4f23-7fb9-08d9721b6049"), 4, 7 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("85c9102c-3149-45d0-7fb8-08d9721b6049"), 4, 6 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("20ae8555-e9a5-4034-7fb7-08d9721b6049"), 4, 5 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("1f5450ee-93a9-40c5-7fb6-08d9721b6049"), 3, 4 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("b4bf53aa-80e2-44a1-7fb5-08d9721b6049"), 3, 3 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("4fd9a172-2fa6-40b9-7fb4-08d9721b6049"), 3, 2 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("61528303-1007-4b86-7fcb-08d9721b6049"), 4, 25 });

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[] { new Guid("f03d376b-3020-47fd-7fcc-08d9721b6049"), 13, 26 });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("fe86c0c4-5c54-48e9-7fa9-08d9721b6049"), new Guid("608f017c-d7b8-4237-7f99-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("9174f131-68cf-47d2-7faf-08d9721b6049"), new Guid("a338d18a-01a2-4427-7fa8-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("9174f131-68cf-47d2-7faf-08d9721b6049"), new Guid("c2c519e8-9258-453d-7fa7-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("9174f131-68cf-47d2-7faf-08d9721b6049"), new Guid("a4d5cc1f-c92f-4105-7fa5-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("9174f131-68cf-47d2-7faf-08d9721b6049"), new Guid("08476b46-5546-41a0-7fa4-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("9174f131-68cf-47d2-7faf-08d9721b6049"), new Guid("6ad4b3ce-2d3b-4741-7fa3-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("ebb4cebc-0f51-4587-7fae-08d9721b6049"), new Guid("a6acf138-f250-44d0-7fa2-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("ebb4cebc-0f51-4587-7fae-08d9721b6049"), new Guid("b34a9e55-2d86-4875-7fa0-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("9174f131-68cf-47d2-7faf-08d9721b6049"), new Guid("f42ce5c7-ea9a-45a5-7fa6-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("7d02e0a8-05fb-4fdf-7fad-08d9721b6049"), new Guid("613c4221-7af7-4609-7f9d-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("7d02e0a8-05fb-4fdf-7fad-08d9721b6049"), new Guid("c828dc04-a100-4f1b-7f98-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("625db075-f113-4487-7fab-08d9721b6049"), new Guid("035bf9b2-046d-400b-7f9b-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("625db075-f113-4487-7fab-08d9721b6049"), new Guid("dab47268-f6bf-4e4f-7f9a-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("9d9f9b5a-7ccb-4f17-7faa-08d9721b6049"), new Guid("21ef72f6-8452-4b71-7f9c-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("fe86c0c4-5c54-48e9-7fa9-08d9721b6049"), new Guid("4f244538-79ff-4241-7f9e-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[] { new Guid("ebb4cebc-0f51-4587-7fae-08d9721b6049"), new Guid("172d110a-1195-4e30-7f9f-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("9174f131-68cf-47d2-7faf-08d9721b6049"), new Guid("7d6397d4-38be-440a-7fce-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("9d9f9b5a-7ccb-4f17-7faa-08d9721b6049"), new Guid("7d6397d4-38be-440a-7fce-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("58b484db-a2fe-4391-7fb0-08d9721b6049"), new Guid("4bec85a6-4093-43e7-7fcd-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("7d02e0a8-05fb-4fdf-7fad-08d9721b6049"), new Guid("4bec85a6-4093-43e7-7fcd-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("ebb4cebc-0f51-4587-7fae-08d9721b6049"), new Guid("4bec85a6-4093-43e7-7fcd-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("625db075-f113-4487-7fab-08d9721b6049"), new Guid("4bec85a6-4093-43e7-7fcd-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("9d9f9b5a-7ccb-4f17-7faa-08d9721b6049"), new Guid("4bec85a6-4093-43e7-7fcd-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("fe86c0c4-5c54-48e9-7fa9-08d9721b6049"), new Guid("4bec85a6-4093-43e7-7fcd-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("625db075-f113-4487-7fab-08d9721b6049"), new Guid("7d6397d4-38be-440a-7fce-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("31c65a6d-8cd6-40fd-7fac-08d9721b6049"), new Guid("4bec85a6-4093-43e7-7fcd-08d9721b6049") });

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[] { new Guid("31c65a6d-8cd6-40fd-7fac-08d9721b6049"), new Guid("7d6397d4-38be-440a-7fce-08d9721b6049") });

			migrationBuilder.CreateIndex(
				name: "IX_AppliedPromocodesInCarts_CartId_ClientId",
				table: "AppliedPromocodesInCarts",
				columns: new[] { "CartId", "ClientId" });

			migrationBuilder.CreateIndex(
				name: "IX_AppliedPromocodesInOrders_OrderId_ClientId",
				table: "AppliedPromocodesInOrders",
				columns: new[] { "OrderId", "ClientId" });

			migrationBuilder.CreateIndex(
				name: "IX_BookedTables_TableId",
				table: "BookedTables",
				column: "TableId");

			migrationBuilder.CreateIndex(
				name: "IX_Carts_TableId",
				table: "Carts",
				column: "TableId");

			migrationBuilder.CreateIndex(
				name: "IX_CartsLinkedDishes_CartId_ClientId",
				table: "CartsLinkedDishes",
				columns: new[] { "CartId", "ClientId" });

			migrationBuilder.CreateIndex(
				name: "IX_CartsLinkedDishes_DishId",
				table: "CartsLinkedDishes",
				column: "DishId");

			migrationBuilder.CreateIndex(
				name: "AK_DishCategories_Name",
				table: "DishCategories",
				column: "Name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "AK_Dishes_Name",
				table: "Dishes",
				column: "Name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_DishesByCategories_CategoryId",
				table: "DishesByCategories",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_DishesInCarts_CartId_ClientId",
				table: "DishesInCarts",
				columns: new[] { "CartId", "ClientId" });

			migrationBuilder.CreateIndex(
				name: "IX_DishesInCarts_DishId",
				table: "DishesInCarts",
				column: "DishId");

			migrationBuilder.CreateIndex(
				name: "IX_DishesInOrders_DishId",
				table: "DishesInOrders",
				column: "DishId");

			migrationBuilder.CreateIndex(
				name: "IX_DishesInOrders_OrderId_ClientId",
				table: "DishesInOrders",
				columns: new[] { "OrderId", "ClientId" });

			migrationBuilder.CreateIndex(
				name: "IX_Orders_TableId",
				table: "Orders",
				column: "TableId");

			migrationBuilder.CreateIndex(
				name: "AK_Promocodes_PromocodeText",
				table: "Promocodes",
				column: "PromocodeText",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_PromocodesForDishCategories_CategoryId",
				table: "PromocodesForDishCategories",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "AK_Tables_TableNumber",
				table: "Tables",
				column: "TableNumber",
				unique: true);

			migrationBuilder.Sql(
			@"CREATE TRIGGER UpdateCartVersion
					AFTER UPDATE ON Carts
					BEGIN
						UPDATE Carts
						SET SqliteVersion = SqliteVersion + 1
						WHERE rowid = NEW.rowid;
					END;");
		}
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AppliedPromocodesInCarts");

			migrationBuilder.DropTable(
				name: "AppliedPromocodesInOrders");

			migrationBuilder.DropTable(
				name: "BookedTables");

			migrationBuilder.DropTable(
				name: "CartsLinkedDishes");

			migrationBuilder.DropTable(
				name: "DishesByCategories");

			migrationBuilder.DropTable(
				name: "DishesInCarts");

			migrationBuilder.DropTable(
				name: "DishesInOrders");

			migrationBuilder.DropTable(
				name: "PromocodesForDishCategories");

			migrationBuilder.DropTable(
				name: "Carts");

			migrationBuilder.DropTable(
				name: "Dishes");

			migrationBuilder.DropTable(
				name: "Orders");

			migrationBuilder.DropTable(
				name: "DishCategories");

			migrationBuilder.DropTable(
				name: "Promocodes");

			migrationBuilder.DropTable(
				name: "Tables");
		}
	}
}

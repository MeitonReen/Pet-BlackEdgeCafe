using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cafe.Databases.Cafe.Migrations.Mssql
{
	public partial class CafeMssqlContext_Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "DishCategories",
				columns: table => new
				{
					CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DishCategories_CategoryId", x => x.CategoryId);
				});

			migrationBuilder.CreateTable(
				name: "Dishes",
				columns: table => new
				{
					DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
					Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
					Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
					Weight = table.Column<int>(type: "int", nullable: false),
					Cost = table.Column<int>(type: "int", nullable: false),
					Calorie = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Dishes_DishId", x => x.DishId);
				});

			migrationBuilder.CreateTable(
				name: "Promocodes",
				columns: table => new
				{
					PromocodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
					PromocodeText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					CoefficientDiscount = table.Column<float>(type: "real", nullable: false),
					IsValid = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Promocodes_PromocodeId", x => x.PromocodeId);
				});

			migrationBuilder.CreateTable(
				name: "Tables",
				columns: table => new
				{
					TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
					TableNumber = table.Column<int>(type: "int", nullable: false),
					NumberOfSeats = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Tables_TableId", x => x.TableId);
				});

			migrationBuilder.CreateTable(
				name: "DishesByCategories",
				columns: table => new
				{
					DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
					PromocodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
					BookedTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					DateTimeATableIsWillBeFree = table.Column<DateTime>(type: "datetime2", nullable: false)
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
					CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
					ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					CookingStatus = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
					Amount = table.Column<int>(type: "int", nullable: false),
					AmountIncluding_Valid_Applied_Promocodes = table.Column<float>(type: "real", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
					SqliteVersion = table.Column<int>(type: "int", nullable: false)
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
					OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
					ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					DateTimeIsCompleted = table.Column<DateTime>(type: "datetime2", nullable: false),
					CookingStatus = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
					Amount = table.Column<int>(type: "int", nullable: false),
					AmountIncluding_Applied_Promocodes = table.Column<float>(type: "real", nullable: true)
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
					PromocodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
					ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CostIncluding_Valid_Applied_Promocodes = table.Column<float>(type: "real", nullable: true)
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
					DishesInCartsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
					CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CostIncluding_Valid_Applied_Promocodes = table.Column<float>(type: "real", nullable: true)
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
					PromocodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
					DishesInOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
					OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CostIncluding_Applied_Promocodes = table.Column<float>(type: "real", nullable: true)
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
				values: new object[,]
				{
					{ new Guid("4fcfa7a1-9b81-4db1-8221-08d9721ad135"), "Основное меню" },
					{ new Guid("f484ee66-1a98-472b-8220-08d9721ad135"), "Безалкогольные" },
					{ new Guid("7c3a2067-91d5-4d8a-821f-08d9721ad135"), "Шоты" },
					{ new Guid("eb4fb1ba-8d9d-4aa0-821e-08d9721ad135"), "Лонги" },
					{ new Guid("2808bfcc-54f9-43ce-821d-08d9721ad135"), "Закуски" },
					{ new Guid("6a5f67c4-ff19-4f3f-821c-08d9721ad135"), "Рыба" },
					{ new Guid("a6219eed-b0c4-44ef-821b-08d9721ad135"), "Жаркое" },
					{ new Guid("263e1f01-d822-4450-821a-08d9721ad135"), "Супы" },
					{ new Guid("67fbf14a-0de6-4a74-8219-08d9721ad135"), "Салаты" },
					{ new Guid("2025a052-324e-4a40-8222-08d9721ad135"), "Напитки" }
				});

			migrationBuilder.InsertData(
				table: "Dishes",
				columns: new[] { "DishId", "Calorie", "Cost", "Description", "Name", "Weight" },
				values: new object[,]
				{
					{ new Guid("3985e3b4-1660-48f0-8208-08d9721ad135"), 500, 255, "Мясная тарелка – это калейдоскоп деликатесов, изящно дополненный овощами и соусом от шеф-повара. Каждая тарелка грамотно создаётся нашим шеф-поваром в рамках вкусовой палитры. Мясная тарелка станет прекрасным дополнением к крепкому алкоголю – коньяку, бренди или виски, а также сухому красному вину", "Мясная тарелка", 240 },
					{ new Guid("d4c3ddae-4cd0-4faa-8218-08d9721ad135"), 260, 290, "Ром — 30 мл, водка — 30 мл, трипл сек — 15 мл, ананасовый сок — 30 мл, фреш лайма — 20 мл, сахарный сироп — 15 мл, долька ананаса", "Джангл Джус", 140 },
					{ new Guid("b30eacc1-e3e7-4bb1-8217-08d9721ad135"), 190, 220, "Яблочный ликёр – 20 мл, лимонный сок – 15 мл, шотландский виски – 20 мл", "Яблочный сауэр", 55 },
					{ new Guid("e913e845-29c8-42f2-8216-08d9721ad135"), 280, 270, "Водка – 45 мл, томатный сок – 90 мл, лимонный сок – 15 мл, вустерский соус – 2-3 капли, табаско – щепотка – 2-3 капли, смесь соли c порошком сельдерея – 1 щепотка, чёрный перец – 1 щепотка", "Кровавая мэри", 155 },
					{ new Guid("c374956e-1554-446b-8215-08d9721ad135"), 270, 250, "Ликер мятный зеленый – 15 мл, ликер сливочный Бейлис – 15 мл, бренди – 15 мл", "Ирландский флаг", 45 },
					{ new Guid("2272d130-fe97-406d-8213-08d9721ad135"), 400, 340, "Кофейный ликёр – 1 часть, айриш крем – 1 часть, светлый ром – 1 часть", "Б-51", 120 },
					{ new Guid("f2ad8ad7-b371-4ba5-8212-08d9721ad135"), 400, 340, "Белый ром – 30 мл, Блю Кюросао – 20 мл, кокосовое молоко – 50 мл, водка – 30 мл, огурец, ломтики персика", "Скай мохито", 330 },
					{ new Guid("11de4db6-eb3e-441c-8211-08d9721ad135"), 440, 370, "Белый ром – 50 мл, клубничный сироп – 15 мл, содовая – 100 мл, лайм – 60 г, клубника – 120 г, мята – 3 г", "Клубничный мохито", 370 },
					{ new Guid("7f1240eb-051c-41db-8210-08d9721ad135"), 480, 345, "Водка – 120 мл, грейпфрутовый сок – 220 мл, гренадин – 15 мл", "Алёша", 320 },
					{ new Guid("f4a3f561-f09c-45b9-820f-08d9721ad135"), 320, 350, "Лимонный сок – 5 мл, сахарный сироп – 5 мл, ликер мараскино – 15 мл, ликер трипл сек – 15 мл, коньяк – 50 мл, ангостуру биттер – 1 дэш, апельсиновый биттер – 1 дэш, лимонная цедра, сахарная окаемка", "Бренди круста", 300 },
					{ new Guid("b71267c0-1b38-4b6b-820e-08d9721ad135"), 480, 350, "Салат Цезарь — одно из самых популярных и востребованных блюд, на протяжении более чем сотни лет это бессменная классика европейской кухни. За долгие годы этот салат набрал десятки интересных вариаций состава и подачи, в нашем кафе его подают с креветками", "Цезарь с креветками", 300 },
					{ new Guid("4b5f8ebf-32e7-4686-820d-08d9721ad135"), 450, 350, "Тартар из лосося это блюдо французской кухни. Основой рецепта является тартар из говядины, но вместо мяса используется лосось. Свежий или малосольный лосось маринуется вместе с луком и специями, и подается порционно на тарелке вместе с авокадо и гренками", "Тартар из лосося", 250 },
					{ new Guid("b7908776-c71e-40a4-820c-08d9721ad135"), 500, 400, "Многокомпонентный красный суп с наваристым вкусом. Мы готовим борщ на мясе телятины и говядины чтобы избежать большого содержания в блюде холестерина, по мере готовности добавляем свёклу, томат, картофель, лук, морковь. В качестве дополнительных ингредиентов мы добавляем сладкий перец и грибы", "Борщец", 400 },
					{ new Guid("2a5704e9-7dbd-4ead-820b-08d9721ad135"), 500, 500, "Благодаря маринаду на базе 9 специй стейк получается очень пикантный, мягкий и сочный. Подаётся с соусом от шеф-повара и овощами", "Говяжий стейк", 350 },
					{ new Guid("232f2163-329a-4743-820a-08d9721ad135"), 600, 500, "Благодаря маринаду на базе 9 специй стейк получается очень пикантный, мягкий и сочный. Подаётся с соусом от шеф-повара и овощами", "Свиной стейк", 350 },
					{ new Guid("ff4f4b78-ba2a-414d-8209-08d9721ad135"), 500, 880, "Греческий салат – легкое, вкусное и исключительно полезное блюдо. Сочный помидор, хрустящий огурец, нарезанный красный лук, сладкий перец, рассыпчатый сыр «Фета», пухлые маслины и оливковое масло", "Греческий салат", 280 },
					{ new Guid("6e034835-6045-4260-8214-08d9721ad135"), 200, 120, "Водка — 25 мл, гренадин – 25 мл, красный острый соус \"Табаско\" – 5 мл", "Боярский", 55 }
				});

			migrationBuilder.InsertData(
				table: "Promocodes",
				columns: new[] { "PromocodeId", "CoefficientDiscount", "PromocodeText" },
				values: new object[,]
				{
					{ new Guid("27be7614-bf30-4c78-823e-08d9721ad135"), 0.07f, "HAPPY_WINTER_7%" },
					{ new Guid("61b99a81-fb12-4122-823d-08d9721ad135"), 0.1f, "BlackEdge_SpringPromotion_10%" }
				});

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[,]
				{
					{ new Guid("14985e5a-0b5e-46a3-8223-08d9721ad135"), 3, 1 },
					{ new Guid("67e7b8d2-9078-4774-823a-08d9721ad135"), 4, 24 },
					{ new Guid("96e19809-d508-4559-8239-08d9721ad135"), 4, 23 },
					{ new Guid("1e0fe4d8-cbca-4eed-8238-08d9721ad135"), 3, 22 },
					{ new Guid("9392548a-1a16-416e-8237-08d9721ad135"), 3, 21 },
					{ new Guid("d8aaa1f1-a062-44e9-8236-08d9721ad135"), 3, 20 },
					{ new Guid("f73fecea-5bb9-4171-8235-08d9721ad135"), 3, 19 },
					{ new Guid("bb50e01b-19ee-461c-8234-08d9721ad135"), 4, 18 },
					{ new Guid("3bc875d2-b99f-4b68-8233-08d9721ad135"), 4, 17 },
					{ new Guid("0ac6178c-3c25-4dc6-8232-08d9721ad135"), 4, 16 },
					{ new Guid("4f944c08-9f27-4674-8231-08d9721ad135"), 2, 15 },
					{ new Guid("0cf5b190-7f15-4e52-8230-08d9721ad135"), 2, 14 },
					{ new Guid("7608dd10-cf1b-4f86-822f-08d9721ad135"), 2, 13 }
				});

			migrationBuilder.InsertData(
				table: "Tables",
				columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
				values: new object[,]
				{
					{ new Guid("aadb451a-56ee-4d9c-822e-08d9721ad135"), 5, 12 },
					{ new Guid("d07865a6-14da-4bf3-822d-08d9721ad135"), 2, 11 },
					{ new Guid("c7ddbcdf-c204-415a-822c-08d9721ad135"), 2, 10 },
					{ new Guid("42f50cac-50c6-4dee-822b-08d9721ad135"), 2, 9 },
					{ new Guid("e9215366-bd19-4b7d-822a-08d9721ad135"), 16, 8 },
					{ new Guid("d90df328-723e-4bf8-8229-08d9721ad135"), 4, 7 },
					{ new Guid("a4672bf4-4061-4425-8228-08d9721ad135"), 4, 6 },
					{ new Guid("5b4bbb2f-5be0-43ec-8227-08d9721ad135"), 4, 5 },
					{ new Guid("866b43ea-c3a5-4c6c-8226-08d9721ad135"), 3, 4 },
					{ new Guid("f1e4567c-7062-4516-8225-08d9721ad135"), 3, 3 },
					{ new Guid("805c244c-a440-489d-8224-08d9721ad135"), 3, 2 },
					{ new Guid("9537a555-11f9-470a-823b-08d9721ad135"), 4, 25 },
					{ new Guid("ea13a612-0af3-4fee-823c-08d9721ad135"), 13, 26 }
				});

			migrationBuilder.InsertData(
				table: "DishesByCategories",
				columns: new[] { "CategoryId", "DishId" },
				values: new object[,]
				{
					{ new Guid("67fbf14a-0de6-4a74-8219-08d9721ad135"), new Guid("ff4f4b78-ba2a-414d-8209-08d9721ad135") },
					{ new Guid("7c3a2067-91d5-4d8a-821f-08d9721ad135"), new Guid("d4c3ddae-4cd0-4faa-8218-08d9721ad135") },
					{ new Guid("7c3a2067-91d5-4d8a-821f-08d9721ad135"), new Guid("b30eacc1-e3e7-4bb1-8217-08d9721ad135") },
					{ new Guid("7c3a2067-91d5-4d8a-821f-08d9721ad135"), new Guid("c374956e-1554-446b-8215-08d9721ad135") },
					{ new Guid("7c3a2067-91d5-4d8a-821f-08d9721ad135"), new Guid("6e034835-6045-4260-8214-08d9721ad135") },
					{ new Guid("7c3a2067-91d5-4d8a-821f-08d9721ad135"), new Guid("2272d130-fe97-406d-8213-08d9721ad135") },
					{ new Guid("eb4fb1ba-8d9d-4aa0-821e-08d9721ad135"), new Guid("f2ad8ad7-b371-4ba5-8212-08d9721ad135") },
					{ new Guid("eb4fb1ba-8d9d-4aa0-821e-08d9721ad135"), new Guid("7f1240eb-051c-41db-8210-08d9721ad135") },
					{ new Guid("7c3a2067-91d5-4d8a-821f-08d9721ad135"), new Guid("e913e845-29c8-42f2-8216-08d9721ad135") },
					{ new Guid("2808bfcc-54f9-43ce-821d-08d9721ad135"), new Guid("4b5f8ebf-32e7-4686-820d-08d9721ad135") },
					{ new Guid("2808bfcc-54f9-43ce-821d-08d9721ad135"), new Guid("3985e3b4-1660-48f0-8208-08d9721ad135") },
					{ new Guid("a6219eed-b0c4-44ef-821b-08d9721ad135"), new Guid("2a5704e9-7dbd-4ead-820b-08d9721ad135") },
					{ new Guid("a6219eed-b0c4-44ef-821b-08d9721ad135"), new Guid("232f2163-329a-4743-820a-08d9721ad135") },
					{ new Guid("263e1f01-d822-4450-821a-08d9721ad135"), new Guid("b7908776-c71e-40a4-820c-08d9721ad135") },
					{ new Guid("67fbf14a-0de6-4a74-8219-08d9721ad135"), new Guid("b71267c0-1b38-4b6b-820e-08d9721ad135") },
					{ new Guid("eb4fb1ba-8d9d-4aa0-821e-08d9721ad135"), new Guid("f4a3f561-f09c-45b9-820f-08d9721ad135") }
				});

			migrationBuilder.InsertData(
				table: "PromocodesForDishCategories",
				columns: new[] { "CategoryId", "PromocodeId" },
				values: new object[,]
				{
					{ new Guid("7c3a2067-91d5-4d8a-821f-08d9721ad135"), new Guid("27be7614-bf30-4c78-823e-08d9721ad135") },
					{ new Guid("263e1f01-d822-4450-821a-08d9721ad135"), new Guid("27be7614-bf30-4c78-823e-08d9721ad135") },
					{ new Guid("f484ee66-1a98-472b-8220-08d9721ad135"), new Guid("61b99a81-fb12-4122-823d-08d9721ad135") },
					{ new Guid("2808bfcc-54f9-43ce-821d-08d9721ad135"), new Guid("61b99a81-fb12-4122-823d-08d9721ad135") },
					{ new Guid("eb4fb1ba-8d9d-4aa0-821e-08d9721ad135"), new Guid("61b99a81-fb12-4122-823d-08d9721ad135") },
					{ new Guid("a6219eed-b0c4-44ef-821b-08d9721ad135"), new Guid("61b99a81-fb12-4122-823d-08d9721ad135") },
					{ new Guid("263e1f01-d822-4450-821a-08d9721ad135"), new Guid("61b99a81-fb12-4122-823d-08d9721ad135") },
					{ new Guid("67fbf14a-0de6-4a74-8219-08d9721ad135"), new Guid("61b99a81-fb12-4122-823d-08d9721ad135") },
					{ new Guid("a6219eed-b0c4-44ef-821b-08d9721ad135"), new Guid("27be7614-bf30-4c78-823e-08d9721ad135") },
					{ new Guid("6a5f67c4-ff19-4f3f-821c-08d9721ad135"), new Guid("61b99a81-fb12-4122-823d-08d9721ad135") },
					{ new Guid("6a5f67c4-ff19-4f3f-821c-08d9721ad135"), new Guid("27be7614-bf30-4c78-823e-08d9721ad135") }
				});

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

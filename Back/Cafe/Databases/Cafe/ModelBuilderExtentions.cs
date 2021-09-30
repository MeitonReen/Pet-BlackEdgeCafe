using Cafe.Databases.Cafe.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace Cafe.Databases.Cafe
{
	public static class ModelBuilderExtentions
	{
		public static void SeedToCafeDB(this ModelBuilder modelBuilder)
		{
			SequentialGuidValueGenerator guidGenerator = new();

			#region Dishes
			Dictionary<string, Dish> dishes = new()
			{
				{
					"Мясная тарелка",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Мясная тарелка",
						Description = "Мясная тарелка – это калейдоскоп деликатесов, изящно дополненный овощами и соусом от шеф-повара. Каждая тарелка грамотно создаётся нашим шеф-поваром в рамках вкусовой палитры. Мясная тарелка станет прекрасным дополнением к крепкому алкоголю – коньяку, бренди или виски, а также сухому красному вину",
						Weight = 240,
						Cost = 255,
						Calorie = 500
					}
				},
				{
					"Греческий салат",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Греческий салат",
						Description = "Греческий салат – легкое, вкусное и исключительно полезное блюдо. Сочный помидор, хрустящий огурец, нарезанный красный лук, сладкий перец, рассыпчатый сыр «Фета», пухлые маслины и оливковое масло",
						Weight = 280,
						Cost = 880,
						Calorie = 500
					}
				},
				{
					"Свиной стейк",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Свиной стейк",
						Description = "Благодаря маринаду на базе 9 специй стейк получается очень пикантный, мягкий и сочный. Подаётся с соусом от шеф-повара и овощами",
						Weight = 350,
						Cost = 500,
						Calorie = 600
					}
				},
				{
					"Говяжий стейк",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Говяжий стейк",
						Description = "Благодаря маринаду на базе 9 специй стейк получается очень пикантный, мягкий и сочный. Подаётся с соусом от шеф-повара и овощами",
						Weight = 350,
						Cost = 500,
						Calorie = 500
					}
				},
				{
					"Борщец",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Борщец",
						Description = "Многокомпонентный красный суп с наваристым вкусом. Мы готовим борщ на мясе телятины и говядины чтобы избежать большого содержания в блюде холестерина, по мере готовности добавляем свёклу, томат, картофель, лук, морковь. В качестве дополнительных ингредиентов мы добавляем сладкий перец и грибы",
						Weight = 400,
						Cost = 400,
						Calorie = 500
					}
				},
				{
					"Тартар из лосося",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Тартар из лосося",
						Description = "Тартар из лосося это блюдо французской кухни. Основой рецепта является тартар из говядины, но вместо мяса используется лосось. Свежий или малосольный лосось маринуется вместе с луком и специями, и подается порционно на тарелке вместе с авокадо и гренками",
						Weight = 250,
						Cost = 350,
						Calorie = 450
					}
				},
				{
					"Цезарь с креветками",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Цезарь с креветками",
						Description = "Салат Цезарь — одно из самых популярных и востребованных блюд, на протяжении более чем сотни лет это бессменная классика европейской кухни. За долгие годы этот салат набрал десятки интересных вариаций состава и подачи, в нашем кафе его подают с креветками",
						Weight = 300,
						Cost = 350,
						Calorie = 480
					}
				},
				{
					"Бренди круста",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Бренди круста",
						Description = "Лимонный сок – 5 мл, сахарный сироп – 5 мл, ликер мараскино – 15 мл, ликер трипл сек – 15 мл, коньяк – 50 мл, ангостуру биттер – 1 дэш, апельсиновый биттер – 1 дэш, лимонная цедра, сахарная окаемка",
						Weight = 300,
						Cost = 350,
						Calorie = 320
					}
				},
				{
					"Алёша",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Алёша",
						Description = "Водка – 120 мл, грейпфрутовый сок – 220 мл, гренадин – 15 мл",
						Weight = 320,
						Cost = 345,
						Calorie = 480
					}
				},
				{
					"Клубничный мохито",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Клубничный мохито",
						Description = "Белый ром – 50 мл, клубничный сироп – 15 мл, содовая – 100 мл, лайм – 60 г, клубника – 120 г, мята – 3 г",
						Weight = 370,
						Cost = 370,
						Calorie = 440
					}
				},
				{
					"Скай мохито",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Скай мохито",
						Description = "Белый ром – 30 мл, Блю Кюросао – 20 мл, кокосовое молоко – 50 мл, водка – 30 мл, огурец, ломтики персика",
						Weight = 330,
						Cost = 340,
						Calorie = 400
					}
				},
				{
					"Б-51",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Б-51",
						Description = "Кофейный ликёр – 1 часть, айриш крем – 1 часть, светлый ром – 1 часть",
						Weight = 120,
						Cost = 340,
						Calorie = 400
					}
				},
				{
					"Боярский",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Боярский",
						Description = "Водка — 25 мл, гренадин – 25 мл, красный острый соус \"Табаско\" – 5 мл",
						Weight = 55,
						Cost = 120,
						Calorie = 200
					}
				},
				{
					"Ирландский флаг",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Ирландский флаг",
						Description = "Ликер мятный зеленый – 15 мл, ликер сливочный Бейлис – 15 мл, бренди – 15 мл",
						Weight = 45,
						Cost = 250,
						Calorie = 270
					}
				},
				{
					"Кровавая мэри",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Кровавая мэри",
						Description = "Водка – 45 мл, томатный сок – 90 мл, лимонный сок – 15 мл, вустерский соус – 2-3 капли, табаско – щепотка – 2-3 капли, смесь соли c порошком сельдерея – 1 щепотка, чёрный перец – 1 щепотка",
						Weight = 155,
						Cost = 270,
						Calorie = 280
					}
				},
				{
					"Яблочный сауэр",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Яблочный сауэр",
						Description = "Яблочный ликёр – 20 мл, лимонный сок – 15 мл, шотландский виски – 20 мл",
						Weight = 55,
						Cost = 220,
						Calorie = 190
					}
				},
				{
					"Джангл Джус",
					new Dish()
					{
						DishId = guidGenerator.Next(null),
						Name = "Джангл Джус",
						Description = "Ром — 30 мл, водка — 30 мл, трипл сек — 15 мл, ананасовый сок — 30 мл, фреш лайма — 20 мл, сахарный сироп — 15 мл, долька ананаса",
						Weight = 140,
						Cost = 290,
						Calorie = 260
					}
				}
			};

			modelBuilder.Entity<Dish>().HasData(dishes.Values.ToArray());
			#endregion

			#region DishCategories
			Dictionary<string, DishCategory> dishCategories = new()
			{
				{
					"Салаты",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Салаты"
					}
				},
				{
					"Супы",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Супы"
					}
				},
				{
					"Жаркое",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Жаркое"
					}
				},
				{
					"Рыба",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Рыба"
					}
				},
				{
					"Закуски",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Закуски"
					}
				},
				{
					"Лонги",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Лонги"
					}
				},
				{
					"Шоты",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Шоты"
					}
				},
				{
					"Безалкогольные",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Безалкогольные"
					}
				},
				{
					"Основное меню",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Основное меню"
					}
				},
				{
					"Напитки",
					new DishCategory()
					{
						CategoryId = guidGenerator.Next(null),
						Name = "Напитки"
					}
				},
			};

			modelBuilder.Entity<DishCategory>().HasData(dishCategories.Values.ToArray());
			#endregion

			#region DishesByCategories
			modelBuilder.Entity<DishesByCategory>().HasData(
				new DishesByCategory()
				{
					DishId = dishes["Мясная тарелка"].DishId,
					CategoryId = dishCategories["Закуски"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Греческий салат"].DishId,
					CategoryId = dishCategories["Салаты"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Свиной стейк"].DishId,
					CategoryId = dishCategories["Жаркое"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Говяжий стейк"].DishId,
					CategoryId = dishCategories["Жаркое"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Борщец"].DishId,
					CategoryId = dishCategories["Супы"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Тартар из лосося"].DishId,
					CategoryId = dishCategories["Закуски"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Цезарь с креветками"].DishId,
					CategoryId = dishCategories["Салаты"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Бренди круста"].DishId,
					CategoryId = dishCategories["Лонги"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Алёша"].DishId,
					CategoryId = dishCategories["Лонги"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Скай мохито"].DishId,
					CategoryId = dishCategories["Лонги"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Б-51"].DishId,
					CategoryId = dishCategories["Шоты"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Боярский"].DishId,
					CategoryId = dishCategories["Шоты"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Ирландский флаг"].DishId,
					CategoryId = dishCategories["Шоты"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Кровавая мэри"].DishId,
					CategoryId = dishCategories["Шоты"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Яблочный сауэр"].DishId,
					CategoryId = dishCategories["Шоты"].CategoryId
				},
				new DishesByCategory()
				{
					DishId = dishes["Джангл Джус"].DishId,
					CategoryId = dishCategories["Шоты"].CategoryId
				});
			#endregion

			#region Tables
			Dictionary<int, Table> tables = new()
			{
				{
					1,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 1,
						NumberOfSeats = 3
					}
				},
				{
					2,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 2,
						NumberOfSeats = 3
					}
				},
				{
					3,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 3,
						NumberOfSeats = 3
					}
				},
				{
					4,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 4,
						NumberOfSeats = 3
					}
				},
				{
					5,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 5,
						NumberOfSeats = 4
					}
				},
				{
					6,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 6,
						NumberOfSeats = 4
					}
				},
				{
					7,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 7,
						NumberOfSeats = 4
					}
				},
				{
					8,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 8,
						NumberOfSeats = 16
					}
				},
				{
					9,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 9,
						NumberOfSeats = 2
					}
				},
				{
					10,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 10,
						NumberOfSeats = 2
					}
				},
				{
					11,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 11,
						NumberOfSeats = 2
					}
				},
				{
					12,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 12,
						NumberOfSeats = 5
					}
				},
				{
					13,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 13,
						NumberOfSeats = 2
					}
				},
				{
					14,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 14,
						NumberOfSeats = 2
					}
				},
				{
					15,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 15,
						NumberOfSeats = 2
					}
				},
				{
					16,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 16,
						NumberOfSeats = 4
					}
				},
				{
					17,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 17,
						NumberOfSeats = 4
					}
				},
				{
					18,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 18,
						NumberOfSeats = 4
					}
				},
				{
					19,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 19,
						NumberOfSeats = 3
					}
				},
				{
					20,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 20,
						NumberOfSeats = 3
					}
				},
				{
					21,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 21,
						NumberOfSeats = 3
					}
				},
				{
					22,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 22,
						NumberOfSeats = 3
					}
				},
				{
					23,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 23,
						NumberOfSeats = 4
					}
				},
				{
					24,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 24,
						NumberOfSeats = 4
					}
				},
				{
					25,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 25,
						NumberOfSeats = 4
					}
				},
				{
					26,
					new Table()
					{
						TableId = guidGenerator.Next(null),
						TableNumber = 26,
						NumberOfSeats = 13
					}
				}
			};

			modelBuilder.Entity<Table>().HasData(tables.Values.ToArray());
			#endregion

			#region Promocodes
			Dictionary<string, Promocode> promocodes = new()
			{
				{
					"BlackEdge_SpringPromotion_10%",
					new Promocode()
					{
						PromocodeId = guidGenerator.Next(null),
						PromocodeText = "BlackEdge_SpringPromotion_10%",
						CoefficientDiscount = 0.1f
					}
				},
				{
					"HAPPY_WINTER_7%",
					new Promocode()
					{
						PromocodeId = guidGenerator.Next(null),
						PromocodeText = "HAPPY_WINTER_7%",
						CoefficientDiscount = 0.07f
					}
				}
			};

			modelBuilder.Entity<Promocode>().HasData(promocodes.Values.ToArray());
			#endregion

			#region PromocodesForDishCategories
			modelBuilder.Entity<PromocodesForDishCategory>().HasData(
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["BlackEdge_SpringPromotion_10%"].PromocodeId,
					CategoryId = dishCategories["Салаты"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["BlackEdge_SpringPromotion_10%"].PromocodeId,
					CategoryId = dishCategories["Лонги"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["BlackEdge_SpringPromotion_10%"].PromocodeId,
					CategoryId = dishCategories["Супы"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["BlackEdge_SpringPromotion_10%"].PromocodeId,
					CategoryId = dishCategories["Жаркое"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["BlackEdge_SpringPromotion_10%"].PromocodeId,
					CategoryId = dishCategories["Рыба"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["BlackEdge_SpringPromotion_10%"].PromocodeId,
					CategoryId = dishCategories["Закуски"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["BlackEdge_SpringPromotion_10%"].PromocodeId,
					CategoryId = dishCategories["Безалкогольные"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["HAPPY_WINTER_7%"].PromocodeId,
					CategoryId = dishCategories["Супы"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["HAPPY_WINTER_7%"].PromocodeId,
					CategoryId = dishCategories["Шоты"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["HAPPY_WINTER_7%"].PromocodeId,
					CategoryId = dishCategories["Жаркое"].CategoryId
				},
				new PromocodesForDishCategory()
				{
					PromocodeId = promocodes["HAPPY_WINTER_7%"].PromocodeId,
					CategoryId = dishCategories["Рыба"].CategoryId
				});
			#endregion
		}
	}
}

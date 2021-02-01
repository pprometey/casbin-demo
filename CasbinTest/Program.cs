using CasbinTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CasbinTest
{
	class Program
	{
		static JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			var ct = new CasbinTest();

			//Список статей Piter
			var articles = ct.GetArticlesForAdminPanel(3);
			Console.WriteLine($"Peter's article list {JsonSerializer.Serialize(articles, options)}");
			Console.WriteLine();

			//Список статей Alice
			articles = ct.GetArticlesForAdminPanel(1);
			Console.WriteLine($"Alice's article list {JsonSerializer.Serialize(articles, options)}");
			Console.WriteLine();

			//Пробуем обновить статью Alice если это Alice
			var article1 = articles.FirstOrDefault(x => x.Id == 1);
			article1.Name = "A changed";
			ct.UpdateArticle(1, article1);

			//Пробуем обновить статью Alice если это Piter
			article1 = articles.FirstOrDefault(x => x.Id == 1);
			article1.Name = "A changed";
			ct.UpdateArticle(3, article1);

			//Попробуем удалить статью Alice c Id=1
			articles = ct.GetArticlesForAdminPanel(1);
			var article2 = articles.FirstOrDefault(x => x.Id == 1);
			ct.DeleteArticle(1, article2);
			articles = ct.GetArticlesForAdminPanel(1);
			Console.WriteLine($"Alice's article list atfer deleted id =1 {JsonSerializer.Serialize(articles, options)}");
			Console.WriteLine();

			//Попробуем удалить статью Bob c Id=5 под Bob
			articles = ct.GetArticlesForAdminPanel(2);
			article2 = articles.FirstOrDefault(x => x.Id == 5);
			ct.DeleteArticle(2, article2);
			articles = ct.GetArticlesForAdminPanel(2);
			Console.WriteLine($"Bob's article list atfer deleted id=5 {JsonSerializer.Serialize(articles, options)}");
			Console.WriteLine();

			//Попробуем удалить статью Bob c Id=4 под Piter
			articles = ct.GetArticlesForAdminPanel(3);
			article2 = articles.FirstOrDefault(x => x.Id == 4);
			ct.DeleteArticle(3, article2);
			articles = ct.GetArticlesForAdminPanel(3);
			Console.WriteLine($"Piter's article list atfer deleted id=4 {JsonSerializer.Serialize(articles, options)}");
			Console.WriteLine();

			//Попробуем удалить статью Piter c Id=8 под Piter
			articles = ct.GetArticlesForAdminPanel(3);
			article2 = articles.FirstOrDefault(x => x.Id == 8);
			ct.DeleteArticle(3, article2);
			articles = ct.GetArticlesForAdminPanel(3);
			Console.WriteLine($"Piter's article list atfer deleted id=8 {JsonSerializer.Serialize(articles, options)}");
			Console.WriteLine();
		}
	}
}

using System;
using System.Collections.Generic;
using CasbinTest.Models;
using NetCasbin;
using System.Linq;

namespace CasbinTest
{
	public class CasbinTest
	{

		private CasbinDbContext _context = new CasbinDbContext();

		public IList<Article> GetArticlesForAdminPanel(int currentUserId)
		{
			var e = new Enforcer("CasbinConfig/rbac_model.conf", "CasbinConfig/rbac_policy.csv");

			var obj = "article";
			var act = "read";

			//Сначала проверям, что пользователь имеет права на чтение статей
			if (e.Enforce(currentUserId.ToString(), obj, act))
			{
				//Получаем список ролей пользователя
				var currentUserRoles = e.GetRolesForUser(currentUserId.ToString());
				//Проверяем, является ли пользователем админиом или супервизором
				var isAdmin = currentUserRoles.Any(x => x == "admin" || x == "supervisor");

				//Если админ, вернуть все записи, иначе только те, которые принадлежат пользователю
				if (isAdmin) return _context.Articles.ToList();
				else return _context.Articles.Where(x => x.OwnerId == currentUserId).ToList();
			}
			else
			{
				// отклонить запрос, показать ошибку
				throw new Exception("403");	
			}
		}

		public void UpdateArticle(int currentUserId, Article newArticle)
		{
			var e = new Enforcer("CasbinConfig/rbac_with_abac_model.conf", "CasbinConfig/rbac_policy.csv");

			var act = "modify";

			//Проверям, что пользователь имеет права на редактирование статьи
			if (e.Enforce(currentUserId.ToString(), newArticle, act))
			{
				//Обновляем статью, и сохраняем изменения
				_context.Articles.Update(newArticle);
				_context.SaveChanges();

				var v = _context.Articles.FirstOrDefault(x => x.Id == 1);
				Console.WriteLine($"New name before changed = {v.Name}");
			}
			else
			{
				// отклонить запрос, показать ошибку
				throw new Exception("403");
			}
		}

		public void DeleteArticle(int currentUserId, Article deleteArticle)
		{
			var e = new Enforcer("CasbinConfig/delete_model.conf", "CasbinConfig/rbac_policy.csv");

			var act = "delete";

			//Проверям, что пользователь имеет права на редактирование статьи
			if (e.Enforce(currentUserId.ToString(), deleteArticle, act))
			{
				//Удаляем статью
				_context.Articles.Remove(deleteArticle);
				_context.SaveChanges();
			}
			else
			{
				// отклонить запрос, показать ошибку
				throw new Exception("403");
			}
		}

	}
}

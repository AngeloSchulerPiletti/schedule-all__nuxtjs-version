using backend.Data.VO;
using backend.Models;
using backend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace backend.Repository.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MySQLContext _context;

        public CategoryRepository(MySQLContext context)
        {
            _context = context;
        }

        public object ChangeCategory(CategoryVO category, long userId)
        {
            Category cat = _context.Categorys.FirstOrDefault(cat => cat.UserId == userId & cat.CategoryId == category.CategoryId);
            cat.Title = category.Title;
            try
            {
                _context.SaveChanges();
                return cat;
            }
            catch (Exception)
            {
                return new ErrorBadgeVO(new List<string> { "A Categoria não pôde ser alterada" });
            }
        }

        public dynamic CreateCategory(string title, long userId)
        {
            var categoryExists = _context.Categorys.FirstOrDefault(task => task.Title.ToLower() == title.ToLower());
            if (categoryExists != null) return new ErrorBadgeVO(new List<string> { "Essa categoria já existe na sua conta" });

            Category category = new(userId, title);
            _context.Categorys.Add(category);
            _context.SaveChanges();

            return new SuccessBadgeVO(new List<string> { "Categoria criada com sucesso" });
        }

        public object DeleteCategory(long categoryId, long userId)
        {
            List<SimpleTodo> tasksOnCategory = _context.SimpleTodos.Where(task => task.CategoryId == categoryId & task.UserId == userId).ToList();
            if (tasksOnCategory.Count > 0) return new ErrorBadgeVO(new List<string> { "Essa categoria não pode ser removida pois existem tarefas atribuídas a ela" });

            var cat = _context.Categorys.FirstOrDefault(cat => cat.CategoryId == categoryId & cat.UserId == userId);
            try
            {
                _context.Categorys.Remove(cat);
                _context.SaveChanges();
                return null;
            }
            catch (Exception)
            {
                return new ErrorBadgeVO(new List<string> { "Não foi possível deletar esta categoria" });
            }
        }

        public object GetUserCategorys(long userId)
        {
            return _context.Categorys.Where(cat => cat.UserId == userId).ToList();
        }

        public ErrorBadgeVO ValidateCategory(CategoryVO category)
        {
            var validateId = ValidateId(category.CategoryId);
            if (validateId != null) return validateId;
            return ValidateTitle(category.Title);
        }

        public ErrorBadgeVO ValidateId(long id)
        {
            if (id <= 0) return new ErrorBadgeVO(new List<string> { "Id inválido" });
            return null;
        }

        public ErrorBadgeVO ValidateTitle(string title)
        {
            ErrorBadgeVO error = new(new List<string>());

            if (title == null) error.messages.Add("Adicione um título para a tarefa");
            if (title.Length > 100) error.messages.Add("Essa categoria é grande demais. No máximo 100 caracteres");

            var match = Regex.Match(title, @"[^A-Za-z0-9 ]+");
            if (match.Success) error.messages.Add("Use apenas letras, números e espaço para categorias");

            return error.messages.Count > 0 ? error : null;
        }
    }
}

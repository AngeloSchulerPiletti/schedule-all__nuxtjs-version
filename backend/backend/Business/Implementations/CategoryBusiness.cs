using backend.Data.VO;
using backend.Models;
using backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace backend.Business.Implementations
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private ICategoryRepository _repository;

        public CategoryBusiness(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public object ChangeCategory(CategoryVO category, long userId)
        {
            return _repository.ChangeCategory(category, userId);
        }

        public MessageBadgeVO CreateCategory(string title, long userId)
        {
            return _repository.CreateCategory(title, userId);
        }

        public MessageBadgeVO DeleteCategory(long categoryId, long userId)
        {
            return _repository.DeleteCategory(categoryId, userId);
        }

        public List<Category> GetUserCategorys(long userId)
        {
            return _repository.GetUserCategorys(userId);
        }

        public MessageBadgeVO ValidateCategory(CategoryVO category)
        {
            var validateId = ValidateId(category.CategoryId);
            if (validateId != null) return validateId;
            return ValidateTitle(category.Title);
        }

        public MessageBadgeVO ValidateId(long id)
        {
            if (id <= 0) return new MessageBadgeVO(new List<string> { "Id inválido" });
            return null;
        }

        public MessageBadgeVO ValidateTitle(string title)
        {
            MessageBadgeVO error = new(new List<string>());

            if (title == null) error.messages.Add("Adicione um título para a tarefa");
            if (title.Length > 100) error.messages.Add("Essa categoria é grande demais. No máximo 100 caracteres");

            var match = Regex.Match(title, @"[^A-Za-z0-9 ]+");
            if (match.Success) error.messages.Add("Use apenas letras, números e espaço para categorias");

            return error.messages.Count > 0 ? error : null;
        }
    }
}

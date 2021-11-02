using backend.Data.VO;
using backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ErrorBadgeVO validateResult = _repository.ValidateCategory(category);
            if (validateResult != null) return validateResult;
            return _repository.ChangeCategory(category, userId);
        }

        public object CreateCategory(string title, long userId)
        {
            string trimmedTitle = title.Trim();
            var validateResult = _repository.ValidateTitle(trimmedTitle);
            if (validateResult is ErrorBadgeVO) return validateResult;
            return _repository.CreateCategory(trimmedTitle, userId);
        }

        public object DeleteCategory(long categoryId, long userId)
        {
            var validateResult = _repository.ValidateId(categoryId);
            if (validateResult is ErrorBadgeVO) return validateResult;
            return _repository.DeleteCategory(categoryId, userId);
        }

        public object GetUserCategorys(long userId)
        {
            return _repository.GetUserCategorys(userId);
        }
    }
}

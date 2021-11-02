using backend.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repository
{
    public interface ICategoryRepository
    {
        public ErrorBadgeVO ValidateTitle(string title);
        public object CreateCategory(string title, long userId);
        public object GetUserCategorys(long userId);
        public ErrorBadgeVO ValidateId(long id);
        public object DeleteCategory(long categoryId, long userId);
        public ErrorBadgeVO ValidateCategory(CategoryVO category);
        public object ChangeCategory(CategoryVO category, long userId);
    }
}

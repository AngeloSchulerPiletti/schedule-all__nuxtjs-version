using backend.Data.VO;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repository
{
    public interface ICategoryRepository
    {
        public MessageBadgeVO CreateCategory(string title, long userId);
        public List<Category> GetUserCategorys(long userId);
        public MessageBadgeVO DeleteCategory(long categoryId, long userId);
        public object ChangeCategory(CategoryVO category, long userId);
        public MessageBadgeVO CheckCategoryAlreadyExists(string title, long userId);
        public Category GetLastCategoryByUserId(long userId);
    }
}

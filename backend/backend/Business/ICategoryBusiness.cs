using backend.Data.VO;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business
{
    public interface ICategoryBusiness
    {
        public object CreateCategory(string title, long userId);
        public List<Category> GetUserCategorys(long userId);
        public MessageBadgeVO DeleteCategory(long categoryId, long userId);
        public object ChangeCategory(CategoryVO category, long userId);
        public MessageBadgeVO ValidateCategory(CategoryVO category);
        public MessageBadgeVO ValidateId(long id);
        public MessageBadgeVO ValidateTitle(string title);
    }
}

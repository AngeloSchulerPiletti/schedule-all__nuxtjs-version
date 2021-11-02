using backend.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business
{
    public interface ICategoryBusiness
    {
        public object CreateCategory(string title, long userId);
        public object GetUserCategorys(long userId);
        public object DeleteCategory(long categoryId, long userId);
        public object ChangeCategory(CategoryVO category, long userId);
    }
}

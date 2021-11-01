using backend.Data.VO;
using backend.Models;

namespace backend.Business
{
    public interface ISimpleTodoBusiness
    {
        public object CreateSimpleTodo(NewSimpleTodoVO simpletodo, User user);
        public object GetSimpleTodosByUserId(long userId);
        public object SetSimpleTodoState(long simpletodoId);
        public object GetSingleSimpleTodoByUserId(long userId, long simpletodoId);
    }
}

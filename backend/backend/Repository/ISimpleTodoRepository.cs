using backend.Data.VO;
using backend.Models;

namespace backend.Repository
{
    public interface ISimpleTodoRepository
    {
        public ErrorBadgeVO ValidateSimpleTodoInput(NewSimpleTodoVO simpletodo);
        public object CreateSimpleTodo(NewSimpleTodoVO simpletodo, User user);
        public object GetSimpleTodosByUserId(long userId);
        public object SetSimpleTodoState(long simpletodoId);
        public object GetSingleSimpleTodoByUserId(long userId, long simpletodoId);
    }
}

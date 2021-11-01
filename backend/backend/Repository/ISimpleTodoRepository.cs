using backend.Data.VO;
using backend.Models;

namespace backend.Repository
{
    public interface ISimpleTodoRepository
    {
        public ErrorBadgeVO ValidateSimpleTodoInput(NewSimpleTodoVO simpletodo);
        public object CreateSimpleTodo(NewSimpleTodoVO simpletodo, User user);
    }
}

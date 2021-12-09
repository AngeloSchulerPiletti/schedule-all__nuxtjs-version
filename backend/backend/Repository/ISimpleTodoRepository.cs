using backend.Data.VO;
using backend.Models;
using System.Collections.Generic;

namespace backend.Repository
{
    public interface ISimpleTodoRepository
    {
        public MessageBadgeVO CreateSimpleTodo(NewSimpleTodoVO simpletodo, User user);
        public MessageBadgeVO UpdateSimpleTodo(SimpleTodoVO simpletodo, User user);
        public List<SimpleTodo> GetSimpleTodosByUserId(long userId);
        public object SetSimpleTodoState(long simpletodoId, long userId);
        public object SetSimpleTodoImportance(long simpletodoId, long userId);
        public SimpleTodo GetSingleSimpleTodoByUserId(long userId, long simpletodoId);
        public MessageBadgeVO DeleteSimpleTodo(long userId, long simpletodoId);
        public MessageBadgeVO CategoryExistsInUser(long categoryId, long userId);
        public SimpleTodo GetLastSimpleTodoByUserId(long userId);
        public List<SimpleTodo> GetImportantSimpleTodos(long userId);
        public List<SimpleTodo> GetSimpleTodosByCategory(int pagination, long userId);
        public SimpleTodo SetSimpleTodoColaborative(long simpletodoId, long userId);
    }
}

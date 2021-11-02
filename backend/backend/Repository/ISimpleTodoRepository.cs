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
        public SimpleTodo GetSingleSimpleTodoByUserId(long userId, long simpletodoId);
        public MessageBadgeVO DeleteSimpleTodo(long userId, long simpletodoId);
    }
}

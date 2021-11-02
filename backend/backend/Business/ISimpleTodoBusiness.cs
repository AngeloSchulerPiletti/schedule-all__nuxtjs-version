using backend.Data.VO;
using backend.Models;
using System.Collections.Generic;

namespace backend.Business
{
    public interface ISimpleTodoBusiness
    {
        public MessageBadgeVO CreateSimpleTodo(NewSimpleTodoVO simpletodo, User user);
        public MessageBadgeVO ValidateId(long id);
        public object UpdateSimpleTodo(SimpleTodoVO simpletodo, User user);
        public List<SimpleTodo> GetSimpleTodosByUserId(long userId);
        public object SetSimpleTodoState(long simpletodoId, long userId);
        public SimpleTodo GetSingleSimpleTodoByUserId(long userId, long simpletodoId);
        public MessageBadgeVO DeleteSimpleTodo(long userId, long simpletodoId);
        public MessageBadgeVO ValidateSimpleTodoInput(NewSimpleTodoVO simpletodo);
        public MessageBadgeVO ValidateSimpleTodoInput(SimpleTodoVO simpletodo);
    }
}

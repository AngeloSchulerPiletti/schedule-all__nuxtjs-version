using backend.Data.VO;
using backend.Models;
using backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business.Implementations
{
    public class SimpleTodoBusiness : ISimpleTodoBusiness
    {
        private ISimpleTodoRepository _repository;

        public SimpleTodoBusiness(ISimpleTodoRepository repository)
        {
            _repository = repository;
        }

        public object CreateSimpleTodo(NewSimpleTodoVO simpletodo, User user)
        {
            var validationResult = _repository.ValidateSimpleTodoInput(simpletodo);
            if (validationResult != null) return validationResult;
            var creationResult = _repository.CreateSimpleTodo(simpletodo, user);
            return creationResult;
        }
        public object UpdateSimpleTodo(SimpleTodoVO simpletodo, User user)
        {
            var validationResult = _repository.ValidateSimpleTodoInput(simpletodo);
            if (validationResult != null) return validationResult;
            var updateResult = _repository.UpdateSimpleTodo(simpletodo, user);
            return updateResult;
        }

        public object GetSimpleTodosByUserId(long userId)
        {
            return _repository.GetSimpleTodosByUserId(userId);
        }

        public object GetSingleSimpleTodoByUserId(long userId, long simpletodoId)
        {
            return _repository.GetSingleSimpleTodoByUserId(userId, simpletodoId);
        }

        public object SetSimpleTodoState(long simpletodoId)
        {
            return _repository.SetSimpleTodoState(simpletodoId);
        }

        public object DeleteSimpleTodo(long userId, long simpletodoId)
        {
            return _repository.DeleteSimpleTodo(userId, simpletodoId);
        }
    }
}

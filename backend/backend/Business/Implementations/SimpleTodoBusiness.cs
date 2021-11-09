using backend.Data.VO;
using backend.Models;
using backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (simpletodo.CategoryId != 0)
            {
                MessageBadgeVO categoryExistsResult = _repository.CategoryExistsInUser(simpletodo.CategoryId, user.Id);
                if (categoryExistsResult != null) return categoryExistsResult;
            }
            MessageBadgeVO creationResult = _repository.CreateSimpleTodo(simpletodo, user);
            if (!creationResult.isError)
            {
                return _repository.GetLastSimpleTodoByUserId(user.Id);
            }
            else
            {
                return creationResult;
            }
        }
        public MessageBadgeVO UpdateSimpleTodo(SimpleTodoVO simpletodo, User user)
        {

            MessageBadgeVO updateResult = _repository.UpdateSimpleTodo(simpletodo, user);
            return updateResult;
        }

        public List<SimpleTodo> GetUserSimpleTodosByCategory(int? pagination, long userId)
        {
            List<SimpleTodo> simpletodos;

            if(pagination == null)
            {
                simpletodos = _repository.GetSimpleTodosByUserId(userId);
            }
            else if (pagination == 0)
            {
                simpletodos = _repository.GetImportantSimpleTodos(userId);
            }
            else
            {
                simpletodos = _repository.GetSimpleTodosByCategory(pagination.Value, userId);
            }
            return simpletodos;
        }

        public List<SimpleTodo> GetSimpleTodosByUserId(long userId)
        {
            return _repository.GetSimpleTodosByUserId(userId);
        }

        public SimpleTodo GetSingleSimpleTodoByUserId(long userId, long simpletodoId)
        {
            SimpleTodo result = _repository.GetSingleSimpleTodoByUserId(userId, simpletodoId);
            return result;
        }

        public object SetSimpleTodoState(long simpletodoId, long userId)
        {
            return _repository.SetSimpleTodoState(simpletodoId, userId);
        }

        public MessageBadgeVO DeleteSimpleTodo(long userId, long simpletodoId)
        {
            return _repository.DeleteSimpleTodo(userId, simpletodoId);
        }

        public MessageBadgeVO ValidateId(long id)
        {
            if (id <= 0) return new MessageBadgeVO(new List<string> { "Id inválido" });
            return null;
        }

        public MessageBadgeVO ValidateSimpleTodoInput(NewSimpleTodoVO simpletodo)
        {
            MessageBadgeVO errors = new(new List<string>());
            return ValidateNonNullablesSimpleTodoInputs(errors, typeof(NewSimpleTodoVO), simpletodo);
        }
        public MessageBadgeVO ValidateSimpleTodoInput(SimpleTodoVO simpletodo)
        {
            MessageBadgeVO errors = new(new List<string>());
            return ValidateNonNullablesSimpleTodoInputs(errors, typeof(SimpleTodoVO), simpletodo);
        }
        private MessageBadgeVO ValidateNonNullablesSimpleTodoInputs(MessageBadgeVO errors, Type type, dynamic simpletodo)
        {
            foreach (PropertyInfo input in type.GetProperties())
            {
                if (!simpletodo.Nullables.Contains(input.Name))
                {
                    var inputValue = input.GetValue(simpletodo);
                    var inputType = input.GetValue(simpletodo).GetType().Name;

                    if (inputType == "String")
                    {
                        if (inputValue == null)
                        {
                            errors.messages.Add($"O campo {simpletodo.InputsName[input.Name]} não pode ser vazio");
                            return errors;
                        }
                    }
                    else if (inputType == "Long" || inputType == "Int")
                    {
                        if (inputValue == 0)
                        {
                            errors.messages.Add($"O campo {simpletodo.InputsName[input.Name]} não pode ser vazio");
                            return errors;
                        }
                    }
                }
            }

            if (simpletodo.Title.Length > 100) errors.messages.Add("O título não pode ter mais de 100 caracteres");
            if (simpletodo.Description?.Length > 500) errors.messages.Add("A descrição não pode ter mais de 100 caracteres");
            return errors.messages.Count > 0 ? errors : null;

        }
    }
}

using backend.Data.VO;
using backend.Models;
using backend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace backend.Repository.Implementations
{
    public class SimpleTodoRepository : ISimpleTodoRepository
    {
        private readonly MySQLContext _context;

        public SimpleTodoRepository(MySQLContext context)
        {
            _context = context;
        }

        public object CreateSimpleTodo(NewSimpleTodoVO simpletodo, User user)
        {
            if (simpletodo.CategoryId != 0)
            {
                var category = _context.Categorys.Where(cat => (cat.CategoryId == simpletodo.CategoryId) & (cat.UserId == user.Id)).SingleOrDefault();
                if (category == null)
                {
                    return new ErrorBadgeVO(new List<string> { "Categoria Inválida" });
                }
            }

            DateTime createdAt = DateTime.Now;

            _context.SimpleTodos.Add(new SimpleTodo(createdAt, simpletodo.Title, simpletodo.Description, simpletodo.CategoryId, user.Id));
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return new ErrorBadgeVO(new List<string> { "Não foi possível salvar as modificações" });
            }

            return new SuccessBadgeVO(new List<string> { "Tarefa criada com sucesso" });
        }
        public object UpdateSimpleTodo(SimpleTodoVO simpletodo, User user)
        {
            if (simpletodo.CategoryId != 0)
            {
                var category = _context.Categorys.Where(cat => (cat.CategoryId == simpletodo.CategoryId) & (cat.UserId == user.Id)).SingleOrDefault();
                if (category == null)
                {
                    return new ErrorBadgeVO(new List<string> { "Categoria Inválida" });
                }
            }

            var task = _context.SimpleTodos.SingleOrDefault(task => (task.Id == simpletodo.Id) & (task.UserId == user.Id));
            if (task == null) return new ErrorBadgeVO(new List<string> { "Essa tarefa não pode ser alterada pois não existe" });

            try
            {
                task.Title = simpletodo.Title;
                task.Description = simpletodo.Description;
                task.CategoryId = simpletodo.CategoryId;

                _context.SaveChanges();
            }
            catch (Exception)
            {
                return new ErrorBadgeVO(new List<string> { "Não foi possível salvar as modificações" });
            }

            return new SuccessBadgeVO(new List<string> { "Tarefa atualizada com sucesso" });
        }

        public object GetSimpleTodosByUserId(long userId)
        {
            var result = _context.SimpleTodos.Where(task => task.UserId == userId).ToList();
            if (result == null) return new ErrorBadgeVO(new List<string> { "Não encontramos essas tarefas..." });
            return result;
        }

        public object GetSingleSimpleTodoByUserId(long userId, long simpletodoId)
        {
            var result = _context.SimpleTodos.Where(task => task.Id == simpletodoId).SingleOrDefault(task => task.UserId == userId);
            if (result == null) return new ErrorBadgeVO(new List<string> { "Não encontramos essas tarefas..." });
            return result;
        }

        public object SetSimpleTodoState(long simpletodoId)
        {
            var task = _context.SimpleTodos.SingleOrDefault(task => task.Id == simpletodoId);
            if (task == null) return new ErrorBadgeVO(new List<string> { "Essa tarefa não existe" });

            if (!task.Finished) task.FinishedAt = DateTime.Now;

            task.Finished = !task.Finished;

            _context.SaveChanges();

            return _context.SimpleTodos.SingleOrDefault(task => task.Id == simpletodoId);
        }

        public ErrorBadgeVO ValidateSimpleTodoInput(NewSimpleTodoVO simpletodo)
        {
            ErrorBadgeVO errors = new(new List<string>());
            return ValidateNonNullablesSimpleTodoInputs(errors, typeof(NewSimpleTodoVO), simpletodo);
        }
        public ErrorBadgeVO ValidateSimpleTodoInput(SimpleTodoVO simpletodo)
        {
            ErrorBadgeVO errors = new(new List<string>());
            return ValidateNonNullablesSimpleTodoInputs(errors, typeof(SimpleTodoVO), simpletodo);
        }

        public object DeleteSimpleTodo(long userId, long simpletodoId)
        {
            if (userId == 0 || simpletodoId == 0) return new ErrorBadgeVO(new List<string> { "Id de usuário ou tarefa inválidos" });
            var task = _context.SimpleTodos.SingleOrDefault(task => (task.UserId == userId) & (task.Id == simpletodoId));
            try
            {
                _context.SimpleTodos.Remove(task);
                _context.SaveChanges();
                return null;
            }
            catch (Exception)
            {
                return new ErrorBadgeVO(new List<string> { "Não foi possível deletar esta tarefa" });
            }
        }


        private ErrorBadgeVO ValidateNonNullablesSimpleTodoInputs(ErrorBadgeVO errors, Type type, dynamic simpletodo)
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
                    else if (inputType == "Long" || inputType == "Int") {
                        if (inputValue == 0)
                        {
                            errors.messages.Add($"O campo {simpletodo.InputsName[input.Name]} não pode ser vazio");
                            return errors;
                        }
                    }
                }
            }

            if (simpletodo.Title.Length > 100) errors.messages.Add("O título não pode ter mais de 100 caracteres");
            if (simpletodo.Description.Length > 500) errors.messages.Add("A descrição não pode ter mais de 100 caracteres");
            return errors.messages.Count > 0 ? errors : null;

        }
    }
}

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

        public object GetSimpleTodosByUserId(long userId)
        {
            return _context.SimpleTodos.Where(task => task.UserId == userId).ToList();
        }

        public object GetSingleSimpleTodoByUserId(long userId, long simpletodoId)
        {
            return _context.SimpleTodos.Where(task => task.Id == simpletodoId).SingleOrDefault(task => task.UserId == userId);
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

            foreach (PropertyInfo input in typeof(NewSimpleTodoVO).GetProperties())
            {
                if (input.GetValue(simpletodo) == null & !simpletodo.Nullables.Contains(input.Name))
                {
                    errors.messages.Add($"O campo {simpletodo.InputsName[input.Name]} não pode ser vazio");
                    return errors;
                }
            }

            if (simpletodo.Title.Length > 100) errors.messages.Add("O título não pode ter mais de 100 caracteres");
            if (simpletodo.Description.Length > 500) errors.messages.Add("A descrição não pode ter mais de 100 caracteres");

            return errors.messages.Count > 0 ? errors : null;
        }
    }
}

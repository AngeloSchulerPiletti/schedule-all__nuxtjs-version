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

        public MessageBadgeVO CreateSimpleTodo(NewSimpleTodoVO simpletodo, User user)
        {
            if (simpletodo.CategoryId != 0)
            {
                var category = _context.Categorys.Where(cat => (cat.CategoryId == simpletodo.CategoryId) & (cat.UserId == user.Id)).SingleOrDefault();
                if (category == null)
                {
                    return new MessageBadgeVO(new List<string> { "Categoria Inválida" });
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
                return new MessageBadgeVO(new List<string> { "Não foi possível salvar as modificações" });
            }

            return new MessageBadgeVO(new List<string> { "Tarefa criada com sucesso" }, false);
        }
        public MessageBadgeVO UpdateSimpleTodo(SimpleTodoVO simpletodo, User user)
        {
            if (simpletodo.CategoryId != 0)
            {
                var category = _context.Categorys.Where(cat => (cat.CategoryId == simpletodo.CategoryId) & (cat.UserId == user.Id)).SingleOrDefault();
                if (category == null)
                {
                    return new MessageBadgeVO(new List<string> { "Categoria Inválida" });
                }
            }

            var task = _context.SimpleTodos.SingleOrDefault(task => (task.Id == simpletodo.Id) & (task.UserId == user.Id));
            if (task == null) return new MessageBadgeVO(new List<string> { "Essa tarefa não pode ser alterada pois não existe" });

            try
            {
                task.Title = simpletodo.Title;
                task.Description = simpletodo.Description;
                task.CategoryId = simpletodo.CategoryId;

                _context.SaveChanges();
            }
            catch (Exception)
            {
                return new MessageBadgeVO(new List<string> { "Não foi possível salvar as modificações" });
            }

            return new MessageBadgeVO(new List<string> { "Tarefa atualizada com sucesso" }, false);
        }

        public List<SimpleTodo> GetSimpleTodosByUserId(long userId)
        {
            List<SimpleTodo> result = _context.SimpleTodos.Where(task => task.UserId == userId).ToList();
            return result;
        }

        public SimpleTodo GetSingleSimpleTodoByUserId(long userId, long simpletodoId)
        {
            SimpleTodo result = _context.SimpleTodos.Where(task => task.Id == simpletodoId).SingleOrDefault(task => task.UserId == userId);
            return result;
        }

        public object SetSimpleTodoState(long simpletodoId, long userId)
        {
            var task = _context.SimpleTodos.SingleOrDefault(task => task.Id == simpletodoId & task.UserId == userId);
            if (task == null) return new MessageBadgeVO(new List<string> { "Essa tarefa não existe" });

            if (!task.Finished) task.FinishedAt = DateTime.Now;

            task.Finished = !task.Finished;

            _context.SaveChanges();

            return _context.SimpleTodos.SingleOrDefault(task => task.Id == simpletodoId);
        }

        public MessageBadgeVO DeleteSimpleTodo(long userId, long simpletodoId)
        {
            try
            {
            var task = _context.SimpleTodos.SingleOrDefault(task => (task.UserId == userId) & (task.Id == simpletodoId));
                _context.SimpleTodos.Remove(task);
                _context.SaveChanges();
                return null;
            }
            catch (Exception)
            {
                return new MessageBadgeVO(new List<string> { "Não foi possível deletar esta tarefa" });
            }
        }
    }
}

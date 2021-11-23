using Microsoft.EntityFrameworkCore;

namespace backend.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {

        }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SimpleTodo> SimpleTodos { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<SimpleTodoBond> SimpleTodoBonds { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
    }
}

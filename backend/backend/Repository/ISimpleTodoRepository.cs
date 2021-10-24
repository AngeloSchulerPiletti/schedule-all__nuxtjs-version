using backend.Models;

namespace backend.Repository
{
    interface ISimpleTodoRepository
    {
        SimpleTodo Create();
        SimpleTodo GetAll();
        SimpleTodo GetOne();
        SimpleTodo AddInvolvedOne();
        SimpleTodo RemoveInvolvedOne();
        SimpleTodo Update();
        SimpleTodo Delete();
    }
}

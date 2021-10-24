using backend.Models;

namespace backend.Business
{
    interface ISimpleTodoBusiness
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

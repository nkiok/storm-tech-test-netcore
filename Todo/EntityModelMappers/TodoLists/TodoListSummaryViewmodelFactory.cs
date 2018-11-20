using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListSummaryViewmodelFactory
    {
        public static TodoListSummaryViewmodel Create(TodoList todoList, string userId)
        {
            var numberOfNotDoneItems = todoList.Items.Where(i => todoList.Owner.Id == userId || i.ResponsiblePartyId == userId).Count(ti => !ti.IsDone);
            return new TodoListSummaryViewmodel(todoList.TodoListId, todoList.Title, numberOfNotDoneItems, UserSummaryViewmodelFactory.Create(todoList.Owner));
        }
    }
}
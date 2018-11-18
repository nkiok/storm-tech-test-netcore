using Todo.Data.Entities;

namespace Todo.Models.TodoItems
{
    public class TodoItemCreateFields : TodoItemBase
    {
        public TodoItemCreateFields() { }

        public TodoItemCreateFields(int todoListId, string todoListTitle, string responsiblePartyId)
        {
            TodoListId = todoListId;

            TodoListTitle = todoListTitle;

            ResponsiblePartyId = responsiblePartyId;

            Importance = Importance.Medium;
        }
    }
}
﻿using System.ComponentModel.DataAnnotations;
using Todo.Data.Entities;

namespace Todo.Models.TodoItems
{
    public class TodoItemEditFields : TodoItemBase
    {
        public int TodoItemId { get; set; }

        [Display(Name = "Completed")]
        public bool IsDone { get; set; }

        public TodoItemEditFields() { }

        public TodoItemEditFields(int todoListId, string todoListTitle, int todoItemId, string title, bool isDone, string responsiblePartyId, Importance importance)
        {
            TodoListId = todoListId;
            TodoListTitle = todoListTitle;
            TodoItemId = todoItemId;
            Title = title;
            IsDone = isDone;
            ResponsiblePartyId = responsiblePartyId;
            Importance = importance;
        }
    }
}
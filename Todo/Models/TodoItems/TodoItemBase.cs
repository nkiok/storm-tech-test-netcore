﻿using System.ComponentModel.DataAnnotations;
using Todo.Data.Entities;

namespace Todo.Models.TodoItems
{
    public abstract class TodoItemBase
    {
        public int TodoListId { get; set; }

        public string Title { get; set; }

        public string TodoListTitle { get; set; }

        [Display(Name = "Responsible Party")]
        public string ResponsiblePartyId { get; set; }

        public Importance Importance { get; set; }
    }
}

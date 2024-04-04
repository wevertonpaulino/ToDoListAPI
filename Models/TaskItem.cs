using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.Models
{
    public class TaskItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}

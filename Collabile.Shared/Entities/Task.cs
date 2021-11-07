using System;

namespace Collabile.Shared.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public decimal EstimatedEffort { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Assignee { get; set; }
        public int Reporter { get; set; }
    }
}
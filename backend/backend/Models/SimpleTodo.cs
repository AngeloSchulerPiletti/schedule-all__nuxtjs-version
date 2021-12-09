using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    [Table("simpletodos")]
    public class SimpleTodo
    {
        public SimpleTodo(DateTime createdAt, string title, string description, long categoryId, long userId, bool important = false, bool colaborative = false)
        {
            CreatedAt = createdAt;
            Title = title;
            Description = description;
            CategoryId = categoryId;
            UserId = userId;
            Important = important;
            Colaborative = colaborative;
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("finished_at")]
        public DateTime FinishedAt { get; set; }
        [Column("finished")]
        public bool Finished { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("category_id")]
        public long CategoryId { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("important")]
        public bool Important { get; set; }
        [Column("colaborative")]
        public bool Colaborative { get; set; }
    }
}

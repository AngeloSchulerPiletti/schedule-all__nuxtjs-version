using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Category
    {
        public Category(long userId, string title)
        {
            UserId = userId;
            Title = title;
        }

        [Key]
        [Column("category_id")]
        public long CategoryId { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("title")]
        public string Title { get; set; }
    }
}

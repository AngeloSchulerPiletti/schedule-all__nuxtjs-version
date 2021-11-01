using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class SimpleTodoBond
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("simpletodo_id")]
        public int SimpleTodoId { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("invite_accepted")]
        public bool inviteAccepted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Friendship
    {
        public Friendship(long senderId, long receiverId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("sender_id")]
        public long SenderId { get; set; }
        [Column("receiver_id")]
        public long ReceiverId { get; set; }
        [Column("invite_accepted")]
        public bool InviteAccepted { get; set; }
    }
}

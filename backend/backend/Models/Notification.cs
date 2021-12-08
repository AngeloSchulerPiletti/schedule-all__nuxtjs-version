using backend.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Notification
    {
        public Notification(long userId, string title, string text = null, string subject = "other", long correlatedUserId = 0, string correlatedLink = null, bool hasQuestion = false)
        {
            UserId = userId;
            Title = title;
            Text = text;
            Subject = AcceptableSubjects.Contains(subject) ? subject : null;
            CorrelatedUserId = correlatedUserId;
            CorrelatedLink = correlatedLink;
            HasQuestion = hasQuestion;
        }

        private List<string> AcceptableSubjects =
            new List<string>{
                "other",
                "friendship",
                "simpletodo",
            };

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("text")]
        public string Text { get; set; }
        [Column("correlated_user_id")]
        public long CorrelatedUserId { get; set; }
        [Column("correlated_link")]
        public string CorrelatedLink { get; set; }
        [Column("subject")]
        public string Subject { get; set; }
        [Column("was_seen")]
        public bool WasSeen { get; set; }
        [Column("has_question")]
        public bool HasQuestion { get; set; }
        [Column("answer")]
        public int Answer { get; set; }
    }
}

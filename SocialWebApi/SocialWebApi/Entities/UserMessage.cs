using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class UserMessage
    {
        [Key]
        public int MessageId { get; set; }

        public string SourceId { get; set; }

        public string TargetId { get; set; }

        public string UserMessageContent { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(50)]
        public string ImageUrl { get; set; }

        [StringLength(50)]
        public string VideoUrl { get; set; }

        [Required]
        [StringLength(6)]
        public string MessageType { get; set; }

        public virtual User Source { get; set; }  

        public virtual User Target { get; set; }

    }
}

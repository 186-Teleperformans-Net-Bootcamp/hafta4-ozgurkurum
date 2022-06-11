using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class MessageHistory
    {
        public int Id { get; set; }

        public int MessageId { get; set; }

        [StringLength(6)]
        public string MessageType { get; set; }

        public string MessageContent { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}


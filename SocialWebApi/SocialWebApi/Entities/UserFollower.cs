using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class UserFollower
    {
        [Key]
        public int FollowerId { get; set; }

        public string SourceId { get; set; }

        public string TargetId { get; set; }

        [StringLength(8)]
        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual User Source { get; set; } 
        public virtual User Target { get; set; }

    }
}

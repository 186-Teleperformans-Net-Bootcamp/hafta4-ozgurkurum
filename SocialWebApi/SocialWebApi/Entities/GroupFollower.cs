using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class GroupFollower
    {
        [Key]
        public int GroupFollowerId { get; set; }

        public int SocialGroupId { get; set; }

        public string UserId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual SocialGroup SocialGroup { get; set; }

        public virtual User User { get; set; }
    }
}

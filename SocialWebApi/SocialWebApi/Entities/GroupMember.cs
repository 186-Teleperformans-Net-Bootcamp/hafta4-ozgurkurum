using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class GroupMember
    {
        public GroupMember()
        {
            GroupPosts = new HashSet<GroupPost>();
        }

        [Key]
        public int MemberId { get; set; }

        public int SocialGroupId { get; set; }

        public string UserId { get; set; }

        public DateTime? JoinedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int TotalPosts { get; set; }

        public virtual SocialGroup SocialGroup { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<GroupPost> GroupPosts { get; set; }
    }
}

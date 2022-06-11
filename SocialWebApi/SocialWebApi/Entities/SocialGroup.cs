using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class SocialGroup
    {
        public SocialGroup()
        {
            GroupFollowers = new HashSet<GroupFollower>();
            GroupMembers = new HashSet<GroupMember>();
            GroupPosts = new HashSet<GroupPost>();
        }

        [Key]
        public int SocialGroupId { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Details { get; set; }

        [StringLength(8)]
        public string GroupStatus { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

       
        public virtual ICollection<GroupFollower> GroupFollowers { get; set; }

        public virtual ICollection<GroupMember> GroupMembers { get; set; }

        public virtual ICollection<GroupPost> GroupPosts { get; set; }
    }
}

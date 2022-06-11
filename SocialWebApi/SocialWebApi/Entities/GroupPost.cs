using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class GroupPost
    {
        [Key]
        public int PostId { get; set; }

        public int SocialGroupId { get; set; }

        public int GroupMemberId { get; set; }

        [StringLength(255)]
        public string Post { get; set; }

        public virtual GroupMember GroupMember { get; set; } 

        public virtual SocialGroup SocialGroup { get; set; }
    }
}

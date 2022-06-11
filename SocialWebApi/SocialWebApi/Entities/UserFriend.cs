using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class UserFriend
    {
        [Key]
        public int FriendId { get; set; }

        public string SourceId { get; set; }

        public string TargetId { get; set; }

        public virtual User Source { get; set; }

        public virtual User Target { get; set; }

    }
}

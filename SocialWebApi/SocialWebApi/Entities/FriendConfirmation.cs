using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class FriendConfirmation
    {
        public int Id { get; set; }

        public int UserFriendSourceId { get; set; }

        public int UserFriendTargetId { get; set; }

        public bool? FriendStatus { get; set; }
    }
}

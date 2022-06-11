using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            GroupFollowers = new HashSet<GroupFollower>();
            GroupMembers = new HashSet<GroupMember>();
            PostLikes = new HashSet<PostLike>();
            UserFollowerSources = new HashSet<UserFollower>();
            UserFollowerTargets = new HashSet<UserFollower>();
            UserFriendSources = new HashSet<UserFriend>();
            UserFriendTargets = new HashSet<UserFriend>();
            UserMessageSources = new HashSet<UserMessage>();
            UserMessageTargets = new HashSet<UserMessage>();
            UserPosts = new HashSet<UserPost>();
        }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        public DateTime? RegisteredAt { get; set; }

        public DateTime? LastLogin { get; set; }

        [StringLength(50)]
        public string ProfileDescription { get; set; }

        public int? NoOfPosts { get; set; }

        [StringLength(100)]
        public string ProfilePicture { get; set; }

        public int? TotalFollowers { get; set; }

        public int? TotalFriends { get; set; }

        public virtual ICollection<GroupFollower> GroupFollowers { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<UserFollower> UserFollowerSources { get; set; }
        public virtual ICollection<UserFollower> UserFollowerTargets { get; set; }
        public virtual ICollection<UserFriend> UserFriendSources { get; set; }
        public virtual ICollection<UserFriend> UserFriendTargets { get; set; }
        public virtual ICollection<UserMessage> UserMessageSources { get; set; }
        public virtual ICollection<UserMessage> UserMessageTargets { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}

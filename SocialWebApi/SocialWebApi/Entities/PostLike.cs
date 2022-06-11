using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class PostLike
    {
        [Key]
        public int LikeId { get; set; }

        public int? UserPostId { get; set; }

        public string UserId { get; set; }

        public virtual UserPost UserPost { get; set; }

        public virtual User User { get; set; }
    }
}

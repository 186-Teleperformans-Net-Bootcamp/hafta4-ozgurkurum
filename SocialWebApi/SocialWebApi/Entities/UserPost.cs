using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class UserPost
    {
        public UserPost()
        {
            PostLikes = new HashSet<PostLike>();
        }

        [Key]
        public int PostId { get; set; }

        public string UserId { get; set; }

        [StringLength(255)]
        public string UserMessages { get; set; }

        public long? TotalLikes { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        
        public virtual ICollection<PostLike> PostLikes { get; set; }

        public virtual User User { get; set; }
    }
}

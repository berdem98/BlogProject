using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? CommentUserName { get; set; }
        [Required]
        
        public string? CommentTitle { get; set; }
        [Required]
        
        public string? CommentContent { get; set; }
        public DateTime CommentDate { get; set; }
        public bool CommentStatus { get; set; }
      
        public string? WriterImage { get; set; }
        public int BlogID { get; set; }
        public Blog Blog { get; set; } = null!;
    }
}

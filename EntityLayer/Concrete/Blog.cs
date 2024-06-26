﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string? BlogName { get; set; }
        public string? BlogContent { get; set; }
        public string? BlogImage { get; set; }
        public DateTime BlogCreateDate { get; set; }
        public bool BlogStatus { get; set; }
        public int CategoryId { get; set; }     
        public Category Category { get; set; } = null!;
        public int WriterId { get; set; }
        public Writer Writer { get; set; } = null!;
        List<Comment> Comments { get; set; } = new List<Comment>();
    }
}

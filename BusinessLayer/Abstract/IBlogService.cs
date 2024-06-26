﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        List<Blog> GetBlogListByCategory(int id);
        List<Blog> GetBlogListWithCategory();
        List<Blog> GetVerifiedBlogListByCategory(int id);
        List<Blog> GetBlogListByWriter(int id);
        List<Blog> GetVerifiedBlogList();
        Blog GetBlogByIdWithProperties(int id);
    }
}

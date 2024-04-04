using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfBlogRepository : GenericRepository<Blog>, IBlogDal
    {
        public List<Blog> BlogListByCategory(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Where(x => x.CategoryId == id).ToList();
            }
        }

        public Blog GetBlogByIdWithProperties(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Include(x => x.Writer).FirstOrDefault(x => x.BlogId == id);
            }
        }

        public List<Blog> GetListWithCategory()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category)
                    .Include(x => x.Writer)
                    .ToList();
            }
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Where(x => x.WriterId == id).ToList();
            }
        }

        public List<Blog> GetVerifiedBlogList()
        {

            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category)
                    .Include(x => x.Writer).Where(x => x.BlogStatus == true)
                    .ToList();
            }

        }

        public List<Blog> VerifiedBlogListByCategory(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Include(x=>x.Writer).Where(x => x.CategoryId == id && x.BlogStatus == true).ToList();
            }
        }
    }
}

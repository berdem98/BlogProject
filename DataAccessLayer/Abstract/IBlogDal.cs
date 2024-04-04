using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IBlogDal: IGenericDal<Blog>
    {
        List<Blog> BlogListByCategory(int id);
        List<Blog> VerifiedBlogListByCategory(int id);
        List<Blog> GetListWithCategory();
        List<Blog> GetVerifiedBlogList();
        List<Blog> GetListWithCategoryByWriter(int id);
        public Blog GetBlogByIdWithProperties(int id);
    }
}

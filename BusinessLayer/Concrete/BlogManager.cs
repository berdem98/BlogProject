using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }
        public List<Blog> GetBlogListByWriter(int id)
        {
            return _blogDal.GetListAll(x => x.WriterId == id);
        }
        public List<Blog> GetListWithCategoryByWriterBm(int id)
        {
            return _blogDal.GetListWithCategoryByWriter(id);
        }

        public List<Blog> GetBlogListWithCategory()
        {
            return _blogDal.GetListWithCategory();
        }
        
        public List<Blog> GetLast4Blog()
        {
            return _blogDal.GetVerifiedBlogList().OrderByDescending(x=>x.BlogCreateDate).Take(4).ToList();
        }

        public List<Blog> GetList()
        {
            return _blogDal.GetListAll();
        }

        public void TAdd(Blog t)
        {
            _blogDal.Insert(t);
        }

        public void TDelete(Blog t)
        {
            _blogDal.Delete(t);
        }

        public Blog TGetById(int id)
        {
            return _blogDal.GetById(id);
        }

        public void TUpdate(Blog t)
        {
            _blogDal.Update(t);
        }
        public List<Blog> GetLast3Blog()
        {
            return _blogDal.GetListAll().Take(3).ToList();
        }

        public List<Blog> GetBlogListByCategory(int id)
        {
            return _blogDal.BlogListByCategory(id);
        }
        public Blog GetBlogById(int id)
        {
            return _blogDal.GetById(id);
        }

        public List<Blog> GetVerifiedBlogList()
        {
            return _blogDal.GetVerifiedBlogList();
        }

        public List<Blog> GetVerifiedBlogListByCategory(int id)
        {
            return _blogDal.VerifiedBlogListByCategory(id);
        }

        public Blog GetBlogByIdWithProperties(int id)
        {
            return _blogDal.GetBlogByIdWithProperties(id);
        }
    }
}

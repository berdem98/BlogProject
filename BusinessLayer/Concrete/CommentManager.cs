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
    public class CommentManager : ICommentService
    {

        ICommentDal _commentdal;

        public CommentManager(ICommentDal commentdal)
        {
            _commentdal = commentdal;
        }
        public List<Comment> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(Comment t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(Comment t)
        {
            _commentdal.Delete(t);
        }

        public Comment TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Comment t)
        {
            _commentdal.Update(t);
        }
        public List<Comment> GetList(int id)
        {
            return _commentdal.GetCommentListByBlog(id);
        }

        public void CommentAdd(Comment comment)
        {
            _commentdal.Insert(comment);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Assignment_WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientService.svc or ClientService.svc.cs at the Solution Explorer and start debugging.
    public class ClientService : IClientService
    {
        dbDataContext db = new dbDataContext();

        // ClientService
        public bool AddClient(User user)
        {
            var encriptionMd5 = Encription.MD5Hash(user.Password);
            user.Password = encriptionMd5;
            db.User.InsertOnSubmit(user);
            db.SubmitChanges();
            return true;
        }

        public bool LoginClient(User user)
        {
            var result = db.User.Where(x => x.Email == user.Email && x.Password == Encription.MD5Hash(user.Password)).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public User GetByEmail(string email)
        {
            return db.User.SingleOrDefault(x => x.Email == email);
        }

        //CommentService
        public bool AddComment(Comment comment)
        {
            //ClientService clientService = new ClientService();
            //var query = db.User.Where(x => x.Email == user.Email && x.Password == Encription.MD5Hash(user.Password)).FirstOrDefault();

            //var result = clientService.LoginClient(query);

            var result1 = db.Place.Where(x => x.Id == comment.PlaceId).FirstOrDefault();

            //if (result == null)
            //{
            //    return false;
            //}
            //else
            //{
            if (result1 == null)
            {

                return false;
            }
            else
            {
                db.Comment.InsertOnSubmit(comment);
                db.SubmitChanges();
                return true;
            }
            //}
        }

        //SubCommentService
        public bool AddSubComment(SubComment subComment)
        {
            var result = db.Comment.Where(x => x.Id == subComment.CommentId).FirstOrDefault();

            if (result == null)
            {
                return false;
            }
            else
            {
                db.SubComment.InsertOnSubmit(subComment);
                db.SubmitChanges();
                return true;
            }
        }
        public List<Comment> GetCommentList()
        {
            try
            {
                return (from comment in db.Comment select comment).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<Comment> GetCommentListById(int id)
        {
            try
            {
                return (from comment in db.Comment where comment.PlaceId == id select comment).ToList();

            }
            catch
            {
                return null;
            }
        }

    }
}

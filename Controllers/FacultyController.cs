using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacultyWebApi.Controllers
{
    public class FacultyController : ApiController
    {
        public IEnumerable<user> Get() {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1()) {
                return db.users.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                var user = db.users.FirstOrDefault(u => u.id == id);
                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is No Such User");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Found, user);
                }
            }
        }


        public HttpResponseMessage post(user User)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                try
                {
                    db.users.Add(User);
                    db.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, User);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + User.id.ToString());
                    return msg;
                }
                catch (Exception exc)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.ToString());

                }
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                var user = db.users.FirstOrDefault(u => u.id == id);
                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no such user");
                }
                else
                {
                    db.users.Remove(user);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }

        public HttpResponseMessage Put(int id, user User)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                try
                {
                    var user = db.users.FirstOrDefault(u => u.id == id);
                    if (user == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no such user");
                    }
                    else
                    {
                        user.name = User.name;
                        user.userEmail = User.userEmail;
                        user.userImage = User.userImage;
                        user.userPassword = User.userPassword;
                        user.userType = User.userType;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, User);

                    }
                }
                catch (Exception exc)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc);
                }
            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacultyWebApi.Controllers
{
    public class CourseController : ApiController
    { 
        public IEnumerable<course> Get()
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                return db.courses.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                var course = db.courses.FirstOrDefault(c => c.id == id);
                if (course == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is No Such course");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Found, course);
                }
            }
        }
        [Route("api/course/getCourseByName/{name}")]
        public HttpResponseMessage Get(string name)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                var course = db.courses.FirstOrDefault(c => c.courseNmae == name);
                if (course == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is No Such course");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Found, course);
                }
            }
        }

        [Route("api/course/getCoursesOfDoctors/{id}")]
        public HttpResponseMessage Get_(int id)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                var course = db.courses.Where(c => c.id == id).ToList();
                if (course == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is No Such course");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Found, course);
                }
            }
        }



        public HttpResponseMessage post(course Course)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                try
                {
                    db.courses.Add(Course);
                    db.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, Course);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + Course.id.ToString());
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
                var course = db.courses.FirstOrDefault(d => d.id == id);
                if (course == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no such course");
                }
                else
                {
                    db.courses.Remove(course);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }

        public HttpResponseMessage Put(int id, course Course)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                try
                {
                    var course = db.courses.FirstOrDefault(d => d.id == id);
                    if (Course == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no such user");
                    }
                    else
                    {
                        if (Course.doctor_id == null)
                        {
                            course.courseNmae = Course.courseNmae;
                            course.courseDescription = Course.courseDescription;
                            //course.doctor_id = Course.doctor_id;
                            db.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, Course);
                        }
                        else {
                            course.courseNmae = Course.courseNmae;
                            course.courseDescription = Course.courseDescription;
                            course.doctor_id = Course.doctor_id;
                            db.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, Course);
                        }


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

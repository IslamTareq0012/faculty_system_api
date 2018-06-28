using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;

namespace FacultyWebApi.Controllers
{
    
    public class DoctorController : ApiController
    {
        public IEnumerable<doctor> Get()
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                return db.doctors.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                var user = db.doctors.FirstOrDefault(d => d.id == id);
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


        public HttpResponseMessage post(doctor Doctor)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                try
                {
                    db.doctors.Add(Doctor);
                    db.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, Doctor);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + Doctor.id.ToString());
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
                var doctor = db.doctors.FirstOrDefault(d => d.id == id);
                if (doctor == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no such user");
                }
                else
                {
                    db.doctors.Remove(doctor);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }

        public HttpResponseMessage Put(int id, doctor Doctor)
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                try
                {
                    var doctor = db.doctors.FirstOrDefault(d => d.id == id);
                    if (Doctor == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no such user");
                    }
                    else
                    {
                        doctor.doctorNmae = Doctor.doctorNmae;
                        doctor.doctorMail = Doctor.doctorMail;
                        doctor.doctorImage = Doctor.doctorImage;
                        doctor.doctorTitle = Doctor.doctorTitle;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, Doctor);

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

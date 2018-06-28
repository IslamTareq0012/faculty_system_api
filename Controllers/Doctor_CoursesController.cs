using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacultyWebApi.Controllers
{
    public class Doctor_CoursesController : ApiController
    {
        public IEnumerable<Object> Get()
        {
            using (multimedia_db2Entities1 db = new multimedia_db2Entities1())
            {
                return null;
                //return db.courses_doctors.ToList().incluc;
            }
        }
    }
}

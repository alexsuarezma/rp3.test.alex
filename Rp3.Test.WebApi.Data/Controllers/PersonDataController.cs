using Rp3.Test.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rp3.Test.WebApi.Data.Controllers
{
    public class PersonDataController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<Rp3.Test.Common.Models.Person> commonModel = new List<Common.Models.Person>();

            using (DataService service = new DataService())
            {
                var query = service.Persons.Get(orderBy: p => p.OrderByDescending(o => o.PersonId));

                commonModel = query.Select(p => new Common.Models.Person()
                {
                    PersonId = p.PersonId,
                    Name = p.Name                    
                }).ToList();                
            }

            return Ok(commonModel);
        }

        [HttpGet]
        public IHttpActionResult GetById(int personId)
        {
            Rp3.Test.Common.Models.Person commonModel = null;
            using (DataService service = new DataService())
            {
                var model = service.Persons.GetByID(personId);

                commonModel = new Common.Models.Person()
                {
                    PersonId = model.PersonId,
                    Name = model.Name  
                };
            }
            return Ok(commonModel);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using lubrizol.nglzconfig.data;
using lubrizol.nglzconfig.entities;

namespace nglzconfig.services.web.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using lubrizol.nglzconfig.entities;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<tblConnectionType>("ConnectionTypes");
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ConnectionTypesController : ODataController
    {
        private LZConfigContext db = new LZConfigContext();

        // GET: odata/ConnectionTypes
        [EnableQuery]
        public IQueryable<tblConnectionType> GetConnectionTypes()
        {
            return db.tblConnectionType;
        }

        // GET: odata/ConnectionTypes(5)
        [EnableQuery]
        public SingleResult<tblConnectionType> GettblConnectionType([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.tblConnectionType.Where(tblConnectionType => tblConnectionType.ID == key));
        }

        // PUT: odata/ConnectionTypes(5)
        public IHttpActionResult Put([FromODataUri] Guid key, Delta<tblConnectionType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tblConnectionType tblConnectionType = db.tblConnectionType.Find(key);
            if (tblConnectionType == null)
            {
                return NotFound();
            }

            patch.Put(tblConnectionType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblConnectionTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tblConnectionType);
        }

        // POST: odata/ConnectionTypes
        public IHttpActionResult Post(tblConnectionType tblConnectionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblConnectionType.Add(tblConnectionType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblConnectionTypeExists(tblConnectionType.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(tblConnectionType);
        }

        // PATCH: odata/ConnectionTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<tblConnectionType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tblConnectionType tblConnectionType = db.tblConnectionType.Find(key);
            if (tblConnectionType == null)
            {
                return NotFound();
            }

            patch.Patch(tblConnectionType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblConnectionTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tblConnectionType);
        }

        // DELETE: odata/ConnectionTypes(5)
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            tblConnectionType tblConnectionType = db.tblConnectionType.Find(key);
            if (tblConnectionType == null)
            {
                return NotFound();
            }

            db.tblConnectionType.Remove(tblConnectionType);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblConnectionTypeExists(Guid key)
        {
            return db.tblConnectionType.Count(e => e.ID == key) > 0;
        }
    }
}

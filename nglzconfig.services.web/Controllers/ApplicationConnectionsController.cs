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
    builder.EntitySet<tblApplicationConnection>("ApplicationConnections");
    builder.EntitySet<tblApplication>("tblApplication"); 
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ApplicationConnectionsController : ODataController
    {
        private LZConfigContext db = new LZConfigContext();

        // GET: odata/ApplicationConnections
        [EnableQuery]
        public IQueryable<tblApplicationConnection> GetApplicationConnections()
        {
            return db.tblApplicationConnection;
        }

        // GET: odata/ApplicationConnections(5)
        [EnableQuery]
        public SingleResult<tblApplicationConnection> GettblApplicationConnection([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.tblApplicationConnection.Where(tblApplicationConnection => tblApplicationConnection.ApplicationID == key));
        }

        // PUT: odata/ApplicationConnections(5)
        public IHttpActionResult Put([FromODataUri] Guid key, Delta<tblApplicationConnection> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tblApplicationConnection tblApplicationConnection = db.tblApplicationConnection.Find(key);
            if (tblApplicationConnection == null)
            {
                return NotFound();
            }

            patch.Put(tblApplicationConnection);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblApplicationConnectionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tblApplicationConnection);
        }

        // POST: odata/ApplicationConnections
        public IHttpActionResult Post(tblApplicationConnection tblApplicationConnection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblApplicationConnection.Add(tblApplicationConnection);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblApplicationConnectionExists(tblApplicationConnection.ApplicationID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(tblApplicationConnection);
        }

        // PATCH: odata/ApplicationConnections(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<tblApplicationConnection> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tblApplicationConnection tblApplicationConnection = db.tblApplicationConnection.Find(key);
            if (tblApplicationConnection == null)
            {
                return NotFound();
            }

            patch.Patch(tblApplicationConnection);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblApplicationConnectionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tblApplicationConnection);
        }

        // DELETE: odata/ApplicationConnections(5)
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            tblApplicationConnection tblApplicationConnection = db.tblApplicationConnection.Find(key);
            if (tblApplicationConnection == null)
            {
                return NotFound();
            }

            db.tblApplicationConnection.Remove(tblApplicationConnection);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ApplicationConnections(5)/tblApplication
        [EnableQuery]
        public SingleResult<tblApplication> GettblApplication([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.tblApplicationConnection.Where(m => m.ApplicationID == key).Select(m => m.tblApplication));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblApplicationConnectionExists(Guid key)
        {
            return db.tblApplicationConnection.Count(e => e.ApplicationID == key) > 0;
        }
    }
}

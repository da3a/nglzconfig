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
        [ODataRoute("ApplicationConnections(ApplicationID={applicationId},Name={name})")]
        public SingleResult<tblApplicationConnection> GettblApplicationConnection([FromODataUri] Guid applicationId, [FromODataUri] string name)
        {
            var connections = db.tblApplicationConnection
                          .Where(x => x.ApplicationID == applicationId)
                          .Where(x => x.Name == name);
            return SingleResult.Create(connections);
        }

        // PUT: odata/ApplicationConnections(5)
        [ODataRoute("ApplicationConnections(ApplicationID={applicationId},Name={name})")]
        public IHttpActionResult Put([FromODataUri] Guid applicationId, [FromODataUri] string name, Delta<tblApplicationConnection> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tblApplicationConnection tblApplicationConnection = db.tblApplicationConnection.Find(new object[] { applicationId, name });
            if (tblApplicationConnection == null)
            {
                return NotFound();
            }

            var userName = User.Identity.Name;
            patch.GetEntity().ModifiedBy = userName == string.Empty ? "user" : userName;
            patch.GetEntity().ModifiedDate = DateTime.Now;

            patch.Put(tblApplicationConnection);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblApplicationConnectionExists(applicationId, name))
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

            var userName = User.Identity.Name;
            tblApplicationConnection.CreatedBy = userName.Substring(userName.LastIndexOf(@"\") + 1);
            if (String.IsNullOrEmpty(tblApplicationConnection.CreatedBy))
                 tblApplicationConnection.CreatedBy = "user";

            tblApplicationConnection.CreatedDate = DateTime.Now;
            tblApplicationConnection.ModifiedBy = tblApplicationConnection.CreatedBy;
            if (String.IsNullOrEmpty(tblApplicationConnection.ModifiedBy))
                tblApplicationConnection.ModifiedBy = "user";
            tblApplicationConnection.ModifiedDate = DateTime.Now;


            db.tblApplicationConnection.Add(tblApplicationConnection);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblApplicationConnectionExists(tblApplicationConnection.ApplicationID, tblApplicationConnection.Name))
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

        [AcceptVerbs("PATCH", "MERGE")]
        [ODataRoute("ApplicationConnections(ApplicationID={applicationId},Name={name})")]
        public IHttpActionResult Patch([FromODataUri] Guid applicationId, [FromODataUri] string name, Delta<tblApplicationConnection> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tblApplicationConnection tblApplicationConnection = db.tblApplicationConnection.Find(applicationId, name);
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
                if (!tblApplicationConnectionExists(applicationId, name))
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
        [ODataRoute("ApplicationConnections(ApplicationID={applicationId},Name={name})")]
        public IHttpActionResult Delete([FromODataUri] Guid applicationId, [FromODataUri] string name)
        {
            tblApplicationConnection tblApplicationConnection = db.tblApplicationConnection.Find(applicationId, name);
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

        private bool tblApplicationConnectionExists(Guid applicationId, string name)
        {
            return db.tblApplicationConnection.Count(e => e.ApplicationID == applicationId && e.Name == name) > 0;
        }
    }
}

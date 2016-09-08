using lubrizol.nglzconfig.data;
using lubrizol.nglzconfig.entities;
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


namespace nglzconfig.services.web.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using Lubrizol.LZConfig.Entities;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<tblApplicationVariable>("ApplicationVariables");
    builder.EntitySet<tblApplication>("tblApplication"); 
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ApplicationVariablesController : ODataController
    {
        private LZConfigContext db = new LZConfigContext();

        // GET: odata/ApplicationVariables
        [EnableQuery]
        public IQueryable<tblApplicationVariable> GetApplicationVariables()
        {
            return db.tblApplicationVariable;
        }

        // GET: odata/ApplicationVariables(5)
        [EnableQuery]
        [ODataRoute("ApplicationVariables(ApplicationID={applicationId},Name={name})")]
        public SingleResult<tblApplicationVariable> GettblApplicationVariable([FromODataUri] Guid applicationId, [FromODataUri] string name)
        {
            var variables = db.tblApplicationVariable
                .Where(tblApplicationVariable => tblApplicationVariable.ApplicationID == applicationId)
                .Where(tblApplicationVariable => tblApplicationVariable.Name == name);
            return SingleResult.Create(variables);
        }

        // PUT: odata/ApplicationVariables(5)
        [ODataRoute("ApplicationVariables(ApplicationID={applicationId},Name={name})")]
        public IHttpActionResult Put([FromODataUri] Guid applicationId, [FromODataUri] string name, Delta<tblApplicationVariable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tblApplicationVariable tblApplicationVariable = db.tblApplicationVariable.Find(new object[] { applicationId, name });
            if (tblApplicationVariable == null)
            {
                return NotFound();
            }
            var userName = User.Identity.Name;
            patch.GetEntity().ModifiedBy = userName == string.Empty ? "user" : userName;

            patch.GetEntity().ModifiedDate = DateTime.Now;

            patch.Put(tblApplicationVariable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblApplicationVariableExists(applicationId, name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tblApplicationVariable);
        }

        // POST: odata/ApplicationVariables

        public IHttpActionResult Post([FromBody] tblApplicationVariable tblApplicationVariable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userName = User.Identity.Name;
            tblApplicationVariable.CreatedBy = userName.Substring(userName.LastIndexOf(@"\") + 1);
            tblApplicationVariable.CreatedDate = DateTime.Now;
            tblApplicationVariable.ModifiedBy = tblApplicationVariable.CreatedBy;
            tblApplicationVariable.ModifiedDate = DateTime.Now;


            db.tblApplicationVariable.Add(tblApplicationVariable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblApplicationVariableExists(tblApplicationVariable.ApplicationID, tblApplicationVariable.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(tblApplicationVariable);
        }

        // PATCH: odata/ApplicationVariables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        [ODataRoute("ApplicationVariables(ApplicationID={applicationId},Name={name})")]
        public IHttpActionResult Patch([FromODataUri] Guid applicationId, [FromODataUri] string name, Delta<tblApplicationVariable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tblApplicationVariable tblApplicationVariable = db.tblApplicationVariable.Find(new object[] { applicationId, name });
            if (tblApplicationVariable == null)
            {
                return NotFound();
            }

            patch.Patch(tblApplicationVariable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblApplicationVariableExists(applicationId, name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tblApplicationVariable);
        }

        // DELETE: odata/ApplicationVariables(5)
        [ODataRoute("ApplicationVariables(ApplicationID={applicationId},Name={name})")]
        public IHttpActionResult Delete([FromODataUri] Guid applicationId, [FromODataUri] string name)
        {
            tblApplicationVariable tblApplicationVariable = db.tblApplicationVariable.Find(new object[] { applicationId, name });
            if (tblApplicationVariable == null)
            {
                return NotFound();
            }

            db.tblApplicationVariable.Remove(tblApplicationVariable);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ApplicationVariables(5)/tblApplication
        [EnableQuery]
        public SingleResult<tblApplication> GettblApplication([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.tblApplicationVariable.Where(m => m.ApplicationID == key).Select(m => m.tblApplication));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblApplicationVariableExists(Guid applicationId, string name)
        {
            return db.tblApplicationVariable.Count(e => e.ApplicationID == applicationId && e.Name == name) > 0;
        }
    }
}

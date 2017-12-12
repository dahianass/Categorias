using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using OnData.PruebaTecnica.DASS.Models;

namespace OnData.PruebaTecnica.DASS.Controllers
{
    /*
    Puede que la clase WebApiConfig requiera cambios adicionales para agregar una ruta para este controlador. Combine estas instrucciones en el método Register de la clase WebApiConfig según corresponda. Tenga en cuenta que las direcciones URL de OData distinguen mayúsculas de minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using OnData.PruebaTecnica.DASS.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Etiqueta>("Etiquetas");
    builder.EntitySet<Contenido>("Contenidos"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EtiquetasController : ODataController
    {
        private ModelDASS db = new ModelDASS();

        // GET: odata/Etiquetas
        [EnableQuery]
        public IQueryable<Etiqueta> GetEtiquetas()
        {
            return db.Etiquetas;
        }

        // GET: odata/Etiquetas(5)
        [EnableQuery]
        public SingleResult<Etiqueta> GetEtiqueta([FromODataUri] int key)
        {
            return SingleResult.Create(db.Etiquetas.Where(etiqueta => etiqueta.IdEtiquetas == key));
        }

        // PUT: odata/Etiquetas(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Etiqueta> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Etiqueta etiqueta = db.Etiquetas.Find(key);
            if (etiqueta == null)
            {
                return NotFound();
            }

            patch.Put(etiqueta);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtiquetaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(etiqueta);
        }

        // POST: odata/Etiquetas
        public IHttpActionResult Post(Etiqueta etiqueta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Etiquetas.Add(etiqueta);
            db.SaveChanges();

            return Created(etiqueta);
        }

        // PATCH: odata/Etiquetas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Etiqueta> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Etiqueta etiqueta = db.Etiquetas.Find(key);
            if (etiqueta == null)
            {
                return NotFound();
            }

            patch.Patch(etiqueta);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtiquetaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(etiqueta);
        }

        // DELETE: odata/Etiquetas(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Etiqueta etiqueta = db.Etiquetas.Find(key);
            if (etiqueta == null)
            {
                return NotFound();
            }

            db.Etiquetas.Remove(etiqueta);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Etiquetas(5)/Contenido
        [EnableQuery]
        public SingleResult<Contenido> GetContenido([FromODataUri] int key)
        {
            return SingleResult.Create(db.Etiquetas.Where(m => m.IdEtiquetas == key).Select(m => m.Contenido));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EtiquetaExists(int key)
        {
            return db.Etiquetas.Count(e => e.IdEtiquetas == key) > 0;
        }
    }
}

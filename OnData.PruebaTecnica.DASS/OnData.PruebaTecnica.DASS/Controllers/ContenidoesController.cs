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
    builder.EntitySet<Contenido>("Contenidoes");
    builder.EntitySet<Categoria>("Categorias"); 
    builder.EntitySet<Etiqueta>("Etiquetas"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ContenidoesController : ODataController
    {
        private ModelDASS db = new ModelDASS();

        // GET: odata/Contenidoes
        [EnableQuery]
        public IQueryable<Contenido> GetContenidoes()
        {
            return db.Contenidos;
        }

        // GET: odata/Contenidoes(5)
        [EnableQuery]
        public SingleResult<Contenido> GetContenido([FromODataUri] int key)
        {
            return SingleResult.Create(db.Contenidos.Where(contenido => contenido.IdContenido == key));
        }

        // PUT: odata/Contenidoes(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Contenido> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contenido contenido = db.Contenidos.Find(key);
            if (contenido == null)
            {
                return NotFound();
            }

            patch.Put(contenido);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContenidoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contenido);
        }

        // POST: odata/Contenidoes
        public IHttpActionResult Post(Contenido contenido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contenidos.Add(contenido);
            db.SaveChanges();

            return Created(contenido);
        }

        // PATCH: odata/Contenidoes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Contenido> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contenido contenido = db.Contenidos.Find(key);
            if (contenido == null)
            {
                return NotFound();
            }

            patch.Patch(contenido);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContenidoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contenido);
        }

        // DELETE: odata/Contenidoes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Contenido contenido = db.Contenidos.Find(key);
            if (contenido == null)
            {
                return NotFound();
            }

            db.Contenidos.Remove(contenido);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Contenidoes(5)/Categorias
        [EnableQuery]
        public IQueryable<Categoria> GetCategorias([FromODataUri] int key)
        {
            return db.Contenidos.Where(m => m.IdContenido == key).SelectMany(m => m.Categorias);
        }

        // GET: odata/Contenidoes(5)/Etiquetas
        [EnableQuery]
        public IQueryable<Etiqueta> GetEtiquetas([FromODataUri] int key)
        {
            return db.Contenidos.Where(m => m.IdContenido == key).SelectMany(m => m.Etiquetas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContenidoExists(int key)
        {
            return db.Contenidos.Count(e => e.IdContenido == key) > 0;
        }
    }
}

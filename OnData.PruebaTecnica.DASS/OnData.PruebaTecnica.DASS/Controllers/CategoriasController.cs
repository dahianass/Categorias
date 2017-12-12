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
    builder.EntitySet<Categoria>("Categorias");
    builder.EntitySet<Contenido>("Contenidos"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CategoriasController : ODataController
    {
        private ModelDASS db = new ModelDASS();

        // GET: odata/Categorias
        [EnableQuery]
        public IQueryable<Categoria> GetCategorias()
        {
            return db.Categorias;
        }

        // GET: odata/Categorias(5)
        [EnableQuery]
        public SingleResult<Categoria> GetCategoria([FromODataUri] int key)
        {
            return SingleResult.Create(db.Categorias.Where(categoria => categoria.IdCategoria == key));
        }

        // PUT: odata/Categorias(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Categoria> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Categoria categoria = db.Categorias.Find(key);
            if (categoria == null)
            {
                return NotFound();
            }

            patch.Put(categoria);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(categoria);
        }

        // POST: odata/Categorias
        public IHttpActionResult Post(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorias.Add(categoria);
            db.SaveChanges();

            return Created(categoria);
        }

        // PATCH: odata/Categorias(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Categoria> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Categoria categoria = db.Categorias.Find(key);
            if (categoria == null)
            {
                return NotFound();
            }

            patch.Patch(categoria);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(categoria);
        }

        // DELETE: odata/Categorias(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Categoria categoria = db.Categorias.Find(key);
            if (categoria == null)
            {
                return NotFound();
            }

            db.Categorias.Remove(categoria);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Categorias(5)/Contenido
        [EnableQuery]
        public SingleResult<Contenido> GetContenido([FromODataUri] int key)
        {
            return SingleResult.Create(db.Categorias.Where(m => m.IdCategoria == key).Select(m => m.Contenido));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaExists(int key)
        {
            return db.Categorias.Count(e => e.IdCategoria == key) > 0;
        }
    }
}

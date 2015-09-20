using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoLote.Models;
using AutoLote.Helpers;

namespace AutoLote.Controllers
{
    public class AutomovilController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Automovil
        public ActionResult Index()
        {
            return View(db.Automovils
                .Include("Modelo")
                .Include("Modelo.Marcas")
                .Include("Tipo")
                .Include("AutomovilImagenes").ToList());
        }

        // GET: Automovil/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Automovil automovil = db.Automovils.Find(id);
            if (automovil == null)
            {
                return HttpNotFound();
            }
            return View(automovil);
        }

        // GET: Automovil/Create
        public ActionResult Create()
        {
            var auto = new Automovil() { FechaPublicacion = DateTime.Now };
            return View(auto);
        }

        // POST: Automovil/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "AutomovilID,ModelosID,TiposID,TieneAireAcondicionado,Comentarios,Anio,Color,FechaPublicacion,Email")]
        public ActionResult Create( Automovil automovil)
        {
            int tipoID = int.Parse(Request.Form["ModelosID"].ToString());

            if (ModelState.IsValid)
            {
                if (automovil.AutomovilImagenes != null && automovil.AutomovilImagenes.Any())
                {
                    var guardarimagen = new clsGuardarImagen();
                    foreach (var imagen in automovil.AutomovilImagenes)
                    {
                        string nombreArchvivo = Guid.NewGuid().ToString();
                        imagen.UrlImagenMiniatura = guardarimagen.RedimensionarAndGuardar(nombreArchvivo, imagen.ImagenSubida.InputStream, Tamanios.Miniatura, true);
                        imagen.UrlImagenMediana = guardarimagen.RedimensionarAndGuardar(nombreArchvivo, imagen.ImagenSubida.InputStream, Tamanios.Mediana, true);
                    }
                }
                automovil.ModeloID = tipoID;
                db.Automovils.Add(automovil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(automovil);
        }

        // GET: Automovil/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Automovil automovil = db.Automovils.Find(id);
            if (automovil == null)
            {
                return HttpNotFound();
            }
            return View(automovil);
        }

        // POST: Automovil/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AutomovilID,ModelosID,TiposID,TieneAireAcondicionado,Comentarios,Anio,Color,FechaPublicacion,Email")] Automovil automovil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(automovil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(automovil);
        }

        // GET: Automovil/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Automovil automovil = db.Automovils.Find(id);
            if (automovil == null)
            {
                return HttpNotFound();
            }
            return View(automovil);
        }

        // POST: Automovil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Automovil automovil = db.Automovils.Find(id);
            db.Automovils.Remove(automovil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

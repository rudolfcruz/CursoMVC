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
using System.IO;


namespace AutoLote.Controllers
{
    public class MarcasController : MessageController
    {
        private DBContext db = new DBContext();

        // GET: Marcas
        public ActionResult Index()
        {
            return View(db.Marcas.ToList());
        }

        // GET: Marcas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcas marcas = db.Marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            return View(marcas);
        }

        // GET: Marcas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Marcas marcas)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var guardarIagen = new clsGuardarImagen();
                    //string nombreArchivo = Guid.NewGuid().ToString();
                    string nombreArchivo = Path.GetFileName(marcas.ImagenSubida.FileName);
                    marcas.UrlImagen = guardarIagen.RedimensionarAndGuardar(nombreArchivo, marcas.ImagenSubida.InputStream, Tamanios.Miniatura, false,"C");
                    if(marcas.UrlImagen == "false")
                    {
                        //return View("<script language='javascript' type='text/javascript'>alert('El Archivo ya Existe!');</script>");
                        //ModelState.AddModelError("Error", "Ex: This login failed");
                        Warning(string.Format("<b>{0}</b> El Archivo ya Existe.", nombreArchivo), true);
                        //TempData["Message"] = "El Archivo ya Existe";

                        return View("Create");
                    }
                    else
                    {
                        db.Marcas.Add(marcas);
                        db.SaveChanges();
                        Success(string.Format("<b>{0}</b> El Archivo se guardo Exitosamente.", nombreArchivo), true);
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return View(marcas);
        }

        // GET: Marcas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcas marcas = db.Marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            return View(marcas);
        }

        // POST: Marcas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "MarcaID,Descripcion,UrlImagen")]
        public ActionResult Edit( Marcas marcas)
        {
            if (ModelState.IsValid)
            {
                var guardarIagen = new clsGuardarImagen();
                //string nombreArchivo = Guid.NewGuid().ToString();
                string nombreArchivo = marcas.ImagenSubida.FileName;
                marcas.UrlImagen = guardarIagen.RedimensionarAndGuardar(nombreArchivo, marcas.ImagenSubida.InputStream, Tamanios.Miniatura, false,"U");
                db.Entry(marcas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marcas);
        }

        // GET: Marcas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcas marcas = db.Marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            return View(marcas);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marcas marcas = db.Marcas.Find(id);
            db.Marcas.Remove(marcas);
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
        public ActionResult ListaMarcas(int id)
        {
            var marcas = db.Marcas;
            var autoMovil = db.Automovils.Where(x => x.AutomovilID == id);
            if (autoMovil != null)
            {
                var query = (from i in db.Marcas
                             join a in db.Automovils.Where(x => x.AutomovilID == id)
                                 on i.MarcaID equals a.Modelo.Marcas.MarcaID into joined
                             from a in joined.DefaultIfEmpty()
                             select new
                             {
                                 Id = i.MarcaID,
                                 Marca = i.Descripcion,
                                 selected = a != null
                             });
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var consulta = (from i in db.Marcas
                                select new
                                {
                                    Id = i.MarcaID,
                                    Marca = i.Descripcion,
                                    selected = false
                                });
                return Json(consulta, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

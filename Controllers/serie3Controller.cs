using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Lifetime;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Audi.Models;
using Rotativa.AspNetCore;

namespace Audi.Controllers
{
    public class serie3Controller : Controller
    {
        
        private audiDeportivoEntities1 db = new audiDeportivoEntities1();

        // GET: serie3
        public ActionResult Index()
        {
            return View(db.serie3.ToList());
        }


        // GET: serie3/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serie3 serie3 = db.serie3.Find(id);
            if (serie3 == null)
            {
                return HttpNotFound();
            }
            return View(serie3);
        }

        // GET: serie3/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: serie3/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_coche,nombre,modelo,cv,consumo,imagen")] serie3 serie3)
        {
            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase.FileName.EndsWith(".jpg")) { 
                WebImage image = new WebImage(FileBase.InputStream); 
                serie3.imagen = image.GetBytes(); } 
            else { ModelState.AddModelError("Imagen", "Solo se admiten imágenes en formato jpg"); }
        
            if (ModelState.IsValid)
            {
                db.serie3.Add(serie3);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serie3);
        }

        // GET: serie3/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serie3 serie3 = db.serie3.Find(id);
            if (serie3 == null)
            {
                return HttpNotFound();
            }
            return View(serie3);
        }

        // POST: serie3/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_coche,nombre,modelo,cv,consumo,imagen")] serie3 serie3)
        {
           

            if (ModelState.IsValid)
            {
                db.Entry(serie3).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serie3);
        }

        // GET: serie3/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serie3 serie3 = db.serie3.Find(id);
            if (serie3 == null)
            {
                return HttpNotFound();
            }
            return View(serie3);
        }

        // POST: serie3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            serie3 serie3 = db.serie3.Find(id);
            db.serie3.Remove(serie3);
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
        public ActionResult getImage(int id)
        {
            serie3 tipos = db.serie3.Find(id);
            byte[] byteImage = tipos.imagen;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            try{
                Image image = Image.FromStream(memoryStream);
                memoryStream = new MemoryStream();

                image.Save(memoryStream, ImageFormat.Png);
                memoryStream.Position = 0;

              
            }
            catch
            {

            }
            return File(memoryStream, "image/png");


        }
    }
}

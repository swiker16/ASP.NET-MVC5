using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Audi.Models;

namespace Audi.Controllers
{
    public class serie5Controller : Controller
    {
        private audiDeportivoEntities1 db = new audiDeportivoEntities1();

        // GET: serie5
        public ActionResult Index()
        {
            return View(db.serie5.ToList());
        }

        // GET: serie5/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serie5 serie5 = db.serie5.Find(id);
            if (serie5 == null)
            {
                return HttpNotFound();
            }
            return View(serie5);
        }

        // GET: serie5/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: serie5/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_coche,nombre,modelo,cv,consumo,imagen")] serie5 serie5)
        {
            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase.FileName.EndsWith(".jpg"))
            {
                WebImage image = new WebImage(FileBase.InputStream);
                serie5.imagen = image.GetBytes();
            }
            else { ModelState.AddModelError("Imagen", "Solo se admiten imágenes en formato jpg"); }
            if (ModelState.IsValid)
            {
                db.serie5.Add(serie5);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serie5);
        }

        // GET: serie5/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serie5 serie5 = db.serie5.Find(id);
            if (serie5 == null)
            {
                return HttpNotFound();
            }
            return View(serie5);
        }

        // POST: serie5/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_coche,nombre,modelo,cv,consumo,imagen")] serie5 serie5)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serie5).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serie5);
        }

        // GET: serie5/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serie5 serie5 = db.serie5.Find(id);
            if (serie5 == null)
            {
                return HttpNotFound();
            }
            return View(serie5);
        }

        // POST: serie5/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            serie5 serie5 = db.serie5.Find(id);
            db.serie5.Remove(serie5);
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
            serie5 tipos = db.serie5.Find(id);
            byte[] byteImage = tipos.imagen;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            try
            {
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

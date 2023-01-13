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
    public class serie7Controller : Controller
    {
        private audiDeportivoEntities1 db = new audiDeportivoEntities1();

        // GET: serie7
        public ActionResult Index()
        {
            return View(db.serie7.ToList());
        }

        // GET: serie7/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serie7 serie7 = db.serie7.Find(id);
            if (serie7 == null)
            {
                return HttpNotFound();
            }
            return View(serie7);
        }

        // GET: serie7/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: serie7/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_coche,nombre,modelo,cv,consumo,imagen")] serie7 serie7)
        {
            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase.FileName.EndsWith(".jpg"))
            {
                WebImage image = new WebImage(FileBase.InputStream);
                serie7.imagen = image.GetBytes();
            }
            else { ModelState.AddModelError("Imagen", "Solo se admiten imágenes en formato jpg"); }
            if (ModelState.IsValid)
            {
                db.serie7.Add(serie7);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serie7);
        }

        // GET: serie7/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serie7 serie7 = db.serie7.Find(id);
            if (serie7 == null)
            {
                return HttpNotFound();
            }
            return View(serie7);
        }

        // POST: serie7/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_coche,nombre,modelo,cv,consumo,imagen")] serie7 serie7)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serie7).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serie7);
        }

        // GET: serie7/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serie7 serie7 = db.serie7.Find(id);
            if (serie7 == null)
            {
                return HttpNotFound();
            }
            return View(serie7);
        }

        // POST: serie7/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            serie7 serie7 = db.serie7.Find(id);
            db.serie7.Remove(serie7);
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
            serie7 tipos = db.serie7.Find(id);
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

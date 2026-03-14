using System.Web.Mvc;
using employees_mangment.Models;
using System.Linq;

namespace employees_mangment.Controllers
{
    public class PropertyDefinitionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var properties = db.PropertyDefinitions.ToList();
            return View(properties);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PropertyDefinition property)
        {
            if (ModelState.IsValid)
            {
                db.PropertyDefinitions.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        public ActionResult Edit(int id)
        {
            var property = db.PropertyDefinitions.Find(id);
            return View(property);
        }

        [HttpPost]
        public ActionResult Edit(int id, PropertyDefinition property)
        {
            if (ModelState.IsValid)
            {
                var existing = db.PropertyDefinitions.Find(id);
                existing.Name = property.Name;
                existing.Type = property.Type;
                existing.IsRequired = property.IsRequired;
                existing.DropdownOptions = property.DropdownOptions;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        public ActionResult Delete(int id)
        {
            var property = db.PropertyDefinitions.Find(id);
            if (property != null)
            {
                db.PropertyDefinitions.Remove(property);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
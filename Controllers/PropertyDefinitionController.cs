using System.Web.Mvc;
using employees_mangment.Models;
using System.Linq;


namespace employees_mangment.Controllers
{
    public class PropertyDefinitionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PropertyDefinition
        public ActionResult Index()
        {
            var properties = db.PropertyDefinitions.ToList();
            return View(properties);
        }

        // GET: PropertyDefinition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyDefinition/Create
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
    }
}
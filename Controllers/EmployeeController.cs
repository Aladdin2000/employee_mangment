using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using employees_mangment.Models;

namespace employees_mangment.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employee
        public ActionResult Index()
        {
            var employees = db.Employees
                .Include(e => e.PropertyValues.Select(pv => pv.PropertyDefinition))
                .ToList();
            return View(employees);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var properties = db.PropertyDefinitions.ToList();
            ViewBag.Properties = properties;
            return View(new employees());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(employees employee, string[] propertyValues)
        {
            if (ModelState.IsValid)
            {
                var properties = db.PropertyDefinitions.ToList();
                for (int i = 0; i < properties.Count; i++)
                {
                    employee.PropertyValues.Add(new EmployeePropertyValue
                    {
                        PropertyDefinitionId = properties[i].Id,
                        Value = propertyValues != null && propertyValues.Length > i
                            ? propertyValues[i] : ""
                    });
                }
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}
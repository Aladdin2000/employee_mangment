using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using employees_mangment.Models;

namespace employees_mangment.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var employees = db.Employees
                .Include(e => e.PropertyValues.Select(pv => pv.PropertyDefinition))
                .ToList();
            return View(employees);
        }

        public ActionResult Create()
        {
            var properties = db.PropertyDefinitions.ToList();
            ViewBag.Properties = properties;
            return View(new employees());
        }

        [HttpPost]
        public ActionResult Create(employees employee, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var properties = db.PropertyDefinitions.ToList();
                db.Employees.Add(employee);
                db.SaveChanges();
                for (int i = 0; i < properties.Count; i++)
                {
                    var value = form["prop_" + i];
                    db.EmployeePropertyValues.Add(new EmployeePropertyValue
                    {
                        EmployeeId = employee.Id,
                        PropertyDefinitionId = properties[i].Id,
                        Value = value ?? ""
                    });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Properties = db.PropertyDefinitions.ToList();
            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            var employee = db.Employees
                .Include(e => e.PropertyValues)
                .FirstOrDefault(e => e.Id == id);
            var properties = db.PropertyDefinitions.ToList();
            ViewBag.Properties = properties;
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(int id, employees employee, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var properties = db.PropertyDefinitions.ToList();
                var existing = db.Employees
                    .Include(e => e.PropertyValues)
                    .FirstOrDefault(e => e.Id == id);

                existing.Code = employee.Code;
                existing.Name = employee.Name;

                foreach (var pv in existing.PropertyValues.ToList())
                {
                    db.EmployeePropertyValues.Remove(pv);
                }

                for (int i = 0; i < properties.Count; i++)
                {
                    var value = form["prop_" + i];
                    db.EmployeePropertyValues.Add(new EmployeePropertyValue
                    {
                        EmployeeId = id,
                        PropertyDefinitionId = properties[i].Id,
                        Value = value ?? ""
                    });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

    
        public ActionResult Delete(int id)
        {
            var employee = db.Employees
                .Include(e => e.PropertyValues)
                .FirstOrDefault(e => e.Id == id);

            foreach (var pv in employee.PropertyValues.ToList())
            {
                db.EmployeePropertyValues.Remove(pv);
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
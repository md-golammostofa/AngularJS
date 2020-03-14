using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get_AllEmployee()
        {
            using (EmployeeDbContext Obj = new EmployeeDbContext())
            {
                List<Employee> Emp = Obj.Employees.ToList();
                return Json(Emp, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Get_EmployeeById(string Id)
        {
            using (EmployeeDbContext Obj = new EmployeeDbContext())
            {
                int EmpId = int.Parse(Id);
                return Json(Obj.Employees.Find(EmpId), JsonRequestBehavior.AllowGet);
            }
        }
        public string Insert_Employee(Employee Employe)
        {
            if (Employe != null)
            {
                using (EmployeeDbContext Obj = new EmployeeDbContext())
                {
                    Obj.Employees.Add(Employe);
                    Obj.SaveChanges();
                    return "Employee Added Successfully";
                }
            }
            else
            {
                return "Employee Not Inserted! Try Again";
            }
        }
        public string Delete_Employee(Employee Emp)
        {
            if (Emp != null)
            {
                using (EmployeeDbContext Obj = new EmployeeDbContext())
                {
                    var Emp_ = Obj.Entry(Emp);
                    if (Emp_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.Employees.Attach(Emp);
                        Obj.Employees.Remove(Emp);
                    }
                    Obj.SaveChanges();
                    return "Employee Deleted Successfully";
                }
            }
            else
            {
                return "Employee Not Deleted! Try Again";
            }
        }
        public string Update_Employee(Employee Emp)
        {
            if (Emp != null)
            {
                using (EmployeeDbContext Obj = new EmployeeDbContext())
                {
                    var Emp_ = Obj.Entry(Emp);
                    Employee EmpObj = Obj.Employees.Where(x => x.Emp_Id == Emp.Emp_Id).FirstOrDefault();
                    EmpObj.Emp_Age = Emp.Emp_Age;
                    EmpObj.Emp_City = Emp.Emp_City;
                    EmpObj.Emp_Name = Emp.Emp_Name;
                    Obj.SaveChanges();
                    return "Employee Updated Successfully";
                }
            }
            else
            {
                return "Employee Not Updated! Try Again";
            }
        }
    }
}
﻿using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Gamedalf.Core.Models;
using Gamedalf.ViewModels;
using Gamedalf.Services;
using PagedList;
using System.Data.SqlClient;
using System;
using System.Data.Entity.Infrastructure;
using Gamedalf.Core.Infrastructure;

namespace Gamedalf.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeService        employees;
        private readonly ApplicationUserManager UserManager;

        public EmployeesController(ApplicationUserManager _userManager, EmployeeService _employees)
        {
            UserManager = _userManager;
            employees   = _employees;
        }

        // GET: Employees
        public async Task<ActionResult> Index(string q = null, int page = 1, int size = 10)
        {
            ViewBag.q = q;

            var list = (await employees.Search(q))
                .ToPagedList(page, size);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", list);
            }

            return View(list);
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(EmployeeRegisterViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var newest = new Employee
                {
                    Email    = employee.Email,
                    UserName = employee.Email,
                    SSN      = employee.SSN
                };

                try
                {
                    await UserManager.CreateAsync(newest, employee.Password);
                    await UserManager.AddToRoleAsync(newest.Id, "employee");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("SSN", "The SSN inserted has already been taken.");
                }
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var viewmodel = new EmployeeEditViewModel
            {
                Id          = employee.Id,
                Email       = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                SSN         = employee.SSN
            };

            return View(viewmodel);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(EmployeeEditViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var modified = await employees.Find(employee.Id);

                modified.Email       = employee.Email;
                modified.UserName    = employee.Email;
                modified.PhoneNumber = employee.PhoneNumber;
                modified.SSN         = employee.SSN;

                try
                 {
                    await employees.Update(modified);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("SSN", "The SSN inserted has already been taken.");
                }
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = await employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await  employees.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                employees.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

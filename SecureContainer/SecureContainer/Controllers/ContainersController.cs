using SecureContainer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecureContainer.Controllers
{
    //[Authorize]

    public class ContainersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContainersController()
        {
            _context = new ApplicationDbContext();
        }
       
        public ActionResult ListContainers()
        {
            var containers = _context.Containers.ToList();
            return View(containers);
        }


        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public ActionResult AddContainer()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult AddContainer(Container container, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.ContentType == "application/pdf" && file.ContentLength <= 5 * 1024 * 1024)
                    {
                        string uploadDirectory = Server.MapPath("~/Uploads");
                        if (!Directory.Exists(uploadDirectory))
                        {
                            Directory.CreateDirectory(uploadDirectory);
                        }

                        string filePath = Path.Combine(uploadDirectory, file.FileName);
                        file.SaveAs(filePath);

                        container.FilePath = "/Uploads/" + file.FileName;
                        _context.Containers.Add(container);
                        _context.SaveChanges();

                        return RedirectToAction("ListContainers");
                    }
                    else
                    {
                        ModelState.AddModelError("file", "Please upload a valid PDF file under 5 MB.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and show an error message
                    ModelState.AddModelError("", "An error occurred: " + ex.Message);
                }
            }

            // If validation fails, or an error occurs, re-render the form
            ViewBag.StatusOptions = new SelectList(new[] { "In Transit", "At Dock", "Delivered" });
            return View(container);
        }
    }
}
using ContactManager.Models;
using System.Web.Mvc;

namespace ContactManager.Controllers
{
   
    public class ContactController : Controller
    {
        private IContactManagerService _service;

        public ContactController()
        {
            _service = new ContactManagerService(new ModelStateWrapper(this.ModelState));
        }
        public ContactController(IContactManagerService service)
        {
            _service = service;           
        }
        [Authorize]
        public ActionResult Index()
        {
            return View(_service.ListContacts());
        }

        // GET: Home/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        // POST: Home/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "FirstName, LastName, Phone, Email")] Contact contactToCreate)
        {
          
            if (_service.CreateContact(contactToCreate))
                return RedirectToAction("Index");
            return View();

        }

        // GET: Home/Edit/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Edit(int id)
        {
            
            return View(_service.GetContact(id));
        }

        // POST: Home/Edit/5
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public ActionResult Edit(int id, Contact contactToEdit)
        {
            if (_service.EditContact(contactToEdit))
                return RedirectToAction("Index");
            return View(contactToEdit);
        }

        // GET: Home/Delete/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Delete(int id)
        {
            return View(_service.GetContact(id));
        }

        // POST: Home/Delete/5
        [Authorize(Roles = "Admin, User")]
      
        [HttpPost]
        public ActionResult Delete(Contact ContactToBeDeleted)
        {
            if (_service.DeleteContact(ContactToBeDeleted))
                return RedirectToAction("Index");
            return View();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using RES.DataAccess.Interfaces.Interfaces;
using RES.BusinessLogic.Core.Entities;
using RES.Commons.Core.Resource;
using System.Threading;
using AutoMapper;

namespace RES.Web.Site.Controllers
{
    public class ContactController : Controller
    {
        // GET: Reservation
        private IContactRepository _repo;
        public ContactController(IContactRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult ListContacts(int page = 0, int sortCode = 0)
        {
            ViewBag.TiTle = Resources.ContactList;
            ViewBag.TiTle2 = Resources.ReservationCreate;
            ViewBag.Method = "InsertContact";
            ViewBag.Controller = "Contact";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;
            ViewBag.Page = page;

            try
            {
                return View("ListContacts", _repo.GetContact(page,sortCode));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return null;
        }

        [HttpGet]
        [ActionName("ContactDetails")]
        public ActionResult GetContactById(int id)
        {
            ContactModel contactModel = ContactDetails(id);
            if(contactModel!=null)
               return View("ContactDetails", contactModel);

            return RedirectToAction("ListContacts", "Contact");
        }

        public ContactModel ContactDetails(int id)
        {
            return _repo.GetContactById(id);
        }

        [HttpGet]
        [ActionName("InsertContact")]
        public ActionResult InsertContact()
        {
            ViewBag.TiTle = Resources.ReservationCreate;
            ViewBag.TiTle2 = Resources.ContactList;
            ViewBag.Method = "ListContacts";
            ViewBag.Controller = "Contact";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;
            ViewBag.ContactTypes = _repo.ContactTypeList();
            return View();
        }

        [HttpPost]
        [ActionName("InsertContact")]
        public ActionResult InsertContact(ContactModel newContact)
        {
            ViewBag.TiTle = Resources.ReservationCreate;
            ViewBag.TiTle2 = Resources.ContactList;
            ViewBag.Method = "ListContacts";
            ViewBag.Controller = "Contact";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;
            ViewBag.ContactTypes = _repo.ContactTypeList();

            if (!ModelState.IsValid) {                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, Resources.MessageContactInvalid);
            }                

          
            var contactId = _repo.InsertContact(newContact);
            if (contactId > 0)
                return RedirectToAction("ListContacts", "Contact");

            return View("InsertContact", newContact);
        }

       [HttpGet]
        [ActionName("UpdateContact")]
        public ActionResult UpdateContact(int id)
        {
            ViewBag.TiTle = Resources.ContactEdit;
            ViewBag.TiTle2 = Resources.ContactList;
            ViewBag.Method = "ListContacts";
            ViewBag.Controller = "Contact";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;
            ViewBag.ContactTypes = _repo.ContactTypeList();

            ContactModel contactModel = ContactDetails(id);
            return View(contactModel);
        }

        [HttpPost]
        [ActionName("UpdateContact")]
        public ActionResult UpdateContact(ContactModel exContactModel)
        {
            ViewBag.TiTle = Resources.ContactEdit;
            ViewBag.TiTle2 = Resources.ContactList;
            ViewBag.Method = "ListContacts";
            ViewBag.Controller = "Contact";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;
            ViewBag.ContactTypes = _repo.ContactTypeList();

            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, Resources.MessageContactInvalid);

            var contactId = _repo.UpdateContact(exContactModel);
            if (contactId > 0)
                return RedirectToAction("ListContacts", "Contact");
            return View("UpdateContact", exContactModel);
        }

        [HttpPost]
        public ActionResult DeleteContact(int id)
        {
            var contactId = _repo.DeleteContact(id);
            if(contactId)
                return this.RedirectToAction("ListContacts", "Contact");

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, Resources.NotFoundContact);
        }
    }
}
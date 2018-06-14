using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RES.DataAccess.Interfaces.Interfaces;
using RES.BusinessLogic.Core.Entities;
using System.Threading;
using RES.Commons.Core.Resource;
using RES.Web.Site.Extensions;

namespace RES.Web.Site.Controllers
{
    public class ReservationController : BaseController
    {
        // GET: Reservation
        private IReservationRepository _repo;
        public ReservationController(IReservationRepository repo)
        {
            _repo = repo;
        }

        
        [HttpGet]
        public ActionResult ListReservations(int page=0,int sortCode=0)
        {
            ViewBag.TiTle = Resources.ReservationList;
            ViewBag.TiTle2 = Resources.ReservationCreate;
            ViewBag.Method = "InsertContact";
            ViewBag.Controller = "Contact";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;
            ViewBag.Page = page;
            try
            {
                return View("ListReservations", _repo.GetReservation(page, sortCode));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return null;
        }

        public Reservation GetReservationDetails(int id)
        {
            return _repo.GetReservationById(id);
        }

        [HttpGet]
        [ActionName("UpdateReservation")]
        public ActionResult UpdateReservation(int id)
        {
            ViewBag.TiTle = Resources.ReservationEdit;
            ViewBag.TiTle2 = Resources.ReservationList;
            ViewBag.Method = "ListReservations";
            ViewBag.Controller = "Reservation";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;
            ViewBag.Contacts = _repo.ContactListForReservation();
            ViewBag.Places = _repo.PlaceList();
           
            Reservation reservation = GetReservationDetails(id);
            ViewBag.Time =reservation.Date.TimeOfDay;
            ViewBag.Date = DateUtil.getDate(reservation.Date);

            return View(reservation);
        }

        [HttpPost]
        [ActionName("UpdateReservation")]
        public ActionResult UpdateReservation(Reservation exReservation)
        {
            ViewBag.TiTle = Resources.ReservationEdit;
            ViewBag.TiTle2 = Resources.ReservationList;
            ViewBag.Method = "ListReservations";
            ViewBag.Controller = "Reservation";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;
            ViewBag.Contacts = _repo.ContactListForReservation();
            ViewBag.Places = _repo.PlaceList();

            if (!ModelState.IsValid) return View("UpdateReservation", exReservation);
            var reservationId = _repo.UpdateReservation(exReservation);
            if (reservationId > 0) 
                return RedirectToAction("ListReservations", "Reservation");
                   
            return View("UpdateReservation", exReservation);
        }

        [HttpPost]
        public ActionResult AddFavorite(int id)
        {   
            if (_repo.AddFavorite(id) )
                return RedirectToAction("ListReservations", "Reservation");
            return  RedirectToAction("ListReservations", "Reservation");
        }


    }
}
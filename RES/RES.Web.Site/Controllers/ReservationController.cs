using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RES.DataAccess.Interfaces.Interfaces;
using RES.BusinessLogic.Core.Entities;
using System.Threading;
using RES.Commons.Core.Resource;

namespace RES.Web.Site.Controllers
{
    public class ReservationController : Controller
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

        [HttpGet]
        [ActionName("ReservationDetails")]
        public ActionResult GetReservationById(int id)
        {
            Reservation reservation = GetReservationDetails(id);
            return View("ReservationDetails", reservation);
        }

        public Reservation GetReservationDetails(int id)
        {
            return _repo.GetReservationById(id);
        }

        [HttpGet]
        [ActionName("InsertReservation")]
        public ActionResult InsertReservation()
        {
            ViewBag.TiTle = Resources.ReservationCreate;
            ViewBag.TiTle2 = Resources.ReservationList;
            ViewBag.Method = "ListReservations";
            ViewBag.Controller = "Reservation";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;

            return View();
        }

        [HttpPost]
        [ActionName("InsertReservation")]
        public ActionResult InsertReservation(Reservation newReservation)
        {
            ViewBag.TiTle = Resources.ReservationCreate;
            ViewBag.TiTle2 = Resources.ReservationList;
            ViewBag.Method = "ListReservations";
            ViewBag.Controller = "Reservation";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;

            if (!ModelState.IsValid) return View("InsertReservation", newReservation);
            var reservationId = _repo.InsertReservation(newReservation);
            if (reservationId > 0)
                return RedirectToAction("ListReservations", "Reservation");
            return View("InsertReservation", newReservation);
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

            Reservation reservation = GetReservationDetails(id);
            return View(reservation);
        }

        [HttpPut]
        [ActionName("UpdateReservation")]
        public ActionResult UpdateReservation(Reservation exReservation)
        {
            ViewBag.TiTle = Resources.ReservationEdit;
            ViewBag.TiTle2 = Resources.ReservationList;
            ViewBag.Method = "ListReservations";
            ViewBag.Controller = "Reservation";
            ViewBag.Language = Thread.CurrentThread.CurrentCulture;

            if (!ModelState.IsValid) return View("UpdateReservation", exReservation);
            var reservationId = _repo.UpdateReservation(exReservation);
            if (reservationId > 0)
                return RedirectToAction("ListReservations", "Reservation");
            return View("UpdateReservation", exReservation);
        }

        [HttpDelete]
        [ActionName("DeleteReservation")]
        public ActionResult DeleteReservation(int id)
        {
            var reservationId = _repo.DeleteReservation(id);
            return RedirectToAction("ListReservations", "Reservation");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using RES.DataAccess.Interfaces.Abstract;
using RES.DataAccess.Interfaces.Interfaces;
using RES.BusinessLogic.Core.Entities;
using RES.BusinessLogic.Core.Data;
using System.Data.Entity;
using PagedList;

namespace RES.DataAccess.Core.Repository.EF
{
    public class ReservationEfRepository : EFRepositoryBase<ContactType>, IReservationRepository
    {
        const int rows = 10;
        public ReservationEfRepository(ResEntities dbCtx) : base(dbCtx)
        {
        }
        public bool DeleteReservation(int id)
        {
            Reservation itemToDelete = Context.Reservations.SingleOrDefault(c => c.Id == id);
            if (itemToDelete != null)
            {
                Context.Entry(itemToDelete).State = EntityState.Deleted;
                return (Context.SaveChanges() > 0);
            }
            else
                return false;
            
        }

        public ReservationList GetReservation()
        {
            List<Reservation> reservations = Context.Reservations.ToList();
            ReservationList reservationList = new ReservationList();
            
            reservationList.SortByCode = 0;
            reservationList.List= reservations;

            return reservationList;
        }

        public ReservationList GetReservation(int page = 0, int sort = 0)
        {
            ReservationList reservationList = new ReservationList();
            IEnumerable<Reservation> result = null;
            Func<IQueryable<Reservation>, IOrderedQueryable<Reservation>> orderBy = null;

            var totalElement = Context.Reservations.Count();           
            var totalPage = totalElement/ rows;
            var decimalNumber =Math.IEEERemainder(totalElement , rows);
            if (decimalNumber != 0)
                totalPage++;

            reservationList.TotalPage=totalPage;
            reservationList.SortByCode = sort;

            if (sort > 0)
            {
                switch (sort)
                {
                    case 1:
                        orderBy = s => s.OrderBy(t => t.Date);
                        break;
                    case 2:
                        orderBy = s => s.OrderByDescending(t => t.Date);
                        break;
                    case 3:
                        orderBy = s => s.OrderBy(t => t.Place.Name);
                        break;
                    case 4:
                        orderBy = s => s.OrderByDescending(t => t.Place.Name);
                        break;
                    default:
                        orderBy = s => s.OrderByDescending(t => t.Ranking);
                        break;
                }
                reservationList.SortByCode = sort;

                result = orderBy(Context.Reservations).ToList().Skip(page* rows).Take(rows).Select(r => new Reservation()
                {
                    Id = r.Id,
                    Date=r.Date,
                    IsFavorite = r.IsFavorite,
                    Ranking = r.Ranking,
                    Place = r.Place

                }
            );
                
            }
            else
            {
                result = Context.Reservations.AsNoTracking().Distinct().ToList().Skip(page * rows).Take(rows).Select(r => new Reservation()
                {
                    Id = r.Id,
                    Date = r.Date,
                    IsFavorite = r.IsFavorite,
                    Ranking = r.Ranking,
                    Place = r.Place
                }
                );

            }
            reservationList.List = result;
            return reservationList;
        }

        public Reservation GetReservationById(int id)
        {
            return Context.Reservations.FirstOrDefault(c => c.Id == id);
        }

        public int InsertReservation(Reservation reservation)
        {
            Context.Reservations.Add(reservation);
            return Context.SaveChanges();
        }

        public int UpdateReservation(Reservation reservation)
        {
            Context.Entry(reservation).State = EntityState.Modified;
            return Context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RES.BusinessLogic.Core.Entities;
using RES.DataAccess.Interfaces.Base;


namespace RES.DataAccess.Interfaces.Interfaces
{
    public interface IReservationRepository : IRepositoryBase
    {
        Reservation GetReservationById(int id);
        int UpdateReservation(Reservation reservation);
        bool AddFavorite(int id );
        ReservationList GetReservation(int page = 0, int sort = 0);
        IEnumerable<Place> PlaceList();

        IEnumerable<Contact> ContactListForReservation();
    }
}

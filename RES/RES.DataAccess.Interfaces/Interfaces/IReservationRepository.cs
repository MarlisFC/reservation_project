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
        ReservationList GetReservation();
        Reservation GetReservationById(int id);
        int InsertReservation(Reservation reservation);
        int UpdateReservation(Reservation reservation);
        bool DeleteReservation(int id);
        ReservationList GetReservation(int page = 0, int sort = 0);
    }
}

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RES.BusinessLogic.Core.Entities;
using Dapper;
using RES.DataAccess.Interfaces.Interfaces;

namespace RES.DataAccess.Core.Repository.Drapper.Mssql
{
    public class ReservationSqlRepository : Interfaces.Abstract.DapperRepositoryBase, Interfaces.Interfaces.IReservationRepository
    {
        public ReservationSqlRepository(IDbConnection conn, string conString)
        {
            _connection = conn;
            _connection.ConnectionString = conString;
        }
        public bool DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }

        public ReservationList GetReservation()
        {
            throw new NotImplementedException();
        }

        public ReservationList GetReservation(int page = -1, int size = -1, int count = -1, int sort = -1)
        {
            throw new NotImplementedException();
        }

        public Reservation GetReservationById(int id)
        {
            throw new NotImplementedException();
        }

        public int InsertReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public int UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public ReservationList GetReservation(int page = 0, int sort = 0)
        {
            throw new NotImplementedException();
        }
    }
}

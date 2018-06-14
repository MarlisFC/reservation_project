using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using RES.BusinessLogic.Core.Entities;
using Dapper;


namespace RES.DataAccess.Core.Repository.Drapper.Mssql
{
    public class ReservationSqlRepository : Interfaces.Abstract.DapperRepositoryBase, Interfaces.Interfaces.IReservationRepository
    {
        const int rows = 10;
        public ReservationSqlRepository(IDbConnection conn, string conString)
        {
            _connection = conn;
            _connection.ConnectionString = conString;
        }     
     
        public Reservation GetReservationById(int id)
        {
            return Query<Reservation>("sp_GetReservationById", new { Id = id }).FirstOrDefault();
        }

        public int UpdateReservation(Reservation reservation)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", reservation.Id, DbType.Int32);
            parameters.Add("IsFavorite", reservation.IsFavorite, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("Ranking", reservation.Ranking, DbType.Int16, ParameterDirection.Input);
            parameters.Add("Description", reservation.Description, DbType.String, ParameterDirection.Input);
            parameters.Add("ContactId", reservation.ContactId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PlaceId", reservation.PlaceId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Date", reservation.Date, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("ReturnValue", null, DbType.Int32, ParameterDirection.Output);
            parameters.Add("ErrorCode", null, DbType.Int32, ParameterDirection.Output);

            Execute("sp_Update_Reservation", parameters); ;
            return parameters.Get<int>("ReturnValue");
        }

        public ReservationList GetReservation(int page = 0, int sort = 0)
        {
            var totalElement = Query<int>("sp_CountReservation").FirstOrDefault();
            var totalPage = totalElement / rows;
            var decimalNumber = Math.IEEERemainder(totalElement, rows);
            if (decimalNumber != 0)
                totalPage++;

            var pageNumber = page + 1;
            int take = 10;

            if ((totalPage == pageNumber) && (pageNumber * rows > totalElement))
            {
                take = (totalElement - (rows * page)) - 1;
            }

            ReservationList reservationList = new ReservationList();

            var parameters = new DynamicParameters();
            parameters.Add("Sort", sort, DbType.Int32);
            parameters.Add("Skip", page * rows, DbType.Int32);
            parameters.Add("Take", take, DbType.Int32);
            parameters.Add("ErrorCode", null, DbType.Int32, ParameterDirection.Output);


            var query = Query<Reservation, Place, Contact, Reservation>("sp_GetReservation",
              (r, pa, c) =>
              {
                  r.Place = pa;
                  r.Contact = c;
                  return r;
              }, parameters, split: "PlaceId, ContactId")
           .AsQueryable();

            IEnumerable<Reservation> result = query.ToList();

           

            reservationList.TotalPage = totalPage;
            reservationList.SortByCode = sort;

            reservationList.List = result;
            return reservationList;
        }

        public bool AddFavorite(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("ErrorCode", null, DbType.Int32, ParameterDirection.Output);

            Execute("sp_AddFavorite", parameters);
            var errorCode = parameters.Get<int>("ErrorCode");
            return errorCode == 0;
        }

        public IEnumerable<Place> PlaceList()
        {
            return Query<Place>("sp_GetAllPlaces").ToList();
        }

        public IEnumerable<Contact> ContactListForReservation()
        {
            return Query<Contact>("sp_GetAllContacts").ToList();
        }
    }
}

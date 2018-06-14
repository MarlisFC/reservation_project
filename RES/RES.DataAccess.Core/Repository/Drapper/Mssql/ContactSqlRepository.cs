using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using RES.BusinessLogic.Core.Entities;
using Dapper;

namespace RES.DataAccess.Core.Repository.Drapper.Mssql
{
    public class ContactSqlRepository : Interfaces.Abstract.DapperRepositoryBase, Interfaces.Interfaces.IContactRepository
    {

        const int rows=10;
        public ContactSqlRepository(IDbConnection conn, string conString)
        {
            _connection = conn;
            _connection.ConnectionString = conString;
        }

        public IEnumerable<ContactType> ContactTypeList()
        {
            return Query<ContactType>("sp_GetContactType").ToList();
        }

        public bool DeleteContact(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("ErrorCode", null, DbType.Int32, ParameterDirection.Output);

            Execute("sp_Delete_Contact", parameters);
            var errorCode = parameters.Get<int>("ErrorCode");
            return errorCode == 0;
        }

         public ContactList GetContact(int page = 0, int sort = 0)
        {
            var totalElement = Query<int>("sp_CountContact").FirstOrDefault();

            var totalPage = totalElement / rows;
            var decimalNumber = Math.IEEERemainder(totalElement, rows);
            if (decimalNumber != 0)
                totalPage++;

            var pageNumber = page + 1;
            int take = 10;

            if ((totalPage == pageNumber) && (pageNumber * rows > totalElement)) {
                take =(totalElement-(rows*page))-1;
            }


            ContactList contactList = new ContactList();

            var parameters = new DynamicParameters();
            parameters.Add("Sort", sort, DbType.Int32);
            parameters.Add("Skip", page * rows, DbType.Int32);
            parameters.Add("Take", take, DbType.Int32);
            parameters.Add("ErrorCode", null, DbType.Int32, ParameterDirection.Output);

            var query = Query<Contact, ContactType, Contact>("sp_GetContact",
            (c, ct) =>
            {
                c.ContactType = ct;
                return c;
            }, parameters, split: "ContactTypeId")
           .AsQueryable();

            IEnumerable<Contact> result = query.ToList();
            

            contactList.TotalPage = totalPage;
            contactList.SortByCode = sort;

            contactList.List = result;
            return contactList;
        }

        public ContactModel GetContactById(int id)
        {
            Contact contact = Query<Contact>("sp_GetContactById", new { Id = id }).FirstOrDefault();
          
            if (contact != null)
            {
                ContactModel contactModel = new ContactModel
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Phone = contact.Phone,
                    Description = contact.Description,
                    ContactTypeId = contact.ContactTypeId,
                    Birthdate = contact.Birthdate.ToString("yyyy/MM/dd")
                };
                return contactModel;
            }
            return null;
        }

        public int InsertContact(ContactModel contact)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", contact.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("Description", contact.Description, DbType.String, ParameterDirection.Input);
            parameters.Add("Phone", contact.Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("ContactTypeId", contact.ContactTypeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Birthdate", contact.Birthdate, DbType.DateTime, ParameterDirection.Input);           
            parameters.Add("ReturnValue", null, DbType.Int32, ParameterDirection.Output);
            parameters.Add("ErrorCode", null, DbType.Int32, ParameterDirection.Output);

            Execute("sp_Insert_Contact", parameters);
            return parameters.Get<int>("ReturnValue");
        }

        public int UpdateContact(ContactModel contact)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", contact.Id, DbType.Int32);
            parameters.Add("Name", contact.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("Description", contact.Description, DbType.String, ParameterDirection.Input);
            parameters.Add("Phone", contact.Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("ContactTypeId", contact.ContactTypeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Birthdate", contact.Birthdate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("ReturnValue", null, DbType.Int32, ParameterDirection.Output);
            parameters.Add("ErrorCode", null, DbType.Int32, ParameterDirection.Output);

            Execute("sp_Update_Contact", parameters); ;
            return parameters.Get<int>("ReturnValue");
        }
    }
}

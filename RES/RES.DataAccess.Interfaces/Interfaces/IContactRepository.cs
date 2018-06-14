using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RES.BusinessLogic.Core.Entities;
using RES.DataAccess.Interfaces.Base;

namespace RES.DataAccess.Interfaces.Interfaces
{
    public interface IContactRepository : IRepositoryBase
    {
        ContactModel GetContactById(int id);
        int InsertContact(ContactModel contact);
        int UpdateContact(ContactModel contact);
        bool DeleteContact(int id);
        IEnumerable<ContactType> ContactTypeList();
        ContactList GetContact(int page = 0, int sort = 0);
        
    }
}

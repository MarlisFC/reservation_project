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
    public class ContactEfRepository: EFRepositoryBase<ContactType>, IContactRepository
    {
        const int rows = 10;
        public ContactEfRepository(ResEntities dbCtx) : base(dbCtx)
        {
        }

        public IEnumerable<ContactType> ContactTypeList()
        {
            List<ContactType> list = Context.ContactType.ToList();
            return list;
        }

        public bool DeleteContact(int id)
        {
            Contact itemToDelete = Context.Contacts.SingleOrDefault(c => c.Id == id);
            if (itemToDelete != null)
            {
                Context.Entry(itemToDelete).State = EntityState.Deleted;
                return (Context.SaveChanges() > 0);
            }
            else
                return false;
            
        }

        public List<Contact> GetContact()
        {
            List<Contact> contacts = Context.Contacts.ToList();
            return contacts;
        }

        public ContactList GetContact(int page = 0, int sort = 0)
        {
            ContactList contactList = new ContactList();
            IEnumerable<Contact> result = null;
            Func<IQueryable<Contact>, IOrderedQueryable<Contact>> orderBy = null;

            var totalElement = Context.Contacts.Count();
            var totalPage = totalElement / rows;
            var decimalNumber = Math.IEEERemainder(totalElement, rows);
            if (decimalNumber !=0)
                totalPage++;

            contactList.TotalPage = totalPage;
            contactList.SortByCode = sort;

            if (sort > 0)
            {
                switch (sort)
                {
                    case 1:
                        orderBy = s => s.OrderBy(t => t.Name);
                        break;
                    case 2:
                        orderBy = s => s.OrderBy(t => t.Phone);
                        break;
                    case 3:
                        orderBy = s => s.OrderBy(t => t.Birthdate);
                        break;
                    default:
                        orderBy = s => s.OrderBy(t => t.ContactType.Value);
                        break;
                   
                }
                contactList.SortByCode = sort;

                result = orderBy(Context.Contacts).ToList().Skip(page * rows).Take(rows).Select(c => new Contact()
                {
                    Id = c.Id,
                    Name=c.Name,
                    Phone=c.Phone,
                    Birthdate=c.Birthdate,
                    ContactType=c.ContactType

                }
            );

            }
            else
            {
                result = Context.Contacts.AsNoTracking().Distinct().ToList().Skip(page * rows).Take(rows).Select(c => new Contact()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Birthdate = c.Birthdate,
                    ContactType = c.ContactType
                }
                );

            }
            contactList.List = result;
            return contactList;
        }

        public ContactModel GetContactById(int id)
        {
            Contact contact= Context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null) {
                ContactModel contactModel = new ContactModel {
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

        public int InsertContact(ContactModel contactModel)
        {
           
            Contact contact = new Contact {
                Id = contactModel.Id,
                Name = contactModel.Name,
                Phone = contactModel.Phone,
                Description = contactModel.Description,
                ContactTypeId = contactModel.ContactTypeId,
                Birthdate = Convert.ToDateTime(contactModel.Birthdate)
            };

            Context.Contacts.Add(contact);
            return Context.SaveChanges();
        }

        public int UpdateContact(ContactModel contactModel)
        {
            Contact contact = new Contact
            {
                Id = contactModel.Id,
                Name = contactModel.Name,
                Phone = contactModel.Phone,
                Description = contactModel.Description,
                ContactTypeId = contactModel.ContactTypeId,
                Birthdate = Convert.ToDateTime(contactModel.Birthdate)
            };
        
            Context.Entry(contact).State = EntityState.Modified;
            return Context.SaveChanges();
        }
    }
}

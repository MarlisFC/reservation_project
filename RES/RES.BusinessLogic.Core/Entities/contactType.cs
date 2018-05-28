using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RES.BusinessLogic.Core.Entities
{
    public class ContactType
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}

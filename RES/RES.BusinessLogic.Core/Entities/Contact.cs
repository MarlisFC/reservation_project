using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RES.Commons.Core;
namespace RES.BusinessLogic.Core.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public string Description { get; set; }

        [Required]       
        public int ContactTypeId { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ContactType ContactType { get; set; }
    }

    public class ContactList
    {
        public int SortByCode { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<Contact> List { get; set; }

    }

    public class ContactModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        public String Birthdate { get; set; }
        public string Description { get; set; }

        [Required]
        public int ContactTypeId { get; set; }
    }
}

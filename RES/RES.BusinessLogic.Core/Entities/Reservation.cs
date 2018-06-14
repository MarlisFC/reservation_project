using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RES.BusinessLogic.Core.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public bool IsFavorite { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public short Ranking { get; set; }

        public string Description { get; set; }

        [Required]
        public int ContactId { get; set; }

        [Required]
        public int PlaceId { get; set; }
        public virtual Contact Contact { get; set; }

        public virtual Place Place { get; set; }

    }

    public class ReservationList
    {
        public int SortByCode { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<Reservation> List { get; set; }

    }
}

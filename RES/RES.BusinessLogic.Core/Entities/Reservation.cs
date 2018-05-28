using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RES.BusinessLogic.Core.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public bool IsFavorite { get; set; }

        public DateTime Date { get; set; }
        public short Ranking { get; set; }
        public int ContactId { get; set; }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RES.Web.Site.Extensions
{
    public class DateUtil
    {
        public static String getDate(DateTime dateTime) {

            var month = "";
            var day="";
            if (dateTime.Month < 10)
            {
                month = "0" + dateTime.Month;
            }
            else
            {
                month = dateTime.Month.ToString();
            }

            if (dateTime.Day < 10)
            {
                day = "0" + dateTime.Day;
            }
            else
            {
                day = dateTime.Day.ToString();
            }


            var date = dateTime.Year + "-" + month + "-" + day;

            return date;
        }
    }
}
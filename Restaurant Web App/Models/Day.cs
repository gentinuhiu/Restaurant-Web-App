using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class Day
    {
        public Day()
        {
        }
        public Day(string StartTime, string EndTime, string StartTime2, string EndTime2)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.StartTime2 = StartTime2;
            this.EndTime2 = EndTime2;
        }

        [Key]
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartTime2 { get; set; }
        public string EndTime2 { get; set; }
        public string GetSchedule()
        {
            if(StartTime != null && StartTime.Length != 0 &&  EndTime != null && EndTime.Length != 0)
                return StartTime + " bis " + EndTime;
            return "";
        }
        public string GetSchedule2()
        {
            if (StartTime2 != null && StartTime2.Length != 0 && EndTime2 != null && EndTime2.Length != 0)
                return StartTime2 + " bis " + EndTime2;
            return "";
        }
    }
}
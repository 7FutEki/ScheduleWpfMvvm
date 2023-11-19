using DevExpress.Mvvm;
using Newtonsoft.Json;
using RestSharp;
using ScheduleWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleWpf.ViewModel
{
    public class ScheduleViewModel
    {
        private string _id = "255";
        public DateTime Date { get; set; }
        public string Schedule { get; set; }
        public ICommand GetSchedule { get; set; }

        public ScheduleViewModel()
        {
            Schedule = string.Empty;
            GetSchedule = new DelegateCommand(GetScheduleFromApi);
        }

        private void GetScheduleFromApi()
        {
            var client = new RestClient();
            var options = new RestRequest($"https://asu.samgk.ru/api/schedule/{_id}/{Date.ToString("yyyy-MM-dd")}");
            var result = client.Get(options);
            var schedule = JsonConvert.DeserializeObject<Schedule>(result.Content);
            Dictionary<int, string> LessonTime = new Dictionary<int, string>()
            {
                {1, "8:25-9:10 | 9:15-10:00" },
                {2, "10:10-10:55 | 11:00-11:45" },
                {3, "12:15-13:00 | 13:05-13:50" },
                {4, "14:00-14:45 | 14:50-15:35" },
                {5, "15:45-16:30 | 16:35-17:20" },
                {6, "17:30-18:15 | 18:20-19:05" },
                {7, "19:15-20:00 | 20:05-20:50" }
            };
            Dictionary<int, string> LessonTimeMonday = new Dictionary<int, string>()
            {
                {1, "9:15-10:00 | 10:10-10:55" },
                {2, "11:00-11:45 | 12:15-13:00" },
                {3, "13:05-13:50 | 14:00-14:45" },
                {4, "14:50-15:35 | 15:45-16:30" },
                {5, "16:35-17:20 | 17:30-18:15" },
                {6, "18:20-19:05 | 19:15-20:00" },
                {7, "null" }
            };

            if (Date.DayOfWeek == DayOfWeek.Monday) LessonTime = LessonTimeMonday;

            foreach (var item in schedule.Lessons)
            {
                switch (item.num)
                {
                    case "1": item.time = LessonTime[1];
                        break;
                    case "2": item.time = LessonTime[2];
                        break;
                    case "3": item.time = LessonTime[3];
                        break;
                    case "4": item.time = LessonTime[4];
                        break;
                    case "5": item.time = LessonTime[5];
                        break;
                    case "6": item.time= LessonTime[6];
                        break;
                    case "7": item.time = LessonTime[7];
                        break;
                }
            }

            string res = string.Empty;

            foreach (var item in schedule.Lessons)
            {
                res += $"{item.num} \n {item.time} \n {item.title} \n {item.teachername} \n {item.cab} \n";
            }
            Schedule = res;

        }
    }
}
